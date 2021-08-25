using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToshibaPosSinkronizimi;

namespace ToshibaPOS
{
    public partial class POSForm : Form
    {
        int Increment = 1;


        public POSForm()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            StartClient();
        }

        public void StartClient()
        {
            byte[] bytes = new byte[1024];            
            try
            {
                //IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                //IPAddress ipAddress = ipHostInfo.AddressList[3];
                //IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11335);

                IPAddress ipAddress = IPAddress.Parse(txtIP.Text);
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, Convert.ToInt32(txtPort.Text));
                
                Socket sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);                
                try
                {
                    sender.Connect(remoteEP);
                    txtExeption.Text = "Socket connected to "+ sender.RemoteEndPoint.ToString();                    
                    byte[] msg = Encoding.ASCII.GetBytes(txtKomanda.Text+" - "+ Increment++.ToString());
                    
                    int bytesSent = sender.Send(msg);
                    
                    //int bytesRec = sender.Receive(bytes);
                    //txtExeption.Text = "Echoed test = {0}"+ Encoding.ASCII.GetString(bytes, 0, bytesRec);                   
                    //sender.Shutdown(SocketShutdown.Both);
                    //sender.Close();
                   
                }
                catch (ArgumentNullException ane)
                {
                    txtExeption.Text = "ArgumentNullException : {0}"+ ane.ToString();
                }
                catch (SocketException se)
                {
                    txtExeption.Text = "SocketException : {0}"+ se.ToString();
                }
                catch (Exception e)
                {
                    txtExeption.Text = "Unexpected exception : {0}"+ e.ToString();
                }

            }
            catch (Exception e)
            {
                txtExeption.Text = e.ToString();
            }
        }      

        private void btnStart_Click(object sender, EventArgs e)
        {
            TextBox.CheckForIllegalCrossThreadCalls = false;
            Task a = ListeningClass.AsyncStartListening(txtListener, txtListenerIP.Text, txtListenerPort.Text);

        }

        FaturaBLL Fatura;
        private void POSForm_Load(object sender, EventArgs e)
        {            
            Fatura = new FaturaBLL();

            SinkronizimiClass.Sync.txtDergo = txtDergimiInfo;
            SinkronizimiClass.Sync.txtImporto = txtImportimiInfo;

            grdDaljaMallitDetale.DataSource = Fatura.dtDaljaMallitDetale;
            grdCash.DataSource = Fatura.dtEkzekutimiPageses;
            grdDetalet.DataSource = Fatura.dtDaljaMallit;
            ListeningClass diqka = new ListeningClass(Fatura);
            btnStart_Click(null, null);

        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                Fatura.ShtoArtikullin(textBox1.Text, 1);
            }
        }

        private void btnCash_Click(object sender, EventArgs e)
        {
            Fatura.ShtoPagesen(22);
        }

        private void btnPos_Click(object sender, EventArgs e)
        {
            Fatura.ShtoPagesen(18);
        }

        private void btnRuaj_Click(object sender, EventArgs e)
        {
            Fatura.Ruaj();
            grdDaljaMallitDetale.Rows.Clear();
            grdCash.Rows.Clear();
        }
    }
}
