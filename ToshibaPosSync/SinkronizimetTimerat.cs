using System;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using ToshibaPos.SDK;
using ToshibaPOS.DAL;

namespace ToshibaPosSinkronizimi
{
    public class SinkronizimetTimerat
    {
        private System.Timers.Timer TimerImporto;
        private System.Timers.Timer TimerDergo;
        Thread _SinkronizoThread;

        public SinkronizimetTimerat()
        {

        }
        public TextBox txtDergo { get; set; } = new TextBox();
        public TextBox txtImporto { get; set; } = new TextBox();
        public void Starto()
        {
            TimerImporto = new System.Timers.Timer();
            TimerImporto.Interval = PublicClass.Arka.IntervaliImportimitSekonda > 120 ?
                PublicClass.Arka.IntervaliImportimitSekonda * 1000 :
                120 * 1000;
            TimerImporto.Elapsed += OnTimedEventImporto;
            TimerImporto.AutoReset = true;
            TimerImporto.Enabled = true;

            TimerDergo = new System.Timers.Timer();
            TimerDergo.Interval = PublicClass.Arka.IntervaliDergimitSekonda > 20 ?
                PublicClass.Arka.IntervaliDergimitSekonda * 1000 :
                20 * 1000;
            TimerDergo.Elapsed += OnTimedEventDergo;
            TimerDergo.AutoReset = true;
            TimerDergo.Enabled = true;

            //Ne startimin e klases behet nje importim
            OnTimedEventImporto(null, null);
        }
        Thread SinkronizoThread
        {
            get
            {
                return _SinkronizoThread;
            }
            set
            {


                if (_SinkronizoThread == null || _SinkronizoThread.ThreadState != ThreadState.Running)
                {
                    _SinkronizoThread = value;
                    _SinkronizoThread.Start();
                }
            }
        }
        private void OnTimedEventImporto(Object source, System.Timers.ElapsedEventArgs e)
        {
            SinkronizoThread = new Thread(Importo);
        }
        void Importo()
        {
            try
            {
                ImportoClass SY = new ImportoClass();
                SY.ImportoShenimet();
                AppendImportimiTextBox("Importimi: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"), true);
            }
            catch (Exception ex)
            {

                AppendImportimiTextBox("Importimi: " + ex.Message, false);
            }
        }
        private void OnTimedEventDergo(object source, System.Timers.ElapsedEventArgs e)
        {
            SinkronizoThread = new Thread(DergoAsync);
        }

        private async void DergoAsync()
        {
            try
            {
                DBLocalClass db = new DBLocalClass();
                if (db.TestoKoneksionin(PublicClass.KoneksioniPrimar) == false)
                {
                    SinkronizimiClass.Sync.AppendDergimiTextBox("Dergimi: Koneksioni me server ka deshtuar!", false);
                    return;
                }
                //Merr Te gjitha shenimet lokale 

                DataTable hederatlokal = new DataTable();
                SinkronizimiClass.GetDaljetLokale(hederatlokal);

                foreach (DataRow r in hederatlokal.Rows)
                {
                    string idlokal = r["IdLokal"].ToString();
                    long ServerId;
                    long.TryParse(ToshibaPosClass.AEshteEgzistonFaturaNeServer(idlokal, PublicClass.OrganizataId).ToString(), out ServerId);
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

                AppendDergimiTextBox("Dergimi: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"), true);
            }
            catch (Exception ex)
            {
                PublicClass.Arka.IntervaliDergimitSekonda = PublicClass.Arka.IntervaliDergimitSekonda + 3;
                AppendDergimiTextBox("Dergimi: " + ex.Message, false);
            }
        }
        public void AppendDergimiTextBox(string value, bool OK)
        {
            if (txtDergo.InvokeRequired)
            {
                try
                {

                    txtDergo.Invoke((MethodInvoker)delegate
                    {
                        txtDergo.Text = value;
                    });
                }
                catch (Exception rc)
                {
                }
                if (OK)
                    txtDergo.ForeColor = Color.White;
                else
                    txtDergo.ForeColor = Color.Red;
            }
            else
            {
                txtDergo.Text = value;

                if (OK)
                    txtDergo.ForeColor = Color.White;
                else
                    txtDergo.ForeColor = Color.Red;

                txtDergo.Refresh();
            }
        }
        public void AppendImportimiTextBox(string value, bool OK)
        {

            if (txtImporto.InvokeRequired)
            {
                try
                {

                    txtImporto.Invoke((MethodInvoker)delegate
                    {
                        txtImporto.Text = value;
                    });
                }
                catch (Exception ed)
                {

                }
                if (OK)
                    txtImporto.ForeColor = Color.White;
                else
                    txtImporto.ForeColor = Color.Red;
            }
            else
            {
                txtImporto.Text = value;

                if (OK)
                    txtImporto.ForeColor = Color.White;
                else
                    txtImporto.ForeColor = Color.Red;

                txtImporto.Refresh();
            }

        }
    }
}
