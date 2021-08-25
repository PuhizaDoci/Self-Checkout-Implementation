using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ToshibaPos.SDK;
using ToshibaPOS.DAL;

namespace ToshibaPosSinkronizimi
{
    public partial class SinkronizimiForm : Form
    {
         
        public SinkronizimiForm()
        {
            InitializeComponent();
            this.Text = this.Text + "Filjala: " + PublicClass.Organizata.Pershkrimi;
        }


        int _dergo = 0;
        int _importo = 0;
        int _updateServerLK = 0;


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        [Browsable(true)]
        public int dergo
        {
            get

            {
                return _dergo;
            }

            set
            {
                _dergo = value;
                OnPropertyChanged("dergo");
            }
        }

        [Browsable(true)]
        public int importo
        {
            get
            {
                return _importo;
            }

            set
            {
                _importo = value;
                OnPropertyChanged("importo");
            }
        }

        [Browsable(true)]
        public int updateServerLK
        {
            get
            {
                return _updateServerLK;
            }

            set
            {
                _updateServerLK = value;
                OnPropertyChanged("updateServerLK");
            }
        }
        private void SinkronizimiForm_Load(object sender, EventArgs e)
        {
            tmrCdoImportimi.Enabled = true;
            tmrCdoSekondDergimi.Enabled = true;

            lblImpotimiTik.DataBindings.Add(new Binding("Text", this, "importo"));
            lblDergimiTik.DataBindings.Add(new Binding("Text", this, "dergo"));// = A.IntervaliImportimitSekonda.ToString();


            lblImportimiSekonda.Text = PublicClass.Arka.IntervaliImportimitSekonda.ToString();
            lblDergimiSekonda.Text = PublicClass.Arka.IntervaliDergimitSekonda.ToString();

            this.WindowState = FormWindowState.Minimized;
        }

        delegate void updateLabelTextDelegate(Label l, string newText);
        delegate void makePictureVisibleDelegate(PictureBox p, bool visible);
        private void SinkronizimiForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.WindowState = FormWindowState.Minimized;
            //Process.GetCurrentProcess().Kill();
        }

        private delegate void ChangeLabelTextDelegate(Label lbl, string text, Color col);
        void ChangeLabelText(Label lbl, string txt, Color col)
        {
            if (lbl.InvokeRequired)
            {
                lbl.Invoke(new ChangeLabelTextDelegate(ChangeLabelText), new object[] { lbl, txt, col });
            }
            else
            {
                lbl.Text = txt;
                lbl.ForeColor = col;
            }
        }
        private void tmrCdoSekond_Tick(object sender, EventArgs e)
        {
            importo = importo + 1;
            if (ImportimiFilloj) return;

            if (PublicClass.Arka.IntervaliImportimitSekonda <= importo)
            {
                ImportoNgaServer();
            }
        }

