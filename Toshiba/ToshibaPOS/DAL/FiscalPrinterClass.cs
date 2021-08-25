using NLog;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using ToshibaPos.SDK;

namespace ToshibaPOS.DAL
{
    public class FiscalPrinterClass
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void ShtypPermbajtjen(string shtegu, string fajlli)
        {
            try
            {
                //\\192.168.160.104\Temp
                string shteguFiskal;

                Logger.Info("Fillimi i shtypjes se kuponit fiskal!");

                string ShteguFiskalStatik = PublicClass.GetKonfigurimetVlere(155);

                Logger.Info("Shtegu fiskal statik = " + ShteguFiskalStatik);

                if (PublicClass.GetKonfigurimet(155))
                {
                    shteguFiskal = @"C:\Temp\" + ShteguFiskalStatik;
                }
                else
                {
                    shteguFiskal = PublicClass.Arka.ShteguFiskal + "\\" + shtegu + ".txt";
                }

                Logger.Info("Shtegu fiskal = " + shteguFiskal);

                if (!Directory.Exists(Path.GetDirectoryName(@"C:\Temp\")))
                    Directory.CreateDirectory(Path.GetDirectoryName(@"C:\Temp\"));


                if (!Directory.Exists(Path.GetDirectoryName(Path.GetTempPath() + "\\KuBITPOS\\")))
                    Directory.CreateDirectory(Path.GetDirectoryName(Path.GetTempPath() + "\\KuBITPOS\\"));

                string path = Path.GetTempPath() + "\\KuBITPOS\\" + Path.GetFileNameWithoutExtension(shtegu) + ".txt";

                Logger.Info("Path per kupon fiskal: " + path);

                StreamWriter sw = new StreamWriter(path, false);
                sw.WriteLine(fajlli);
                sw.Close();

                Logger.Info("Teksti fiskal u shkrua ne file!");


                if (File.Exists(shteguFiskal))
                    File.Delete(shteguFiskal);

                File.Copy(Path.GetTempPath() + "\\KuBITPOS\\" + Path.GetFileNameWithoutExtension(shtegu) + ".txt", shteguFiskal);
                Logger.Info("Shkrimi i kuponit fiskal u shkrua me sukses.");

            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Shkrimi i kuponit fiskal deshtoi. Error: " + ex.Message);
                throw ex;
            }
        }

        public static string MerrTXTFiskal(long daljaMallitId)
        {
            Logger.Info("Gjenerimi i kuponit fiskal filloi.");
            SqlConnection cnn = new SqlConnection(PublicClass.Koneksioni);
            string komandafiskale = "";
            try
            {
                cnn.Open();

                SqlCommand cmd = new SqlCommand("TOSHIBA.POS_GjenerimiKuponitFiskal_Sp", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DaljaMallitId", daljaMallitId);

                komandafiskale = cmd.ExecuteScalar().ToString();
                Logger.Info("Komanda fiskale u shkrua me sukses");

                return komandafiskale;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Gjenerimi i kuponit fiskal deshtoi. Error: " + ex.Message);
            }
            finally
            {
                cnn.Close();
            }

            return "";
        }
    }
}
