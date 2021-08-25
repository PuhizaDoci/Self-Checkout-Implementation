using System;
using System.Data;
using System.Data.SqlClient;
using ToshibaPos.SDK;

namespace ToshibaPos.DAL
{
    public class LoginClass
    {

        public static DataTable GetOperatoret(string shifra = null, int? id = null)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(
                "TOSHIBA.POS_ArkaOperatoretSelect_sp",
                PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Statusi", true);
            da.SelectCommand.Parameters.AddWithValue("@Id", id);
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", PublicClass.OrganizataId);
            da.SelectCommand.Parameters.AddWithValue("@ShifraOperatorit", shifra);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetNderrimetEHapura(int OperatoriId)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(
                "TOSHIBA.POS_BarazimiArkatarevePerSinkronizimSelect_sp",
                PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@OperatoriId", OperatoriId);
            da.SelectCommand.Parameters.AddWithValue("@NderrimiMbyllur", 0);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetRaportinEOperatoreveSelect(DataTable tabela, long Id)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter(
                "TOSHIBA.POS_BarazimiArkatarevePerSinkronizimSelect_sp",
                PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", PublicClass.OrganizataId);
            da.SelectCommand.Parameters.AddWithValue("@Id", Id);
            da.Fill(tabela);
            return tabela;
        }

        public static void FilloNderrimin(DataTable tabela,
            long Id, int OperatoriId, int LlojiDokumentitId)
        {
            SqlConnection cnn = new SqlConnection(PublicClass.Koneksioni);
            SqlTransaction tran = default(SqlTransaction);

            try
            {
                cnn.Open();
                tran = cnn.BeginTransaction();
            }

            catch (Exception ex)
            {
                throw ex;
            }


            SqlCommand cmdBarazimiArkatareveFilloNderrimin_sp = 
                new SqlCommand("TOSHIBA.POS_BarazimiArkatareveFilloNderrimin_sp", cnn, tran);
            cmdBarazimiArkatareveFilloNderrimin_sp.CommandType = CommandType.StoredProcedure;

            cmdBarazimiArkatareveFilloNderrimin_sp.Parameters.Add("@OperatoriId", SqlDbType.Int);
            cmdBarazimiArkatareveFilloNderrimin_sp.Parameters["@OperatoriId"].Direction = ParameterDirection.Input;
            cmdBarazimiArkatareveFilloNderrimin_sp.Parameters.Add("@OrganizataId", SqlDbType.Int);
            cmdBarazimiArkatareveFilloNderrimin_sp.Parameters["@Organizataid"].Direction = ParameterDirection.Input;
            cmdBarazimiArkatareveFilloNderrimin_sp.Parameters.Add("@LlojiDokumentitId", SqlDbType.Int);
            cmdBarazimiArkatareveFilloNderrimin_sp.Parameters["@LlojiDokumentitId"].Direction = ParameterDirection.Input;
            cmdBarazimiArkatareveFilloNderrimin_sp.Parameters.Add("@BarazuarNga", SqlDbType.Int);
            cmdBarazimiArkatareveFilloNderrimin_sp.Parameters["@BarazuarNga"].Direction = ParameterDirection.Input;
            cmdBarazimiArkatareveFilloNderrimin_sp.Parameters.Add("@GjendjaFillestare", SqlDbType.Decimal);
            cmdBarazimiArkatareveFilloNderrimin_sp.Parameters["@GjendjaFillestare"].Direction = ParameterDirection.Input;

            try
            {
                foreach (DataRow dr in tabela.Rows)
                {
                    cmdBarazimiArkatareveFilloNderrimin_sp.Parameters["@OperatoriId"].Value = OperatoriId;
                    cmdBarazimiArkatareveFilloNderrimin_sp.Parameters["@OrganizataId"].Value = PublicClass.OrganizataId;
                    cmdBarazimiArkatareveFilloNderrimin_sp.Parameters["@LlojiDokumentitId"].Value = LlojiDokumentitId;
                    cmdBarazimiArkatareveFilloNderrimin_sp.Parameters["@BarazuarNga"].Value = OperatoriId;
                    cmdBarazimiArkatareveFilloNderrimin_sp.Parameters["@GjendjaFillestare"].Value = dr["GjendjaFillestare"];
                    Id = Convert.ToInt64(cmdBarazimiArkatareveFilloNderrimin_sp.ExecuteScalar());
                    dr["Id"] = Id;
                }

                tran.Commit();
            }

            catch (Exception ex)
            {
                tran.Rollback();

                throw ex;
            }

            finally
            {
                cnn.Close();
            }
        }

        public static DataTable GetOrganizatat()
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("dbo.Mxh_FilialetSelectF1_sp", PublicClass.KoneksioniPrimar);
            da.Fill(tabela);
            return tabela;
        }
        public static DataTable GetOperatoret()
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("dbo.Mxh_OperatoretSelectF1_sp", PublicClass.KoneksioniPrimar);
            da.Fill(tabela);
            return tabela;
        }
    }
}