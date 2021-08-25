using System;
using System.Data;
using System.Data.SqlClient;
using ToshibaPos.SDK;

namespace ToshibaPOS.DAL
{
    public static class FaturaClass
    {
        public static string Ruaj(FaturaBLL Fatura)
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
            SqlCommand cmdDaljaMallitDetaleInsert_spIns = new SqlCommand("TOSHIBA.POS_DaljaMallitDetaleInsert_sp", cnn, tran);
            cmdDaljaMallitDetaleInsert_spIns.CommandType = CommandType.StoredProcedure;
            SqlCommand cmdEkzekutimiPagesesInsert_spIns = new SqlCommand("TOSHIBA.POS_EkzekutimiPagesesInsert_sp", cnn, tran);
            cmdEkzekutimiPagesesInsert_spIns.CommandType = CommandType.StoredProcedure;
            SqlCommand cmdDaljaMallitInsert_spIns = new SqlCommand("TOSHIBA.POS_DaljaMallitInsert_sp", cnn, tran);
            cmdDaljaMallitInsert_spIns.CommandType = CommandType.StoredProcedure;


            cmdDaljaMallitDetaleInsert_spIns.Parameters.Add("@DaljaMallitID", SqlDbType.BigInt);
            cmdDaljaMallitDetaleInsert_spIns.Parameters["@DaljaMallitID"].Direction = ParameterDirection.Input;
            cmdDaljaMallitDetaleInsert_spIns.Parameters.Add("@NR", SqlDbType.SmallInt);
            cmdDaljaMallitDetaleInsert_spIns.Parameters["@NR"].Direction = ParameterDirection.Input;
            cmdDaljaMallitDetaleInsert_spIns.Parameters.Add("@OrganizataId", SqlDbType.Int);
            cmdDaljaMallitDetaleInsert_spIns.Parameters["@OrganizataId"].Direction = ParameterDirection.Input;
            cmdDaljaMallitDetaleInsert_spIns.Parameters.Add("@ArtikulliId", SqlDbType.Int);
            cmdDaljaMallitDetaleInsert_spIns.Parameters["@ArtikulliId"].Direction = ParameterDirection.Input;
            cmdDaljaMallitDetaleInsert_spIns.Parameters.Add("@NjesiaID", SqlDbType.TinyInt);
            cmdDaljaMallitDetaleInsert_spIns.Parameters["@NjesiaID"].Direction = ParameterDirection.Input;
            cmdDaljaMallitDetaleInsert_spIns.Parameters.Add("@Sasia", SqlDbType.Decimal);
            cmdDaljaMallitDetaleInsert_spIns.Parameters["@Sasia"].Direction = ParameterDirection.Input;
            cmdDaljaMallitDetaleInsert_spIns.Parameters.Add("@Tvsh", SqlDbType.Decimal);
            cmdDaljaMallitDetaleInsert_spIns.Parameters["@Tvsh"].Direction = ParameterDirection.Input;
            cmdDaljaMallitDetaleInsert_spIns.Parameters.Add("@QmimiShitjes", SqlDbType.Decimal);
            cmdDaljaMallitDetaleInsert_spIns.Parameters["@QmimiShitjes"].Direction = ParameterDirection.Input;
            cmdDaljaMallitDetaleInsert_spIns.Parameters.Add("@Rabati", SqlDbType.Decimal);
            cmdDaljaMallitDetaleInsert_spIns.Parameters["@Rabati"].Direction = ParameterDirection.Input;
            cmdDaljaMallitDetaleInsert_spIns.Parameters.Add("@EkstraRabati", SqlDbType.Decimal);
            cmdDaljaMallitDetaleInsert_spIns.Parameters["@EkstraRabati"].Direction = ParameterDirection.Input;
            cmdDaljaMallitDetaleInsert_spIns.Parameters.Add("@QmimiFinal", SqlDbType.Decimal);
            cmdDaljaMallitDetaleInsert_spIns.Parameters["@QmimiFinal"].Direction = ParameterDirection.Input;


