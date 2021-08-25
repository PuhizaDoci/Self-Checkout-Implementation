using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace ToshibaPos.SDK
{
    public partial class PublicClass
    {
        public static int AplikacioniId { get; set; }
        public static ArkatClass Arka { get; set; } = new ArkatClass();
        public static OrganizataClass Organizata { get; set; } = new OrganizataClass();
        public static ConfigurationDataClass ConfigurationData { get; set; } = new ConfigurationDataClass();
        public delegate void VleratKaneNdryshuarEvent(bool? vlera, string propertyName);
        public static event VleratKaneNdryshuarEvent VleratKaneNdryshuar;
        public static int OperatoriId { get; set; } = 10019;
        public static bool FreskoGridinETransaksioneve { get; set; } = false;
        public static string VerzioniIArkes { get; set; } = "364";
        public static int OrganizataId
        {
            get
            {
                return Arka.OrganizataId;
            }
        }
        public static bool MicroControllerOnline { get; set; } = false;
        public static bool LejoStokunNegative { get; set; } = true;
        private static bool _LejoZbritjenNeArke = false;

        public static bool LejoZbritjenNeArke
        {
            get { return _LejoZbritjenNeArke; }
            set
            {
                _LejoZbritjenNeArke = value;
                VleratKaneNdryshuar?.Invoke(_LejoZbritjenNeArke, nameof(LejoZbritjenNeArke));
            }
        }
        private static bool _LejoNDerrimineCmimit = false;
        public static bool LejoNDerrimineCmimit
        {
            get { return _LejoNDerrimineCmimit; }
            set
            {
                _LejoNDerrimineCmimit = value;
                VleratKaneNdryshuar?.Invoke(_LejoNDerrimineCmimit, nameof(LejoNDerrimineCmimit));
            }
        }
        private static bool _ShtypKopjenEKuponitFiskal;
        public static bool ShtypKopjenEKuponitFiskal
        {
            get { return _ShtypKopjenEKuponitFiskal; }
            set
            {
                _ShtypKopjenEKuponitFiskal = value;
                VleratKaneNdryshuar?.Invoke(_ShtypKopjenEKuponitFiskal, nameof(ShtypKopjenEKuponitFiskal));
            }
        }
        public static bool KerkoPassWordPerAplikiminEZbritjes { get; set; }
        public static bool LejoRabatPerTeGjitheArtikujt { get; set; }
        public static bool LejoZbritjeNeTotalVler { get; set; }

        private static bool? _RegjimiPunesOffline = null;

        public static bool? RegjimiPunesOffline
        {
            get { return _RegjimiPunesOffline; }
            set
            {
                _RegjimiPunesOffline = value;
                VleratKaneNdryshuar?.Invoke(_RegjimiPunesOffline, nameof(RegjimiPunesOffline));
            }
        }
        const string QelesiPerSiguri = "5M@R7P05";
        //public static Label lblRegjimiIPunes { get; set; }

        public static string Mesazhet
        {
            get; set;
        }


        private static bool _dcrOnline;
        public static bool dcrOnline
        {
            get
            {
                return _dcrOnline;
            }
            set
            {
                _dcrOnline = value;
                if (_dcrOnline == true)
                {
                    NgjryaPanellit = Color.Cornsilk;
                }
            }
        }

        public static Color NgjryaPanellit { get; set; }
        /// <summary>
        /// Shtegu per kuponin fiskal
        /// </summary>
        public static string ShteguPerKuponinFiskal { get; set; }

        public static string EmriOperatorit { get; set; }
        /// <summary>
        /// Pershkrimi per koneksionin e perzgjedhur
        /// </summary>
        public static string PershkrimiKoneksionit { get; set; }

        /// <summary>
        /// Sherben per statusbar
        /// </summary>
        public static string Filiala { get; set; }


        /// <summary>
        /// Id e operatorit te perdoruesit te kyqur
        /// </summary>
        public static string PershkrimiDepartamentit { get; set; }


        /// <summary>
        /// Id e departamentit te kyqur
        /// </summary>
        public static int DepartamentiId { get; set; }
        public static string KoneksioniPrimar
        {
            get
            {
                return Arka.KoneksioniPrimar;
            }
        }
        public static string KoneksioniLokal
        {
            get
            {


                return Arka.KoneksioniLokal;
            }
        }
        public static bool KontrolleriEshteOffline { get; set; } = false;
        public static bool PerdorKoneksionPrimar { get; set; }

        public static void HapKoneksionin(SqlConnection cnn)
        {
            if (cnn.State == ConnectionState.Open)
            {

            }
            else
            {
                cnn.Open();
            }
        }
        public static int CmimiShitjesNrDecimaleve { get; set; }
        public static DateTime DataNgaServeri
        {
            get
            {

                SqlConnection cnn = new SqlConnection(KoneksioniPrimar);
                SqlCommand cmd = new SqlCommand("PB.DataKohaSelect_Sp", cnn);
                cmd.CommandTimeout = 1;
                cmd.CommandType = CommandType.StoredProcedure;
                DateTime Data;
                try
                {
                    if (cnn.ConnectionString.Contains("Timeout"))
                    {

                    }
                    else
                    {
                        string connection = KoneksioniPrimar + ";Connection Timeout=1";
                        cnn.ConnectionString = connection;
                    }
                    HapKoneksionin(cnn);
                    Data = Convert.ToDateTime(cmd.ExecuteScalar());

                }
                catch (Exception)
                {

                    Data = DateTime.Now;
                }
                finally
                {
                    cnn.Close();
                }

                return Data;
            }
        }
        public static bool KontrolloServerinAEshteOnline()
        {

            SqlConnection cnn = new SqlConnection(KoneksioniPrimar);
            SqlCommand cmd = new SqlCommand("PB.DataKohaSelect_Sp", cnn);
            cmd.CommandTimeout = 1;
            cmd.CommandType = CommandType.StoredProcedure;
            DateTime Data;
            try
            {
                if (cnn.ConnectionString.Contains("Timeout"))
                {

                }
                else
                {
                    string connection = KoneksioniPrimar + ";Connection Timeout=1";
                    cnn.ConnectionString = connection;
                }
                HapKoneksionin(cnn);
                Data = Convert.ToDateTime(cmd.ExecuteScalar());
                ServeriEshteOnline = true;
                Mesazhet = "Serveri është online!";
                return ServeriEshteOnline;
            }
            catch (Exception)
            {
                ServeriEshteOnline = false;

                Data = DateTime.Now;
                return ServeriEshteOnline;
            }
            finally
            {
                cnn.Close();
            }
        }

        private static bool _ServeriEshteOnline = false;
        public static bool ServeriEshteOnline
        {
            get
            {
                return _ServeriEshteOnline;
            }
            set
            {
                _ServeriEshteOnline = value;
            }
        }
        public static bool GetKonfigurimet(int ID)
        {
            SqlConnection cnn = new SqlConnection(PublicClass.Koneksioni);
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_KonfigurimetGetID_sp @ID=" + ID, cnn);
            bool i = false;
            try
            {
                cnn.Close();
                cnn.Open();
                i = Convert.ToBoolean(da.SelectCommand.ExecuteScalar());
            }
            catch (Exception ex)
            {
                cnn.Close();
                throw ex;
            }
            cnn.Close();
            return i;
        }
        public static string GetKonfigurimetVlere(int ID)
        {
            SqlConnection cnn = new SqlConnection(PublicClass.Koneksioni);
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_KonfigurimetGetVleren_Sp @ID=" + ID, cnn);
            string i;
            try
            {
                cnn.Close();
                cnn.Open();
                i = Convert.ToString(da.SelectCommand.ExecuteScalar());
            }
            catch (Exception ex)
            {
                cnn.Close();
                throw ex;
            }
            cnn.Close();
            return i;
        }
        public static string Koneksioni
        {
            get
            {
                string c = "";
                if (Arka != null)
                {
                    switch (Arka.RegjimiPunesOffline)
                    {
                        case true:
                            c = KoneksioniLokal;
                            break;
                        case false:
                            c = KoneksioniPrimar;
                            break;
                        default:
                            c = PerdorKoneksionPrimar ? KoneksioniPrimar : KoneksioniLokal;
                            break;
                    }

                    if (Arka.RegjimiPunesOffline == null)
                    {
                        SqlConnection conn = new SqlConnection(c);
                        try
                        {
                            conn.Open();
                            conn.Close();
                        }
                        catch (Exception)
                        {
                            if (PerdorKoneksionPrimar)
                            {
                                PerdorKoneksionPrimar = false;
                                c = KoneksioniLokal;
                            }
                            else
                            {
                                PerdorKoneksionPrimar = true;
                                c = KoneksioniPrimar;
                            }
                        }
                    }
                }
                return c;
            }
        }
        public static bool TestoKoneksionin(string cnnStr)
        {
            SqlConnection cnn = default(SqlConnection);
            try
            {
                cnn = new SqlConnection(cnnStr);
                if (cnn.ConnectionString.Contains("Timeout"))
                {

                }

                else
                {
                    string connection = cnnStr + ";Connection Timeout=3";
                    cnn.ConnectionString = connection;
                }
                cnn.Open();
                cnn.Close();

                return true;
            }
            catch
            {

                return false;
            }
            finally
            {
            }
        }
        public static void btnRegjimiPunes_Click(object sender, EventArgs e)
        {
            Button btnRegjimiPunes = ((Button)sender);
            if (PublicClass.PerdorKoneksionPrimar == false)
            {
                if (PublicClass.Arka.RegjimiPunesOffline == null)
                    PublicClass.PerdorKoneksionPrimar = true;
                if (!TestoKoneksionin(Koneksioni))
                {
                    if (
                        MessageBox.Show(
                            "Lidhja me serverin Kryesor nuk mund të realizohet! Vazdho punën në regjim lokal?",
                            "Kalo në Regjim Lokal?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        PerdorKoneksionPrimar = false;
                        btnRegjimiPunes.Text = PerdorKoneksionPrimar ? "Server" : "Lokal";
                        btnRegjimiPunes.BackColor = PerdorKoneksionPrimar ? Color.Transparent : Color.Red;
                        return;
                    }
                }

                btnRegjimiPunes.Text = PerdorKoneksionPrimar ? "Server" : "Lokal";
                btnRegjimiPunes.BackColor = PerdorKoneksionPrimar ? Color.Transparent : Color.Red;

            }
            else
            {
                if (Arka.RegjimiPunesOffline == null && MessageBox.Show(
                        "A jeni të sigurtë për të kaluar në regjimin lokal?",
                        "Kalo në Regjim Lokal?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    PerdorKoneksionPrimar = false;
                    btnRegjimiPunes.Text = PerdorKoneksionPrimar ? "Server" : "Lokal";
                    btnRegjimiPunes.BackColor = PerdorKoneksionPrimar ? Color.Transparent : Color.Red;
                }
            }
        }

        public static string EnkriptoStringun(string Mesazhi)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(QelesiPerSiguri));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToEncrypt = UTF8.GetBytes(Mesazhi);
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return Convert.ToBase64String(Results);
        }

        public static string DekriptoStringun(string Mesazhi)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(QelesiPerSiguri));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToDecrypt = Convert.FromBase64String(Mesazhi);
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return UTF8.GetString(Results);
        }

        public static string ShifraEOperatorit { get; set; }

        public static string EmriTransaksionit
        {
            get
            {
                string emri = OperatoriId.ToString() + "_" + EmriIPlote.Replace(" ", "_");
                return emri.Substring(0, emri.Length >= 30 ? 30 : emri.Length);
            }
        }

        public static string EmriIPlote { get; set; }
        public static string ShteguPerCmime { get; set; }
        public static string FTPUserName { get; set; }
        public static string FTPPassword { get; set; }
        public static string FTPShtegu { get; set; }

        public static bool FiscalPrinterHasError { get; set; }
        public static bool FiscalPrinterRememberContinueSale { get; set; }

        //public static Sirtari Sirtari { get; set; }
        public static int CashDrawerSecureId { get; set; }

        public string Gjuha { get; set; } = "";

        public static bool LejoNdryshiminESasise
        {
            get { return Arka.LejoNdryshiminESasise; }
        }

        public static bool LejoFshirjenEArtikujve
        {
            get { return Arka.LejoFshirjenEArtikujve; }
        }

        public static bool TransferetEnable
        {
            get { return Arka.TransferetEnable; }
        }

        public static bool ShperblimetEnable
        {
            get { return Arka.ShperblimetEnable; }
        }

        public static bool SubjektiDetaleEnable
        {
            get { return Arka.SubjektiDetaleEnable; }
        }
    }
}
