using ECRCommXLib;
using NLog;
using System.Threading;

namespace ToshibaPOS
{
    public class PosPcb
    {
        public static BPOS1Lib obj = new BPOS1Lib();
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public string ConnectToPos()
        {
            try
            {
                obj.CommClose();
                obj.useLogging(1, "D:\\ECRCommX.log");
                obj.CommOpen(11, 115200);
                obj.CheckConnection(1);
                waitResponse();
                if (obj.LastResult == 0)
                {
                    Logger.Info("Pergjigja nga POS per konektim: " + obj.LastResult);
                    return "OK";
                }
                else
                {
                    Logger.Info("Gabim gjate konektimit POS: " + obj.LastResult + "/ ErrorCode: " + obj.LastErrorCode 
                        + "/ ErrorDescription: " + obj.LastErrorDescription + "/ LastStatMsgCode: " + obj.LastStatMsgCode);
                    return "NOT OK";
                }
            }
            catch (System.Exception ex)
            {
                Logger.Error(ex, "Error ne lidhjen me POS: " + ex.Message);
                return "NOT OK";
            }
        }

        public string Purchase(uint amount, uint addAmount, byte merchIndexId)
        {
            try
            {
                Thread.Sleep(3000);
                obj.CommOpen(11, 115200);
                Thread.Sleep(3000);
                waitResponse();

                if(obj.LastResult != 0)
                {
                    Logger.Info("Gabim gjate konektimit ne shitje te POS: " + obj.LastResult + "/ ErrorCode: " + obj.LastErrorCode
                        + "/ ErrorDescription: " + obj.LastErrorDescription + "/ LastStatMsgCode: " + obj.LastStatMsgCode);
                    Cancel();
                    return "NOT OK";
                }

                obj.Purchase(amount, addAmount, merchIndexId);
                waitResponse();

                if (obj.LastResult == 0)
                {
                    obj.Confirm();
                    waitResponse();

                    if(obj.LastResult != 0)
                    {
                        Logger.Info("Pergjigja e dyte nga pos ka ardhe negative. Pergjigja nga pos: " + obj.LastResult);
                        Cancel();
                        return "NOT OK";
                    }
                    else
                    {
                        //obj.CommClose();
                        Logger.Info("Blerja ka ardhe me sukses. Pergjigja nga pos: " + obj.LastResult);
                        return "OK";
                    }
                }
                else
                {
                    Logger.Info("Gabim gjate pageses ne POS: " + obj.LastResult + "/ ErrorCode: " + obj.LastErrorCode
                        + "/ ErrorDescription: " + obj.LastErrorDescription + "/ LastStatMsgCode: " + obj.LastStatMsgCode);

                    Cancel();
                    Logger.Info("U thirr funksioni Cancel()");
                    return "NOT OK";
                }
            }
            catch (System.Exception ex)
            {
                Logger.Error(ex, "Error ne pagese me POS: " + ex.Message);
                return "NOT OK";
            }            
        }

        public void Refund(uint amount, uint addAmount, byte merchIndexId, string bs)
        {
            obj.Refund(amount, addAmount, merchIndexId, bs);
            waitResponse();
            if (obj.LastResult == 0)
            {
                obj.Confirm();
                waitResponse();
            }
        }

        public void Cancel()
        {
            try
            {
                obj.Cancel();
                //obj.CommClose();
                waitResponse();

                Logger.Info("U thirr funksioni WaitResponse()");
            }
            catch (System.Exception ex)
            {
                Logger.Error(ex, "Error ne anulim te pageses me POS: " + ex.Message);
            }
        }

        public void Confirm()
        {
            obj.Confirm();
            waitResponse();
            //obj.CommClose();
        }

        public void Reversal()
        {
            obj.Void(3, 0);
            waitResponse();
            obj.ReqCurrReceipt();
            waitResponse();
        }

        public void printBatch(byte merchIndexId)
        {
            obj.PrintBatchJournal(merchIndexId);
            waitResponse();
            obj.ReqCurrReceipt();
            waitResponse();
        }

        public void waitResponse()
        {
            int LastStMsCode = 0;

            while (obj.LastResult == 2)
            {
                if (obj.LastStatMsgCode != 0 && obj.LastStatMsgCode != LastStMsCode)
                {
                    //if (obj.LastStatMsgDescription != null)
                        //MessageBox.Show(obj.LastStatMsgDescription, "Verejtje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                LastStMsCode = obj.LastStatMsgCode;
            }
            //if (obj.LastStatMsgDescription != null)
            //    MessageBox.Show(obj.LastStatMsgDescription, "Verejtje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
