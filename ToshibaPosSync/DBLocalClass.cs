using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ToshibaPos.SDK;

namespace ToshibaPosSinkronizimi
{
    public class DBLocalClass : IDisposable
    {
        TextBox txtInformo;
        public DBLocalClass(TextBox txt = null)
        {
            if (txt != null)
            {
                txtInformo = txt;
            }
            if (txtInformo == null)
            {
                txtInformo = new TextBox();
            }
        }
        public bool TestoKoneksionin(string cnnStr)
        {
            SqlConnection cnn = default(SqlConnection);
            SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder(cnnStr);
            sb.ConnectTimeout = 3;
            try
            {
                cnn = new SqlConnection(sb.ConnectionString);
                cnn.Open();
                cnn.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                cnn.Close();
            }
        }
        public void FreskoDatabazen()
        {

            txtInformo.Text = "Freskimi databazes !";
            txtInformo.Refresh();

            ArkatClass a1 = new ArkatClass();
            if (a1.GetFreskimIPlote(PublicClass.Arka.OrganizataId, PublicClass.Arka.NrArkes))
            {
                if (BackUpAndDropDatabaseNeseNukKaFatura())
                {
                    KrijoDatabazenLokale();
                    KrijoTabelatNeDatabazenLokale();
                    AplikoNdryshimetNeDatabazenLokale();
                    KrijoFunksionetNeDatabazenLokale();
                    KrijoProceduratNeDatabazenLokale();

                    FunctionClass.CopyClass(PublicClass.Arka, a1);

                    a1.FreskimIPlote = false;
                    a1.Ruaj(false);

                }

            }
            if (!DatabazaEkziston())
            {
                KrijoDatabazenLokale();
                KrijoTabelatNeDatabazenLokale();
                AplikoNdryshimetNeDatabazenLokale();
                KrijoFunksionetNeDatabazenLokale();
                KrijoProceduratNeDatabazenLokale();
                ImportoClass ji = new ImportoClass();
                ji.ImportoShenimet();
            }


            ArkatClass arkaverz = new ArkatClass();
            string verz = arkaverz.GetVerzioninNgaServeri(PublicClass.Arka.OrganizataId, PublicClass.Arka.NrArkes, System.Net.Dns.GetHostName());
            if (PublicClass.VerzioniIArkes != verz)
            {
                bool A1 = AplikoNdryshimetNeDatabazenLokale();
                bool A2 = KrijoFunksionetNeDatabazenLokale();
                bool A3 = KrijoProceduratNeDatabazenLokale();
                //RegistryKeyUtility mod = new RegistryKeyUtility();
                //mod.ShowError = true;
                //mod.Write("VerzioniIArkes", PublicClass.Arka.VerzioniIArkes);
                PublicClass.Arka.VerzioniIArkes = PublicClass.VerzioniIArkes;
                if (A1 && A2 && A3)
                    PublicClass.Arka.Ruaj(true);
            }
        }