            cmdEkzekutimiPagesesInsert_spIns.Parameters.Add("@MenyraEPagesesId", SqlDbType.Int);
            cmdEkzekutimiPagesesInsert_spIns.Parameters["@MenyraEPagesesId"].Direction = ParameterDirection.Input;
            cmdEkzekutimiPagesesInsert_spIns.Parameters.Add("@DaljaMallitID", SqlDbType.BigInt);
            cmdEkzekutimiPagesesInsert_spIns.Parameters["@DaljaMallitID"].Direction = ParameterDirection.Input;
            cmdEkzekutimiPagesesInsert_spIns.Parameters.Add("@ShifraOperatorit", SqlDbType.Int);
            cmdEkzekutimiPagesesInsert_spIns.Parameters["@ShifraOperatorit"].Direction = ParameterDirection.Input;
            cmdEkzekutimiPagesesInsert_spIns.Parameters.Add("@Vlera", SqlDbType.Decimal);
            cmdEkzekutimiPagesesInsert_spIns.Parameters["@Vlera"].Direction = ParameterDirection.Input;
            cmdEkzekutimiPagesesInsert_spIns.Parameters.Add("@Paguar", SqlDbType.Decimal);
            cmdEkzekutimiPagesesInsert_spIns.Parameters["@Paguar"].Direction = ParameterDirection.Input;
            cmdEkzekutimiPagesesInsert_spIns.Parameters.Add("@DhenjeKesh", SqlDbType.Decimal);
            cmdEkzekutimiPagesesInsert_spIns.Parameters["@DhenjeKesh"].Direction = ParameterDirection.Input;

            cmdDaljaMallitInsert_spIns.Parameters.Add("@OrganizataId", SqlDbType.Int);
            cmdDaljaMallitInsert_spIns.Parameters["@OrganizataId"].Direction = ParameterDirection.Input;
            cmdDaljaMallitInsert_spIns.Parameters.Add("@Viti", SqlDbType.Int);
            cmdDaljaMallitInsert_spIns.Parameters["@Viti"].Direction = ParameterDirection.Input;
            cmdDaljaMallitInsert_spIns.Parameters.Add("@Data", SqlDbType.Date);
            cmdDaljaMallitInsert_spIns.Parameters["@Data"].Direction = ParameterDirection.Input;
            cmdDaljaMallitInsert_spIns.Parameters.Add("@NrFatures", SqlDbType.Int);
            cmdDaljaMallitInsert_spIns.Parameters["@NrFatures"].Direction = ParameterDirection.Input;
            cmdDaljaMallitInsert_spIns.Parameters.Add("@DokumentiId", SqlDbType.Int);
            cmdDaljaMallitInsert_spIns.Parameters["@DokumentiId"].Direction = ParameterDirection.Input;
            cmdDaljaMallitInsert_spIns.Parameters.Add("@RegjistruarNga", SqlDbType.Int);
            cmdDaljaMallitInsert_spIns.Parameters["@RegjistruarNga"].Direction = ParameterDirection.Input;
            cmdDaljaMallitInsert_spIns.Parameters.Add("@NumriArkes", SqlDbType.Int);
            cmdDaljaMallitInsert_spIns.Parameters["@NumriArkes"].Direction = ParameterDirection.Input;
            cmdDaljaMallitInsert_spIns.Parameters.Add("@SubjektiId", SqlDbType.Int);
            cmdDaljaMallitInsert_spIns.Parameters["@SubjektiId"].Direction = ParameterDirection.Input;
            cmdDaljaMallitInsert_spIns.Parameters.Add("@IdLokal", SqlDbType.VarChar);
            cmdDaljaMallitInsert_spIns.Parameters["@IdLokal"].Direction = ParameterDirection.Input;
            cmdDaljaMallitInsert_spIns.Parameters.Add("@ZbritjeNgaOperatori", SqlDbType.Int);
            cmdDaljaMallitInsert_spIns.Parameters["@ZbritjeNgaOperatori"].Direction = ParameterDirection.Input;
            cmdDaljaMallitInsert_spIns.Parameters.Add("@Validuar", SqlDbType.Bit);
            cmdDaljaMallitInsert_spIns.Parameters["@Validuar"].Direction = ParameterDirection.Input;

