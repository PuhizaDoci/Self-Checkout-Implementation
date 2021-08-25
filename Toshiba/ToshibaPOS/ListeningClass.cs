using NLog;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;


namespace ToshibaPOS
{
    public class ListeningClass
    {
        public ListeningClass(FaturaBLL _Fatura)
        {
            Fatura = _Fatura;
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 8 * 60 * 60 * 1000;
            timer.Enabled = true;
        }

        public static FaturaBLL Fatura { get; set; }
        static string data = null;
        static string tender = null;
        static PosPcb posObj;
        static decimal amount3 = 0;
        static decimal amount4 = 0;
        static decimal amount = 0;
        static decimal amount2 = 0;
        static string idNumber = "1";
        static bool printFaulted = false;
        static System.Timers.Timer timer = new System.Timers.Timer();
        private static readonly Logger Logger 
            = LogManager.GetCurrentClassLogger();

        public static async Task AsyncStartListening(TextBox textBox, string IP, string Port)
        {
            await Task.Run(() =>
            {
                StartListening(textBox, IP, Port);
            });
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            try
            {
                posObj = new PosPcb();
                string something = posObj.ConnectToPos();
                Thread.Sleep(7000); //sleep until bank terminal takes request

                if (something != "OK")
                {
                    posObj = null;
                }
                else
                {
                    Logger.Info("Konektimi me POS te bankes!");
                }
            }
            catch (Exception ex)
            {
                posObj = null;
                Logger.Error(ex, ex.Message);
            }
        }

        public static void StartListening(TextBox textBox, string IP, string Port)
        {
        returnToSocket:
            byte[] bytes = new Byte[2048 * 2];
            IPAddress ipAddress = IPAddress.Parse(IP);
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, Convert.ToInt32(Port));
            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listener.Bind(remoteEP);
                listener.Listen(10);
                textBox.Invoke(new MethodInvoker(() =>
                {
                    textBox.Text = "Waiting for a connection...";
                }));
                Socket handler = listener.Accept();
                Logger.Info("ToshibaPos eshte startuar dhe ka filluar te degjoje!");

                int numberOfTotalRequest = 0;
                int numberOfAddItemRequest = 1;
                int numberOfItems = 0;
                bool laneOpen = false;

