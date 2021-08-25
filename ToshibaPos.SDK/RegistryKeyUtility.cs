using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToshibaPos.SDK
{
    public class RegistryKeyUtility
    {
        private bool showError = false;
        /// <summary>
        /// A property to show or hide error messages 
        /// (default = false)
        /// </summary>
        public bool ShowError
        {
            get { return showError; }
            set { showError = value; }
        }

        private string subKey = "KuBITPOS\\KuBITPOS\\Konfigurimi";
        private string regEditMultiKey = "KuBITPOS\\KuBITPOS\\";
        /// <summary>
        /// A property to set the SubKey value
        /// (default = "SOFTWARE\\" + Application.ProductName.ToUpper())
        /// </summary>
        public string SubKey
        {
            get { return subKey; }
            set { subKey = value; }
        }

        private RegistryKey baseRegistryKey = Registry.CurrentUser;
        /// <summary>
        /// A property to set the BaseRegistryKey value.
        /// (default = Registry.LocalMachine)
        /// </summary>
        public RegistryKey BaseRegistryKey
        {
            get { return baseRegistryKey; }
            set { baseRegistryKey = value; }
        }

        /* **************************************************************************
         * **************************************************************************/



        /// <summary>
        /// To read a registry key.
        /// input: KeyName (string)
        /// output: value (string) 
        /// </summary>
        public string Read(string KeyName, string emriKoneksionit)
        {
            // Opening the registry key
            RegistryKey rk = baseRegistryKey;
            // Open a subKey as read-only

            RegistryKey sk1 = rk.OpenSubKey(regEditMultiKey + emriKoneksionit);
            // If the RegistrySubKey doesn't exist -> (null)
            if (sk1 == null)
            {
                return string.Empty;
            }
            else
            {
                try
                {
                    // If the RegistryKey exists I get its value
                    // or null is returned.
                    return Siguria.DekriptoStringun((string)sk1.GetValue(KeyName.ToUpper()));
                }
                catch (Exception)
                {
                    // AAAAAAAAAAARGH, an error!
                    //ShowErrorMessage(e, "Reading registry " + KeyName.ToUpper());
                    return string.Empty;
                }
            }
        }

        public DataTable ReadAll(DataTable dt)
        {
            // Opening the registry key
            RegistryKey rk = baseRegistryKey;
            // Open a subKey as read-only

            RegistryKey sk1 = rk.OpenSubKey(regEditMultiKey);
            // If the RegistrySubKey doesn't exist -> (null)
            if (sk1 == null)
            {
                return null;
            }

            try
            {
                dt.Rows.Clear();
                dt.Columns.Clear();
                dt.Columns.Add(new DataColumn("DEPARTAMENTIID", typeof(int)));
                dt.Columns.Add(new DataColumn("DEPARTAMENTI", typeof(string)));
                dt.Columns.Add(new DataColumn("FILIALA", typeof(string)));
                dt.Columns.Add(new DataColumn("KONEKSIONI", typeof(string)));
                dt.Columns.Add(new DataColumn("ORGANIZATAID", typeof(int)));
                dt.Columns.Add(new DataColumn("Vendi", typeof(string)));
                dt.Columns.Add(new DataColumn("PershkrimiKoneksionit", typeof(string)));
                dt.Columns.Add(new DataColumn("ShteguKuponitFiskal", typeof(string)));


                foreach (var s in sk1.GetSubKeyNames())
                {
                    RegistryKey r = sk1.OpenSubKey(s);
                    if (r != null)
                    {
                        var dr = dt.NewRow();
                        try
                        {
                            dr["DEPARTAMENTIID"] = Siguria.DekriptoStringun((string)r.GetValue("DEPARTAMENTIID"));
                        }
                        catch
                        {

                        }

                        try
                        {
                            dr["DEPARTAMENTI"] = Siguria.DekriptoStringun((string)r.GetValue("DEPARTAMENTI"));
                        }
                        catch (Exception)
                        {

                        }

                        try
                        {
                            dr["FILIALA"] = Siguria.DekriptoStringun((string)r.GetValue("FILIALA"));
                        }
                        catch
                        {

                        }

                        try
                        {
                            SqlConnectionStringBuilder cmb = new SqlConnectionStringBuilder(Siguria.DekriptoStringun((string)r.GetValue("KONEKSIONI")));

                            dr["KONEKSIONI"] = string.Format("Databaza:{0}, Serveri:{1}", cmb.InitialCatalog,
                                cmb.DataSource);

                        }
                        catch
                        {

                        }

                        try
                        {
                            dr["ORGANIZATAID"] = Siguria.DekriptoStringun((string)r.GetValue("ORGANIZATAID"));
                        }
                        catch
                        {

                        }

                        try
                        {
                            dr["PershkrimiKoneksionit"] = Siguria.DekriptoStringun((string)r.GetValue("PershkrimiKoneksionit"));
                        }
                        catch
                        {

                        }

                        try
                        {
                            dr["ShteguKuponitFiskal"] = Siguria.DekriptoStringun((string)r.GetValue("ShteguKuponitFiskal"));
                        }
                        catch
                        {

                        }

                        dr["Vendi"] = regEditMultiKey + "\\" + s;
                        dt.Rows.Add(dr);
                    }
                }
                // If the RegistryKey exists I get its value
                // or null is returned.
                return dt;
            }
            catch (Exception)
            {
                // AAAAAAAAAAARGH, an error!
                //ShowErrorMessage(e, "Reading registry " + KeyName.ToUpper());
                return null;
            }
        }


        /// <summary>
        /// To read a registry key.
        /// input: KeyName (string)
        /// output: value (string) 
        /// </summary>
        public string Read(string KeyName)
        {
            // Opening the registry key
            RegistryKey rk = baseRegistryKey;
            // Open a subKey as read-only
            RegistryKey sk1 = rk.OpenSubKey(subKey);
            // If the RegistrySubKey doesn't exist -> (null)
            if (sk1 == null)
            {
                return string.Empty;
            }
            else
            {
                try
                {
                    // If the RegistryKey exists I get its value
                    // or null is returned.
                    return PublicClass.DekriptoStringun((string)sk1.GetValue(KeyName.ToUpper()));
                }
                catch (Exception e)
                {
                    // AAAAAAAAAAARGH, an error!
                    ShowErrorMessage(e, "Reading registry " + KeyName.ToUpper());
                    return string.Empty;
                }
            }
        }

        /* **************************************************************************
         * **************************************************************************/

        /// <summary>
        /// To write into a registry key.
        /// input: KeyName (string) , Value (object)
        /// output: true or false 
        /// </summary>
        public bool Write(string KeyName, object Value)
        {
            try
            {
                // Setting
                RegistryKey rk = baseRegistryKey;
                // I have to use CreateSubKey 
                // (create or open it if already exits), 
                // 'cause OpenSubKey open a subKey as read-only
                RegistryKey sk1 = rk.CreateSubKey(subKey);
                // Save the value
                sk1.SetValue(KeyName.ToUpper(), PublicClass.EnkriptoStringun(Value.ToString()));

                return true;
            }
            catch (Exception e)
            {
                // AAAAAAAAAAARGH, an error!
                ShowErrorMessage(e, "Writing registry " + KeyName.ToUpper());
                return false;
            }
        }

        /* **************************************************************************
         * **************************************************************************/

        /// <summary>
        /// To delete a registry key.
        /// input: KeyName (string)
        /// output: true or false 
        /// </summary>
        public bool DeleteKey(string KeyName)
        {
            try
            {
                // Setting
                RegistryKey rk = baseRegistryKey;
                RegistryKey sk1 = rk.CreateSubKey(subKey);
                // If the RegistrySubKey doesn't exists -> (true)
                if (sk1 == null)
                    return true;
                else
                    sk1.DeleteValue(KeyName);

                return true;
            }
            catch (Exception e)
            {
                // AAAAAAAAAAARGH, an error!
                ShowErrorMessage(e, "Deleting SubKey " + subKey);
                return false;
            }
        }

        /* **************************************************************************
         * **************************************************************************/

        /// <summary>
        /// To delete a sub key and any child.
        /// input: void
        /// output: true or false 
        /// </summary>
        public bool DeleteSubKeyTree()
        {
            try
            {
                // Setting
                RegistryKey rk = baseRegistryKey;
                RegistryKey sk1 = rk.OpenSubKey(subKey);
                // If the RegistryKey exists, I delete it
                if (sk1 != null)
                    rk.DeleteSubKeyTree(subKey);

                return true;
            }
            catch (Exception e)
            {
                // AAAAAAAAAAARGH, an error!
                ShowErrorMessage(e, "Deleting SubKey " + subKey);
                return false;
            }
        }

        /* **************************************************************************
         * **************************************************************************/

        private void ShowErrorMessage(Exception e, string Title)
        {
            if (showError == true)
                MessageBox.Show(e.Message,
                                Title
                                , MessageBoxButtons.OK
                                , MessageBoxIcon.Error);
        }
    }
}
