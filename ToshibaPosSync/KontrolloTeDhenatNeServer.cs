using ToshibaPos.SDK;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToshibaPosSinkronizimi
{
    public static class KontrolloTeDhenatNeServer
    {
        public static void  KontrolloTeDhenatNeServerDheDergoEmail(DateTime? DataERegjistrimit = null)
        {
            //try
            //{
            //    DataTable dtLokal = KontrolloTeDhenatNeServerSelect();
            //    DataTable dtServer = KontrolloTeDhenatNeServerRezultatiSelect(dtLokal, DataERegjistrimit);

            //    if (dtServer.Rows.Count > 0)
            //    {
            //        if (!string.IsNullOrEmpty(dtServer.Rows[0]["Result"].ToString()))
            //        {
            //            DergoEmail.Dergo("Sasia eshte ndryshe : " + dtServer.Rows[0]["Result"].ToString(), PublicClass.Organizata, "Shënimet nuk janë të njejta Lokalisht dhe në Server! Pika e shitjës: ");
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    DergoEmail.Dergo(ex.Message + "\n\n"+ ex.StackTrace, PublicClass.Organizata, "Ka ndodhur gabim gjat kontrollimit për krahasim të të dhënave në server! Pika e shitjës: ");
            //}           
        }

        private static DataTable KontrolloTeDhenatNeServerSelect()
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.KontrolloTeDhenatNeServerSelect_sp", PublicClass.KoneksioniLokal);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;       
            da.Fill(tabela);
            return tabela;
        }
        private static DataTable KontrolloTeDhenatNeServerRezultatiSelect(DataTable KontrolloTeDhenatType, DateTime? DataERegjistrimit = null)
        {
            DataTable tabela1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.KontrolloTeDhenatNeServerRezultatiSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DataERegjistrimit", DataERegjistrimit);
            da.SelectCommand.Parameters.AddWithValue("@KontrolloTeDhenatType", KontrolloTeDhenatType);
            da.Fill(tabela1);
            return tabela1;
        }
    }
}