                while (true)
                {
                    data = null;

                    if (handler.Available > 0)
                    {
                        string Req;
                        int bytesRec = handler.Receive(bytes);
                        data = Encoding.UTF8.GetString(bytes, 4, bytesRec - 4);
                        int index = data.IndexOf("|~");

                        idNumber = data.Substring(index - 1, 1);

                        Logger.Info("Numri id: " + idNumber);

                        Req = data;

                        Logger.Info("Kerkesa nga toshiba me header: " + data);

                        data = data.Replace("soeps~Message-Type=REQ|Session-Id="
                            + idNumber + "|~", string.Empty);

                        if (data.Contains("<PML>"))
                        {
                            printFaulted = true;
                            int indexPML = data.IndexOf("<PML>");
                            data = data.Remove(indexPML);
                            data += "</PrintRequest></scsns:Print>";
                        }

                        Logger.Info("Kerkesa nga toshiba: " + data);

                        XmlDocument doc = new XmlDocument();
                        XmlElement rootElement = null;

                        try
                        {
                            doc.LoadXml(data);
                            rootElement = (XmlElement)doc.DocumentElement.FirstChild;

                            Logger.Info("Header i kerkesave perfundoi!");
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(ex, "Header i kerkesave nuk u rregullua: " + ex.Message);
                        }

                        switch (rootElement.Name)
                        {
                            case "InitializeRequest":

                                try
                                {
                                    XmlSerializer serializer = new XmlSerializer(typeof(InitializeType));
                                    StringReader rdr = new StringReader(doc.InnerXml);
                                    InitializeType initialRequest = (InitializeType)serializer.Deserialize(rdr);

                                    handler.Send(ConvertToBytes(InitializignStatus("RELEASING_SESSION_RESOURCES")));
                                    handler.Send(ConvertToBytes(InitializignStatus("CONNECTING_TO_POS")));
                                    handler.Send(ConvertToBytes(InitializignStatus("CONNECTED_TO_POS")));

                                    handler.Send(ConvertToBytes(InitializignStatus("POS_RESOURCES_INITIALIZED")));
                                    handler.Send(ConvertToBytes(InitializignStatus("INITIALIZED")));
                                    handler.Send(ConvertToBytes(InitializignStatus("ONLINE")));
                                    handler.Send(ConvertToBytes(InitialResponse()));
                                    amount = 0;
                                    amount2 = 0;
                                    amount3 = 0;
                                    amount4 = 0;

                                    Logger.Info("Kerkesa \"InitializeRequest\" u perfundua!");
                                }
                                catch (Exception ex)
                                {
                                    Logger.Error(ex, "Kerkesa \"InitializeRequest\" deshtoi: " + ex.Message);
                                }

                                break;
                            case "QueryStatusRequest":

                                Thread.Sleep(1000);

                                try
                                {
                                    posObj = new PosPcb();
                                    string something = posObj.ConnectToPos();
                                    Thread.Sleep(7000); //sleep until bank terminal takes request

                                    if (something != "OK")
                                    {
                                        posObj = null;
                                    }
                                    else
                                    {
                                        Logger.Info("Konektimi me POS te bankes!");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    posObj = null;
                                    Logger.Error(ex, ex.Message);
                                }

                                try
                                {
                                    handler.Send(ConvertToBytes(QueryResponse()));
                                    handler.Send(ConvertToBytes(QueryResponsewith3()));
                                    Logger.Info("Kerkesa \"QueryStatusRequest\" u perfundua!");
                                }
                                catch (Exception ex)
                                {
                                    Logger.Error(ex, "Kerkesa \"QueryStatusRequest\" deshtoi: " + ex.Message);
                                }

                                break;
                            case "ReportStatusEventsRequest":

                                try
                                {
                                    XDocument reportStatusResponse = XDocument.Parse(data);
                                    var requestId = TagQuery(reportStatusResponse, "ReportStatusEventsRequest", "RequestID");

                                    handler.Send(ConvertToBytes(_PosbcPrinterStatus()));
                                    handler.Send(ConvertToBytes(ReportStatusEventsRequest(Convert.ToInt32(requestId))));
                                    Logger.Info("Kerkesa \"ReportStatusEventsRequest\" u perfundua!");
                                }
                                catch (Exception ex)
                                {
                                    Logger.Error(ex, "Kerkesa \"ReportStatusEventsRequest\" deshtoi: " + ex.Message);
                                }

                                break;
                            case "AddItemRequest":

                                //state that lane is open and working
                                laneOpen = true;

                                //continue with request data
                                try
                                {
                                    XDocument addItemResponse = XDocument.Parse(data);
                                    Logger.Info("Kerkesa \"AddItemRequest\" filloi!");

                                    int largimi;
                                    var addItemRequestId = TagQuery(
                                        addItemResponse, "AddItemRequest", "RequestID");
                                    var voidFlag = TagQuery(
                                        addItemResponse, "ItemIdentifier", "VoidFlag");
                                    var _barkodiTemp = TagQuery(
                                        addItemResponse, "ItemIdentifier", "KeyedItemID");
                                    var _barkodi = TagQuery(
                                        addItemResponse, "BarCode", "ScanDataLabel");
                                    var quantityAsRequest = TagQuery(
                                        addItemResponse, "ItemIdentifier", "Quantity");

                                    if (_barkodi == null)
                                        _barkodi = _barkodiTemp;

                                    //number of items is 1, if more count them
                                    if (quantityAsRequest == null)
                                        quantityAsRequest = "1";
                                    else
                                    {
                                        for (int i = 1; i <= Convert.ToInt32(quantityAsRequest); i++)
                                            numberOfItems++;
                                    }

                                    //count number of items to be voided
                                    if (voidFlag == "true")
                                    {
                                        largimi = -1;
                                        for (int i = 1; i <= Convert.ToInt32(quantityAsRequest); i++)
                                        {
                                            string test = Fatura.ShtoArtikullin(_barkodi, largimi);
                                            numberOfItems--;
                                        }
                                    }
                                    else if (quantityAsRequest == "1")
                                    {
                                        numberOfItems++;
                                        largimi = 1;
                                        string test = Fatura.ShtoArtikullin(_barkodi, largimi);
                                    }
                                    else
                                    {
                                        largimi = 1;
                                        string test;
                                        for (int i = 1; i <= Convert.ToInt32(quantityAsRequest); i++)
                                            test = Fatura.ShtoArtikullin(_barkodi, largimi);
                                    }

                                    //if item does not exist, send error message and break
                                    if (Fatura.ArtikulliIFunditIShtuar.ArtikulliId == 0 && numberOfAddItemRequest == 1)
                                    {
                                        handler.Send(ConvertToBytes(StartTransaction(
                                            Convert.ToInt32(addItemRequestId))));
                                        handler.Send(ConvertToBytes(_POSReceiptEven(
                                            Convert.ToInt32(addItemRequestId))));
                                        handler.Send(ConvertToBytes(_AddItemNotFoundResponse(
                                            Convert.ToInt32(addItemRequestId))));
                                        numberOfAddItemRequest++;
                                        break;
                                    }
                                    else if (Fatura.ArtikulliIFunditIShtuar.ArtikulliId == 0 && numberOfAddItemRequest != 1)
                                    {
                                        handler.Send(ConvertToBytes(_AddItemNotFoundResponse(
                                            Convert.ToInt32(addItemRequestId))));
                                        numberOfAddItemRequest++;
                                        break;
                                    }

                                    //depending on the number of items and if item voided, different responses
                                    if (numberOfAddItemRequest == 1 && voidFlag == "true")
                                    {
                                        handler.Send(ConvertToBytes(StartTransaction(
                                            Convert.ToInt32(addItemRequestId))));
                                        handler.Send(ConvertToBytes(_POSReceiptEven(
                                            Convert.ToInt32(addItemRequestId))));
                                        handler.Send(ConvertToBytes(TransactionStatus(
                                            Convert.ToInt32(addItemRequestId), "TRANSACTION_START", "1")));
                                        handler.Send(ConvertToBytes(_POSReceiptEvenItem(
                                            Convert.ToInt32(addItemRequestId), Fatura,
                                            Convert.ToBoolean(voidFlag), Convert.ToInt32(quantityAsRequest))));
                                        handler.Send(ConvertToBytes(_TotalsEvent(
                                            Convert.ToInt32(addItemRequestId), Fatura, numberOfItems)));
                                        handler.Send(ConvertToBytes(_AddItemResponse(
                                            Convert.ToInt32(addItemRequestId), Fatura,
                                            _barkodi.ToString(), Convert.ToBoolean(voidFlag),
                                            Convert.ToDecimal(quantityAsRequest))));
                                    }
                                    else if (numberOfAddItemRequest != 1 && voidFlag == "true")
                                    {
                                        handler.Send(ConvertToBytes(_POSReceiptEvenItem(
                                            Convert.ToInt32(addItemRequestId), Fatura,
                                            Convert.ToBoolean(voidFlag), Convert.ToInt32(quantityAsRequest))));
                                        handler.Send(ConvertToBytes(_TotalsEvent(
                                            Convert.ToInt32(addItemRequestId), Fatura, numberOfItems)));
                                        handler.Send(ConvertToBytes(_AddItemResponse(
                                            Convert.ToInt32(addItemRequestId), Fatura,
                                            _barkodi.ToString(), Convert.ToBoolean(voidFlag),
                                            Convert.ToDecimal(quantityAsRequest))));
                                    }
                                    else if (numberOfAddItemRequest == 1 && voidFlag != "true")
                                    {
                                        handler.Send(ConvertToBytes(StartTransaction(
                                            Convert.ToInt32(addItemRequestId))));
                                        handler.Send(ConvertToBytes(_POSReceiptEven(
                                            Convert.ToInt32(addItemRequestId))));
                                        handler.Send(ConvertToBytes(TransactionStatus(
                                            Convert.ToInt32(addItemRequestId), "TRANSACTION_START", "1")));
                                        handler.Send(ConvertToBytes(_POSReceiptEvenItem(
                                            Convert.ToInt32(addItemRequestId), Fatura,
                                            false, Convert.ToInt32(quantityAsRequest))));
                                        handler.Send(ConvertToBytes(_TotalsEvent(
                                            Convert.ToInt32(addItemRequestId), Fatura, numberOfItems)));
                                        handler.Send(ConvertToBytes(_AddItemResponse(
                                            Convert.ToInt32(addItemRequestId), Fatura,
                                            _barkodi.ToString(), false, Convert.ToDecimal(quantityAsRequest))));
                                    }
                                    else
                                    {
                                        handler.Send(ConvertToBytes(_POSReceiptEven(
                                            Convert.ToInt32(addItemRequestId))));
                                        handler.Send(ConvertToBytes(_POSReceiptEvenItem(
                                            Convert.ToInt32(addItemRequestId), Fatura,
                                            false, Convert.ToInt32(quantityAsRequest))));
                                        handler.Send(ConvertToBytes(_TotalsEvent(
                                            Convert.ToInt32(addItemRequestId), Fatura, numberOfItems)));
                                        handler.Send(ConvertToBytes(_AddItemResponse(
                                            Convert.ToInt32(addItemRequestId), Fatura,
                                            _barkodi.ToString(), false, Convert.ToDecimal(quantityAsRequest))));
                                    }
                                    numberOfAddItemRequest++;
                                    Logger.Info("Kerkesa \"AddItemRequest\" u perfundua!");
                                }
                                catch (Exception ex)
                                {
                                    Logger.Error(ex, "Kerkesa \"AddItemRequest\" deshtoi: " + ex.Message);
                                }

                                break;
                            case "AddReceiptLinesRequest":

                                try
                                {
                                    XDocument addLinesResponse = XDocument.Parse(data);
                                    var addLinesRequestId = TagQuery(addLinesResponse,
                                        "AddReceiptLinesRequest", "RequestID");

                                    handler.Send(ConvertToBytes(_POSReceiptEventAfter(
                                        Convert.ToInt32(addLinesRequestId))));
                                    handler.Send(ConvertToBytes(receiptlinesresponse(
                                        Convert.ToInt32(addLinesRequestId))));

                                    Logger.Info("Kerkesa \"AddReceiptLinesRequest\" u perfundua!");
                                }
                                catch (Exception ex)
                                {
                                    Logger.Error(ex, "Kerkesa \"AddReceiptLinesRequest\" deshtoi: " + ex.Message);
                                }

                                break;
                            case "RemoveReceiptLinesRequest":

                                try
                                {
                                    XDocument removeLinesResponse = XDocument.Parse(data);
                                    var removeLinesRequestId = TagQuery(removeLinesResponse,
                                        "RemoveReceiptLinesRequest", "RequestID");

                                    handler.Send(ConvertToBytes(_RemoveReceiptLinesResponse(
                                        Convert.ToInt32(removeLinesRequestId))));

                                    Logger.Info("Kerkesa \"RemoveReceiptLinesRequest\" u perfundua!");
                                }
                                catch (Exception ex)
                                {
                                    Logger.Error(ex, "Kerkesa \"RemoveReceiptLinesRequest\" deshtoi: " + ex.Message);
                                }

                                break;
                            case "CancelActionRequest":

                                try
                                {
                                    XDocument cancelActionResponse = XDocument.Parse(data);
                                    var cancelActionRequestId = TagQuery(cancelActionResponse, "CancelActionRequest", "RequestID");

                                    handler.Send(ConvertToBytes(_CancelAction(
                                        Convert.ToInt32(cancelActionRequestId))));

                                    Logger.Info("Kerkesa \"CancelActionRequest\" u perfundua!");
                                }
                                catch (Exception ex)
                                {
                                    Logger.Error(ex, "Kerkesa \"CancelActionRequest\" deshtoi: " + ex.Message);
                                }

                                break;
                            case "GetTotalsRequest":

                                try
                                {
                                    Logger.Info("Kerkesa \"GetTotalsRequest\" filloi!");

                                    XDocument getTotalsResponse = XDocument.Parse(data);
                                    var getTotalsRequestId = TagQuery(getTotalsResponse,
                                        "GetTotalsRequest", "RequestID");

                                    if (numberOfTotalRequest != 0)
                                    {
                                        Logger.Info("Funksioni totals event ka ardhe shuma e amount2 =  "
                                            + amount2 + " kurse amount4 = " + amount4);
                                        handler.Send(ConvertToBytes(_TotalsEvent(
                                            Convert.ToInt32(getTotalsRequestId), Fatura, numberOfItems, amount4)));
                                        Logger.Info("Funksioni totals event ka perfunduar, fillon totals response");
                                        handler.Send(ConvertToBytes(_GetTotalsResponse(
                                            Convert.ToInt32(getTotalsRequestId), Fatura)));
                                        Logger.Info("Funksioni totals response u perfundua");
                                    }
                                    else
                                    {

                                        Logger.Info("Funksioni PosReceiptEventTotal filloi");
                                        handler.Send(ConvertToBytes(_POSReceiptEventTotal(
                                            Convert.ToInt32(getTotalsRequestId), Fatura)));
                                        Logger.Info("Funksioni totals event ka ardhe shuma e amount2 =  "
                                            + amount2 + " kurse amount4 = " + amount4);
                                        handler.Send(ConvertToBytes(_TotalsEvent(
                                            Convert.ToInt32(getTotalsRequestId), Fatura, numberOfItems, amount4)));
                                        Logger.Info("Funksioni totals event ka perfunduar, fillon totals response");
                                        handler.Send(ConvertToBytes(_GetTotalsResponse(
                                            Convert.ToInt32(getTotalsRequestId), Fatura)));
                                        Logger.Info("Funksioni totals response u perfundua");
                                    }
                                    numberOfTotalRequest++;

                                    Logger.Info("Kerkesa \"GetTotalsRequest\" u perfundua!");
                                }
                                catch (Exception ex)
                                {
                                    Logger.Error(ex, "Kerkesa \"GetTotalsRequest\" deshtoi: " + ex.Message);
                                }

                                break;
                            case "AddTenderRequest":

                                try
                                {
                                    Logger.Info("Kerkesa \"AddTenderRequest\" filloi!");

                                    XDocument addTemderResponse = XDocument.Parse(data);
                                    var addTenderRequestId = TagQuery(addTemderResponse, "AddTenderRequest", "RequestID");
                                    var cash = TagQuery(addTemderResponse, "AddTenderRequest", "CashIdentifier");
                                    var credit = TagQuery(addTemderResponse, "AddTenderRequest", "CreditIdentifier");

                                    XmlElement pagesa = (XmlElement)doc.DocumentElement.LastChild;
                                    amount = AmountCalculation(pagesa.LastChild.Name, response: addTemderResponse);
                                    amount2 = AddAmount(amount);
                                    string _credit = "OK";
                                    bool balanceDueSatisfied = true;

                                    //check if payment cash or credit card
                                    if (cash != null)
                                    {
                                        string _cash = Fatura.ShtoPagesen(22);
                                        tender = "Cash";
                                        Logger.Info("U shtua menyra e pageses cash! Shuma: " + amount2);
                                    }
                                    else if (credit != null)
                                    {
                                        _credit = Fatura.ShtoPagesen(13, posObj, amount);
                                        tender = "Credit";
                                        Logger.Info("U shtua menyra e pageses credit! Shuma: " + amount);
                                        Thread.Sleep(5000);
                                    }
                                    int requestIdParsed = Convert.ToInt32(addTenderRequestId);

                                    handler.Send(ConvertToBytes(_POSReceiptEventTender(requestIdParsed, "Add")));
                                    handler.Send(ConvertToBytes(_POSReceiptEventTenderModify(requestIdParsed)));
                                    handler.Send(ConvertToBytes(transactionend(requestIdParsed)));

                                    //If bank POS payment did end correctly proceed, else return to payment methods 
                                    if (credit != null && _credit == "OK")
                                    {
                                        balanceDueSatisfied = true;
                                        handler.Send(ConvertToBytes(_POSReceiptEventAddCash(requestIdParsed,
                                            Fatura, amount: amount, addTender: tender)));
                                        handler.Send(ConvertToBytes(_TotalsEventWithTender(requestIdParsed,
                                            Fatura, amount: amount2, _counter: numberOfItems)));
                                        handler.Send(ConvertToBytes(_AddTenderResponseCredit(requestIdParsed,
                                            Fatura, amount, balanceDueSatisfied)));
                                    }
                                    else if (credit != null && _credit != "OK")
                                    {
                                        balanceDueSatisfied = false;
                                        //send amount 0 bc payment not finished, add error message on screen
                                        handler.Send(ConvertToBytes(_POSReceiptEventAddCash(requestIdParsed,
                                            Fatura, amount: 0.00m, addTender: tender, errorMessage: "ANULUAR")));
                                        handler.Send(ConvertToBytes(_TotalsEventWithTender(requestIdParsed,
                                            Fatura, amount: amount4, _counter: numberOfItems)));
                                        handler.Send(ConvertToBytes(_AddTenderResponseCredit(requestIdParsed,
                                            Fatura, amount, balanceDueSatisfied)));
                                        amount3 = AddAmountOfFailed(amount);
                                    }
                                    else if (cash != null)
                                    {
                                        amount4 = AddAmountOfCash(amount);
                                        amount2 = amount4;

                                        Logger.Info("Ne AddTenderRequest amount2 eshte: " + amount2 + " kurse amount4: " + amount4);

                                        handler.Send(ConvertToBytes(_POSReceiptEventAddCash(requestIdParsed,
                                            Fatura, amount: amount, addTender: tender)));
                                        handler.Send(ConvertToBytes(_TotalsEventWithTender(requestIdParsed,
                                            Fatura, amount: amount2, _counter: numberOfItems)));
                                        handler.Send(ConvertToBytes(_AddTenderResponse(requestIdParsed,
                                            Fatura, _amount: amount, tenderString: tender)));
                                        amount3 = 0;
                                    }
                                    Logger.Info("Kerkesa \"AddTenderRequest\" u perfundua!");
                                }
                                catch (Exception ex)
                                {
                                    Logger.Error(ex, "Kerkesa \"AddTenderRequest\" deshtoi: " + ex.Message);
                                }

                                break;
                            case "PrintCurrentReceiptsRequest":

                                try
                                {
                                    XDocument response5 = XDocument.Parse(data);
                                    var printReceiptRequestId = TagQuery(response5,
                                        "PrintCurrentReceiptsRequest", "RequestID");

                                    handler.Send(ConvertToBytes(_PrintReceipt(
                                        Convert.ToInt32(printReceiptRequestId))));
                                    Logger.Info("Kerkesa \"PrintCurrentReceiptsRequest\" u perfundua!");
                                }
                                catch (Exception ex)
                                {
                                    Logger.Error(ex, "Kerkesa \"PrintCurrentReceiptsRequest\" deshtoi: " + ex.Message);
                                }

                                break;
                            case "PrintRequest":

                                try
                                {

                                    XDocument printResponse = XDocument.Parse(data);
                                    string printRequestId;

                                    if (printFaulted)
                                        printRequestId = TagQuery(printResponse, "PrintRequest", "RequestID");
                                    else
                                        printRequestId = TagQuery(printResponse,
                                            "SuspendTransactionRequest", "RequestID");

                                    handler.Send(ConvertToBytes(_PosBcPrintRequest(
                                        Convert.ToInt32(printRequestId))));
                                    Logger.Info("Kerkesa \"PrintRequest\" u perfundua!");
                                    printFaulted = false;

                                }
                                catch (Exception ex)
                                {
                                    Logger.Error(ex, "Kerkesa \"PrintRequest\" deshtoi: " + ex.Message);
                                }

                                break;
                            case "PrintArchivedReceiptsRequest":
                                try
                                {
                                    XDocument printArchiveResponse = XDocument.Parse(data);
                                    var printArchivedRequestId = TagQuery(printArchiveResponse,
                                        "PrintArchivedReceiptsRequest", "RequestID");

                                    handler.Send(ConvertToBytes(_PrintArchivedReceipts(
                                        Convert.ToInt32(printArchivedRequestId))));
                                    Logger.Info("Kerkesa \"PrintArchivedReceiptsRequest\" u perfundua!");

                                }
                                catch (Exception ex)
                                {
                                    Logger.Error(ex, "Kerkesa \"PrintArchivedReceiptsRequest\" deshtoi: " + ex.Message);
                                }

                                break;
                            case "ReprintReceiptsRequest":
                                try
                                {
                                    XDocument reprintResponse = XDocument.Parse(data);
                                    var reprintRequestId = TagQuery(reprintResponse,
                                        "ReprintReceiptsRequest", "RequestID");
                                    var transactionIndex = TagQuery(reprintResponse,
                                        "ReprintReceiptsRequest", "ReverseTransactionIndex");

                                    if (transactionIndex == "1")
                                        handler.Send(ConvertToBytes(_RePrintReceipts(
                                            Convert.ToInt32(reprintRequestId))));
                                    else
                                        handler.Send(ConvertToBytes(_RePrintReceiptsNoReceipts(
                                            Convert.ToInt32(reprintRequestId))));

                                    Logger.Info("Kerkesa \"ReprintReceiptsRequest\" u perfundua!");

                                }
                                catch (Exception ex)
                                {
                                    Logger.Error(ex, "Kerkesa \"ReprintReceiptsRequest\" deshtoi: " + ex.Message);
                                }

                                break;
                            case "ReportStatusEvents":

                                try
                                {
                                    XDocument statusResponse = XDocument.Parse(data);
                                    var statusRequestId = TagQuery(statusResponse,
                                        "ReportStatusEvents", "RequestID");

                                    handler.Send(ConvertToBytes(_PosbcPrinterStatus()));
                                    handler.Send(ConvertToBytes(ReportStatusEventsRequest(
                                        Convert.ToInt32(statusRequestId))));
                                    Logger.Info("Kerkesa \"ReportStatusEvents\" u perfundua!");

                                }
                                catch (Exception ex)
                                {
                                    Logger.Error(ex, "Kerkesa \"ReportStatusEvents\" deshtoi: " + ex.Message);
                                }

                                break;
                            case "SignOffRequest":

                                Logger.Info("Kerkesa \"SignOffRequest\" filloi!");
                                try
                                {
                                    handler.Send(ConvertToBytes(_SignOff()));
                                    if (laneOpen)
                                    {
                                        Fatura.ShtoFaturen();
                                        Fatura.Ruaj();
                                    }
                                    Fatura = new FaturaBLL();
                                    Logger.Info("Fatura e vjeter u ruajt dhe u krijua ajo e reja!");
                                }
                                catch (Exception ex)
                                {
                                    Logger.Error(ex, "Gabim gjate ruajtjes se fatures ose" +
                                        " krijimit te fatures se re " + ex.Message);
                                }

                                numberOfTotalRequest = 0;
                                numberOfAddItemRequest = 1;
                                numberOfItems = 0;
                                sum = 0;
                                sumCash = 0;
                                sumFailed = 0;
                                amount3 = 0;
                                amount4 = 0;
                                amount2 = 0;
                                /*if signing off because transaction finished just break,
                                else close socket and start from the start*/
                                if (laneOpen)
                                {
                                    laneOpen = false;

                                    Logger.Info("Kerkesa \"SignOffRequest\" perfundoi!");
                                    break;
                                }
                                else
                                {
                                    listener.Close();
                                    laneOpen = false;

                                    Logger.Info("Kerkesa \"SignOffRequest\" perfundoi! Kthimi ne fillim!");
                                    goto returnToSocket;
                                }


                            case "VoidTransactionRequest":
                                Logger.Info("Kerkesa \"VoidTransactionRequest\" filloi!");
                                try
                                {
                                    XDocument voidTranResponse = XDocument.Parse(data);
                                    var voidTranRequestId = TagQuery(voidTranResponse,
                                        "SuspendTransactionRequest", "RequestID");

                                    handler.Send(ConvertToBytes(_POSReceiptEventVoid(
                                        Convert.ToInt32(voidTranRequestId), Fatura)));
                                    handler.Send(ConvertToBytes(TransactionStatus(
                                        Convert.ToInt32(voidTranRequestId), "TRANSACTION_VOID", "2")));
                                    handler.Send(ConvertToBytes(voidTransaction(
                                        Convert.ToInt32(voidTranRequestId))));

                                    Fatura = new FaturaBLL();
                                    numberOfTotalRequest = 0;
                                    numberOfAddItemRequest = 1;
                                    numberOfItems = 0;
                                    laneOpen = false;
                                    amount4 = 0;
                                    amount3 = 0;
                                    amount2 = 0;
                                    sum = 0;
                                    sumCash = 0;
                                    sumFailed = 0;
                                    Logger.Info("Kerkesa \"VoidTransactionRequest\" perfundoi!");
                                }
                                catch (Exception ex)
                                {
                                    Logger.Error(ex, "Kerkesa \"VoidTransactionRequest\" deshtoi: " + ex.Message);
                                }

                                break;

                            case "SuspendTransactionRequest":

                                try
                                {
                                    Logger.Info("Kerkesa \"SuspendTransactionRequest\" filloi!");
                                    XDocument suspendTranResponse = XDocument.Parse(data);
                                    var suspendTranRequestId = TagQuery(suspendTranResponse,
                                        "SuspendTransactionRequest", "RequestID");

                                    handler.Send(ConvertToBytes(_POSReceiptEventSuspend(
                                        Convert.ToInt32(suspendTranRequestId))));
                                    handler.Send(ConvertToBytes(TransactionStatus(
                                        Convert.ToInt32(suspendTranRequestId), "TRANSACTION_SUSPENDED", "1")));
                                    handler.Send(ConvertToBytes(SuspendTransaction(
                                        Convert.ToInt32(suspendTranRequestId))));

                                    Fatura = new FaturaBLL();
                                    numberOfTotalRequest = 0;
                                    numberOfAddItemRequest = 1;
                                    numberOfItems = 1;
                                    amount4 = 0;
                                    amount3 = 0;
                                    sum = 0;
                                    sumCash = 0;
                                    sumFailed = 0;
                                    Logger.Info("Kerkesa \"SuspendTransactionRequest\" perfundoi!");
                                }
                                catch (Exception ex)
                                {
                                    Logger.Error(ex, "Kerkesa \"SuspendTransactionRequest\" deshtoi: " + ex.Message);
                                }
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                textBox.Invoke(new MethodInvoker(() =>
                {
                    textBox.Text = e.ToString();
                }));
            }
        }

        public static TransactionTotalsType tre = new TransactionTotalsType();
        static decimal sum = 0;
        static decimal sumFailed = 0;
        static decimal sumCash = 0;
        public static decimal AddAmount(params decimal[] values)
        {
            foreach (decimal value in values)
            {
                sum += value;
            }
            return sum;
        }

        public static decimal AddAmountOfFailed(params decimal[] values)
        {
            foreach (decimal value in values)
            {
                sumFailed += value;
            }
            return sumFailed;
        }

        public static decimal AddAmountOfCash(params decimal[] values)
        {
            foreach (decimal value in values)
            {
                sumCash += value;
            }
            return sumCash;
        }

        public static string TagQuery(XDocument responseFromToshiba, string parentTag, string returnTag)
        {
            var requestedTag = (string)responseFromToshiba.Descendants(parentTag)
            .Select(e => e.Element(returnTag))
            .SingleOrDefault();
            return requestedTag;
        }

        public static byte[] InitializignStatus(string status)
        {
            POSBCStatusType posBCstatustype = new POSBCStatusType();
            posBCstatustype.Status = status;
            posBCstatustype.StatusMessage = status;
            posBCstatustype.Severity = SeverityLevelType.INFO;

            POSBCStatusEventType posBCStatEvent = new POSBCStatusEventType();

            posBCStatEvent.POSBCStatus = posBCstatustype;
            posBCStatEvent.RequestID = 1;
            posBCStatEvent.RequestIDSpecified = true;

            return XmlSerializing(posBCStatEvent, "Statuses");
        }
        public static decimal AmountCalculation(string paymentDesc, XDocument response)
        {
            var amount = (string)response.Descendants(paymentDesc)
                .Select(e => e.Element("Amount"))
                .SingleOrDefault();

            return Convert.ToDecimal(amount);
        }
        public static byte[] InitialResponse()
        {
            InitializeResultType InitialResult = new InitializeResultType();
            InitialResult.RequestID = 1;
            InitialResult.RequestIDSpecified = true;

            InitializeResponseType InResponse = new InitializeResponseType();
            InResponse.InitializeResult = InitialResult;

            return XmlSerializing(InResponse, "InitialResponse");
        }
        public static byte[] QueryResponse()
        {
            QueryStatusResultType QstatRes = new QueryStatusResultType();
            QstatRes.RequestID = 2;
            QstatRes.RequestIDSpecified = true;
            QstatRes.APIHistory = null;
            QstatRes.POSBCStatus = "NOT_IN_TRANSACTION";
            VersionType typeVersion = new VersionType();
            typeVersion.Version = 4;
            typeVersion.VersionSpecified = true;
            typeVersion.Release = 2;
            typeVersion.ReleaseSpecified = true;
            typeVersion.MaintenanceLevel = "2018";
            typeVersion.Description = "Toshiba Store Integrator Application" +
                " Extension Facility version: 4.2.2018 Built:" +
                " Oct 15, 2018 06:12 PM (SI.base.wyvern )";
            QstatRes.POSBCVersion = typeVersion;
            QstatRes.CurrentDateAndTime = DateTime.Now.ToString("MMMM dd, yyyy HH:mm:ss");
            //"February 29, 2020 11:27:02 AM CET";
            QstatRes.LastSuccessfulRequestID = 2;
            QstatRes.LastSuccessfulRequestIDSpecified = true;
            QstatRes.IsInTransaction = false;
            QstatRes.IsInTransactionSpecified = true;

            QueryStatusResponseType QStatus = new QueryStatusResponseType();
            QStatus.QueryStatusResult = QstatRes;

            return XmlSerializing(QStatus, "QueryStatus");
        }
        public static byte[] QueryResponsewith3()
        {

            QueryStatusResultType QstatRes = new QueryStatusResultType();
            QstatRes.RequestID = 3;
            QstatRes.RequestIDSpecified = true;
            QstatRes.APIHistory = null;
            QstatRes.POSBCStatus = "NOT_IN_TRANSACTION";
            VersionType typeVersion = new VersionType();
            typeVersion.Version = 4;
            typeVersion.VersionSpecified = true;
            typeVersion.Release = 2;
            typeVersion.ReleaseSpecified = true;
            typeVersion.MaintenanceLevel = "2018";
            typeVersion.Description = "Toshiba Store Integrator Application" +
                " Extension Facility version: 4.2.2018 Built:" +
                " Oct 15, 2018 06:12 PM (SI.base.wyvern )";
            QstatRes.POSBCVersion = typeVersion;
            QstatRes.CurrentDateAndTime = DateTime.Now.ToString("MMMM dd, yyyy HH:mm:ss");
            QstatRes.LastSuccessfulRequestID = 2;
            QstatRes.LastSuccessfulRequestIDSpecified = true;
            QstatRes.IsInTransaction = false;
            QstatRes.IsInTransactionSpecified = true;

            QueryStatusResponseType QStatus = new QueryStatusResponseType();
            QStatus.QueryStatusResult = QstatRes;

            return XmlSerializing(QStatus, "QueryStatus");
        }
        public static byte[] ReportStatusEventsRequest(int id)
        {

            ReportStatusEventsResultType ReportER = new ReportStatusEventsResultType();
            ReportER.RequestID = id;
            ReportER.RequestIDSpecified = true;


            ReportStatusEventsResponseType ReportEventStat = new ReportStatusEventsResponseType();
            ReportEventStat.ReportStatusEventsResult = ReportER;

            return XmlSerializing(ReportEventStat, "ReportEventtatus");
        }
        public static byte[] _PosbcPrinterStatus()
        {
            POSBCStatusEventType sts = new POSBCStatusEventType();
            PrinterStatusType printerStatus = new PrinterStatusType();

            printerStatus.Severity = SeverityLevelType.WARNING;
            printerStatus.Status = "NOT_INTITIALIZED";
            printerStatus.StatusMessage = "Printer Not Initialized";
            sts.PrinterStatus = printerStatus;
            return XmlSerializing(printerStatus, "PrinterStatus");
        }
        public static byte[] _PosBcPrintRequest(int id)
        {
            PrintResponseType print = new PrintResponseType();
            PrintResultType printerStatus = new PrintResultType();
            ExceptionResultType exp = new ExceptionResultType();

            printerStatus.RequestID = id;
            printerStatus.RequestIDSpecified = true;
            exp.Message = "Printer Not Initialized";
            exp.ErrorCode = "POSBC_PRINTER_NOT_INITIALIZED";
            printerStatus.ExceptionResult = exp;
            print.PrintResult = printerStatus;

            return XmlSerializing(print, "PrintAgain");
        }
        public static byte[] _POSReceiptEven(int id)
        {
            TextReceiptLineType r1 = new TextReceiptLineType();
            r1.Text = "***************************************";

            TextReceiptLineType r2 = new TextReceiptLineType();
            r2.Text = "Mire se vini në arkat veteshitese";

            TextReceiptLineType r3 = new TextReceiptLineType();
            r3.Text = "***************************************";

            FormattedReceiptLineType h1 = new FormattedReceiptLineType();
            h1.Feeds = 2;
            h1.Align = "center";
            h1.LineCategory = "Header";
            h1.LineType = "StoreHeader";
            h1.TextReceiptLine = r1;

            FormattedReceiptLineType h2 = new FormattedReceiptLineType();
            h2.Feeds = 2;
            h2.Align = "center";
            h2.LineCategory = "Header";
            h2.LineType = "StoreHeader";
            h2.TextReceiptLine = r2;

            FormattedReceiptLineType h3 = new FormattedReceiptLineType();
            h3.Feeds = 2;
            h3.Align = "center";
            h3.LineCategory = "Header";
            h3.LineType = "StoreHeader";
            h3.TextReceiptLine = r3;

            POSReceiptEventType pe = new POSReceiptEventType();
            pe.RequestID = id;
            pe.RequestIDSpecified = true;
            pe.Type = "Customer";
            pe.Index = Convert.ToInt32(0);
            pe.Section = "Header";
            pe.Group = 6;
            pe.GroupSpecified = true;
            pe.UpdateType = "Add";
            pe.FormattedReceiptLineList = new FormattedReceiptLineType[] { h1, h2, h3 };

            return XmlSerializing(pe, "POSReceiptEvent");
        }
        public static byte[] _POSReceiptEvenItem(int id, 
            FaturaBLL _fatura, bool flagu, int quantity)
        {
            FormattedReceiptLineType h1 = new FormattedReceiptLineType();
            TextReceiptLineType r1 = new TextReceiptLineType();
            h1.Feeds = 2;
            h1.Align = "center";
            h1.LineCategory = "LineItem";
            h1.TextReceiptLine = r1;

            int count = 0;
            foreach (char c in _fatura.ArtikulliIFunditIShtuar.Emertimi)
            {
                count++;
            }

            if (flagu)
            {
                if (count <= 24)
                {
                    r1.Text = (_fatura.ArtikulliIFunditIShtuar.Emertimi + "            "
                        + (_fatura.ArtikulliIFunditIShtuar.QmimiShitjes * quantity)
                        .ToString("-0.00"));
                }
                else
                    r1.Text = (_fatura.ArtikulliIFunditIShtuar.Emertimi.Substring(0, 24) + "         "
                        + (_fatura.ArtikulliIFunditIShtuar.QmimiShitjes * quantity)
                        .ToString("-0.00"));
                h1.LineType = "ItemVoid";
            }
            else
            {
                if (count <= 24)
                {
                    r1.Text = (_fatura.ArtikulliIFunditIShtuar.Emertimi + "            "
                        + (_fatura.ArtikulliIFunditIShtuar.QmimiShitjes * quantity)
                        .ToString("0.00"));
                }
                else
                    r1.Text = (_fatura.ArtikulliIFunditIShtuar.Emertimi.Substring(0, 24) + "         "
                        + (_fatura.ArtikulliIFunditIShtuar.QmimiShitjes * quantity)
                        .ToString("0.00"));
                h1.LineType = "ItemSale";
            }

            POSReceiptEventType pe = new POSReceiptEventType();
            pe.RequestID = id;
            pe.RequestIDSpecified = true;
            pe.Type = "Customer";
            pe.Index = Convert.ToInt32(0);
            pe.Section = "Body";
            pe.Group = id;
            pe.GroupSpecified = true;
            pe.UpdateType = "Add";

            if (quantity > 1)
            {
                TextReceiptLineType r2 = new TextReceiptLineType();
                FormattedReceiptLineType h2 = new FormattedReceiptLineType();
                r2.Text = "     " + quantity.ToString() + "@ " +
                    _fatura.ArtikulliIFunditIShtuar.QmimiShitjes.ToString("0.00");
                h2.LineType = "ItemSale";
                h2.Feeds = 2;
                h2.Align = "center";
                h2.LineCategory = "LineItem";
                h2.TextReceiptLine = r2;
                pe.FormattedReceiptLineList = new FormattedReceiptLineType[] { h2, h1 };
            }
            else
                pe.FormattedReceiptLineList = new FormattedReceiptLineType[] { h1 };

            return XmlSerializing(pe, "POSReceiptEvent");
        }
        public static byte[] _POSReceiptEventAfter(int id)
        {
            POSReceiptEventType pe = new POSReceiptEventType();
            pe.UpdateType = "Add";
            pe.RequestID = id;
            pe.RequestIDSpecified = true;
            pe.Type = "Customer";
            pe.Index = 0;
            pe.Section = "Body";
            pe.Group = id;
            pe.GroupSpecified = true;

            TextReceiptLineType r1 = new TextReceiptLineType();
            r1.Bold = true;
            r1.BoldSpecified = true;
            r1.Text = "*** No Security ***";

            FormattedReceiptLineType h1 = new FormattedReceiptLineType();
            h1.Feeds = 2;
            h1.Align = "center";
            h1.LineCategory = "LineItem";
            h1.LineType = "PersistentAdditionalLine";
            h1.TextReceiptLine = r1;
            pe.FormattedReceiptLineList = new FormattedReceiptLineType[] { h1 };

            return XmlSerializing(pe, "POSReceiptEvent");
        }
        public static byte[] _TotalsEvent(int id, FaturaBLL _Fatura,
            int _counter3, decimal amountPaid = 0, decimal? largimiProduktit = null)
        {
            TotalsEventType te = new TotalsEventType();
            KeyValuePairType ke = new KeyValuePairType();

            ke.Key = "EchoTax";
            ke.Value = "0";

            TransactionTotalsType tre = new TransactionTotalsType();
            try
            {
                tre.Total = Math.Round(_Fatura.TotaliFatures, 2, MidpointRounding.AwayFromZero);
                tre.SubTotal = Math.Round(_Fatura.Subtotali, 2, MidpointRounding.AwayFromZero);
                tre.Tax = Math.Round(_Fatura.TotaliVleraTvsh, 2, MidpointRounding.AwayFromZero);

                Logger.Info("Totali Fatures: " + Fatura.TotaliFatures +
                    " Parat e dhena: " + amountPaid);

                if (Fatura.TotaliFatures - amountPaid < 0)
                    tre.BalanceDue = 0.00m;
                else
                    tre.BalanceDue = Math.Round(_Fatura.TotaliFatures, 2,
                        MidpointRounding.AwayFromZero) - amountPaid;
                tre.TenderApplied = amountPaid;

                Logger.Info("Balance due: " + tre.BalanceDue);
            }
            catch (Exception ex)
            {
                tre.Total = 0.00m;
                tre.SubTotal = 0.00m;
                tre.Tax = 0.00m;
                tre.BalanceDue = 0.00m;
                Logger.Error(ex, "Error ne total te fatures: " + ex.Message);
            }
            tre.ChangeDue = 0.00m;
            tre.FoodstampChangeDue = 0.00m;
            tre.FoodstampChangeDueSpecified = true;
            tre.FoodstampTotal = 0.00m;
            tre.FoodstampTotalSpecified = true;
            tre.FoodstampBalanceDue = 0.00m;
            tre.FoodstampBalanceDueSpecified = true;
            tre.CouponTotal = 0.00m;
            tre.CouponTotalSpecified = true;
            tre.TotalItems = _counter3;
            tre.TotalItemsSpecified = true;
            tre.TotalCoupons = 0;
            tre.TotalCouponsSpecified = true;
            tre.TotalSavings = 0.00m;
            tre.TotalSavingsSpecified = true;
            tre.TenderAppliedSpecified = true;

            te.RequestID = id;
            te.RequestIDSpecified = true;
            te.ParameterExtension = new KeyValuePairType[] { ke };
            te.TransactionTotals = tre;

            //_counter3++;
            return XmlSerializing(te, "TotalsEvent");

        }
        public static byte[] _TotalsEventWithTender(int id,
            FaturaBLL Fatura, decimal amount, int _counter)
        {
            TotalsEventType te = new TotalsEventType();
            KeyValuePairType ke = new KeyValuePairType();
            ke.Key = "EchoTax";
            ke.Value = "0";

            tre.Total = Fatura.TotaliFatures;
            tre.SubTotal = Fatura.Subtotali;
            tre.Tax = Fatura.TotaliVleraTvsh;
            if (Fatura.TotaliFatures - amount < 0)
                tre.BalanceDue = 0.00m;
            else
                tre.BalanceDue = Fatura.TotaliFatures - amount;
            tre.TenderApplied = amount;
            tre.TenderAppliedSpecified = true;
            tre.ChangeDue = tre.TenderApplied - Fatura.TotaliFatures;
            tre.FoodstampChangeDue = 0.00m;
            tre.FoodstampChangeDueSpecified = true;
            tre.FoodstampTotal = 0.00m;
            tre.FoodstampTotalSpecified = true;
            tre.FoodstampBalanceDue = 0.00m;
            tre.FoodstampBalanceDueSpecified = true;
            tre.CouponTotal = 0.00m;
            tre.CouponTotalSpecified = true;
            tre.TotalItems = Convert.ToInt32(_counter);
            tre.TotalItemsSpecified = true;
            tre.TotalCoupons = 0;
            tre.TotalCouponsSpecified = true;
            tre.TotalSavings = 0.00m;
            tre.TotalSavingsSpecified = true;

            te.RequestID = id;
            te.RequestIDSpecified = true;
            te.ParameterExtension = new KeyValuePairType[] { ke };
            te.TransactionTotals = tre;

            //counter++;
            return XmlSerializing(te, "TotalsEvent");
        }
        public static byte[] _AddItemResponse(int id, FaturaBLL _Fatura,
            string barkodi, bool voidArticle, decimal quantity)
        {
            AddItemResponseType at = new AddItemResponseType();
            AddItemResultType ait = new AddItemResultType();
            ItemInfoType iteminfo = new ItemInfoType();

            iteminfo.ItemIdentifier = barkodi;
            iteminfo.ItemEntryMethod = "ScannedItemCode";
            iteminfo.RegularUnitPrice = Math.Round(
                _Fatura.ArtikulliIFunditIShtuar.QmimiShitjes,
                2, MidpointRounding.AwayFromZero);

            iteminfo.RegularUnitPriceSpecified = true;

            iteminfo.VoidFlag = Convert.ToBoolean(voidArticle);
            if (iteminfo.VoidFlag == true)
            {
                iteminfo.ExtendedPrice = -Math.Round(
                    _Fatura.ArtikulliIFunditIShtuar.QmimiShitjes,
                    2, MidpointRounding.AwayFromZero);

                iteminfo.Quantity = -1;
            }
            else
            {
                iteminfo.Quantity = quantity;
                iteminfo.ExtendedPrice = Math.Round(
                    _Fatura.ArtikulliIFunditIShtuar.QmimiShitjes * iteminfo.Quantity,
                    2, MidpointRounding.AwayFromZero);
            }
            iteminfo.VoidFlagSpecified = true;
            iteminfo.ExtendedPriceSpecified = true;
            iteminfo.Weight = 0;
            iteminfo.WeightSpecified = true;
            iteminfo.DealPrice = 0.00m;
            iteminfo.DealPriceSpecified = true;
            iteminfo.DealQuantity = 0;
            iteminfo.DealQuantitySpecified = true;
            iteminfo.DepartmentNumber = "1";
            iteminfo.MixAndMatchPricingGroup = "0";
            iteminfo.PriceDerivationMethod = "unit";
            iteminfo.TimeRestrictedFlag = false;
            iteminfo.TimeRestrictedFlagSpecified = true;
            iteminfo.RestrictedAge = "0";
            iteminfo.FoodStampEligibleFlag = false;
            iteminfo.FoodStampEligibleFlagSpecified = true;
            iteminfo.WICEligibleFlag = false;
            iteminfo.WICEligibleFlagSpecified = true;
            iteminfo.ItemRepeatAllowedFlag = true;
            iteminfo.ItemRepeatAllowedFlagSpecified = true;
            iteminfo.TaxableFlag = false;
            iteminfo.TaxableFlagSpecified = true;
            iteminfo.Description = _Fatura.ArtikulliIFunditIShtuar.Emertimi;
            iteminfo.ReturnFlag = false;
            iteminfo.ReturnFlagSpecified = true;
            iteminfo.DepositFlag = false;
            iteminfo.DepositFlagSpecified = true;
            iteminfo.QuantitySpecified = true;

            ait.RequestID = id;
            ait.RequestIDSpecified = true;
            ait.LineItem = new ItemInfoType[] { iteminfo };
            at.AddItemResult = ait;

            return XmlSerializing(at, "AddItemResponse");
        }

        public static byte[] _AddItemNotFoundResponse(int id)
        {
            AddItemResponseType at = new AddItemResponseType();
            AddItemResultType ait = new AddItemResultType();
            ExceptionResultType exp = new ExceptionResultType();
            exp.Message = "Item not found on system";
            exp.ErrorCode = "ITEM_NOT_FOUND";
            ait.ExceptionResult = exp;
            at.AddItemResult = ait;

            return XmlSerializing(at, "AddItemResponse");
        }
        public static byte[] _GetTotalsResponse(int id, FaturaBLL _Fatura)
        {
            GetTotalsResponseType getotals = new GetTotalsResponseType();
            GetTotalsResultType result = new GetTotalsResultType();
            TransactionTotalsType transactotal = new TransactionTotalsType();
            try
            {
                transactotal.Total = Math.Round(_Fatura.TotaliFatures,
                    2, MidpointRounding.AwayFromZero);
                transactotal.SubTotal = Math.Round(_Fatura.Subtotali,
                    2, MidpointRounding.AwayFromZero);
                transactotal.Tax = Math.Round(_Fatura.TotaliVleraTvsh,
                    2, MidpointRounding.AwayFromZero);
                transactotal.BalanceDue = Math.Round(_Fatura.TotaliFatures,
                    2, MidpointRounding.AwayFromZero);
            }
            catch (Exception)
            {
                transactotal.Total = 0.00m;
                transactotal.SubTotal = 0.00m;
                transactotal.Tax = 0.00m;
                transactotal.BalanceDue = 0.00m;
            }

            transactotal.ChangeDue = 0.00m;
            transactotal.FoodstampChangeDue = 0.00m;
            transactotal.FoodstampChangeDueSpecified = true;
            transactotal.FoodstampTotal = 0.00m;
            transactotal.FoodstampTotalSpecified = true;
            transactotal.FoodstampBalanceDue = 0.00m;
            transactotal.FoodstampBalanceDueSpecified = true;
            transactotal.CouponTotal = 0.00m;
            transactotal.CouponTotalSpecified = true;
            transactotal.TotalItems = Convert.ToInt32(
                _Fatura.ArtikulliIFunditIShtuar.Sasia);
            transactotal.TotalItemsSpecified = true;
            transactotal.TotalCoupons = 0;
            transactotal.TotalCouponsSpecified = true;
            transactotal.TotalSavings = 0.00m;
            transactotal.TotalSavingsSpecified = true;
            transactotal.TenderApplied = 0.00m;
            transactotal.TenderAppliedSpecified = true;

            result.RequestID = id;
            result.RequestIDSpecified = true;
            result.TransactionTotals = transactotal;

            getotals.GetTotalsResult = result;

            return XmlSerializing(getotals, "GetTotals");
        }
        public static byte[] transactionend(int id)
        {
            TransactionStatusEventType status = new TransactionStatusEventType();
            status.RequestID = id;
            TransactionStatusType sts = new TransactionStatusType();
            sts.Status = "TRANSACTION_END";
            sts.ID = "1";
            sts.Type = "regularSale";
            sts.Category = "sales";
            sts.Date = DateTime.Now.Date.ToString();
            sts.Time = DateTime.Now.ToString("HH:mm");

            return XmlSerializing(status, "TransactionStatus");
        }
        public static byte[] receiptlinesresponse(int id)
        {
            AddReceiptLinesResponseType lines = new AddReceiptLinesResponseType();
            AddReceiptLinesResultType linesresult = new AddReceiptLinesResultType
            {
                RequestID = id,
                RequestIDSpecified = true
            };
            lines.AddReceiptLinesResult = linesresult;

            return XmlSerializing(lines, "receiptlinesresponse");
        }
        public static byte[] _RemoveReceiptLinesResponse(int id)
        {
            RemoveReceiptLinesResponseType removeLine = new RemoveReceiptLinesResponseType();
            RemoveReceiptLinesResultType removeResult = new RemoveReceiptLinesResultType
            {
                RequestID = id,
                RequestIDSpecified = true
            };
            removeLine.RemoveReceiptLinesResult = removeResult;

            return XmlSerializing(removeLine, "RemoveLinesResponse");
        }

        public static byte[] _POSReceiptEventTotal(int id, FaturaBLL _Fatura)
        {
            POSReceiptEventType pe = new POSReceiptEventType();
            pe.UpdateType = "Add";
            pe.RequestID = id;
            pe.RequestIDSpecified = true;
            pe.Type = "Customer";
            pe.Index = Convert.ToInt32(0);
            pe.Section = "Body";
            pe.Group = id;
            pe.GroupSpecified = true;
            decimal tempDec;
            decimal tempTot;
            try
            {
                tempDec = _Fatura.TotaliVleraTvsh;
                tempTot = _Fatura.TotaliFatures;
            }
            catch (Exception)
            {
                tempDec = 0.00m;
                tempTot = 0.00m;
            }
            TextReceiptLineType r1 = new TextReceiptLineType();
            r1.Text = "        TAX                       " + tempDec.ToString("0.00");

            TextReceiptLineType r2 = new TextReceiptLineType();
            r2.Text = "   **** BALANCE                  " + tempTot;

            FormattedReceiptLineType h1 = new FormattedReceiptLineType();
            h1.Feeds = 2;
            h1.Align = "center";
            h1.LineCategory = "LineItem";
            h1.LineType = "TransactionTotal";
            h1.TextReceiptLine = r1;

            FormattedReceiptLineType h2 = new FormattedReceiptLineType();
            h2.Feeds = 2;
            h2.Align = "center";
            h2.LineCategory = "LineItem";
            h2.LineType = "TransactionTotal";
            h2.TextReceiptLine = r2;

            pe.FormattedReceiptLineList = new FormattedReceiptLineType[] { h1, h2 };

            return XmlSerializing(pe, "POSReceiptEvent");
        }
        public static byte[] _POSReceiptEventTender(int id, string updateType)
        {
            int counter = 1;
            POSReceiptEventType pe = new POSReceiptEventType();
            pe.UpdateType = updateType;
            pe.RequestID = id;
            pe.RequestIDSpecified = true;
            pe.Type = "Customer";
            pe.Index = Convert.ToInt32(0);
            pe.Section = "Trailer";
            pe.Group = id;
            pe.GroupSpecified = true;

            TextReceiptLineType r1 = new TextReceiptLineType();
            r1.Text = DateTime.Now.ToString() + "                       " + counter;

            FormattedReceiptLineType h1 = new FormattedReceiptLineType
            {
                Feeds = 2,
                Align = "center",
                LineCategory = "Trailer",
                LineType = "WorkstationInfo",
                TextReceiptLine = r1
            };

            pe.FormattedReceiptLineList = new FormattedReceiptLineType[] { h1 };

            counter++;
            return XmlSerializing(pe, "POSReceiptEvent");
        }
        public static byte[] _POSReceiptEventTenderModify(int id)
        {
            int counter = 1;
            POSReceiptEventType pe = new POSReceiptEventType();
            pe.UpdateType = "Modify";
            pe.RequestID = id;
            pe.RequestIDSpecified = true;
            pe.Type = "Customer";
            pe.Index = Convert.ToInt32(0);
            pe.Section = "Trailer";
            pe.Group = id;
            pe.GroupSpecified = true;

            TextReceiptLineType r1 = new TextReceiptLineType();
            r1.Text = DateTime.Now.ToString() + "                       " + counter;

            TextReceiptLineType r2 = new TextReceiptLineType();
            r2.Text = "***************************************";

            TextReceiptLineType r3 = new TextReceiptLineType();
            r2.Text = "Faleminderit që kryet blerjet me ne!";

            FormattedReceiptLineType h1 = new FormattedReceiptLineType();
            h1.Feeds = 1;
            h1.Align = "center";
            h1.LineCategory = "Trailer";
            h1.LineType = "WorkstationInfo";
            h1.TextReceiptLine = r1;

            FormattedReceiptLineType h2 = new FormattedReceiptLineType();
            h2.Feeds = 1;
            h2.Align = "center";
            h2.LineCategory = "Trailer";
            h2.LineType = "LoyaltyMessages";
            h2.TextReceiptLine = r2;

            FormattedReceiptLineType h3 = new FormattedReceiptLineType();
            h3.Feeds = 1;
            h3.Align = "center";
            h3.LineCategory = "Trailer";
            h3.LineType = "LoyaltyMessages";
            h3.TextReceiptLine = r3;

            FormattedReceiptLineType h4 = new FormattedReceiptLineType();
            h3.Feeds = 1;
            h3.Align = "center";
            h3.LineCategory = "Trailer";
            h3.LineType = "LoyaltyMessages";
            h3.TextReceiptLine = r2;

            pe.FormattedReceiptLineList = new FormattedReceiptLineType[] { h1, h2, h3, h4 };

            counter++;
            return XmlSerializing(pe, "POSReceiptEvent");
        }
        public static byte[] _POSReceiptEventSuspend(int id)
        {
            int counter = 1;
            POSReceiptEventType pe = new POSReceiptEventType
            {
                UpdateType = "Add",
                RequestID = id,
                RequestIDSpecified = true,
                Type = "Customer",
                Index = Convert.ToInt32(0),
                Section = "Trailer",
                Group = id,
                GroupSpecified = true
            };

            TextReceiptLineType r1 = new TextReceiptLineType
            {
                Text = DateTime.Now.ToString() +
                "                       " + counter
            };

            TextReceiptLineType r2 = new TextReceiptLineType
            {
                Text = " *TRANSACTION SUSPENDED*              "
            };

            FormattedReceiptLineType h1 = new FormattedReceiptLineType
            {
                Feeds = 1,
                Align = "center",
                LineCategory = "LineItem",
                LineType = "SuspendTransaction",
                TextReceiptLine = r2
            };

            FormattedReceiptLineType h2 = new FormattedReceiptLineType
            {
                Feeds = 2,
                Align = "center",
                LineCategory = "Trailer",
                LineType = "WorkstationInfo",
                TextReceiptLine = r1
            };

            pe.FormattedReceiptLineList = new FormattedReceiptLineType[] { h1, h2 };
            counter++;
            return XmlSerializing(pe, "POSReceiptEvent");
        }
        public static byte[] _POSReceiptEventAddCash(int id,
            FaturaBLL _Fatura, decimal amount, string addTender,
            string errorMessage = null)
        {
            POSReceiptEventType pe = new POSReceiptEventType
            {
                UpdateType = "Add",
                RequestID = id,
                RequestIDSpecified = true,
                Type = "Customer",
                Index = Convert.ToInt32(0),
                Section = "Body",
                Group = id,
                GroupSpecified = true
            };

            TextReceiptLineType r1 = new TextReceiptLineType
            {
                Text = amount != 0 ? "        " + addTender + "                      "
                + amount.ToString("0.00") : "        " + addTender + "       "
                + errorMessage + "      " + amount.ToString("0.00")
            };

            TextReceiptLineType r2 = new TextReceiptLineType();
            r2.Text = "        CHANGE                    " +
                (amount > 0 ? amount - _Fatura.TotaliFatures : 0.00m).ToString();

            FormattedReceiptLineType h1 = new FormattedReceiptLineType
            {
                Feeds = 2,
                Align = "center",
                LineCategory = "LineItem",
                LineType = "Tender",
                TextReceiptLine = r1
            };

            FormattedReceiptLineType h2 = new FormattedReceiptLineType
            {
                Feeds = 2,
                Align = "center",
                LineCategory = "LineItem",
                LineType = "Change",
                TextReceiptLine = r2
            };

            pe.FormattedReceiptLineList = new FormattedReceiptLineType[] { h1, h2 };

            return XmlSerializing(pe, "POSReceiptEvent");
        }
        public static byte[] _POSReceiptEventVoid(int id, FaturaBLL _fatura)
        {
            POSReceiptEventType pe = new POSReceiptEventType
            {
                UpdateType = "Add",
                RequestID = id,
                RequestIDSpecified = true,
                Type = "Customer",
                Index = 0,
                Section = "Trailer",
                Group = id,
                GroupSpecified = true
            };

            TextReceiptLineType r1 = new TextReceiptLineType();
            r1.Text = " *Përfundim i transaksionit*               ";

            FormattedReceiptLineType h1 = new FormattedReceiptLineType
            {
                Feeds = 1,
                Align = "center",
                LineCategory = "LineItem",
                LineType = "VoidTransaction",
                TextReceiptLine = r1
            };
            pe.FormattedReceiptLineList = new FormattedReceiptLineType[] { h1 };

            return XmlSerializing(pe, "POSReceiptEvent");
        }
        public static byte[] _AddTenderResponseCredit(int id,
            FaturaBLL _fatura, decimal _amount, bool _balanceDueSatisfied)
        {
            AddTenderResponseType addTender = new AddTenderResponseType();
            AddTenderResultType addResult = new AddTenderResultType();
            CreditInfoType creditInfo = new CreditInfoType();
            creditInfo.Description = "CREDIT";
            creditInfo.ReturnFlag = false;
            creditInfo.ReturnFlagSpecified = true;
            creditInfo.DepositFlag = false;
            creditInfo.DepositFlagSpecified = true;
            creditInfo.VoidFlag = false;
            creditInfo.VoidFlagSpecified = true;
            creditInfo.LineItemType = "EPSEFT";
            creditInfo.BalanceDueSatisfied = _balanceDueSatisfied; //qitu munesh
            if (!creditInfo.BalanceDueSatisfied)
                creditInfo.Amount = 0.00m;
            creditInfo.BalanceDueSatisfiedSpecified = true;
            creditInfo.IsDeclined = false;
            creditInfo.IsDeclinedSpecified = true;

            addResult.SignatureNeeded = false;
            addResult.SignatureIndexSpecified = false;
            addResult.CreditInfo = creditInfo;

            addTender.AddTenderResult = addResult;

            return XmlSerializing(addTender, "AddTenderResponse");
        }
        public static byte[] _AddTenderResponse(int id,
            FaturaBLL _fatura, decimal _amount, string tenderString)
        {
            AddTenderResponseType addTender = new AddTenderResponseType();
            AddTenderResultType addResult = new AddTenderResultType();
            CashInfoType cashInfo = new CashInfoType
            {
                ReturnFlag = false,
                ReturnFlagSpecified = true,
                DepositFlag = false,
                DepositFlagSpecified = true,
                VoidFlag = false,
                VoidFlagSpecified = true,
                AmountSpecified = true,
                LineItemType = tenderString,
                Amount = _amount
            };

            if (tre.BalanceDue != 0.00m)
            {
                cashInfo.BalanceDueSatisfied = false;
            }
            else
            {
                cashInfo.BalanceDueSatisfied = true;
                cashInfo.Change = cashInfo.Amount - _fatura.TotaliFatures;
            }
            cashInfo.BalanceDueSatisfiedSpecified = true;
            cashInfo.ChangeSpecified = true;

            addResult.RequestID = id;
            addResult.RequestIDSpecified = true;
            addResult.SignatureNeeded = false;
            addResult.CashInfo = cashInfo;
            addTender.AddTenderResult = addResult;

            return XmlSerializing(addTender, "AddTenderResponse");
        }
        public static byte[] _PrintReceipt(int id)
        {
            PrintCurrentReceiptsResponseType print = new PrintCurrentReceiptsResponseType
            {
                PrintCurrentReceiptsResult = new PrintCurrentReceiptsResultType
                {
                    RequestID = id,
                    RequestIDSpecified = true,
                    ExceptionResult = new ExceptionResultType
                    {
                        Message = "Printer Not Initialized",
                        ErrorCode = "POSBC_PRINTER_NOT_INITIALIZED"
                    },
                }
            };

            return XmlSerializing(print, "PrintReceipt");
        }
        public static byte[] _PrintArchivedReceipts(int id)
        {
            PrintArchivedReceiptsResponseType printArchived = new PrintArchivedReceiptsResponseType
            {
                PrintArchivedReceiptsResult = new PrintArchivedReceiptsResultType
                {
                    ExceptionResult = new ExceptionResultType
                    {
                        Message = "ArchiveImpl: No archived receipts available.",
                        ErrorCode = "ARCHIVED_RECEIPTS_NOT_AVAILABLE"
                    },
                    RequestID = id,
                    RequestIDSpecified = true
                }
            };

            return XmlSerializing(printArchived, "PrintArchivedReceipts");
        }
        public static byte[] _RePrintReceipts(int id)
        {
            ReprintReceiptsResponseType reprint = new ReprintReceiptsResponseType
            {
                ReprintReceiptsResult = new ReprintReceiptsResultType
                {
                    RequestID = id,
                    RequestIDSpecified = true,
                    ExceptionResult = new ExceptionResultType
                    {
                        Message = "Printer Not Initialized",
                        ErrorCode = "POSBC_PRINTER_NOT_INITIALIZED"
                    }
                }
            };
            return XmlSerializing(reprint, "RePrint");
        }
        public static byte[] _RePrintReceiptsNoReceipts(int id)
        {
            ReprintReceiptsResponseType reprint = new ReprintReceiptsResponseType
            {
                ReprintReceiptsResult = new ReprintReceiptsResultType
                {
                    RequestID = id,
                    RequestIDSpecified = true,
                    ExceptionResult = new ExceptionResultType
                    {
                        Message = "Transaction Receipts Not Available",
                        ErrorCode = "TRANSACTION_RECEIPTS_NOT_AVAILABLE"
                    }
                }
            };

            return XmlSerializing(reprint, "RePrint");
        }
        public static byte[] _SignOff()
        {
            SignOffResponseType signOff = new SignOffResponseType();
            signOff.SignOffResult = new SignOffResultType();

            return XmlSerializing(signOff, "SignOff");
        }

        public static byte[] _CancelAction(int id)
        {
            CancelActionResponseType cancelAction = new CancelActionResponseType
            {
                CancelActionResult = new CancelActionResultType
                {
                    RequestID = id,
                    RequestIDSpecified = true
                }
            };

            return XmlSerializing(cancelAction, "CancelAction");
        }

        public static byte[] TransactionStatus(int id, string transactionStatus, string transactionId)
        {
            TransactionStatusEventType status = new TransactionStatusEventType
            {
                RequestID = id,
                RequestIDSpecified = true,
                TransactionStatus = new TransactionStatusType
                {
                    Status = transactionStatus,
                    ID = transactionId,
                    Type = "regularSale",
                    Category = "sales",
                    Date = DateTime.Now.Date.ToString(),
                    Time = DateTime.Now.ToString("HH:mm")
                }
            };

            return XmlSerializing(status, "TransactionStatus");
        }
        public static byte[] StartTransaction(int id)
        {
            TransactionStatusEventType se = new TransactionStatusEventType
            {
                RequestID = id,
                RequestIDSpecified = true,
                TransactionStatus = new TransactionStatusType
                {
                    Status = "TRANSACTION_START",
                    ID = "1",
                    Type = "regularSale",
                    Category = "sales",
                    Date = DateTime.Now.Date.ToString(),
                    Time = DateTime.Now.ToString("HH:mm")
                }
            };

            return XmlSerializing(se, "TransactionStatus");
        }
        public static byte[] voidTransaction(int id)
        {
            VoidTransactionResponseType voidTransaction = new VoidTransactionResponseType
            {
                VoidTransactionResult = new VoidTransactionResultType
                {
                    RequestID = id,
                    RequestIDSpecified = true
                }
            };

            return XmlSerializing(voidTransaction, "VoidTransaction");
        }
        public static byte[] SuspendTransaction(int id)
        {
            SuspendResponseType suspend = new SuspendResponseType();
            suspend.SuspendTransactionResult = new SuspendTransactionResultType
            {
                RequestID = id,
                RequestIDSpecified = true
            };

            return XmlSerializing(suspend, "SuspendResponse");
        }

        public static byte[] XmlSerializing(object Klasa, string Lloji)
        {
            string header = "";
            XmlSerializer xml = null;
            switch (Lloji.ToString())
            {
                case "Statuses":
                    {
                        xml = new XmlSerializer(typeof(POSBCStatusEventType));
                        header = "soeps~Message-Type=EVENT|Session-Id=" + idNumber + "|~";
                    }
                    break;
                case "InitialResponse":
                    {
                        xml = new XmlSerializer(typeof(InitializeResponseType));
                        header = "soeps~Message-Type=RESP|Session-Id=" + idNumber + "|~";
                    }
                    break;
                case "QueryStatus":
                    {
                        xml = new XmlSerializer(typeof(QueryStatusResponseType));
                        header = "soeps~Message-Type=RESP|Session-Id=" + idNumber + "|~";
                    }
                    break;
                case "ReportEventtatus":
                    {
                        xml = new XmlSerializer(typeof(ReportStatusEventsResponseType));
                        header = "soeps~Message-Type=RESP|Session-Id=" + idNumber + "|~";
                    }
                    break;
                case "POSReceiptEvent":
                    {
                        xml = new XmlSerializer(typeof(POSReceiptEventType));
                        header = "soeps~Message-Type=EVENT|Session-Id=" + idNumber + "|~";
                    }
                    break;
                case "AddItemResponse":
                    {
                        xml = new XmlSerializer(typeof(AddItemResponseType));
                        header = "soeps~Message-Type=RESP|Session-Id=" + idNumber + "|~";
                    }
                    break;
                case "TotalsEvent":
                    {
                        xml = new XmlSerializer(typeof(TotalsEventType));
                        header = "soeps~Message-Type=EVENT|Session-Id=" + idNumber + "|~";
                    }
                    break;
                case "GetTotals":
                    {
                        xml = new XmlSerializer(typeof(GetTotalsResponseType));
                        header = "soeps~Message-Type=RESP|Session-Id=" + idNumber + "|~";
                    }
                    break;
                case "SignOff":
                    {
                        xml = new XmlSerializer(typeof(SignOffResponseType));
                        header = "soeps~Message-Type=RESP|Session-Id=" + idNumber + "|~";
                    }
                    break;
                case "receiptlinesresponse":
                    {
                        xml = new XmlSerializer(typeof(AddReceiptLinesResponseType));
                        header = "soeps~Message-Type=RESP|Session-Id=" + idNumber + "|~";
                    }
                    break;
                case "RemoveLinesResponse":
                    {
                        xml = new XmlSerializer(typeof(RemoveReceiptLinesResponseType));
                        header = "soeps~Message-Type=RESP|Session-Id=" + idNumber + "|~";
                    }
                    break;
                case "PrinterStatus":
                    {
                        xml = new XmlSerializer(typeof(PrinterStatusType));
                        header = "soeps~Message-Type=EVENT|Session-Id=" + idNumber + "|~";
                    }
                    break;
                case "AddTenderResponse":
                    {
                        xml = new XmlSerializer(typeof(AddTenderResponseType));
                        header = "soeps~Message-Type=RESP|Session-Id=" + idNumber + "|~";
                    }
                    break;
                case "PrintReceipt":
                    {
                        xml = new XmlSerializer(typeof(PrintCurrentReceiptsResponseType));
                        header = "soeps~Message-Type=RESP|Session-Id=" + idNumber + "|~";
                    }
                    break;
                case "TransactionStatus":
                    {
                        xml = new XmlSerializer(typeof(TransactionStatusEventType));
                        header = "soeps~Message-Type=EVENT|Session-Id=" + idNumber + "|~";
                    }
                    break;
                case "SuspendResponse":
                    {
                        xml = new XmlSerializer(typeof(SuspendResponseType));
                        header = "soeps~Message-Type=RESP|Session-Id=" + idNumber + "|~";
                    }
                    break;
                case "VoidTransaction":
                    {
                        xml = new XmlSerializer(typeof(VoidTransactionResponseType));
                        header = "soeps~Message-Type=RESP|Session-Id=" + idNumber + "|~";
                    }
                    break;
                case "PrintAgain":
                    {
                        xml = new XmlSerializer(typeof(PrintResponseType));
                        header = "soeps~Message-Type=RESP|Session-Id=" + idNumber + "|~";
                    }
                    break;
                case "PrintArchivedReceipts":
                    {
                        xml = new XmlSerializer(typeof(PrintArchivedReceiptsResponseType));
                        header = "soeps~Message-Type=RESP|Session-Id=" + idNumber + "|~";
                    }
                    break;
                case "RePrint":
                    {
                        xml = new XmlSerializer(typeof(ReprintReceiptsResponseType));
                        header = "soeps~Message-Type=RESP|Session-Id=" + idNumber + "|~";
                    }
                    break;
                case "CancelAction":
                    {
                        xml = new XmlSerializer(typeof(CancelActionResponseType));
                        header = "soeps~Message-Type=RESP|Session-Id=" + idNumber + "|~";
                    }
                    break;
                default:
                    break;
            }

            byte[] msg;
            try
            {
                StringWriterUtf8 text = new StringWriterUtf8();
                xml.Serialize(text, Klasa);
                msg = Encoding.UTF8.GetBytes(header.ToString() + text.ToString());
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Serializimi ne xml deshtoi: " + ex.Message);
                msg = new byte[0];
            }

            return msg;
        }

        public static byte[] ConvertToBytes(byte[] msg)
        {
            byte[] msgFinal = new byte[4 + msg.Length];

            string numberOfBytes = string.Format("{0:0000}", msg.Length);

            msgFinal[0] = 0x00;
            msgFinal[1] = 0x00;
            msgFinal[2] = StringToByteArray(Convert.ToInt32(numberOfBytes), 1)[0];
            msgFinal[3] = StringToByteArray(Convert.ToInt32(numberOfBytes), 2)[0];
            int i = 3;
            foreach (byte B in msg)
            {
                i++;
                msgFinal[i] = B;
            }

            return msgFinal;
        }

        public static byte[] StringToByteArray(int number, int subpart, string hexNo = "")
        {
            string hex = "";
            hex = number.ToString("X4");

            if (subpart == 1)
                hex = string.Format("{0:x2}", hex.Substring(0, 2));
            else
                hex = string.Format("{0:x2}", hex.Substring(2, 2));

            if (hexNo != string.Empty)
            {
                if (subpart == 1)
                    hex = string.Format("{0:x2}", hexNo).Substring(0, 2);
                else
                    hex = string.Format("{0:x2}", hexNo).Length == 3 ? hexNo.Substring(1, 2) : hexNo.Substring(2, 2);
            }

            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }
        public class StringWriterUtf8 : System.IO.StringWriter
        {
            public override Encoding Encoding
            {
                get
                {
                    return Encoding.UTF8;
                }
            }
        }
    }
}
