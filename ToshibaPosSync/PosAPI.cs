using System.Data;
using System.Data.SqlClient;
using ToshibaPos.SDK;

namespace ToshibaPosSinkronizimi
{
    public class PosAPI
    {
        public int Id { get; set; }
        public int? MxhObjektiId { get; set; }
        public string Url { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public int? AplikacioniId { get; } = 3124;

        public PosAPI()
        {
            if (Id > 0)
            {
                using var dataTable = GetPosAPISelect();
                if (dataTable != null)
                {
                    Id = (int)dataTable.Rows[0]["Id"];
                    MxhObjektiId = (int?)dataTable.Rows[0]["MxhObjektiId"];
                    Url = dataTable.Rows[0]["URL"].ToString();
                    Username = dataTable.Rows[0]["Username"].ToString();
                    Password = dataTable.Rows[0]["Password"].ToString();
                    Key = dataTable.Rows[0]["Key"].ToString();
                    Value = dataTable.Rows[0]["Value"].ToString();
                    AplikacioniId = (int?)dataTable.Rows[0]["AplikacioniId"];
                }
            }
        }

        public DataTable GetPosAPISelect()
        {
            DataTable tabela = new DataTable();
            using var da = new SqlDataAdapter("TOSHIBA.Mxh_APISelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@AplikacioniId", AplikacioniId);
            da.Fill(tabela);
            return tabela;
        }
    }
}
