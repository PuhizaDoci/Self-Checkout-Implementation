using System.Data;
using System.Data.SqlClient;
using ToshibaPos.SDK;

namespace ToshibaPOS.DAL
{
    public class MenyratEPagesesClass
    {
        public static DataTable GetMenyratEPageses(DataTable tabela, int? Id = null)
        {
            if (tabela == null)
                tabela = new DataTable();

            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_MenyratEPagesesSelect_sp", PublicClass.KoneksioniLokal);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Id", Id);
            da.SelectCommand.CommandTimeout = 2;
            da.Fill(tabela);
            return tabela;
        }
    }
}
