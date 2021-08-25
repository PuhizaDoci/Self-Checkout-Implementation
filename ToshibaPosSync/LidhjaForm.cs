using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using ToshibaPos.DAL;
using ToshibaPos.SDK;
using ToshibaPOS;

namespace ToshibaPosSinkronizimi
{
    public partial class LidhjaForm : Form
    {
        public LidhjaForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (A.NrArkes == 0)
            {
                MessageBox.Show("Shenoni numrin e Arkës!", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            bsA.EndEdit();
            string a = A.Ruaj(false);

            if (a != "OK")
            {
                MessageBox.Show(a);
                txtInformimi.Text = a;
                DialogResult = DialogResult.Abort;
                return;
            }

            try
            {
                //Fresko Db-ne
                DBLocalClass db = new DBLocalClass(txtInformimi);
                db.FreskoDatabazen();
                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {

            }
        }
        private void btnAnulo_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void txtEmriIServerit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }


        ArkatClass A = new ArkatClass();
        BindingSource bsA = new BindingSource();
        private void LidhjaForm_Load(object sender, EventArgs e)
        {
            txtEmriIServerit.Focus();
            bsA.DataSource = A;
            A.LoadFromDiscToPublicClassArka();
            if (PublicClass.Arka.OrganizataId == 0)
            {
                txtNrArkes.DataBindings.Add(new Binding("Text", bsA, "NrArkes", true, DataSourceUpdateMode.OnPropertyChanged));
                LejoKerkiminMeEmerchb.DataBindings.Add(new Binding("Checked", bsA, "LejoKerkiminmeEmer", true, DataSourceUpdateMode.OnPropertyChanged));
                AplikocmiminMeShumiceKurarrihetPaketimicheckBox.DataBindings.Add(new Binding("Checked", bsA, "AplikocmiminMeShumiceKurarrihetPaketimi", true, DataSourceUpdateMode.OnPropertyChanged));
                LejostokunNegativechb.DataBindings.Add(new Binding("Checked", bsA, "LejoStokunNegative", true, DataSourceUpdateMode.OnPropertyChanged));
                LejoZbritjenNeArkechb.DataBindings.Add(new Binding("Checked", bsA, "LejoZbritjenNeArke", true, DataSourceUpdateMode.OnPropertyChanged));
                LejoNDerrimineCmimitchb.DataBindings.Add(new Binding("Checked", bsA, "LejoNDerrimineCmimit", true, DataSourceUpdateMode.OnPropertyChanged));
                ShtypKopjenEKuponitFiskalchkb.DataBindings.Add(new Binding("Checked", bsA, "ShtypKopjenEKuponitFiskal", true, DataSourceUpdateMode.OnPropertyChanged));
                KerkoPassWordPerAplikiminEZbritjescheckBox.DataBindings.Add(new Binding("Checked", bsA, "KerkoPassWordPerAplikiminEZbritjes", true, DataSourceUpdateMode.OnPropertyChanged));
                LejoRabatPerTeGjitheArtikujtcheckBox.DataBindings.Add(new Binding("Checked", bsA, "LejoRabatPerTeGjitheArtikujt", true, DataSourceUpdateMode.OnPropertyChanged));
                LejoZbritjeNeTotalVlercheckBox.DataBindings.Add(new Binding("Checked", bsA, "LejoZbritjeNeTotalVler", true, DataSourceUpdateMode.OnPropertyChanged));
                chkRegjimiPunesOffline.DataBindings.Add(new Binding("Checked", bsA, "RegjimiPunesOffline", true, DataSourceUpdateMode.OnPropertyChanged));
                txtServeriLokal.DataBindings.Add(new Binding("Text", bsA, "LokalServeri", true, DataSourceUpdateMode.OnPropertyChanged));
                txtDatabazaLokale.DataBindings.Add(new Binding("Text", bsA, "LokalDataBaza", true, DataSourceUpdateMode.OnPropertyChanged));
                txtShfrytezuesiLokal.DataBindings.Add(new Binding("Text", bsA, "LokalUserDB", true, DataSourceUpdateMode.OnPropertyChanged));
                txtFjalekalimiLokal.DataBindings.Add(new Binding("Text", bsA, "LokalUserPas", true, DataSourceUpdateMode.OnPropertyChanged));
                txtIntervaliImportimitSekonda.DataBindings.Add(new Binding("Text", bsA, "IntervaliImportimitSekonda", true, DataSourceUpdateMode.OnPropertyChanged));
                txtIntervaliDergimitSekonda.DataBindings.Add(new Binding("Text", bsA, "IntervaliDergimitSekonda", true, DataSourceUpdateMode.OnPropertyChanged));
                txtShteguFiskal.DataBindings.Add(new Binding("Text", bsA, "ShteguFiskal", true, DataSourceUpdateMode.OnPropertyChanged));
                txtIpEKontrollerit.DataBindings.Add(new Binding("Text", bsA, "IPEKontrollerit", true, DataSourceUpdateMode.OnPropertyChanged));
                cmpLlojiPrinteritFiskal.DataBindings.Add(new Binding("Text", bsA, "TipiPrinterit", true, DataSourceUpdateMode.OnPropertyChanged));
                cmpCOMPort.DataBindings.Add(new Binding("Text", bsA, "Porti", true, DataSourceUpdateMode.OnPropertyChanged));
                chkPrintonDirekt.DataBindings.Add(new Binding("Checked", bsA, "PrintonDirekt", true, DataSourceUpdateMode.OnPropertyChanged));
                ChkOpenPrinterCnnOnce.DataBindings.Add(new Binding("Checked", bsA, "HapPortinNjeHere", true, DataSourceUpdateMode.OnPropertyChanged));
                txtEmriIServerit.DataBindings.Add(new Binding("Text", bsA, "Serveri", true, DataSourceUpdateMode.OnPropertyChanged));
                txtShfrytezuesi.DataBindings.Add(new Binding("Text", bsA, "Shfrytezuesi", true, DataSourceUpdateMode.OnPropertyChanged));
                txtFjalekalimi.DataBindings.Add(new Binding("Text", bsA, "Fjalekalimi", true, DataSourceUpdateMode.OnPropertyChanged));
                txtDatabazat.DataBindings.Add(new Binding("Text", bsA, "Databaza", true, DataSourceUpdateMode.OnPropertyChanged));
            }
            else
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void txtFjalekalimi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtFjalekalimi.Text != "")
                {
                    txtDatabazat.Select();
                    txtDatabazat.SelectAll();
                    txtDatabazat.Focus();
                    txtDatabazat_KeyDown(null, new KeyEventArgs(Keys.F1));
                }
            }
        }

