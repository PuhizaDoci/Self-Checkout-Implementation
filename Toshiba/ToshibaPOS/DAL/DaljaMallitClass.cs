using System;
using System.Data;
using System.Data.SqlClient;
using ToshibaPos.SDK;

namespace ToshibaPOS.DAL
{
    public static class DaljaMallitClass
    {
        public static DataTable GetDaljet(DataTable tabela, int? Id = null)
        { 
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_DaljaMallitSelect_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Id", Id);
            da.Fill(tabela);
            return tabela;
        }
        public static DataTable GetDaljetDetalet(DataTable tabela,int? Id=null)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_DaljaMallitDetaleSelect_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Id", Id);
            da.Fill(tabela);
            return tabela;
        }
        public static DataTable GetEkzekutimiPageses(DataTable tabela, int? Id =null, long? DaljaMallitId=null )
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_EkzekutimiPagesesSelect_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Id", Id);
            da.SelectCommand.Parameters.AddWithValue("@DaljaMallitId", DaljaMallitId);
            da.Fill(tabela);
            return tabela;
        }

    }
}
