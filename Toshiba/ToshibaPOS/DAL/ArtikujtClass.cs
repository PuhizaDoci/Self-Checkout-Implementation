using System.Data;
using System.Data.SqlClient;
using ToshibaPos.SDK;

namespace ToshibaPOS.DAL
{
    public static class ArtikujtClass
    {
        public static DataRow GetArtikullin(string shifra)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_ArtikujtSelect_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Shifra", shifra);
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", PublicClass.OrganizataId);
            da.Fill(tabela);
            if (tabela.Rows.Count > 0)

            {
                return tabela.Rows[0];
            }
            else
                return null;
        }
    }
}