            long idngabaza=-50;
            try
            {
                cmdDaljaMallitInsert_spIns.Parameters["@OrganizataId"].Value = Fatura.DaljaMallitCL.OrganizataId;
                cmdDaljaMallitInsert_spIns.Parameters["@Viti"].Value = Fatura.DaljaMallitCL.Viti;
                cmdDaljaMallitInsert_spIns.Parameters["@Data"].Value = Fatura.DaljaMallitCL.Data;
                cmdDaljaMallitInsert_spIns.Parameters["@NrFatures"].Value = Fatura.DaljaMallitCL.NrFatures;
                cmdDaljaMallitInsert_spIns.Parameters["@DokumentiId"].Value = Fatura.DaljaMallitCL.DokumentiId;
                cmdDaljaMallitInsert_spIns.Parameters["@NumriArkes"].Value = Fatura.DaljaMallitCL.NumriArkes;
                cmdDaljaMallitInsert_spIns.Parameters["@ZbritjeNgaOperatori"].Value = Fatura.DaljaMallitCL.ZbritjeNgaOperatori;
                cmdDaljaMallitInsert_spIns.Parameters["@SubjektiId"].Value = Fatura.DaljaMallitCL.SubjektiId;
                cmdDaljaMallitInsert_spIns.Parameters["@RegjistruarNga"].Value = Fatura.DaljaMallitCL.RegjistruarNga;
                cmdDaljaMallitInsert_spIns.Parameters["@Validuar"].Value = Fatura.DaljaMallitCL.Validuar;
                cmdDaljaMallitInsert_spIns.Parameters["@IdLokal"].Value = Fatura.DaljaMallitCL.IdLokal;
                idngabaza = Convert.ToInt64(cmdDaljaMallitInsert_spIns.ExecuteScalar());

                

                foreach (DataRow RowDetajet in Fatura.dtDaljaMallitDetale.Rows)
                {
                    RowDetajet["DaljaMallitID"] = idngabaza;
                    cmdDaljaMallitDetaleInsert_spIns.Parameters["@DaljaMallitID"].Value = RowDetajet["DaljaMallitID"]; ;
                    cmdDaljaMallitDetaleInsert_spIns.Parameters["@OrganizataId"].Value = DBNull.Value;
                    cmdDaljaMallitDetaleInsert_spIns.Parameters["@ArtikulliId"].Value = RowDetajet["ArtikulliId"];
                    cmdDaljaMallitDetaleInsert_spIns.Parameters["@NjesiaID"].Value = RowDetajet["NjesiaID"];
                    cmdDaljaMallitDetaleInsert_spIns.Parameters["@NR"].Value = RowDetajet["NR"];
                    cmdDaljaMallitDetaleInsert_spIns.Parameters["@Sasia"].Value = RowDetajet["Sasia"];
                    cmdDaljaMallitDetaleInsert_spIns.Parameters["@Tvsh"].Value = RowDetajet["Tvsh"];
                    cmdDaljaMallitDetaleInsert_spIns.Parameters["@QmimiShitjes"].Value = RowDetajet["QmimiShitjes"];
                    cmdDaljaMallitDetaleInsert_spIns.Parameters["@Rabati"].Value = RowDetajet["Rabati"];
                    cmdDaljaMallitDetaleInsert_spIns.Parameters["@EkstraRabati"].Value = RowDetajet["EkstraRabati"];
                    cmdDaljaMallitDetaleInsert_spIns.Parameters["@QmimiFinal"].Value = RowDetajet["QmimiFinal"];
                    RowDetajet["Id"] = Convert.ToInt32(cmdDaljaMallitDetaleInsert_spIns.ExecuteScalar());
                    RowDetajet.EndEdit();
                }

                foreach (DataRow RowPagesat in Fatura.dtEkzekutimiPageses.Rows)
                {
                    RowPagesat["DaljaMallitID"] = idngabaza;
                    cmdEkzekutimiPagesesInsert_spIns.Parameters["@Vlera"].Value = RowPagesat["Vlera"];
                    cmdEkzekutimiPagesesInsert_spIns.Parameters["@MenyraEPagesesId"].Value = RowPagesat["MenyraEPagesesId"];
                    cmdEkzekutimiPagesesInsert_spIns.Parameters["@DaljaMallitID"].Value = RowPagesat["DaljaMallitID"]; ;
                    cmdEkzekutimiPagesesInsert_spIns.Parameters["@ShifraOperatorit"].Value = RowPagesat["ShifraOperatorit"];
                    cmdEkzekutimiPagesesInsert_spIns.Parameters["@Paguar"].Value = RowPagesat["Paguar"];
                    cmdEkzekutimiPagesesInsert_spIns.Parameters["@DhenjeKesh"].Value = 0;
                    RowPagesat["Id"] = Convert.ToInt32(cmdEkzekutimiPagesesInsert_spIns.ExecuteScalar());
                }

                Fatura.dtEkzekutimiPageses.AcceptChanges();
                Fatura.dtDaljaMallitDetale.AcceptChanges();
                Fatura.dtDaljaMallit.AcceptChanges();
                Fatura.DaljaMallitCL.Id = idngabaza;
                tran.Commit();
                return "OK";
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
    }
}
