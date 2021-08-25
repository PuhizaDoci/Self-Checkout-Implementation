using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ToshibaPos.SDK
{
    [Serializable]
    public class ArkatClass
    {
        public ArkatClass()
        {
        }

        public ArkatClass(int _OrganizataId, int _ArkaNr)
        {
            DataTable dt = GetArkenServer(_OrganizataId, _ArkaNr);
            if (dt.Rows.Count > 0)
            {
                Id = Convert.ToInt32(dt.Rows[0]["Id"].ToString());
                OrganizataId = Convert.ToInt32(dt.Rows[0]["OrganizataId"].ToString());
                NrArkes = Convert.ToInt32(dt.Rows[0]["NrArkes"].ToString());
                HostName = Convert.ToString(dt.Rows[0]["HostName"].ToString());
                FreskimIPlote = Convert.ToBoolean(dt.Rows[0]["FreskimIPlote"].ToString());
                FLinkCode = Convert.ToString(dt.Rows[0]["FLinkCode"].ToString());
                PGMCode = Convert.ToString(dt.Rows[0]["PGMCode"].ToString());
                NumriArkesGK = Convert.ToString(dt.Rows[0]["NumriArkesGK"].ToString());
                VerzioniIArkes = Convert.ToString(dt.Rows[0]["VerzioniIArkes"].ToString());
                if (dt.Rows[0]["DataERegjistrimit"].ToString() != "")
                    DataERegjistrimit = Convert.ToDateTime(dt.Rows[0]["DataERegjistrimit"].ToString());
                if (dt.Rows[0]["ShtypjaAutomatikeZRaport"].ToString() != "")
                    ShtypjaAutomatikeZRaport = Convert.ToBoolean(dt.Rows[0]["ShtypjaAutomatikeZRaport"].ToString());
                if (dt.Rows[0]["KohaEShtypjesSeZRaportit"].ToString() != "")
                    KohaEShtypjesSeZRaportit = Convert.ToDateTime(dt.Rows[0]["KohaEShtypjesSeZRaportit"].ToString());
                if (dt.Rows[0]["LejoKerkiminmeEmer"].ToString() != "")
                    LejoKerkiminmeEmer = Convert.ToBoolean(dt.Rows[0]["LejoKerkiminmeEmer"].ToString());
                if (dt.Rows[0]["AplikocmiminMeShumiceKurarrihetPaketimi"].ToString() != "")
                    AplikocmiminMeShumiceKurarrihetPaketimi = Convert.ToBoolean(dt.Rows[0]["AplikocmiminMeShumiceKurarrihetPaketimi"].ToString());
                if (dt.Rows[0]["LejoStokunNegative"].ToString() != "")
                    LejoStokunNegative = Convert.ToBoolean(dt.Rows[0]["LejoStokunNegative"].ToString());
                if (dt.Rows[0]["LejoZbritjenNeArke"].ToString() != "")
                    LejoZbritjenNeArke = Convert.ToBoolean(dt.Rows[0]["LejoZbritjenNeArke"].ToString());
                if (dt.Rows[0]["LejoNDerrimineCmimit"].ToString() != "")
                    LejoNDerrimineCmimit = Convert.ToBoolean(dt.Rows[0]["LejoNDerrimineCmimit"].ToString());
                if (dt.Rows[0]["ShtypKopjenEKuponitFiskal"].ToString() != "")
                    ShtypKopjenEKuponitFiskal = Convert.ToBoolean(dt.Rows[0]["ShtypKopjenEKuponitFiskal"].ToString());
                if (dt.Rows[0]["KerkoPassWordPerAplikiminEZbritjes"].ToString() != "")
                    KerkoPassWordPerAplikiminEZbritjes = Convert.ToBoolean(dt.Rows[0]["KerkoPassWordPerAplikiminEZbritjes"].ToString());
                if (dt.Rows[0]["LejoRabatPerTeGjitheArtikujt"].ToString() != "")
                    LejoRabatPerTeGjitheArtikujt = Convert.ToBoolean(dt.Rows[0]["LejoRabatPerTeGjitheArtikujt"].ToString());
                if (dt.Rows[0]["LejoZbritjeNeTotalVler"].ToString() != "")
                    LejoZbritjeNeTotalVler = Convert.ToBoolean(dt.Rows[0]["LejoZbritjeNeTotalVler"].ToString());
                if (dt.Rows[0]["RegjimiPunesOffline"].ToString() != "")
                    RegjimiPunesOffline = Convert.ToBoolean(dt.Rows[0]["RegjimiPunesOffline"].ToString());
                if (dt.Rows[0]["IntervaliImportimitSekonda"].ToString() != "")
                    IntervaliImportimitSekonda = Convert.ToInt32(dt.Rows[0]["IntervaliImportimitSekonda"].ToString());
                if (dt.Rows[0]["IntervaliDergimitSekonda"].ToString() != "")
                    IntervaliDergimitSekonda = Convert.ToInt32(dt.Rows[0]["IntervaliDergimitSekonda"].ToString());
                if (dt.Rows[0]["KaTeDrejtTePunojOffline"].ToString() != "")
                    KaTeDrejtTePunojOffline = Convert.ToBoolean(dt.Rows[0]["KaTeDrejtTePunojOffline"].ToString());
                if (dt.Rows[0]["OperatoriAutomatikId"].ToString() != "")
                    OperatoriAutomatikId = Convert.ToInt32(dt.Rows[0]["OperatoriAutomatikId"].ToString());
                if (dt.Rows[0]["ShteguFiskal"].ToString() != "")
                    ShteguFiskal = Convert.ToString(dt.Rows[0]["ShteguFiskal"].ToString());
                if (dt.Rows[0]["TipiPrinterit"].ToString() != "")
                    TipiPrinterit = Convert.ToString(dt.Rows[0]["TipiPrinterit"].ToString());
                if (dt.Rows[0]["Porti"].ToString() != "")
                    Porti = Convert.ToString(dt.Rows[0]["Porti"].ToString());
                if (dt.Rows[0]["PrintonDirekt"].ToString() != "")
                    PrintonDirekt = Convert.ToBoolean(dt.Rows[0]["PrintonDirekt"].ToString());
                if (dt.Rows[0]["ToshibaSkaneri"].ToString() != "")
                    ToshibaSkaneri = Convert.ToBoolean(dt.Rows[0]["ToshibaSkaneri"].ToString());
                if (dt.Rows[0]["ToshibaSirtari"].ToString() != "")
                    ToshibaSirtari = Convert.ToBoolean(dt.Rows[0]["ToshibaSirtari"].ToString());
                if (dt.Rows[0]["ToshibaEkraniKlientit"].ToString() != "")
                    ToshibaEkraniKlientit = Convert.ToBoolean(dt.Rows[0]["ToshibaEkraniKlientit"].ToString());
                if (dt.Rows[0]["NeTestim"].ToString() != "")
                    NeTestim = Convert.ToBoolean(dt.Rows[0]["NeTestim"].ToString());
                if (dt.Rows[0]["TouchScreen"].ToString() != "")
                    TouchScreen = Convert.ToBoolean(dt.Rows[0]["TouchScreen"].ToString());
                if (dt.Rows[0]["HapPortinNjeHere"].ToString() != "")
                    HapPortinNjeHere = Convert.ToBoolean(dt.Rows[0]["HapPortinNjeHere"].ToString());
                if (dt.Rows[0]["ToshibaTipiSkanerit"].ToString() != "")
                    ToshibaTipiSkanerit = dt.Rows[0]["ToshibaTipiSkanerit"].ToString();
                if (dt.Rows[0]["PMPDispenzeriId"].ToString() != "")
                    PMPDispenzeriId = Convert.ToInt32(dt.Rows[0]["PMPDispenzeriId"].ToString());
                if (dt.Rows[0]["PMPDispenzeri"].ToString() != "")
                    PMPDispenzeri = (dt.Rows[0]["PMPDispenzeri"].ToString());
            }
        }

        public DataTable GetArkenLokalisht(int? Id = null, int? OrganizataId = null, int? _NrArkes = null)
        {
            //Kerkoje ne bazen lokale 
            DataTable tabela = new DataTable();
            SqlConnectionStringBuilder a = new SqlConnectionStringBuilder(PublicClass.KoneksioniLokal);
            a.ConnectTimeout = 6;
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_ArkatSelect_sp", a.ConnectionString);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Id", Id);
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", OrganizataId);
            da.SelectCommand.Parameters.AddWithValue("@NrArkes", _NrArkes);
            da.SelectCommand.CommandTimeout = 6;
            da.Fill(tabela);
            return tabela;
        }

        public string Ruaj(bool UpdateVerzionin)
        {


            SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniPrimar);
            SqlTransaction tran = default;
            try
            {
                cnn.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            try
            {
                SqlCommand cmdArkatInsert_spIns = new SqlCommand("dbo.ArkatInsert_sp", cnn);
                cmdArkatInsert_spIns.CommandType = CommandType.StoredProcedure;
                cmdArkatInsert_spIns.CommandTimeout = 10;

                cmdArkatInsert_spIns.Parameters.AddWithValue("@OrganizataId", OrganizataId);
                cmdArkatInsert_spIns.Parameters.AddWithValue("@NrArkes", NrArkes);
                cmdArkatInsert_spIns.Parameters.AddWithValue("@HostName", HostName);
                cmdArkatInsert_spIns.Parameters.AddWithValue("@FreskimIPlote", FreskimIPlote);
                cmdArkatInsert_spIns.Parameters.AddWithValue("@FLinkCode", FLinkCode);
                cmdArkatInsert_spIns.Parameters.AddWithValue("@PGMCode", PGMCode);
                cmdArkatInsert_spIns.Parameters.AddWithValue("@NumriArkesGK", NumriArkesGK);
                if (UpdateVerzionin)
                    cmdArkatInsert_spIns.Parameters.AddWithValue("@VerzioniIArkes", VerzioniIArkes);
                cmdArkatInsert_spIns.Parameters.AddWithValue("@DataERegjistrimit", DataERegjistrimit);
                cmdArkatInsert_spIns.Parameters.AddWithValue("@ShtypjaAutomatikeZRaport", ShtypjaAutomatikeZRaport);
                cmdArkatInsert_spIns.Parameters.AddWithValue("@KohaEShtypjesSeZRaportit", KohaEShtypjesSeZRaportit);
                cmdArkatInsert_spIns.Parameters.AddWithValue("@LejoKerkiminmeEmer", LejoKerkiminmeEmer);
                cmdArkatInsert_spIns.Parameters.AddWithValue("@AplikocmiminMeShumiceKurarrihetPaketimi", AplikocmiminMeShumiceKurarrihetPaketimi);
                cmdArkatInsert_spIns.Parameters.AddWithValue("@LejoStokunNegative", LejoStokunNegative);
                cmdArkatInsert_spIns.Parameters.AddWithValue("@LejoZbritjenNeArke", LejoZbritjenNeArke);
                cmdArkatInsert_spIns.Parameters.AddWithValue("@LejoNDerrimineCmimit", LejoNDerrimineCmimit);
                cmdArkatInsert_spIns.Parameters.AddWithValue("@ShtypKopjenEKuponitFiskal", ShtypKopjenEKuponitFiskal);
                cmdArkatInsert_spIns.Parameters.AddWithValue("@KerkoPassWordPerAplikiminEZbritjes", KerkoPassWordPerAplikiminEZbritjes);
                cmdArkatInsert_spIns.Parameters.AddWithValue("@LejoRabatPerTeGjitheArtikujt", LejoRabatPerTeGjitheArtikujt);
                cmdArkatInsert_spIns.Parameters.AddWithValue("@LejoZbritjeNeTotalVler", LejoZbritjeNeTotalVler);
                cmdArkatInsert_spIns.Parameters.AddWithValue("@RegjimiPunesOffline", RegjimiPunesOffline);
                cmdArkatInsert_spIns.Parameters.AddWithValue("@IntervaliImportimitSekonda", IntervaliImportimitSekonda);
                cmdArkatInsert_spIns.Parameters.AddWithValue("@IntervaliDergimitSekonda", IntervaliDergimitSekonda);
                cmdArkatInsert_spIns.Parameters.AddWithValue("@OperatoriAutomatikId", OperatoriAutomatikId);
                cmdArkatInsert_spIns.Parameters.AddWithValue("@ShteguFiskal", ShteguFiskal);
                cmdArkatInsert_spIns.Parameters.AddWithValue("@TipiPrinterit", TipiPrinterit);
                cmdArkatInsert_spIns.Parameters.AddWithValue("@Porti", Porti);
                cmdArkatInsert_spIns.Parameters.AddWithValue("@LejoNdryshiminESasise", LejoNdryshiminESasise);
                cmdArkatInsert_spIns.Parameters.AddWithValue("@LejoFshirjenEArtikujve", LejoFshirjenEArtikujve);
                cmdArkatInsert_spIns.Parameters.AddWithValue("@PMPDispenzeriId", PMPDispenzeriId);
                cmdArkatInsert_spIns.Parameters.AddWithValue("@TransferetEnable", TransferetEnable);
                cmdArkatInsert_spIns.Parameters.AddWithValue("@ShperblimetEnable", ShperblimetEnable);
                cmdArkatInsert_spIns.Parameters.AddWithValue("@SubjektiDetaleEnable", SubjektiDetaleEnable);
                Id = Convert.ToInt32(cmdArkatInsert_spIns.ExecuteScalar().ToString());


                PublicClass.Arka = this;
                WriteToDisc();

                OrganizataClass O = new OrganizataClass(this.OrganizataId);
                O.WriteToDisc();
                PublicClass.Organizata = O;

                ConfigurationDataClass C = new ConfigurationDataClass();
                C.GetFromDB();
                C.WriteToDisc();
                PublicClass.ConfigurationData = C;


                return "OK";
            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show("Ruajtja te Arka: " + ex.Message);
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

        }

        public bool GetFreskimIPlote(int _OrganizataId, int _NrArkes)
        {
            DataTable tabela = new DataTable();
            SqlConnectionStringBuilder a = new SqlConnectionStringBuilder(PublicClass.KoneksioniPrimar);
            a.ConnectTimeout = 8;

            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_ArkatSelect_sp", a.ConnectionString);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", _OrganizataId);
            da.SelectCommand.Parameters.AddWithValue("@NrArkes", _NrArkes);
            try
            {
                da.Fill(tabela);
            }
            catch (Exception ex)
            {
                return false;
            }


            if (tabela.Rows.Count > 0)
            {
                bool.TryParse(tabela.Rows[0]["FreskimIPlote"].ToString(), out bool lb);
                return lb;
            }
            else
                return false;

        }
        public bool ADuhetTeRuehtArkaNeServer(int _OrganizataId, int _NrArkes)
        {
            DataTable tabela = new DataTable();
            SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder(PublicClass.Arka.KoneksioniPrimar);
            sb.ConnectTimeout = 3;
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_ArkatSelect_sp", sb.ConnectionString);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", _OrganizataId);
            da.SelectCommand.Parameters.AddWithValue("@NrArkes", _NrArkes);
            string error;
            try
            {
                da.Fill(tabela);
            }
            catch (Exception ex)
            {
                return false;
            }

            if (tabela != null && tabela.Rows.Count > 0)
                return false;
            else
                return true;
        }
        public DataTable GetArkenServer(int _OrganizataId, int _NrArkes)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_ArkatSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", _OrganizataId);
            da.SelectCommand.Parameters.AddWithValue("@NrArkes", _NrArkes);
            da.SelectCommand.CommandTimeout = 6;
            da.Fill(tabela);
            return tabela;
        }
        public string GetVerzioninNgaServeri(int _OrganizataId, int _NrArkes, string HostName)
        {
            DataTable tabela = new DataTable();
            SqlConnectionStringBuilder a = new SqlConnectionStringBuilder(PublicClass.KoneksioniPrimar);
            a.ConnectTimeout = 5;

            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_ArkatSelect_sp", a.ConnectionString);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", _OrganizataId);
            da.SelectCommand.Parameters.AddWithValue("@NrArkes", _NrArkes);
            da.SelectCommand.Parameters.AddWithValue("@HostName", HostName);
            try
            {
                da.Fill(tabela);
            }
            catch (Exception)
            {
                return "";
            }

            if (tabela.Rows.Count > 0)
                return tabela.Rows[0]["VerzioniIArkes"].ToString();
            else
                return "";
        }
        int _Id;
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public int OrganizataId { get; set; }
        public string Organizata { get; set; }
        public int NrArkes { get; set; }
        public string HostName { get; set; } = Dns.GetHostName();
        public bool FreskimIPlote { get; set; }
        public string FLinkCode { get; set; }
        public string PGMCode { get; set; }
        public string NumriArkesGK { get; set; }
        public string VerzioniIArkes { get; set; }
        public DateTime? DataERegjistrimit { get; set; }
        public bool? ShtypjaAutomatikeZRaport { get; set; } = false;
        public DateTime? KohaEShtypjesSeZRaportit { get; set; }
        public bool LejoKerkiminmeEmer { get; set; }
        public bool AplikocmiminMeShumiceKurarrihetPaketimi { get; set; }
        public bool LejoStokunNegative { get; set; } = true;
        public bool LejoZbritjenNeArke { get; set; }
        public bool LejoNDerrimineCmimit { get; set; }
        public bool ShtypKopjenEKuponitFiskal { get; set; }
        public bool KerkoPassWordPerAplikiminEZbritjes { get; set; }
        public bool LejoRabatPerTeGjitheArtikujt { get; set; }
        public bool LejoZbritjeNeTotalVler { get; set; }
        public bool? RegjimiPunesOffline { get; set; } = true;
        public int? OperatoriAutomatikId { get; set; }
        public int? OperatoriAutomatik { get; set; }
        public string IPEKontrollerit { get; set; }
        public string LokalServeri { get; set; }
        public string LokalDataBaza { get; set; } = "POS";
        public string LokalUserDB { get; set; }
        public string LokalUserPas { get; set; }
        public int IntervaliImportimitSekonda { get; set; } = 250;
        public int IntervaliDergimitSekonda { get; set; } = 20;
        public int IntervaliUpdateServerLKSekonda { get; set; } = 60;
        public bool PompaEHapKubitPosin { get; set; } = true;
        public string ShteguFiskal { get; set; } = "C:\\Temp\\";
        public bool ShfaqPjesenEHotelit { get; set; } = false;
        public bool ShtypKuponFiskalNeMbylljeTeTavolines { get; set; }
        public string TipiPrinterit { get; set; } = "DATECS";
        public string Porti { get; set; } = "COM1";
        public bool PrintonDirekt { get; set; }
        public bool HapPortinNjeHere { get; set; }
        public string Serveri { get; set; }
        public string Shfrytezuesi { get; set; }
        public string Fjalekalimi { get; set; }
        public string Databaza { get; set; }
        public bool KaTeDrejtTePunojOffline { get; set; } = false;
        public string KoneksioniPrimar
        {
            get
            {
                SqlConnectionStringBuilder cb = new SqlConnectionStringBuilder();
                return cb.ConnectionString;
            }
        }
        public string KoneksioniLokal
        {
            get
            {
                SqlConnectionStringBuilder cb = new SqlConnectionStringBuilder();
                cb.InitialCatalog = LokalDataBaza != null ? LokalDataBaza : "POS";

                if (PublicClass.Arka.NeTestim)
                    cb.InitialCatalog = "TEST";

                if (LokalServeri != null && LokalServeri != "")
                    cb.DataSource = LokalServeri;
                else
                    cb.DataSource = ".\\SqlExpress";

                if (LokalUserDB != null && LokalUserDB != "")
                {
                    cb.UserID = LokalUserDB;
                    cb.Password = LokalUserPas;
                }
                else
                {
                    cb.IntegratedSecurity = true;
                }

                cb.ConnectTimeout = 3600;
                return cb.ConnectionString;
            }
        }
        public void WriteToDisc()
        {
            using (var ms = new MemoryStream())
            {
                RegistryKeyUtility mod = new RegistryKeyUtility();
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, this);
                var data = ms.ToArray();
                mod.BaseRegistryKey.SetValue("Arka", data, Microsoft.Win32.RegistryValueKind.Binary);
            }
        }
        public void LoadFromDiscToPublicClassArka()
        {
            ArkatClass ArkaNgaDisku = new ArkatClass();
            try
            {
                RegistryKeyUtility mod = new RegistryKeyUtility();
                byte[] a = (byte[])mod.BaseRegistryKey.GetValue("Arka");
                using (MemoryStream ms1 = new MemoryStream(a))
                {
                    IFormatter br = new BinaryFormatter();
                    ms1.Position = 0;
                    ArkaNgaDisku = ((ArkatClass)br.Deserialize(ms1));
                    PublicClass.Arka = ArkaNgaDisku;
                }
                return;
            }
            catch (Exception)
            {

            }
        }
        public void LoadFromRegedit()
        {
            try
            {
                RegistryKeyUtility mod = new RegistryKeyUtility();
                SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder();
                sb.ConnectionString = mod.Read("Koneksioni");
                Serveri = sb.DataSource;
                Shfrytezuesi = sb.UserID;
                Fjalekalimi = sb.Password;
                Databaza = sb.InitialCatalog;

                if (
                    string.IsNullOrEmpty(Serveri) == true ||
                    string.IsNullOrEmpty(Shfrytezuesi) == true ||
                    string.IsNullOrEmpty(Fjalekalimi) == true ||
                    string.IsNullOrEmpty(Databaza) == true
                    )
                {
                    return;
                }
                NrArkes = Convert.ToInt32(mod.Read("NrArkes"));
                if (mod.Read("LejoKerkiminmeEmer").ToString() != "")
                    AplikocmiminMeShumiceKurarrihetPaketimi = Convert.ToBoolean(mod.Read("LejoKerkiminmeEmer"));
                if (mod.Read("LejoKerkiminmeEmer").ToString() != "")
                    LejoStokunNegative = Convert.ToBoolean(mod.Read("LejoKerkiminmeEmer"));

                LejoNDerrimineCmimit = Convert.ToBoolean(mod.Read("LejoKerkiminmeEmer"));
                ShtypKopjenEKuponitFiskal = Convert.ToBoolean(mod.Read("LejoKerkiminmeEmer"));
                KerkoPassWordPerAplikiminEZbritjes = Convert.ToBoolean(mod.Read("LejoKerkiminmeEmer"));
                LejoRabatPerTeGjitheArtikujt = Convert.ToBoolean(mod.Read("LejoKerkiminmeEmer"));
                LejoZbritjeNeTotalVler = Convert.ToBoolean(mod.Read("LejoKerkiminmeEmer"));



                LejoStokunNegative = Convert.ToBoolean(mod.Read("LejoStokunNegative"));
                LejoZbritjenNeArke = Convert.ToBoolean(mod.Read("LejoZbritjenNeArke"));
                LejoNDerrimineCmimit = Convert.ToBoolean(mod.Read("LejoNDerrimineCmimit"));
                LejoKerkiminmeEmer = Convert.ToBoolean(mod.Read("LejoKerkiminmeEmer"));
                VerzioniIArkes = mod.Read("VerzioniIArkes").ToString();

                if (mod.Read("PompaEHapKubitPosin").ToString() != "")
                    PompaEHapKubitPosin = Convert.ToBoolean(mod.Read("PompaEHapKubitPosin"));

                if (mod.Read("LokalServeri").ToString() != "")
                {
                    LokalServeri = mod.Read("LokalServeri");
                    LokalDataBaza = mod.Read("LokalDataBaza");
                    LokalUserDB = mod.Read("LokalUserDB");
                    LokalUserPas = mod.Read("LokalUserPas");
                }
                if (mod.Read("OperatoriAutomatikId").ToString() != "")
                    OperatoriAutomatikId = Convert.ToInt32(mod.Read("OperatoriAutomatikId").ToString());
                if (mod.Read("RegjimiPunesOffline").ToString() != "")
                    RegjimiPunesOffline = Convert.ToBoolean(mod.Read("RegjimiPunesOffline"));

                if (mod.Read("IntervaliImportimitSekonda").ToString() != "")
                    IntervaliImportimitSekonda = Convert.ToInt16(mod.Read("IntervaliImportimitSekonda"));

                if (mod.Read("IntervaliDergimitSekonda").ToString() != "")
                    IntervaliDergimitSekonda = Convert.ToInt16(mod.Read("IntervaliDergimitSekonda"));

                if (mod.Read("ShteguFiskal").ToString() != "")
                    ShteguFiskal = mod.Read("ShteguFiskal");
                if (!string.IsNullOrEmpty(mod.Read("ShfaqPjesenEHotelit")))
                {
                    bool.TryParse(mod.Read("ShfaqPjesenEHotelit").ToString(), out bool asdf);
                    ShfaqPjesenEHotelit = asdf;
                }
                if (!string.IsNullOrEmpty(mod.Read("ShtypKuponFiskalNeMbylljeTeTavolines")))
                {
                    bool.TryParse(mod.Read("ShtypKuponFiskalNeMbylljeTeTavolines").ToString(), out bool shtpsk);
                    ShtypKuponFiskalNeMbylljeTeTavolines = shtpsk;
                }
                if (!string.IsNullOrEmpty(mod.Read("IPEKontrollerit")))
                {
                    IPEKontrollerit = (mod.Read("IPEKontrollerit"));
                }
                if (!string.IsNullOrEmpty(mod.Read("TipiPrinterit")))
                {
                    TipiPrinterit = (mod.Read("TipiPrinterit"));
                }
                if (!string.IsNullOrEmpty(mod.Read("Porti")))
                {
                    Porti = (mod.Read("Porti"));
                }
                if (!string.IsNullOrEmpty(mod.Read("PrintonDirekt")))
                {
                    PrintonDirekt = (bool.Parse(mod.Read("PrintonDirekt")));
                }
                if (!string.IsNullOrEmpty(mod.Read("HapPortinNjeHere")))
                {
                    HapPortinNjeHere = (bool.Parse(mod.Read("HapPortinNjeHere")));
                }
                if (!string.IsNullOrEmpty(mod.Read("Id")))
                {
                    Id = Convert.ToInt32(mod.Read("Id"));
                }
                if (!string.IsNullOrEmpty(mod.Read("ToshibaSkaneri")))
                {
                    ToshibaSkaneri = (bool.Parse(mod.Read("ToshibaSkaneri")));
                }
                if (!string.IsNullOrEmpty(mod.Read("ToshibaSirtari")))
                {
                    ToshibaSirtari = (bool.Parse(mod.Read("ToshibaSirtari")));
                }
                if (!string.IsNullOrEmpty(mod.Read("ToshibaEkraniKlientit")))
                {
                    ToshibaEkraniKlientit = (bool.Parse(mod.Read("ToshibaEkraniKlientit")));
                }
                if (!string.IsNullOrEmpty(mod.Read("ToshibaTipiSkanerit")))
                {
                    ToshibaTipiSkanerit = mod.Read("ToshibaTipiSkanerit");
                }

                OrganizataId = Convert.ToInt32(mod.Read("OrganizataId"));



                //Organizata = PublicClass.Organizata.Pershkrimi;
                //OrganizataId = PublicClass.OrganizataId;
            }
            catch (Exception)
            {

            }
        }
        public bool ToshibaSkaneri { get; set; }
        public bool ToshibaSirtari { get; set; }
        public bool ToshibaEkraniKlientit { get; set; }
        public bool NeTestim
        {
            get;
            set;
        } = false;
        public bool TouchScreen { get; set; }
        public string ToshibaTipiSkanerit { get; set; } = "RS232Scanner";
        public Nullable<int> PMPDispenzeriId { get; set; } = null;
        public string PMPDispenzeri { get; set; }

        private bool _lejondryshiminESasise = true;
        public bool LejoNdryshiminESasise
        {
            get
            {
                try
                {
                    DataTable dt = GetArkenLokalisht(OrganizataId, NrArkes);

                    if (dt.Rows.Count > 0)
                    {
                        _lejondryshiminESasise = Convert.ToBoolean(dt.Rows[0]["LejoNdryshiminESasise"]);
                        return _lejondryshiminESasise;
                    }
                    else
                        return _lejondryshiminESasise;
                }
                catch
                {

                }

                return _lejondryshiminESasise;
            }
            set
            {
                _lejondryshiminESasise = value;
            }
        }

        private bool _lejoFshirjenEArtikujve = false;
        public bool LejoFshirjenEArtikujve
        {
            get
            {
                try
                {
                    DataTable dt = GetArkenLokalisht(OrganizataId, NrArkes);

                    if (dt.Rows.Count > 0)
                    {
                        _lejoFshirjenEArtikujve = Convert.ToBoolean(dt.Rows[0]["LejoFshirjenEArtikujve"]);
                        return _lejoFshirjenEArtikujve;
                    }
                    else
                        return _lejoFshirjenEArtikujve;
                }
                catch
                {

                }

                return _lejoFshirjenEArtikujve;
            }
            set
            {
                _lejoFshirjenEArtikujve = value;
            }
        }

        private bool _transferetEnable = false;

        public bool TransferetEnable
        {
            get
            {
                try
                {
                    DataTable dt = GetArkenLokalisht(OrganizataId, NrArkes);

                    if (dt.Rows.Count > 0)
                    {
                        _transferetEnable = Convert.ToBoolean(dt.Rows[0]["TransferetEnable"]);
                        return _transferetEnable;
                    }
                    else
                        return _transferetEnable;
                }
                catch
                {

                }

                return _transferetEnable;
            }
            set
            {
                _transferetEnable = value;
            }
        }

        private bool _shperblimetEnable = false;

        public bool ShperblimetEnable
        {
            get
            {
                try
                {
                    DataTable dt = GetArkenLokalisht(OrganizataId, NrArkes);

                    if (dt.Rows.Count > 0)
                    {
                        _shperblimetEnable = Convert.ToBoolean(dt.Rows[0]["ShperblimetEnable"]);
                        return _shperblimetEnable;
                    }
                    else
                        return _shperblimetEnable;
                }
                catch
                {

                }

                return _shperblimetEnable;
            }
            set
            {
                _shperblimetEnable = value;
            }
        }

        private bool _subjektiDetaleEnable = false;

        public bool SubjektiDetaleEnable
        {
            get
            {
                try
                {
                    DataTable dt = GetArkenLokalisht(OrganizataId, NrArkes);

                    if (dt.Rows.Count > 0)
                    {
                        _subjektiDetaleEnable = Convert.ToBoolean(dt.Rows[0]["SubjektiDetaleEnable"]);
                        return _subjektiDetaleEnable;
                    }
                    else
                        return _subjektiDetaleEnable;
                }
                catch
                {

                }

                return _subjektiDetaleEnable;
            }
            set
            {
                _subjektiDetaleEnable = value;
            }
        }
    }
}