        private bool BackUpAndDropDatabaseNeseNukKaFatura()
        {
            if (DatabazaEkziston())
            {
                if (!Directory.Exists(@"C:\POSBackup\"))
                    Directory.CreateDirectory(@"C:\POSBackup\");

                if (
                    EkzekutoScalarQueryLokalisht(string.Format(backupQuery, "POS",
                        "POS_" + string.Format("{0:dd_MM_yyyy_HH_mm_ss_backup}", DateTime.Now) + ".bak")).ToString() == "1")
                {

                    if (EkzekutoScalarQueryLokalisht(dropDatabaseQuery).ToString() == "1")
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    return false;
                }
            }
            else
                return false;
        }
        public void KrijoDatabazenLokale()
        {
            if (DatabazaEkziston() == false)
            {
                txtInformo.Text = "Vendosja e koneksionit lokal!";
                txtInformo.Refresh();
                SqlConnectionStringBuilder cb = new SqlConnectionStringBuilder();
                cb.ConnectionString = PublicClass.KoneksioniLokal;
                cb.InitialCatalog = "Master";
                SqlConnection cnn = new SqlConnection(cb.ConnectionString);
                SqlDataAdapter daPerGjenerimTeDb = new SqlDataAdapter();
                cnn.Open();
                txtInformo.Text = "Krijimi i databazes lokale !";
                txtInformo.Refresh();
                daPerGjenerimTeDb.SelectCommand = new SqlCommand();
                daPerGjenerimTeDb.SelectCommand.Connection = cnn;
                // databaza gjendet ne rreshtin e pare te tabeles
                if (PublicClass.Arka.NeTestim)
                    daPerGjenerimTeDb.SelectCommand.CommandText = "CREATE DATABASE TEST";
                else
                    daPerGjenerimTeDb.SelectCommand.CommandText = "CREATE DATABASE POS";
                daPerGjenerimTeDb.SelectCommand.ExecuteNonQuery(); // gjenero databazen
                cnn.Close();

            }

        }
        public bool DatabazaEkziston()
        {
            try
            {
                SqlConnectionStringBuilder conn = new SqlConnectionStringBuilder(PublicClass.KoneksioniLokal);
                conn.InitialCatalog = "Master";

                DataTable dt = new DataTable();
                SqlDataAdapter da;
                if (PublicClass.Arka.NeTestim)
                    da = new SqlDataAdapter(@"select 1 from master.sys.databases where name = 'TEST'", conn.ConnectionString);
                else
                    da = new SqlDataAdapter(@"select 1 from master.sys.databases where name = 'POS'", conn.ConnectionString);

                da.SelectCommand.CommandTimeout = 1;
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    txtInformo.Text = "Databaza nuk egziston!";
                    txtInformo.Refresh();
                    return false;
                }
                else
                {
                    txtInformo.Text = "Databaza egziston!";
                    txtInformo.Refresh();
                    return true;
                }
            }
            catch (Exception ex)
            {
                txtInformo.Text = "Duke kontrolluar se a egziston databaza : " + ex.Message;
                txtInformo.Refresh();
                return false;
            }

        }
        public bool KrijoTabelatNeDatabazenLokale()
        {
            string a = "";
            try
            {
                DataTable objektet = new DataTable();
                objektet.Columns.Add("Nr", typeof(int));
                objektet.Columns.Add("Query", typeof(string));


                string FjallidString = Properties.Resources.Tabelat;
                Char delimiter = ';';
                String[] substrings = FjallidString.Split(delimiter);
                foreach (string substring in substrings)
                {
                    string[] parts1 = substring.Split(new string[] { "Go --" }, StringSplitOptions.None);
                    foreach (string str in parts1)
                    {

                        if (Regex.Matches(str, @"[a-zA-Z]").Count == 0) { continue; }
                        objektet.Rows.Add(objektet.Rows.Count, str);
                    }
                }

                int x = 0, y = objektet.Rows.Count;
                int rez = 0;
                foreach (DataRow dr in objektet.Rows)
                {
                    a = dr["Query"].ToString();
                    txtInformo.Text = "Gjenerimi i objekteve:" + dr["Query"].ToString();
                    txtInformo.Refresh();
                    EkzekutoQueryLokalisht(dr["Query"].ToString());
                    x = x + 1;
                    rez = x / y * 100;
                }


                return true;
            }
            catch (Exception ex)
            {
                txtInformo.Text = "Krijimi i tabelave: " + ex.Message;
                txtInformo.Refresh();
                return false;
                throw;
            }
        }
        public bool AplikoNdryshimetNeDatabazenLokale()
        {
            try
            {
                DataTable objektet = new DataTable();
                objektet.Columns.Add("Nr", typeof(int));
                objektet.Columns.Add("Query", typeof(string));

                string FjallidString = Properties.Resources.Ndryshimet;
                Char delimiter = ';';
                String[] substrings = FjallidString.Split(delimiter);
                foreach (string substring in substrings)
                {
                    string[] parts1 = substring.Split(new string[] { "Go --" }, StringSplitOptions.None);
                    foreach (string str in parts1)
                    {

                        if (Regex.Matches(str, @"[a-zA-Z]").Count == 0) { continue; }
                        objektet.Rows.Add(objektet.Rows.Count, str);
                    }
                }
                int x = 0, y = objektet.Rows.Count;
                int rez = 0;
                foreach (DataRow dr in objektet.Rows)
                {
                    txtInformo.Text = "Aplikimi i ndryshimeve:" + dr["Query"].ToString();
                    txtInformo.Refresh();
                    EkzekutoQueryLokalisht(dr["Query"].ToString());
                    x = x + 1;
                    rez = x / y * 100;

                }

                return true;
            }
            catch (Exception ex)
            {
                txtInformo.Text = "Aplikimi i ndryshimeve : " + ex.Message;
                txtInformo.Refresh();
                return false;
                throw;
            }
        }
        public bool KrijoFunksionetNeDatabazenLokale()
        {
            try
            {
                DataTable objektet = new DataTable();
                objektet.Columns.Add("Nr", typeof(int));
                objektet.Columns.Add("Query", typeof(string));
                string FjallidString = Properties.Resources.Funksionet;
                Char delimiter = ';';
                String[] substrings = FjallidString.Split(delimiter);
                foreach (string substring in substrings)
                {
                    string[] parts1 = substring.Split(new string[] { "Go --" }, StringSplitOptions.None);
                    foreach (string str in parts1)
                    {

                        if (Regex.Matches(str, @"[a-zA-Z]").Count == 0) { continue; }
                        objektet.Rows.Add(objektet.Rows.Count, str);
                    }
                }
                int x = 0, y = objektet.Rows.Count;
                int rez = 0;
                foreach (DataRow dr in objektet.Rows)
                {
                    txtInformo.Text = "Gjenerimi i Funksioneve:" + dr["Query"].ToString();
                    txtInformo.Refresh();
                    EkzekutoQueryLokalisht(dr["Query"].ToString());
                    x = x + 1;
                    rez = x / y * 100;
                }
                return true;
            }
            catch (Exception ex)
            {
                txtInformo.Text = "Gjenerimi i Funksioneve : " + ex.Message;
                txtInformo.Refresh();
                return false;
                throw;
            }
        }
        public bool KrijoProceduratNeDatabazenLokale()

        {
            string proc = "";
            try
            {
                DataTable objektet = new DataTable();
                objektet.Columns.Add("Nr", typeof(int));
                objektet.Columns.Add("Query", typeof(string));
                string FjallidString = Properties.Resources.ProceduratLokal;

                string[] parts1 = FjallidString.Split(new string[] { "Go --" }, StringSplitOptions.None);
                foreach (string str in parts1)
                {

                    if (Regex.Matches(str, @"[a-zA-Z]").Count == 0) { continue; }
                    objektet.Rows.Add(objektet.Rows.Count, str);
                }

                int x = 0, y = objektet.Rows.Count;
                int rez = 0;

                foreach (DataRow dr in objektet.Rows)
                {
                    proc = dr["Query"].ToString();
                    txtInformo.Text = "Gjenerimi i Procedurave:" + proc;
                    txtInformo.Refresh();
                    EkzekutoQueryLokalisht(proc);
                    x = x + 1;
                    rez = x / y * 100;
                }
            }
            catch (Exception ex)
            {
                txtInformo.Text = "Gjenerimi i Procedurave : " + ex.Message;
                txtInformo.Refresh();
                return false;
                throw ex;
            }

            try
            {
                DataTable objektet = new DataTable();
                objektet.Columns.Add("Nr", typeof(int));
                objektet.Columns.Add("Query", typeof(string));
                string FjallidString = Properties.Resources.KomandatFiskale;
                Char delimiter = '#';
                String[] substrings = FjallidString.Split(delimiter);
                foreach (string substring in substrings)
                {
                    string[] parts1 = substring.Split(new string[] { "Go --" }, StringSplitOptions.None);
                    foreach (string str in parts1)
                    {

                        if (Regex.Matches(str, @"[a-zA-Z]").Count == 0) { continue; }
                        objektet.Rows.Add(objektet.Rows.Count, str);
                    }
                }
                int x = 0, y = objektet.Rows.Count;
                int rez = 0;

                foreach (DataRow dr in objektet.Rows)
                {
                    proc = dr["Query"].ToString();
                    EkzekutoQueryLokalisht(proc);
                    x = x + 1;
                    rez = x / y * 100;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }

        }
        public string EkzekutoScalarQueryLokalisht(string query)
        {
            SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
            SqlDataAdapter da = new SqlDataAdapter(query, cnn);

            try
            {
                cnn.Open();
                return da.SelectCommand.ExecuteScalar().ToString();

            }
            catch (SqlException s)
            {
                MessageBox.Show(s.Message);
                MessageBox.Show("Scalar Lokallisht: " + query);
            }
            finally
            {
                cnn.Close();
            }

            return "";
        }
        public void EkzekutoQueryLokalisht(string query)
        {

            SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
            SqlDataAdapter da = new SqlDataAdapter(query, cnn);

            try
            {
                cnn.Open();
                da.SelectCommand.ExecuteNonQuery();
                cnn.Close();
            }
            catch (SqlException s)
            {
                MessageBox.Show(s.Message);
                MessageBox.Show("Lokalisht: " + query);
                throw;
            }
        }
        public bool ShtoLinkedServer()
        {
            try
            {
                string shtoLinkedServerQuery = @"
                                            IF NOT  EXISTS (SELECT srv.name FROM sys.servers srv WHERE srv.server_id != 0 AND srv.name = N'{0}')
                                            begin
                                            exec sp_addlinkedserver @server = '{0}'

                                            exec sys.sp_addlinkedsrvlogin 
                                            @rmtsrvname ='{0}',
                                            @useself='false', 
                                            @rmtuser='{1}', 
                                            @rmtpassword='{2}'
                                            end ";

                SqlConnectionStringBuilder cb = new SqlConnectionStringBuilder();
                cb.ConnectionString = PublicClass.KoneksioniPrimar;
                EkzekutoQueryLokalisht(string.Format(shtoLinkedServerQuery, cb.DataSource, cb.UserID, cb.Password));

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #region Queries

        /*
        * Query per te nderruar modin e autentikimit ne windows auth. ose ne Sql Server Auth.
        */
        #region Konfiguro Auth. Mode query

        /// <summary>
        /// Nje query qe duhet per te konfiguruar serverin ne SqlServerAuth. Mode
        /// </summary>
        private string queryPerAuthMode = @"USE [master]
EXEC xp_instance_regwrite N'HKEY_LOCAL_MACHINE', N'Software\Microsoft\MSSQLServer\MSSQLServer', N'LoginMode', REG_DWORD, 2
";

        #endregion

        /*
         * Query qe duhet per te gjeneruar nje log in ne databaze 'CDI'
         */
        #region Gjenero login query

        /// <summary>
        /// Query qe duhet per te gjeneruar nje log in ne databaze 'CDI'
        /// </summary>
        private string gjeneroLogInQuery = @"USE [master]
 
CREATE LOGIN [POS] WITH PASSWORD=N'123456', DEFAULT_DATABASE=[POS], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
 
EXEC master..sp_addsrvrolemember @loginame = N'POS', @rolename = N'sysadmin'
 
USE [POS]
 
CREATE USER [POS] FOR LOGIN [POS]
 ";

        #endregion

        /*
         * Ben verifikimin se a ekziston databaza ne server me emrin 'POS'
         */
        #region Verifikon se a ekziston databaza

        /// <summary>
        /// Query per verifikimin se a ekziston databaza ne server me emrin 'POS'
        /// </summary>

        #endregion

        /*
         * Shton linked server ne lokal
         */
        #region Shto linked server query



        #endregion



        /*
         * Ben backup databazen
         */
        #region Backup query

        private string backupQuery = @"use master
IF EXISTS (SELECT 1 FROM sys.databases WHERE name ='POS')
BEGIN
BEGIN 
    BEGIN TRY 
        BACKUP DATABASE [{0}] TO  DISK = N'C:\POSBackup\{1}' WITH NOFORMAT, NOINIT,  NAME = N'POS-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10
    END TRY
    BEGIN CATCH
        SELECT 0
        RETURN;
    END CATCH

    SELECT 1
END
select 1
End
ELSE
BEGIN
select 1 
end
";



        #endregion


        #region DropDatabase

        private string dropDatabaseQuery = @"
IF EXISTS (SELECT 1 FROM sys.databases WHERE name ='POS')
BEGIN
			use master 
			IF NOT EXISTS (SELECT 1 FROM TOSHIBA.SYS.objects WHERE name = 'DaljaMallit')
			BEGIN
				EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'POS' 
				ALTER DATABASE [POS] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE 
				DROP DATABASE [POS]
				SELECT 1
			RETURN;
			END 

			IF NOT EXISTS(
				SELECT 1 FROM TOSHIBA.dbo.DaljaMallit where ServerId IS NULL
				)
			BEGIN 
				EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'POS' 
				ALTER DATABASE [POS] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE 
				DROP DATABASE [POS]
				SELECT 1
				RETURN;
			END
			ELSE
			BEGIN
				SELECT 0
				RETURN;
			END
			 
END
ELSE
Begin
SELECT 0;
RETURN;
End
";

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        #endregion


        #endregion
    }
}