        public void ImportoNgaServer()
        {
            ImportimiFilloj = true;
            try
            {
                ImportoClass SY = new ImportoClass();
                SY.ImportoShenimet();
                ChangeLabelText(lblMesazhi, "Importimi:" + DateTime.Now.ToString("dd.MM.yyyy HH:mm"), Color.Green);
                importo = 0;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("A network-related or instance-specific error occurred"))
                {
                    ChangeLabelText(lblMesazhi, " Importimi : Lidhja me server nuk është e realizueshme ! ", Color.Red);
                }
                else
                {
                    ChangeLabelText(lblMesazhi, "Importimi: " + ex.Message, Color.Red);
                }
            }
            finally
            {
                importo = 0;
                ImportimiFilloj = false;
            }
        }
        private void SinkronizimiForm_MaximizedBoundsChanged(object sender, EventArgs e)
        {
            this.ShowIcon = false;
        }

        private void SinkronizimiForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //foreach (var process in Process.GetProcessesByName("POSSinkronizimi"))
            //{
            //    process.Kill();
            //}
        }

        private void SinkronizimiForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon.Visible = true;
            }
        }

        private delegate void ChangeForm(Form A);

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            Show();
            notifyIcon.Visible = false;
            this.BringToFront();
        }
        bool DergimiFillon = false;
        private void tmrCdoSekondDergimi_Tick(object sender, EventArgs e)
        {
            if (DergimiFillon) { return; }

            DergimiFillon = true;
            //TmrKontrolloRfid();
            dergo = dergo + 1;
            if (PublicClass.Arka.IntervaliDergimitSekonda <= dergo)
            {
                try
                {
                    DBLocalClass db = new DBLocalClass();
                    if (db.TestoKoneksionin(PublicClass.KoneksioniPrimar) == false)
                        return;
                    //Merr Te gjitha shenimet lokale 
                    DataTable hederatlokal = new DataTable();
                    ToshibaPosClass.GetDaljetLokale(hederatlokal);


                    foreach (DataRow r in hederatlokal.Rows)
                    {
                        long ServerId;
                        long.TryParse(ToshibaPosClass.AEshteEgzistonFaturaNeServer(r["IdLokal"].ToString(), PublicClass.OrganizataId).ToString(), out ServerId);
                        if (ServerId == 0)
                        {
                            Tabelat Ta = new Tabelat();
                            DataSet ds = Ta.DsHeader.Clone();
                            DataRow drHeder = ds.Tables["dtHeader"].NewRow();
                            foreach (DataColumn dc in r.Table.Columns)
                            {
                                if (ds.Tables["dtHeader"].Columns.Contains(dc.ColumnName))
                                {
                                    drHeder[dc.ColumnName] = r[dc.ColumnName];
                                }
                            }
                            ds.Tables["dtHeader"].Rows.Add(drHeder);
                            ToshibaPosClass.GetDaljetDetaletLokale(ds.Tables["dtDaljaDetalet"], r["Id"].ToString());
                            ToshibaPosClass.GetEkzekutimetEPagesesLokale(ds.Tables["dtEkzekutimetEPageses"], r["Id"].ToString());
                            ToshibaPosClass.GetDaljetDetaletHistoriLokale(ds.Tables["dtArtikujtEFshire"], r["Id"].ToString());
                            ToshibaPosClass.GetCCPFaturatLokale(ds.Tables["dtCCPFatura"], r["Id"].ToString());
                            ToshibaPosClass.GetPOSZbritjaMeKuponLokale(ds.Tables["dtPOSZbritjaMeKupon"], r["Id"].ToString());
                            ToshibaPosClass.GetKuponat(ds.Tables["dtKuponat"], Convert.ToInt64(r["Id"]));
                            ToshibaPosClass.GetDaljaMallitVaucherat(ds.Tables["dtDaljaMallitVaucherat"], Convert.ToInt64(r["Id"]));

                            if (Convert.ToInt32(r["DokumentiId"]) == 45 || Convert.ToInt32(r["DokumentiId"]) == 54)
                            {
                                ToshibaPosClass.SinkronizoFleteDergesat(ds, 0);
                            }
                            else
                            {
                                ToshibaPosClass.RuajServer(ds, 0, PublicClass.KoneksioniPrimar);
                            }

                            ds.Dispose();
                        }
                        else
                        {
                            long.TryParse(r["Id"].ToString(), out long Id);
                            ToshibaPosClass.UpdateLokal(ServerId, Id);
                        }
                        System.Threading.Thread.Sleep(23);
                    }

                    ChangeLabelText(lblKohaDerguar, "Dergimi: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm"), Color.Green);
                    dergo = 0; ;
                }
                catch (Exception ex)
                {
                    ChangeLabelText(lblKohaDerguar, "Dergimi: " + ex.Message, Color.Red);
                }
                finally
                {
                    DergimiFillon = false;
                }
            }
            DergimiFillon = false;
        }

        bool ImportimiFilloj = false;
        private void btnImporto_Click(object sender, EventArgs e)
        {
            ImportoNgaServer();
        }
    }
}