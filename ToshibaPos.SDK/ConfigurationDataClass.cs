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
    public class ConfigurationDataClass
    {
        public ConfigurationDataClass()
        {
            //SetConfiguration(GetKonfiguriminPerEmail());
        }
        private string _gjuha, _email, _smtp, _userName, _pass;
        private int _port;

        public string Gjuha { get { return _gjuha; } }
        public string Email { get { return _email; } }
        public string Smtp { get { return _smtp; } }
        public int Port { get { return _port; } }
        public string UserName { get { return _userName; } }
        public string Pass { get { return _pass; } }
        public void GetFromDB()
        {
            DataTable dtKonfigurimi = GetKonfiguriminPerEmail();

            if (dtKonfigurimi.Rows.Count == 0)
            {
                return;
            }
            else if (dtKonfigurimi.Rows.Count == 1)
            {
                DataRow dr = dtKonfigurimi.Rows[0];

                _gjuha = dr["Gjuha"].ToString();
                _email = dr["Email"].ToString();
                _smtp = dr["Smtp"].ToString();
                _userName = dr["UserName"].ToString();
                _pass = dr["Pass"].ToString();

                int p = 0;
                int.TryParse(dr["Porti"].ToString(), out p);
                _port = p;
            }
        }
        DataTable GetKonfiguriminPerEmail()
        {
            DataTable konfigurimi = new DataTable();
            konfigurimi.Clear();
            SqlDataAdapter da = new SqlDataAdapter("[POS].[POS_Mxh_OrganizataDetaletSelect_Sp]", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            try
            {
                da.Fill(konfigurimi);
            }
            catch (Exception)
            {

            }

            if (konfigurimi.Rows.Count == 0)
            {
                SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder(PublicClass.KoneksioniLokal);
                sb.ConnectTimeout = 5;
                da = new SqlDataAdapter("[POS].[POS_Mxh_OrganizataDetaletSelect_Sp]", sb.ConnectionString);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(konfigurimi);
            }

            return konfigurimi;
        }
        public void WriteToDisc()
        {
            using (var ms = new MemoryStream())
            {
                RegistryKeyUtility mod = new RegistryKeyUtility();
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, this);
                var data = ms.ToArray();
                mod.BaseRegistryKey.SetValue("ConfigurationDataClass", data, Microsoft.Win32.RegistryValueKind.Binary);
            }
        }
        public void LoadFromDisc()
        {
            ConfigurationDataClass ConfigurationData = new ConfigurationDataClass();
            try
            {
                RegistryKeyUtility mod = new RegistryKeyUtility();
                byte[] a = (byte[])mod.BaseRegistryKey.GetValue("ConfigurationDataClass");
                using (MemoryStream ms1 = new MemoryStream(a))
                {
                    IFormatter br = new BinaryFormatter();
                    ms1.Position = 0;
                    ConfigurationData = ((ConfigurationDataClass)br.Deserialize(ms1));
                    PublicClass.ConfigurationData = ConfigurationData;
                }
                return;
            }
            catch (Exception)
            {

            }
        }
    }
}