        private void txtDatabazat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                SqlConnectionStringBuilder cb = new SqlConnectionStringBuilder();
                cb.DataSource = txtEmriIServerit.Text;
                cb.UserID = txtShfrytezuesi.Text;
                cb.Password = txtFjalekalimi.Text;
                cb.InitialCatalog = "master";
                cb.IntegratedSecurity = false;

                SqlConnection cnn = new SqlConnection(cb.ConnectionString);

                try
                {
                    cnn.Open();
                    DataTable tb = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT Name Databaza, database_id Id FROM sys.Databases	where name not in ('master','tempdb','model','msdb')", cnn);
                    da.Fill(tb);
                    DataRow dr = ShfaqF1Dritaren(tb, "Databaza");
                    if (dr != null)
                    {
                        txtDatabazat.Text = dr["Databaza"].ToString();

                        txtLokacioni.Select();
                        txtLokacioni.SelectAll();
                        txtLokacioni.Focus();
                        txtLokacioni_KeyDown(null, new KeyEventArgs(Keys.F1));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if (e.KeyCode == Keys.Enter)
            {
                //try
                //{
                if (txtEmriIServerit.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Emri i serverit eshte shenim i kerkuar!", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                txtLokacioni.Select();
                txtLokacioni.SelectAll();
                txtLokacioni.Focus();
                txtLokacioni_KeyDown(null, new KeyEventArgs(Keys.F1));

            }
        }

        public DataRow ShfaqF1Dritaren(DataTable dataSource, params string[] kolonatPerShfaqje)
        {
            long Rezultati;
            ZgjedhjetForm testDialog = new ZgjedhjetForm();
            testDialog.Zgjidhja = dataSource;
            if (kolonatPerShfaqje.Length == 0)
                testDialog.KolonatPerShfaqje = new string[2] { "Shifra", "Pershkrimi" };
            else
                testDialog.KolonatPerShfaqje = kolonatPerShfaqje;

            testDialog.ShowDialog();

            try
            {
                if (Int64.TryParse(testDialog.Text.ToString(), out Rezultati) == true)
                {
                    testDialog.Dispose();
                    return testDialog.Rreshti;
                }
                else if (testDialog.Text.Trim() != string.Empty && testDialog.Text.Trim() != "Anulo")
                {
                    testDialog.Dispose();
                    return testDialog.Rreshti;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //return null;
            }
        }

        private void txtLokacioni_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                PublicClass.Arka.Serveri = A.Serveri;
                PublicClass.Arka.Shfrytezuesi = A.Shfrytezuesi;
                PublicClass.Arka.Fjalekalimi = A.Fjalekalimi;
                PublicClass.Arka.Databaza = A.Databaza;

                DataRow dr = ShfaqF1Dritaren(LoginClass.GetOrganizatat(), "Id", "Pershkrimi");
                if (dr != null)
                {
                    //vendos operatorin automatik
                    DataRow dr1 = LoginClass.GetOperatoret().Rows[0];
                    if (dr1 != null)
                    {
                        txtShfrytezuesiLoginAutomatik.Text = dr1["Emri"].ToString() + " " + dr1["Mbiemri"].ToString();
                        A.OperatoriAutomatikId = Convert.ToInt32(dr1["Id"].ToString());
                    }

                    txtLokacioni.Text = dr["Pershkrimi"].ToString();
                    PublicClass.Organizata.Id = Convert.ToInt32(dr["Id"].ToString());
                    A.OrganizataId = Convert.ToInt32(dr["Id"].ToString());

                    txtNrArkes.Select();
                    txtNrArkes.SelectAll();
                    txtNrArkes.Focus();
                }
            }

            if (e.KeyCode == Keys.Enter)
            {
                if (txtLokacioni.Text.Trim() != string.Empty)
                {
                    if (txtLokacioni.ReadOnly) return;

                    DataTable dt = LoginClass.GetOrganizatat();
                    if (dt.Rows.Count > 0)
                    {
                        txtLokacioni.Text = dt.Rows[0]["Pershkrimi"].ToString();

                        txtNrArkes.Select();
                        txtNrArkes.SelectAll();
                        txtNrArkes.Focus();
                    }
                }
                else
                {
                    txtNrArkes.Select();
                    txtNrArkes.SelectAll();
                    txtNrArkes.Focus();
                }
            }
            else
            {
                txtNrArkes.Select();
                txtNrArkes.SelectAll();
                txtNrArkes.Focus();
            }
        }

        private void txtShfrytezuesi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtFjalekalimi.Select();
                txtFjalekalimi.SelectAll();
                txtFjalekalimi.Focus();
            }
        }

        private void txtShfrytezuesiLoginAutomatik_KeyDown(object sender, KeyEventArgs e)
        {

            DataRow dr = ShfaqF1Dritaren(LoginClass.GetOperatoret(), "Emri", "Mbiemri", "Grupi", "PershkrimiOrganizates");
            if (dr != null)
            {
                txtShfrytezuesiLoginAutomatik.Text = dr["Emri"].ToString() + " " + dr["Mbiemri"].ToString();
                A.OperatoriAutomatikId = Convert.ToInt32(dr["Id"].ToString());

            }
        }

        private void txtShfrytezuesiLokal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtFjalekalimiLokal.Select();
                txtFjalekalimiLokal.SelectAll();
                txtFjalekalimiLokal.Focus();
            }
        }

        private void txtFjalekalimiLokal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtFjalekalimiLokal.Text != "")
                {
                    txtDatabazaLokale.Select();
                    txtDatabazaLokale.SelectAll();
                    txtDatabazaLokale.Focus();
                    txtDatabazat_KeyDown(null, new KeyEventArgs(Keys.F1));
                }
            }
        }

        private void txtDatabazaLokale_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 && txtServeriLokal.Text != "" && txtShfrytezuesiLokal.Text != "" && txtFjalekalimiLokal.Text != "")
            {
                SqlConnectionStringBuilder cb = new SqlConnectionStringBuilder();
                cb.DataSource = txtServeriLokal.Text;
                cb.UserID = txtShfrytezuesiLokal.Text;
                cb.Password = txtFjalekalimiLokal.Text;
                cb.InitialCatalog = "Master";
                cb.IntegratedSecurity = false;

                SqlConnection cnn = new SqlConnection(cb.ConnectionString);

                try
                {
                    cnn.Open();
                    DataTable tb = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT Name Databaza, database_id Id FROM sys.Databases	where name not in ('master','tempdb','model','msdb')", cnn);
                    da.Fill(tb);
                    txtDatabazaLokale.Text = "POS";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if (e.KeyCode == Keys.Enter)
            {
                //try
                //{
                if (txtEmriIServerit.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Emri i serverit eshte shenim i kerkuar!", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                txtDatabazaLokale.Select();
                txtDatabazaLokale.SelectAll();
                txtDatabazaLokale.Focus();
                txtDatabazaLokale_KeyDown(null, new KeyEventArgs(Keys.F1));
            }
        }

        private void txtNrArkes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //if (A?.OrganizataId != null && A?.NrArkes != null && A?.OrganizataId > 0 && A?.NrArkes > 0)
                //{
                //    ArkatClass arkangaserveri = new ArkatClass(A.OrganizataId, A.NrArkes);
                //    if (arkangaserveri.Id > 0)
                //    {
                //        BartiVleratClass.Barti(arkangaserveri, A);
                //        bsA.EndEdit();
                //        bsA.ResetBindings(false);
                //    }
                //}
                cmpLlojiPrinteritFiskal.Focus();
            }
        }

        private void btnDrop_Click(object sender, EventArgs e)
        {
            SqlConnectionStringBuilder cb = new SqlConnectionStringBuilder();
            if (!string.IsNullOrEmpty(txtServeriLokal.Text))
                cb.DataSource = txtServeriLokal.Text;
            else
                cb.DataSource = ".\\sqlexpress";
            if (!string.IsNullOrEmpty(txtShfrytezuesiLokal.Text))
                cb.UserID = txtShfrytezuesiLokal.Text;
            if (!string.IsNullOrEmpty(txtFjalekalimiLokal.Text))
                cb.Password = txtFjalekalimiLokal.Text;
            cb.InitialCatalog = "Master";
            cb.IntegratedSecurity = true;

            SqlConnection cnn = new SqlConnection(cb.ConnectionString);

            try
            {
                cnn.Open();
                DataTable tb = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("SELECT Name Databaza, database_id Id FROM sys.Databases	where name not in ('master','tempdb','model','msdb')", cnn);
                da.Fill(tb);

                if (tb.Rows.Count > 0)
                {
                    if (tb.Select("Databaza='POS'").Length > 0)
                    {
                        DataTable daljaMallit = new DataTable();
                        SqlDataAdapter da1 = new SqlDataAdapter("select top 1 Sinkronizuar from TOSHIBA.dbo.DaljaMallit where Sinkronizuar=0", cnn);
                        da1.Fill(daljaMallit);

                        if (daljaMallit.Rows.Count >= 1)
                        {
                            MessageBox.Show("Nuk mund të fshini bazën lokale sepse ka shenime që nuk janë sinkronizuar!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        SqlDataAdapter d = new SqlDataAdapter("drop database POS", cnn);
                        d.SelectCommand.ExecuteNonQuery();

                        MessageBox.Show("Baza lokale u fshi!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grbLidhjaServer_Enter(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
