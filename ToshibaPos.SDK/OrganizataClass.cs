using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ToshibaPos.SDK
{
    [Serializable]
    public class OrganizataClass
    {
        public OrganizataClass(int? _id = null)
        {
            int.TryParse(_id.ToString(), out int ioio);
            if (ioio > 0)
            {
                GetFromDB(ioio);
            }
        }
        public void GetFromDB(int id)
        {
            if (id > 0)
            {
                DataTable dt = GetOrganizaten(id);
                if (dt.Rows.Count > 0)
                {
                    _Id = Convert.ToInt16(dt.Rows[0]["FilialaId"].ToString());
                    FilialaId = Convert.ToInt16(dt.Rows[0]["FilialaId"].ToString());
                    Pershkrimi = dt.Rows[0]["Pershkrimi"].ToString();
                    PershkrimiOrganizates = dt.Rows[0]["PershkrimiOrganizates"].ToString();
                    NRB = dt.Rows[0]["NRB"].ToString();
                    NrTVSH = dt.Rows[0]["NrTVSH"].ToString();
                    NumriFiskal = dt.Rows[0]["NumriFiskal"].ToString();
                    Email = dt.Rows[0]["Email"].ToString();
                    Telefoni = dt.Rows[0]["Telefoni"].ToString();
                    Fax = dt.Rows[0]["Fax"].ToString();
                    Teleporosia = dt.Rows[0]["Teleporosia"].ToString();
                    Adresa = dt.Rows[0]["Adresa"].ToString();
                    Vendi = dt.Rows[0]["Vendi"].ToString();
                    NumriPostar = dt.Rows[0]["NumriPostar"].ToString();
                    Shteti = dt.Rows[0]["Shteti"].ToString();
                    Komuna = dt.Rows[0]["Komuna"].ToString();
                    PersoniKontaktues = dt.Rows[0]["PersoniKontaktues"].ToString();
                    LlojiISubjektitID = Convert.ToInt16(dt.Rows[0]["LlojiISubjektitID"].ToString());
                    Koment = dt.Rows[0]["Koment"].ToString();
                    DataERegjistrimit = Convert.ToDateTime(dt.Rows[0]["DataERegjistrimit"].ToString());
                    RegjistruarNga = Convert.ToInt16(dt.Rows[0]["RegjistruarNga"].ToString());
                    if (dt.Rows[0]["Logo"].ToString() != "")
                        Logo = ((byte[])dt.Rows[0]["Logo"]);
                    if (dt.Rows[0]["LogoBardhEZi"].ToString() != "")
                        LogoBardhEZi = (byte[])dt.Rows[0]["LogoBardhEZi"];
                    if (dt.Rows[0]["LogoOrganizates"].ToString() != "")
                        LogoOrganizates = (byte[])dt.Rows[0]["LogoOrganizates"];
                    if (dt.Rows[0]["LogoBardhEZiOrganizates"].ToString() != "")
                        LogoBardhEZiOrganizates = (byte[])dt.Rows[0]["LogoBardhEZiOrganizates"];
                    //if (dt.Rows[0]["NrTerminalit"].ToString() != "")
                    //  NrTerminalit = dt.Rows[0]["NrTerminalit"].ToString();
                    Xhirollogaria = dt.Rows[0]["Xhirollogaria"].ToString();
                    SwiftCode = dt.Rows[0]["SwiftCode"].ToString();
                    IBAN = dt.Rows[0]["IBAN"].ToString();
                    PershkrimiFiliales = dt.Rows[0]["PershkrimiFiliales"].ToString();
                    NIT = dt.Rows[0]["NIT"].ToString();
                    NrLicenses = dt.Rows[0]["NrLicenses"].ToString();
                    Qyteti = dt.Rows[0]["Qyteti"].ToString();
                }
            }
        }
        int _Id;
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        public int FilialaId { get; set; }
        public string Pershkrimi { get; set; }
        public string PershkrimiOrganizates { get; set; }
        public string NRB { get; set; }
        public string NrTVSH { get; set; }
        public string NumriFiskal { get; set; }
        public string Email { get; set; }
        public string Telefoni { get; set; }
        public string Fax { get; set; }
        public string Teleporosia { get; set; }
        public string Adresa { get; set; }
        public string NrTerminalit { get; set; }
        public string Vendi { get; set; }
        public string NumriPostar { get; set; }
        public string Shteti { get; set; }
        public string Komuna { get; set; }
        public string PersoniKontaktues { get; set; }
        public int LlojiISubjektitID { get; set; }
        public string Koment { get; set; }
        public DateTime DataERegjistrimit { get; set; }
        public int RegjistruarNga { get; set; }
        public byte[] Logo { get; set; }
        public byte[] LogoBardhEZi { get; set; }
        public byte[] LogoOrganizates { get; set; }
        public byte[] LogoBardhEZiOrganizates { get; set; }
        public string Xhirollogaria { get; set; }
        public string SwiftCode { get; set; }
        public string IBAN { get; set; }
        public string PershkrimiFiliales { get; set; }
        public string NIT { get; set; }
        public string NrLicenses { get; set; }
        public string Qyteti { get; set; }
        public DataTable GetOrganizaten(int? id = null)
        {

            DataTable tabela = new DataTable();
            if (id != null && id > 0)
            {
                SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder(PublicClass.KoneksioniPrimar);
                sb.ConnectTimeout = 5;

                SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_OrganizataSelect_sp", sb.ConnectionString);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (id != null && id > 0)
                    da.SelectCommand.Parameters.AddWithValue("@FilialaIdi", id);
                da.SelectCommand.CommandTimeout = 20;
                try
                {
                    da.Fill(tabela);
                }
                catch (Exception)
                {

                }
                if (tabela.Rows.Count == 0)
                {
                    sb = new SqlConnectionStringBuilder(PublicClass.KoneksioniLokal);
                    sb.ConnectTimeout = 5;
                    da = new SqlDataAdapter("TOSHIBA.POS_OrganizataSelect_sp", sb.ConnectionString);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    if (id != null && id > 0)
                        da.SelectCommand.Parameters.AddWithValue("@FilialaIdi", id);
                    da.SelectCommand.CommandTimeout = 5;
                    da.Fill(tabela);
                }
            }
            return tabela;
        }
        public void WriteToDisc()
        {
            using (var ms = new MemoryStream())
            {
                RegistryKeyUtility mod = new RegistryKeyUtility();
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, this);
                var data = ms.ToArray();
                mod.BaseRegistryKey.SetValue("Organizata", data, Microsoft.Win32.RegistryValueKind.Binary);
            }
        }
        public void LoadFromDisc()
        {
            try
            {
                RegistryKeyUtility mod = new RegistryKeyUtility();
                byte[] a = (byte[])mod.BaseRegistryKey.GetValue("Organizata");
                using (MemoryStream ms1 = new MemoryStream(a))
                {
                    IFormatter br = new BinaryFormatter();
                    ms1.Position = 0;
                    PublicClass.Organizata = ((OrganizataClass)br.Deserialize(ms1));

                }
                return;
            }
            catch (Exception)
            {

            }
        }
    }
}
