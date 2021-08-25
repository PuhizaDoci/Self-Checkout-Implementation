using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using ToshibaPos.SDK;

namespace ToshibaPOS.DAL
{
    public class ToshibaPosClass
    {
        public static long SinkronizoFleteDergesat(DataSet dsHeader, long id)
        {
            SqlConnection cnnServer = new SqlConnection(PublicClass.KoneksioniPrimar);
            SqlTransaction tranServer = default(SqlTransaction);

            SqlConnection cnnLokal = new SqlConnection(PublicClass.KoneksioniLokal);
            SqlTransaction tranLokal = default(SqlTransaction);

            try
            {
                cnnServer.Open();
                tranServer = cnnServer.BeginTransaction();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try
            {
                cnnLokal.Open();
                tranLokal = cnnLokal.BeginTransaction();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            SqlCommand cmdFleteDergesaInsert_spIns =
                new SqlCommand("TOSHIBA.FleteDergesaInsert_sp", cnnServer, tranServer);
            cmdFleteDergesaInsert_spIns.CommandType = CommandType.StoredProcedure;

            SqlCommand cmdFleteDergesaDetaleInsert_spIns =
                new SqlCommand("TOSHIBA.FleteDergesaDetaleInsert_sp", cnnServer, tranServer);
            cmdFleteDergesaDetaleInsert_spIns.CommandType = CommandType.StoredProcedure;

            SqlCommand cmdUpdateDaljaMallit =
                new SqlCommand("TOSHIBA.DaljaMallitDerguarNeServer_Sp", cnnLokal, tranLokal);
            cmdUpdateDaljaMallit.CommandType = CommandType.StoredProcedure;

            SqlCommand cmdDaljaMallitIdServerUpdate_spUpd =
                new SqlCommand("TOSHIBA.POS_DaljaMallitIdServerUpdate_sp", cnnLokal, tranLokal);
            cmdDaljaMallitIdServerUpdate_spUpd.CommandType = CommandType.StoredProcedure;

            cmdFleteDergesaInsert_spIns.Parameters.Add("@OrganizataId", SqlDbType.Int);
            cmdFleteDergesaInsert_spIns.Parameters["@OrganizataId"].Direction = ParameterDirection.Input;
            cmdFleteDergesaInsert_spIns.Parameters.Add("@Viti", SqlDbType.Int);
            cmdFleteDergesaInsert_spIns.Parameters["@Viti"].Direction = ParameterDirection.Input;
            cmdFleteDergesaInsert_spIns.Parameters.Add("@Data", SqlDbType.Date);
            cmdFleteDergesaInsert_spIns.Parameters["@Data"].Direction = ParameterDirection.Input;
            cmdFleteDergesaInsert_spIns.Parameters.Add("@NrDokumentit", SqlDbType.Int);
            cmdFleteDergesaInsert_spIns.Parameters["@NrDokumentit"].Direction = ParameterDirection.Input;
            cmdFleteDergesaInsert_spIns.Parameters.Add("@DokumentiId", SqlDbType.Int);
            cmdFleteDergesaInsert_spIns.Parameters["@DokumentiId"].Direction = ParameterDirection.Input;
            cmdFleteDergesaInsert_spIns.Parameters.Add("@RegjistruarNga", SqlDbType.Int);
            cmdFleteDergesaInsert_spIns.Parameters["@RegjistruarNga"].Direction = ParameterDirection.Input;
            cmdFleteDergesaInsert_spIns.Parameters.Add("@BleresiId", SqlDbType.Int);
            cmdFleteDergesaInsert_spIns.Parameters["@BleresiId"].Direction = ParameterDirection.Input;
            cmdFleteDergesaInsert_spIns.Parameters.Add("@DepartamentiId", SqlDbType.Int);
            cmdFleteDergesaInsert_spIns.Parameters["@DepartamentiId"].Direction = ParameterDirection.Input;
            cmdFleteDergesaInsert_spIns.Parameters.Add("@IdLokal", SqlDbType.VarChar);
            cmdFleteDergesaInsert_spIns.Parameters["@IdLokal"].Direction = ParameterDirection.Input;

            cmdFleteDergesaDetaleInsert_spIns.Parameters.Add("@FleteDergesaId", SqlDbType.BigInt);
            cmdFleteDergesaDetaleInsert_spIns.Parameters["@FleteDergesaId"].Direction = ParameterDirection.Input;
            cmdFleteDergesaDetaleInsert_spIns.Parameters.Add("@ArtikulliId", SqlDbType.Int);
            cmdFleteDergesaDetaleInsert_spIns.Parameters["@ArtikulliId"].Direction = ParameterDirection.Input;
            cmdFleteDergesaDetaleInsert_spIns.Parameters.Add("@NjesiaID", SqlDbType.TinyInt);
            cmdFleteDergesaDetaleInsert_spIns.Parameters["@NjesiaID"].Direction = ParameterDirection.Input;
            cmdFleteDergesaDetaleInsert_spIns.Parameters.Add("@NR", SqlDbType.SmallInt);
            cmdFleteDergesaDetaleInsert_spIns.Parameters["@NR"].Direction = ParameterDirection.Input;
            cmdFleteDergesaDetaleInsert_spIns.Parameters.Add("@Sasia", SqlDbType.Decimal);
            cmdFleteDergesaDetaleInsert_spIns.Parameters["@Sasia"].Direction = ParameterDirection.Input;
            cmdFleteDergesaDetaleInsert_spIns.Parameters.Add("@BazaFurnizuese", SqlDbType.Decimal);
            cmdFleteDergesaDetaleInsert_spIns.Parameters["@BazaFurnizuese"].Direction = ParameterDirection.Input;
            cmdFleteDergesaDetaleInsert_spIns.Parameters.Add("@Kostoja", SqlDbType.Decimal);
            cmdFleteDergesaDetaleInsert_spIns.Parameters["@Kostoja"].Direction = ParameterDirection.Input;
            cmdFleteDergesaDetaleInsert_spIns.Parameters.Add("@KostoMesatare", SqlDbType.Decimal);
            cmdFleteDergesaDetaleInsert_spIns.Parameters["@KostoMesatare"].Direction = ParameterDirection.Input;
            cmdFleteDergesaDetaleInsert_spIns.Parameters.Add("@Tvsh", SqlDbType.Decimal);
            cmdFleteDergesaDetaleInsert_spIns.Parameters["@Tvsh"].Direction = ParameterDirection.Input;
            cmdFleteDergesaDetaleInsert_spIns.Parameters.Add("@QmimiShitjes", SqlDbType.Decimal);
            cmdFleteDergesaDetaleInsert_spIns.Parameters["@QmimiShitjes"].Direction = ParameterDirection.Input;
            cmdFleteDergesaDetaleInsert_spIns.Parameters.Add("@QmimiRregullt", SqlDbType.Decimal);
            cmdFleteDergesaDetaleInsert_spIns.Parameters["@QmimiRregullt"].Direction = ParameterDirection.Input;
            cmdFleteDergesaDetaleInsert_spIns.Parameters.Add("@QmimiShumice", SqlDbType.Decimal);
            cmdFleteDergesaDetaleInsert_spIns.Parameters["@QmimiShumice"].Direction = ParameterDirection.Input;
            cmdFleteDergesaDetaleInsert_spIns.Parameters.Add("@Rabati", SqlDbType.Decimal);
            cmdFleteDergesaDetaleInsert_spIns.Parameters["@Rabati"].Direction = ParameterDirection.Input;
            cmdFleteDergesaDetaleInsert_spIns.Parameters.Add("@EkstraRabati", SqlDbType.Decimal);
            cmdFleteDergesaDetaleInsert_spIns.Parameters["@EkstraRabati"].Direction = ParameterDirection.Input;

            cmdUpdateDaljaMallit.Parameters.Add("@DaljaMallitId", SqlDbType.BigInt);
            cmdUpdateDaljaMallit.Parameters["@DaljaMallitId"].Direction = ParameterDirection.Input;

            cmdDaljaMallitIdServerUpdate_spUpd.Parameters.Add("@DaljaMallitIdServer", SqlDbType.BigInt);
            cmdDaljaMallitIdServerUpdate_spUpd.Parameters["@DaljaMallitIdServer"].Direction = ParameterDirection.Input;
            cmdDaljaMallitIdServerUpdate_spUpd.Parameters.Add("@DaljaMallitIdLokal", SqlDbType.BigInt);
            cmdDaljaMallitIdServerUpdate_spUpd.Parameters["@DaljaMallitIdLokal"].Direction = ParameterDirection.Input;

            DataTable dtShitjaPerUpdate = new DataTable();
            dtShitjaPerUpdate.Columns.Add("DaljaMallitId", typeof(long));

            try
            {
                foreach (DataRow dr in dsHeader.Tables["dtHeader"].Rows)
                {
                    cmdFleteDergesaInsert_spIns.Parameters["@OrganizataId"].Value = PublicClass.OrganizataId;
                    cmdFleteDergesaInsert_spIns.Parameters["@Viti"].Value = dr["Viti"];
                    cmdFleteDergesaInsert_spIns.Parameters["@Data"].Value = dr["Data"];
                    cmdFleteDergesaInsert_spIns.Parameters["@NrDokumentit"].Value = null;
                    cmdFleteDergesaInsert_spIns.Parameters["@DokumentiId"].Value = dr["DokumentiId"];
                    cmdFleteDergesaInsert_spIns.Parameters["@RegjistruarNga"].Value = PublicClass.OperatoriId;
                    cmdFleteDergesaInsert_spIns.Parameters["@BleresiId"].Value = dr["SubjektiId"];
                    cmdFleteDergesaInsert_spIns.Parameters["@DepartamentiId"].Value = 10;
                    cmdFleteDergesaInsert_spIns.Parameters["@IdLokal"].Value = dr["IdLokal"];
                    long idTemp = Convert.ToInt64(dr["Id"]);
                    id = Convert.ToInt64(cmdFleteDergesaInsert_spIns.ExecuteScalar());
                    dr["Id"] = id;
                    dtShitjaPerUpdate.Rows.Add(idTemp);
                }

                foreach (DataRow drDetale in dsHeader.Tables["dtDaljaDetalet"].Rows)
                {
                    cmdFleteDergesaDetaleInsert_spIns.Parameters["@FleteDergesaId"].Value = id;
                    cmdFleteDergesaDetaleInsert_spIns.Parameters["@ArtikulliId"].Value = drDetale["ArtikulliId"];
                    cmdFleteDergesaDetaleInsert_spIns.Parameters["@NjesiaID"].Value = drDetale["NjesiaID"];
                    cmdFleteDergesaDetaleInsert_spIns.Parameters["@NR"].Value = drDetale["NR"];
                    cmdFleteDergesaDetaleInsert_spIns.Parameters["@Sasia"].Value = drDetale["Sasia"];
                    cmdFleteDergesaDetaleInsert_spIns.Parameters["@BazaFurnizuese"].Value = 0;
                    cmdFleteDergesaDetaleInsert_spIns.Parameters["@Kostoja"].Value = 0;
                    cmdFleteDergesaDetaleInsert_spIns.Parameters["@KostoMesatare"].Value = 0;
                    cmdFleteDergesaDetaleInsert_spIns.Parameters["@Tvsh"].Value = drDetale["Tvsh"];
                    cmdFleteDergesaDetaleInsert_spIns.Parameters["@QmimiShitjes"].Value = drDetale["QmimiShitjes"];
                    cmdFleteDergesaDetaleInsert_spIns.Parameters["@QmimiRregullt"].Value =
                        drDetale["QmimiRregullt"].ToString() != "" ? drDetale["QmimiRregullt"] : 0;
                    cmdFleteDergesaDetaleInsert_spIns.Parameters["@QmimiShumice"].Value =
                        drDetale["QmimiShumices"].ToString() != "" ? drDetale["QmimiShumices"] : 0;
                    cmdFleteDergesaDetaleInsert_spIns.Parameters["@Rabati"].Value = drDetale["Rabati"];
                    cmdFleteDergesaDetaleInsert_spIns.Parameters["@EkstraRabati"].Value = drDetale["EkstraRabati"];
                    cmdFleteDergesaDetaleInsert_spIns.ExecuteNonQuery();
                }

                foreach (DataRow dr in dtShitjaPerUpdate.Rows)
                {
                    cmdUpdateDaljaMallit.Parameters["@DaljaMallitID"].Value = dr["DaljaMallitID"];
                    cmdUpdateDaljaMallit.ExecuteNonQuery();
                }

                foreach (DataRow dr in dtShitjaPerUpdate.Rows)
                {
                    cmdDaljaMallitIdServerUpdate_spUpd.Parameters["@DaljaMallitIdServer"].Value = id;
                    cmdDaljaMallitIdServerUpdate_spUpd.Parameters["@DaljaMallitIdLokal"].Value = dr["DaljaMallitId"];
                    cmdDaljaMallitIdServerUpdate_spUpd.ExecuteNonQuery();
                }

                tranServer.Commit();
                tranLokal.Commit();
            }
            catch (Exception ex)
            {
                tranServer.Rollback();
                tranLokal.Rollback();
                throw ex;
            }
            finally
            {
                cnnServer.Close();
                cnnLokal.Close();
            }

            return id;
        }

        public static DataTable GetDaljaMallitKontrolloVleratServer(
            DataTable tabela, long daljaMallitId, SqlConnection cnn, SqlTransaction tran)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_DaljaMallitKontrolloVleratSelect_sp", cnn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Transaction = tran;
            da.SelectCommand.Parameters.AddWithValue("@DaljaMallitId", daljaMallitId);
            da.Fill(tabela);
            return tabela;
        }

        public static int GetIdLokalServer(string idLokal,
            int organizataId, SqlConnection cnn, SqlTransaction tran)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.DaljaMallitCountIdLokalSelect_sp", cnn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Transaction = tran;
            da.SelectCommand.Parameters.AddWithValue("@IdLokal", idLokal);
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", organizataId);
            da.Fill(tabela);
            if (tabela.Rows.Count > 0)
                return Convert.ToInt32(tabela.Rows[0]["CountId"].ToString());
            else
                return 0;
        }


        public static void RuajServer(DataSet dsHeader, long id, string koneksioni)
        {
            SqlConnectionStringBuilder sbs = new SqlConnectionStringBuilder(koneksioni);
            sbs.ConnectTimeout = 13;
            SqlConnection cnn = new SqlConnection(sbs.ConnectionString);
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
            SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder(PublicClass.KoneksioniLokal);
            sb.ConnectTimeout = 5;
            SqlConnection cnnLokal = new SqlConnection(sb.ConnectionString);
            SqlTransaction tran2 = default(SqlTransaction);
            try
            {
                cnnLokal.Open();
                tran2 = cnnLokal.BeginTransaction();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            try
            {
                long DaljaMallitIdLokal = Convert.ToInt64(dsHeader.Tables["dtHeader"].Rows[0]["Id"]);
                long DaljaMallitIdServer = Sinkronizo(dsHeader, id, cnn, cnnLokal, tran, tran2);
                DaljaMallitIdServer = Convert.ToInt64(dsHeader.Tables["dtHeader"].Rows[0]["Id"]);

                SqlCommand cmdDaljaMallitIdServerUpdateUpdate_spUpd = new SqlCommand("TOSHIBA.POS_DaljaMallitIdServerUpdate_sp", cnnLokal, tran2);
                cmdDaljaMallitIdServerUpdateUpdate_spUpd.CommandType = CommandType.StoredProcedure;
                cmdDaljaMallitIdServerUpdateUpdate_spUpd.Parameters.Add("@DaljaMallitIdServer", SqlDbType.BigInt);
                cmdDaljaMallitIdServerUpdateUpdate_spUpd.Parameters["@DaljaMallitIdServer"].Direction = ParameterDirection.Input;
                cmdDaljaMallitIdServerUpdateUpdate_spUpd.Parameters.Add("@DaljaMallitIdLokal", SqlDbType.BigInt);
                cmdDaljaMallitIdServerUpdateUpdate_spUpd.Parameters["@DaljaMallitIdLokal"].Direction = ParameterDirection.Input;

                foreach (DataRow Row in dsHeader.Tables["dtHeader"].Rows)
                {
                    cmdDaljaMallitIdServerUpdateUpdate_spUpd.Parameters["@DaljaMallitIdServer"].Value = DaljaMallitIdServer;
                    cmdDaljaMallitIdServerUpdateUpdate_spUpd.Parameters["@DaljaMallitIdLokal"].Value = DaljaMallitIdLokal;
                    cmdDaljaMallitIdServerUpdateUpdate_spUpd.ExecuteNonQuery();
                }

                DataTable dtServer = GetDaljaMallitKontrolloVleratServer(new DataTable(), DaljaMallitIdServer, cnn, tran);
                DataTable dtLokal = GetDaljaMallitKontrolloVleratLokal(new DataTable(), DaljaMallitIdLokal, cnnLokal, tran2);

                decimal.TryParse(dtServer.Compute("Sum(Vlera)", "Tab='DD'").ToString(),
                    out decimal vleraDDServer);
                decimal.TryParse(dtServer.Compute("Sum(Vlera)", "Tab='EK'").ToString(),
                    out decimal vleraEKServer);

                decimal.TryParse(dtLokal.Compute("Sum(Vlera)", "Tab='DD'").ToString(),
                    out decimal vleraDDLokal);
                decimal.TryParse(dtLokal.Compute("Sum(Vlera)", "Tab='EK'").ToString(),
                    out decimal vleraEKLokal);

                int countIdLokal = GetIdLokalServer(dsHeader.Tables["dtHeader"]
                    .Rows[0]["IdLokal"].ToString(), PublicClass.OrganizataId, cnn, tran);

                if (countIdLokal > 1)
                    throw new Exception("Transaksioni me IDLokal te njejtë egziston ne server !");

                if (vleraDDServer == vleraDDLokal && vleraEKServer == vleraEKLokal)
                {
                    tran.Commit();
                    tran2.Commit();
                }
                else
                {
                    throw new Exception("Të dhënat në server nuk janë dërguar," +
                        " vlerat nuk janë të barabarta lokalisht me server!");
                }
            }
            catch (Exception ex)
            {
                tran.Rollback();
                tran2.Rollback();
                throw ex;
            }
            finally
            {
                cnn.Close();
                cnnLokal.Close();
            }
        }

        public static DataTable GetDaljaMallitKontrolloVleratLokal(
            DataTable tabela, long daljaMallitId, SqlConnection cnn, SqlTransaction tran)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_DaljaMallitKontrolloVleratSelect_sp", cnn);
            da.SelectCommand.Transaction = tran;
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DaljaMallitId", daljaMallitId);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetArtikujtPaZbritje(DataTable tabela)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_ArtikujtPaZbritjeSelect_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Tipi", "POS");
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetZbritjenMeKupon(DataTable tabela, string kodiKuponit)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POSKuponatPerZbritjeSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@KodiKuponit", kodiKuponit);
            da.SelectCommand.Parameters.AddWithValue("@Aplikuar", false);
            da.SelectCommand.Parameters.AddWithValue("@OrganizataID", PublicClass.OrganizataId);
            da.Fill(tabela);
            return tabela;
        }

        public static DateTime GetDataFunditSinkronizimit()
        {
            DateTime DataKohaSinkronizimitParaprak;
            SqlDataAdapter daPerGjenerimTeDbLokal = new SqlDataAdapter("TOSHIBA.POS_LokalDBKohaSinkronizimitMaxSelect_Sp", PublicClass.KoneksioniLokal);
            daPerGjenerimTeDbLokal.SelectCommand.CommandType = CommandType.StoredProcedure;
            daPerGjenerimTeDbLokal.SelectCommand.Connection.Open();
            var obj = daPerGjenerimTeDbLokal.SelectCommand.ExecuteScalar();
            if (obj != DBNull.Value)
            {
                DataKohaSinkronizimitParaprak = (DateTime)obj;
            }
            else
            {
                DataKohaSinkronizimitParaprak = PublicClass.DataNgaServeri;
            }
            daPerGjenerimTeDbLokal.SelectCommand.Connection.Close();
            return DataKohaSinkronizimitParaprak;
        }

        public static DataTable GetPOSZbritjenMeKupon(DataTable tabela)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_ZbritjaMeKuponSelect_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@POSKuponatPerZbritjeId", 0);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetArkat()
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_ArkatSelect_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", PublicClass.OrganizataId);
            da.SelectCommand.Parameters.AddWithValue("@NrArkes", PublicClass.Arka.NrArkes);
            da.Fill(tabela);
            return tabela;
        }

        [Obsolete("Kjo metode do te largohet ne versionet e ardhshme. Perdorni kete metod te FiscalPrinterModule/DB")]
        public static string MerrTXTFiskal(long daljaMallitId)
        {

            SqlConnection cnn = new SqlConnection(PublicClass.Koneksioni);

            try
            {
                cnn.Open();

                SqlCommand cmd = new SqlCommand("TOSHIBA.POS_GjenerimiKuponitFiskal_Sp", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DaljaMallitId", daljaMallitId);

                return cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                cnn.Close();
            }

            return "";

        }

        //POS_GjenerimiKomandaveFiskale_Sp
        [Obsolete("Kjo metode do te largohet ne versionet e ardhshme." +
            " Perdorni kete metod te FiscalPrinterModule/DB")]
        public static string MerKomandenFiskale(string kerkesa, int OperatoriId)
        {

            SqlConnection cnn = new SqlConnection(PublicClass.Koneksioni);

            try
            {
                cnn.Open();
                string a = "";
                SqlCommand cmd = new SqlCommand("TOSHIBA.POS_GjenerimiKomandaveFiskale_Sp", cnn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Kerkesa", kerkesa);
                cmd.Parameters.AddWithValue("@OperatoriId", OperatoriId);
                a = cmd.ExecuteScalar().ToString();
                return a;
            }
            catch
            {
            }
            finally
            {
                cnn.Close();
            }

            return "";
        }


        public static DataTable GetCCPFaturatSelect_Sp(DataTable tabela, int id)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_CCPFaturatSelect_Sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Id", id);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetQarkulliminEOperatoreve(DataTable tabela)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_QarkullimiSipasOperatoritDetale_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", PublicClass.OrganizataId);
            da.SelectCommand.Parameters.AddWithValue("@ShifraOperatorit", PublicClass.ShifraEOperatorit);
            da.SelectCommand.Parameters.AddWithValue("@DataPrej", DateTime.Now);
            da.SelectCommand.Parameters.AddWithValue("@DataDeri", DateTime.Now);
            da.Fill(tabela);
            return tabela;
        }


        /// <summary>
        /// Kthen tabelen vetem nese ka rreshta!!
        /// </summary>
        /// <param name="tabela">Tabela qe do te mbushet</param>
        /// <param name="kodi">Nr karteles apo kodi</param>
        /// <param name="passi">fjalekalimi</param>
        /// <returns>Kthen tabelen vetem nese ka rreshta!!</returns>
        public static DataTable GetBleresitMeKartele(DataTable tabela, string kodi)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_CCPKompaniteSelect_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add(new SqlParameter("@KodiKarteles", kodi));
            da.Fill(tabela);

            return tabela.Rows.Count > 0 ? tabela : null;
        }

        public static int GetQasjeNeZbritje(string passi)
        {
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_ZbritjaNeArka_Sp", PublicClass.Koneksioni);
            try
            {
                da.SelectCommand.Parameters.Add("@Pass", SqlDbType.VarChar, 50);
                da.SelectCommand.Parameters["@Pass"].Value = passi;
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Connection.Open();
                int vlera = Convert.ToInt32(da.SelectCommand.ExecuteScalar());
                da.SelectCommand.Connection.Close();
                return vlera;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetDaljetLokale(DataTable tabela)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_DaljaMallitSelect_sp", PublicClass.KoneksioniLokal);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Sinkronizuar", false);
            da.SelectCommand.Parameters.AddWithValue("@Validuar", true);
            da.Fill(tabela);
            return tabela;
        }

        public static long AEshteEgzistonFaturaNeServer(string IdLokal, int organizataid)
        {
            string b = PublicClass.KoneksioniPrimar.ToString();
            SqlConnection cnnServerKryesore = new SqlConnection(b);
            try
            {
                cnnServerKryesore.Open();
                string a = "";
                SqlCommand cmd = new SqlCommand("TOSHIBA.DaljaMallitVerifikoFaturenLokale_Sp", cnnServerKryesore);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdLokal", IdLokal);
                cmd.Parameters.AddWithValue("@organizataid", organizataid);
                return Convert.ToInt64(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnnServerKryesore.Close();
            }
        }

        public static bool GetKonfigurimin(int id)
        {
            SqlConnection cnn = new SqlConnection(PublicClass.Koneksioni);
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_KonfigurimetGetId_Sp", cnn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@ID", id);
            bool i = false;
            try
            {
                cnn.Close();
                cnn.Open();
                i = Convert.ToBoolean(da.SelectCommand.ExecuteScalar());
            }
            catch (Exception ex)
            {
                cnn.Close();
                return i;
                throw ex;
            }
            finally
            {
                cnn.Close();

            }
            return i;
        }

        public static DataTable GetDaljetDetalet(DataTable tabela)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_DaljaMallitDetaleSelect_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Id", -1);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetDaljetDetaletLokale(DataTable tabela, string daljamallitid)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_DaljaMallitDetaleSelect_sp", PublicClass.KoneksioniLokal);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DaljaMallitId", daljamallitid);
            da.Fill(tabela);
            return tabela;
        }


        public static DataTable GetPOSZbritjaMeKuponLokale(DataTable tabela, string daljamallitid)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_ZbritjaMeKuponSelect_sp", PublicClass.KoneksioniLokal);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DaljaMallitId", daljamallitid);
            da.Fill(tabela);
            return tabela;
        }



        public static DataTable GetDaljetDetaletHistori(DataTable tabela)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter(
                "TOSHIBA.POS_DaljaMallitDetaleHistoriSelect_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.Fill(tabela);
            return tabela;
        }


        public static DataTable GetDaljetDetaletHistoriLokale(DataTable tabela, string daljamallitid)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter(
                "TOSHIBA.POS_DaljaMallitDetaleHistoriSelect_sp", PublicClass.KoneksioniLokal);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DaljaMallitId", daljamallitid);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetPOSArtikullin(string shifra)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_ArtikujtSelect_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Shifra", shifra);
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", PublicClass.OrganizataId);
            da.Fill(tabela);
            return tabela;
        }
        public static DataTable GetKthimiMallitArsyet(DataTable dt, int? @Id = null)
        {
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.KthimiMallitArsyetSelect_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Id", @Id);
            da.Fill(dt);
            return dt;
        }

        public static DataTable GetMenyratEPageses(DataTable tabela)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_MenyratEPagesesSelect_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@ParaqitetNePos", true);
            da.Fill(tabela);
            return tabela;
        }
        public static DataTable GetdtMenyratEPageses(
            DataTable dtMenyratEPageses = null, int? Id = null,
            string Shkurtesa = null, string Pershkrimi = null,
            bool? ParaqitetNePos = null)
        {
            if (dtMenyratEPageses == null)
                dtMenyratEPageses = new DataTable();

            dtMenyratEPageses.Clear();

            SqlDataAdapter da = new SqlDataAdapter(
                "TOSHIBA.POS_MenyratEPagesesSelect_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (Id > 0) da.SelectCommand.Parameters.AddWithValue("@Id", Id);
            da.SelectCommand.Parameters.AddWithValue("@Shkurtesa", Shkurtesa);
            da.SelectCommand.Parameters.AddWithValue("@Pershkrimi", Pershkrimi);
            da.SelectCommand.Parameters.AddWithValue("@ParaqitetNePos", ParaqitetNePos);
            da.Fill(dtMenyratEPageses);

            return dtMenyratEPageses;
        }
        public static DataTable GetLlojetEDokumenteve(
            DataTable dtLlojetEDokumenteve = null, int? Id = null,
            string Shkurtesa = null, string Pershkrimi = null,
            int? PrindiID = null)
        {
            if (dtLlojetEDokumenteve == null)
                dtLlojetEDokumenteve = new DataTable();

            dtLlojetEDokumenteve.Clear(); SqlDataAdapter da =
                new SqlDataAdapter("TOSHIBA.LlojetEDokumenteveSelect_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (Id > 0) da.SelectCommand.Parameters.AddWithValue("@Id", Id);
            da.SelectCommand.Parameters.AddWithValue("@Shkurtesa", Shkurtesa);
            da.SelectCommand.Parameters.AddWithValue("@Pershkrimi", Pershkrimi);
            da.SelectCommand.Parameters.AddWithValue("@PrindiID", PrindiID);
            da.Fill(dtLlojetEDokumenteve);

            return dtLlojetEDokumenteve;
        }
        public static DataTable GetEkzekutimiPageses(long DaljaMallitId, bool kerkoNeServer)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da;
            if (kerkoNeServer)
                da = new SqlDataAdapter("TOSHIBA.POS_EkzekutimiPagesesSelect_sp",
                    PublicClass.KoneksioniPrimar);
            else
                da = new SqlDataAdapter("TOSHIBA.POS_EkzekutimiPagesesSelect_sp",
                    PublicClass.KoneksioniLokal);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DaljaMallitId", DaljaMallitId);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetEkzekutimetEPagesesLokale(DataTable tabela, string daljamallitid)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_EkzekutimiPagesesSelect_sp",
                PublicClass.KoneksioniLokal);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DaljaMallitID", daljamallitid);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.Fill(tabela);
            return tabela;
        }


        public static int VitiNgaServeri
        {
            get
            {
                DataTable tabela = new DataTable();
                SqlConnection cnn = new SqlConnection(PublicClass.Koneksioni);

                SqlDataAdapter da = new SqlDataAdapter("Select GetDate() Data", PublicClass.Koneksioni);
                da.Fill(tabela);
                DateTime data = Convert.ToDateTime(tabela.Rows[0]["Data"]);
                return data.Year;
            }
        }

        public static DateTime DataNeServer
        {
            get
            {
                DataTable tabela = new DataTable();
                SqlConnection cnn = new SqlConnection(PublicClass.Koneksioni);

                SqlDataAdapter da = new SqlDataAdapter("Select GetDate() Data", PublicClass.Koneksioni);
                da.Fill(tabela);
                DateTime data = Convert.ToDateTime(tabela.Rows[0]["Data"]);
                return data;
            }
        }

        public static string GetPikat(long daljaMallitId)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("dbo.POS_KalkulimiPikave_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DaljaMallitId", daljaMallitId);
            da.Fill(tabela);

            if (tabela.Rows.Count > 0)
                return tabela.Rows[0]["Pershkrimi"].ToString();
            else
                return "";
        }

        public static void UpdateLokal(long DaljaMallitIdServer, long DaljaMallitIdLokal)
        {
            string b = PublicClass.KoneksioniLokal.ToString();
            SqlConnection cnnLokal = new SqlConnection(b);
            try
            {
                cnnLokal.Open();
                string a = "";
                SqlCommand cmdDaljaMallitIdServerUpdateUpdate_spUpd =
                    new SqlCommand("TOSHIBA.POS_DaljaMallitIdServerUpdate_sp", cnnLokal);
                cmdDaljaMallitIdServerUpdateUpdate_spUpd.CommandType = CommandType.StoredProcedure;
                cmdDaljaMallitIdServerUpdateUpdate_spUpd.Parameters.AddWithValue("@DaljaMallitIdServer", DaljaMallitIdServer);
                cmdDaljaMallitIdServerUpdateUpdate_spUpd.Parameters.AddWithValue("@DaljaMallitIdLokal", DaljaMallitIdLokal);
                cmdDaljaMallitIdServerUpdateUpdate_spUpd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnnLokal.Close();
            }
        }
        public static void RuajRreshtiFshire(DataRow dr)
        {
            var connection = new SqlConnection(PublicClass.Arka.KoneksioniLokal);
            var cmd = new SqlCommand("TOSHIBA.POS_DaljaMallitDetaleHistoriInsert_sp", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            var a = dr["DaljaMallitId"];
            cmd.Parameters.AddWithValue("@DaljaMallitId", dr["DaljaMallitId"]);
            cmd.Parameters.AddWithValue("@OrganizataId", dr["OrganizataId"]);
            cmd.Parameters.AddWithValue("@ArtikulliId", dr["ArtikulliId"]);
            cmd.Parameters.AddWithValue("@NjesiaID", dr["NjesiaID"]);
            cmd.Parameters.AddWithValue("@NR", dr["NR"]);
            cmd.Parameters.AddWithValue("@Sasia", dr["Sasia"]);
            cmd.Parameters.AddWithValue("@Tvsh", dr["Tvsh"]);
            cmd.Parameters.AddWithValue("@QmimiShitjes", dr["QmimiShitjes"]);
            cmd.Parameters.AddWithValue("@Rabati", dr["Rabati"]);
            cmd.Parameters.AddWithValue("@EkstraRabati", dr["EkstraRabati"]);
            cmd.Parameters.AddWithValue("@QmimiFinal", dr["QmimiFinal"]);
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
        public static long Ruaj(DataSet dsHeader, long Id,
            SqlConnection cnn, SqlTransaction tran, int? kujtesaOrderId = null)
        {
            SqlCommand cmdDaljaMallitInsert_spIns =
                new SqlCommand("TOSHIBA.POS_DaljaMallitInsert_sp", cnn, tran);
            cmdDaljaMallitInsert_spIns.CommandType = CommandType.StoredProcedure;

            SqlCommand cmdDaljaMallitDetaleInsert_spIns =
                new SqlCommand("TOSHIBA.POS_DaljaMallitDetaleInsert_sp", cnn, tran);
            cmdDaljaMallitDetaleInsert_spIns.CommandType = CommandType.StoredProcedure;

            SqlCommand cmdEkzekutimiPagesesInsert_spIns =
                new SqlCommand("TOSHIBA.POS_EkzekutimiPagesesInsert_sp", cnn, tran);
            cmdEkzekutimiPagesesInsert_spIns.CommandType = CommandType.StoredProcedure;


            SqlCommand cmdPOSZbritjaMeKuponInsert_spIns =
                new SqlCommand("TOSHIBA.POS_ZbritjaMeKuponInsert_sp", cnn, tran);
            cmdPOSZbritjaMeKuponInsert_spIns.CommandType = CommandType.StoredProcedure;

            SqlCommand cmdCCPFaturatInsert_spIns =
                new SqlCommand("TOSHIBA.POS_CCPFaturatInsert_sp", cnn, tran);
            cmdCCPFaturatInsert_spIns.CommandType = CommandType.StoredProcedure;

            SqlCommand cmdBleresitMeKartelaDetaleInsert_SpIns =
                new SqlCommand("TOSHIBA.POS_BleresitMeKartelaDetaleInsert_Sp", cnn, tran);
            cmdBleresitMeKartelaDetaleInsert_SpIns.CommandType = CommandType.StoredProcedure;

            SqlCommand cmdVerifikimiVlerave =
                new SqlCommand("TOSHIBA.POS_DaljaMallitVerifikoVlerat_sp", cnn, tran);
            cmdVerifikimiVlerave.CommandType = CommandType.StoredProcedure;

            SqlCommand cmdBarazimiArkatareveFilloNderrimin =
                new SqlCommand("TOSHIBA.POS_BarazimiArkatareveFilloNderrimin_sp", cnn, tran);
            cmdBarazimiArkatareveFilloNderrimin.CommandType = CommandType.StoredProcedure;

            SqlCommand cmdDaljaMallitDetaleAktivitetetInsert_spIns =
                new SqlCommand("TOSHIBA.DaljaMallitDetaleAktivitetetInsert_sp", cnn, tran);
            cmdDaljaMallitDetaleAktivitetetInsert_spIns.CommandType = CommandType.StoredProcedure;

            SqlCommand cmdKuponatInsert_spIns =
                new SqlCommand("TOSHIBA.KuponatInsert_sp", cnn, tran);
            cmdKuponatInsert_spIns.CommandType = CommandType.StoredProcedure;

            SqlCommand cmdDaljaMallitVaucheratInsert_spIns =
                new SqlCommand("TOSHIBA.DaljaMallitVaucheratInsert_Sp", cnn, tran);
            cmdDaljaMallitVaucheratInsert_spIns.CommandType = CommandType.StoredProcedure;

            SqlCommand cmdDaljaMallitKonto =
                new SqlCommand("TOSHIBA.POS_DaljaMallitKontimiGjenero_sp", cnn, tran);
            cmdDaljaMallitKonto.CommandType = CommandType.StoredProcedure;

            ///////////////////////////////////////////////

            //////////////////////////////////////////

            cmdBleresitMeKartelaDetaleInsert_SpIns.Parameters.Add("@SubjektiId", SqlDbType.Int);
            cmdBleresitMeKartelaDetaleInsert_SpIns.Parameters["@SubjektiId"].Direction = ParameterDirection.Input;
            cmdBleresitMeKartelaDetaleInsert_SpIns.Parameters.Add("@BleresitMeKartelaId", SqlDbType.Int);
            cmdBleresitMeKartelaDetaleInsert_SpIns.Parameters["@BleresitMeKartelaId"].Direction = ParameterDirection.Input;
            cmdBleresitMeKartelaDetaleInsert_SpIns.Parameters.Add("@DaljaMallitId", SqlDbType.BigInt);
            cmdBleresitMeKartelaDetaleInsert_SpIns.Parameters["@DaljaMallitId"].Direction = ParameterDirection.Input;

            cmdPOSZbritjaMeKuponInsert_spIns.Parameters.Add("@POSKuponatPerZbritjeId", SqlDbType.Int);
            cmdPOSZbritjaMeKuponInsert_spIns.Parameters["@POSKuponatPerZbritjeId"].Direction = ParameterDirection.Input;
            cmdPOSZbritjaMeKuponInsert_spIns.Parameters.Add("@Vlera", SqlDbType.Decimal);
            cmdPOSZbritjaMeKuponInsert_spIns.Parameters["@Vlera"].Direction = ParameterDirection.Input;
            cmdPOSZbritjaMeKuponInsert_spIns.Parameters.Add("@DaljaMallitId", SqlDbType.BigInt);
            cmdPOSZbritjaMeKuponInsert_spIns.Parameters["@DaljaMallitId"].Direction = ParameterDirection.Input;
            cmdPOSZbritjaMeKuponInsert_spIns.Parameters.Add("@KodiKuponit", SqlDbType.VarChar);
            cmdPOSZbritjaMeKuponInsert_spIns.Parameters["@KodiKuponit"].Direction = ParameterDirection.Input;

            cmdDaljaMallitDetaleInsert_spIns.Parameters.Add("@DaljaMallitId", SqlDbType.BigInt);
            cmdDaljaMallitDetaleInsert_spIns.Parameters["@DaljaMallitId"].Direction = ParameterDirection.Input;
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
            cmdDaljaMallitDetaleInsert_spIns.Parameters.Add("@AplikimiVoucherit", SqlDbType.Bit);
            cmdDaljaMallitDetaleInsert_spIns.Parameters["@AplikimiVoucherit"].Direction = ParameterDirection.Input;
            cmdDaljaMallitDetaleInsert_spIns.Parameters.Add("@KujtesaOrderId", SqlDbType.Int);
            cmdDaljaMallitDetaleInsert_spIns.Parameters["@KujtesaOrderId"].Direction = ParameterDirection.Input;
            cmdDaljaMallitDetaleInsert_spIns.Parameters.Add("@Barkodi", SqlDbType.VarChar);
            cmdDaljaMallitDetaleInsert_spIns.Parameters["@Barkodi"].Direction = ParameterDirection.Input;

            cmdEkzekutimiPagesesInsert_spIns.Parameters.Add("@MenyraEPagesesId", SqlDbType.Int);
            cmdEkzekutimiPagesesInsert_spIns.Parameters["@MenyraEPagesesId"].Direction = ParameterDirection.Input;
            cmdEkzekutimiPagesesInsert_spIns.Parameters.Add("@DaljaMallitId", SqlDbType.BigInt);
            cmdEkzekutimiPagesesInsert_spIns.Parameters["@DaljaMallitId"].Direction = ParameterDirection.Input;
            cmdEkzekutimiPagesesInsert_spIns.Parameters.Add("@ShifraOperatorit", SqlDbType.Int);
            cmdEkzekutimiPagesesInsert_spIns.Parameters["@ShifraOperatorit"].Direction = ParameterDirection.Input;
            cmdEkzekutimiPagesesInsert_spIns.Parameters.Add("@Vlera", SqlDbType.Decimal);
            cmdEkzekutimiPagesesInsert_spIns.Parameters["@Vlera"].Direction = ParameterDirection.Input;
            cmdEkzekutimiPagesesInsert_spIns.Parameters.Add("@Paguar", SqlDbType.Decimal);
            cmdEkzekutimiPagesesInsert_spIns.Parameters["@Paguar"].Direction = ParameterDirection.Input;
            cmdEkzekutimiPagesesInsert_spIns.Parameters.Add("@DhenjeKesh", SqlDbType.Decimal);
            cmdEkzekutimiPagesesInsert_spIns.Parameters["@DhenjeKesh"].Direction = ParameterDirection.Input;
            cmdEkzekutimiPagesesInsert_spIns.Parameters.Add("@ValutaId", SqlDbType.Int);
            cmdEkzekutimiPagesesInsert_spIns.Parameters["@ValutaId"].Direction = ParameterDirection.Input;
            cmdEkzekutimiPagesesInsert_spIns.Parameters.Add("@Kursi", SqlDbType.Decimal);
            cmdEkzekutimiPagesesInsert_spIns.Parameters["@Kursi"].Direction = ParameterDirection.Input;
            cmdEkzekutimiPagesesInsert_spIns.Parameters.Add("@KodiVoucherit", SqlDbType.VarChar);
            cmdEkzekutimiPagesesInsert_spIns.Parameters["@KodiVoucherit"].Direction = ParameterDirection.Input;
            cmdEkzekutimiPagesesInsert_spIns.Parameters.Add("@LlojiVoucherit", SqlDbType.VarChar);
            cmdEkzekutimiPagesesInsert_spIns.Parameters["@LlojiVoucherit"].Direction = ParameterDirection.Input;

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
            cmdDaljaMallitInsert_spIns.Parameters.Add("@NumriATK", SqlDbType.VarChar);
            cmdDaljaMallitInsert_spIns.Parameters["@NumriATK"].Direction = ParameterDirection.Input;
            cmdDaljaMallitInsert_spIns.Parameters.Add("@NumriArkesGK", SqlDbType.VarChar);
            cmdDaljaMallitInsert_spIns.Parameters["@NumriArkesGK"].Direction = ParameterDirection.Input;
            cmdDaljaMallitInsert_spIns.Parameters.Add("@Personi", SqlDbType.VarChar);
            cmdDaljaMallitInsert_spIns.Parameters["@Personi"].Direction = ParameterDirection.Input;
            cmdDaljaMallitInsert_spIns.Parameters.Add("@NumriPersonal", SqlDbType.VarChar);
            cmdDaljaMallitInsert_spIns.Parameters["@NumriPersonal"].Direction = ParameterDirection.Input;
            cmdDaljaMallitInsert_spIns.Parameters.Add("@Adresa", SqlDbType.VarChar);
            cmdDaljaMallitInsert_spIns.Parameters["@Adresa"].Direction = ParameterDirection.Input;
            cmdDaljaMallitInsert_spIns.Parameters.Add("@NrTel", SqlDbType.VarChar);
            cmdDaljaMallitInsert_spIns.Parameters["@NrTel"].Direction = ParameterDirection.Input;
            cmdDaljaMallitInsert_spIns.Parameters.Add("@Koment", SqlDbType.VarChar);
            cmdDaljaMallitInsert_spIns.Parameters["@Koment"].Direction = ParameterDirection.Input;
            cmdDaljaMallitInsert_spIns.Parameters.Add("@DaljaMallitKorrektuarId", SqlDbType.BigInt);
            cmdDaljaMallitInsert_spIns.Parameters["@DaljaMallitKorrektuarId"].Direction = ParameterDirection.Input;
            cmdDaljaMallitInsert_spIns.Parameters.Add("@AplikacioniId", SqlDbType.Int);
            cmdDaljaMallitInsert_spIns.Parameters["@AplikacioniId"].Direction = ParameterDirection.Input;
            cmdDaljaMallitInsert_spIns.Parameters.Add("@KthimiMallitArsyejaId", SqlDbType.Int);
            cmdDaljaMallitInsert_spIns.Parameters["@KthimiMallitArsyejaId"].Direction = ParameterDirection.Input;

            cmdCCPFaturatInsert_spIns.Parameters.Add("@CCPKompaniaId", SqlDbType.Int);
            cmdCCPFaturatInsert_spIns.Parameters["@CCPKompaniaId"].Direction = ParameterDirection.Input;
            cmdCCPFaturatInsert_spIns.Parameters.Add("@DaljaMallitID", SqlDbType.BigInt);
            cmdCCPFaturatInsert_spIns.Parameters["@DaljaMallitID"].Direction = ParameterDirection.Input;
            cmdCCPFaturatInsert_spIns.Parameters.Add("@RegjistruarNga", SqlDbType.Int);
            cmdCCPFaturatInsert_spIns.Parameters["@RegjistruarNga"].Direction = ParameterDirection.Input;

            cmdDaljaMallitKonto.Parameters.Add("@DaljaMallitId", SqlDbType.BigInt);
            cmdDaljaMallitKonto.Parameters["@DaljaMallitId"].Direction = ParameterDirection.Input;

            cmdVerifikimiVlerave.Parameters.Add("@DaljaMallitId", SqlDbType.BigInt);
            cmdVerifikimiVlerave.Parameters["@DaljaMallitId"].Direction = ParameterDirection.Input;

            cmdBarazimiArkatareveFilloNderrimin.Parameters.Add("@OrganizataId", SqlDbType.Int);
            cmdBarazimiArkatareveFilloNderrimin.Parameters["@OrganizataId"].Direction = ParameterDirection.Input;
            cmdBarazimiArkatareveFilloNderrimin.Parameters.Add("@OperatoriId", SqlDbType.Int);
            cmdBarazimiArkatareveFilloNderrimin.Parameters["@OperatoriId"].Direction = ParameterDirection.Input;
            cmdBarazimiArkatareveFilloNderrimin.Parameters.Add("@LlojiDokumentitId", SqlDbType.Int);
            cmdBarazimiArkatareveFilloNderrimin.Parameters["@LlojiDokumentitId"].Direction = ParameterDirection.Input;
            cmdBarazimiArkatareveFilloNderrimin.Parameters.Add("@Komenti", SqlDbType.VarChar);
            cmdBarazimiArkatareveFilloNderrimin.Parameters["@Komenti"].Direction = ParameterDirection.Input;
            cmdBarazimiArkatareveFilloNderrimin.Parameters.Add("@BarazuarNga", SqlDbType.Int);
            cmdBarazimiArkatareveFilloNderrimin.Parameters["@BarazuarNga"].Direction = ParameterDirection.Input;
            cmdBarazimiArkatareveFilloNderrimin.Parameters.Add("@GjendjaFillestare", SqlDbType.Decimal);
            cmdBarazimiArkatareveFilloNderrimin.Parameters["@GjendjaFillestare"].Direction = ParameterDirection.Input;

            cmdDaljaMallitDetaleAktivitetetInsert_spIns.Parameters.Add("@DaljaMallitDetaleId", SqlDbType.Int);
            cmdDaljaMallitDetaleAktivitetetInsert_spIns.Parameters["@DaljaMallitDetaleId"].Direction = ParameterDirection.Input;
            cmdDaljaMallitDetaleAktivitetetInsert_spIns.Parameters.Add("@AktivitetiId", SqlDbType.BigInt);
            cmdDaljaMallitDetaleAktivitetetInsert_spIns.Parameters["@AktivitetiId"].Direction = ParameterDirection.Input;
            cmdDaljaMallitDetaleAktivitetetInsert_spIns.Parameters.Add("@Zbritja", SqlDbType.Decimal);
            cmdDaljaMallitDetaleAktivitetetInsert_spIns.Parameters["@Zbritja"].Direction = ParameterDirection.Input;
            cmdDaljaMallitDetaleAktivitetetInsert_spIns.Parameters.Add("@DaljaMallitId", SqlDbType.BigInt);
            cmdDaljaMallitDetaleAktivitetetInsert_spIns.Parameters["@DaljaMallitId"].Direction = ParameterDirection.Input;

            cmdKuponatInsert_spIns.Parameters.Add("@KuponatPerZbritjeId", SqlDbType.Int);
            cmdKuponatInsert_spIns.Parameters["@KuponatPerZbritjeId"].Direction = ParameterDirection.Input;
            cmdKuponatInsert_spIns.Parameters.Add("@KodiKuponit", SqlDbType.VarChar);
            cmdKuponatInsert_spIns.Parameters["@KodiKuponit"].Direction = ParameterDirection.Input;
            cmdKuponatInsert_spIns.Parameters.Add("@DaljaMallitIdGjeneruar", SqlDbType.BigInt);
            cmdKuponatInsert_spIns.Parameters["@DaljaMallitIdGjeneruar"].Direction = ParameterDirection.Input;

            cmdDaljaMallitVaucheratInsert_spIns.Parameters.Add("@DaljaMallitID", SqlDbType.BigInt);
            cmdDaljaMallitVaucheratInsert_spIns.Parameters["@DaljaMallitID"].Direction = ParameterDirection.Input;
            cmdDaljaMallitVaucheratInsert_spIns.Parameters.Add("@Vlera", SqlDbType.Decimal);
            cmdDaljaMallitVaucheratInsert_spIns.Parameters["@Vlera"].Direction = ParameterDirection.Input;
            cmdDaljaMallitVaucheratInsert_spIns.Parameters.Add("@KodiVaucherit", SqlDbType.VarChar);
            cmdDaljaMallitVaucheratInsert_spIns.Parameters["@KodiVaucherit"].Direction = ParameterDirection.Input;
            cmdDaljaMallitVaucheratInsert_spIns.Parameters.Add("@Emri", SqlDbType.VarChar);
            cmdDaljaMallitVaucheratInsert_spIns.Parameters["@Emri"].Direction = ParameterDirection.Input;
            cmdDaljaMallitVaucheratInsert_spIns.Parameters.Add("@Mbiemri", SqlDbType.VarChar);
            cmdDaljaMallitVaucheratInsert_spIns.Parameters["@Mbiemri"].Direction = ParameterDirection.Input;
            cmdDaljaMallitVaucheratInsert_spIns.Parameters.Add("@Lloji", SqlDbType.VarChar);
            cmdDaljaMallitVaucheratInsert_spIns.Parameters["@Lloji"].Direction = ParameterDirection.Input;
            cmdDaljaMallitVaucheratInsert_spIns.Parameters.Add("@KuponatPerZbritjeLlojetEZbritjesId", SqlDbType.Int);
            cmdDaljaMallitVaucheratInsert_spIns.Parameters["@KuponatPerZbritjeLlojetEZbritjesId"].Direction = ParameterDirection.Input;

            DataTable dtShitjaPerFshirje = new DataTable();
            dtShitjaPerFshirje.Columns.Add("DaljaMallitId", typeof(long));

            foreach (DataRow Row in dsHeader.Tables["dtHeader"].Rows)
            {
                cmdDaljaMallitInsert_spIns.Parameters["@OrganizataId"].Value = Row["OrganizataId"];
                cmdDaljaMallitInsert_spIns.Parameters["@Viti"].Value = VitiNgaServeri;
                cmdDaljaMallitInsert_spIns.Parameters["@Data"].Value = DataNeServer;
                cmdDaljaMallitInsert_spIns.Parameters["@NrFatures"].Value = Row["NrFatures"];
                cmdDaljaMallitInsert_spIns.Parameters["@DokumentiId"].Value = Row["DokumentiId"];
                cmdDaljaMallitInsert_spIns.Parameters["@NumriArkes"].Value = Row["NumriArkes"];
                cmdDaljaMallitInsert_spIns.Parameters["@ZbritjeNgaOperatori"].Value = Row["ZbritjeNgaOperatori"];
                cmdDaljaMallitInsert_spIns.Parameters["@SubjektiId"].Value = Row["SubjektiId"];
                cmdDaljaMallitInsert_spIns.Parameters["@RegjistruarNga"].Value = Row["RegjistruarNga"];
                cmdDaljaMallitInsert_spIns.Parameters["@Validuar"].Value = true;
                cmdDaljaMallitInsert_spIns.Parameters["@NumriATK"].Value = Row["NumriATK"];
                cmdDaljaMallitInsert_spIns.Parameters["@NumriArkesGK"].Value = Row["NumriArkesGK"];
                cmdDaljaMallitInsert_spIns.Parameters["@IdLokal"].Value = Row["IdLokal"];
                cmdDaljaMallitInsert_spIns.Parameters["@Personi"].Value = Row["Personi"];
                cmdDaljaMallitInsert_spIns.Parameters["@Adresa"].Value = Row["Adresa"];
                cmdDaljaMallitInsert_spIns.Parameters["@NumriPersonal"].Value = Row["NumriPersonal"];
                cmdDaljaMallitInsert_spIns.Parameters["@NrTel"].Value = Row["NrTel"];
                cmdDaljaMallitInsert_spIns.Parameters["@Koment"].Value = Row["Koment"];
                cmdDaljaMallitInsert_spIns.Parameters["@DaljaMallitKorrektuarId"].Value = Row["DaljaMallitKorrektuarId"];
                cmdDaljaMallitInsert_spIns.Parameters["@AplikacioniId"].Value = PublicClass.AplikacioniId;
                cmdDaljaMallitInsert_spIns.Parameters["@KthimiMallitArsyejaId"].Value = Row["KthimiMallitArsyejaId"];

                long idTemp = Convert.ToInt64(Row["Id"]);
                Id = Convert.ToInt64(cmdDaljaMallitInsert_spIns.ExecuteScalar());

                //-----------------------------------------
                DataTable dtInsert = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_DaljaMallitSelect_sp", cnn);
                da.SelectCommand.Transaction = tran;
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Id", Id);
                da.Fill(dtInsert);
                if (dtInsert.Rows.Count > 0)
                {
                    foreach (DataColumn column in dtInsert.Columns)
                    {
                        if (Row.Table.Columns.Contains(column.ColumnName))
                            Row[column.ColumnName] = dtInsert.Rows[0][column.ColumnName];
                    }
                }
                //-----------------------------
                Row["Id"] = Id;
                dtShitjaPerFshirje.Rows.Add(idTemp);
            }

            foreach (DataRow RowDetajet in dsHeader.Tables["dtDaljaDetalet"].Rows)
            {
                cmdDaljaMallitDetaleInsert_spIns.Parameters["@DaljaMallitId"].Value = RowDetajet["DaljaMallitId"];
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
                cmdDaljaMallitDetaleInsert_spIns.Parameters["@AplikimiVoucherit"].Value = RowDetajet["AplikimiVoucherit"];
                cmdDaljaMallitDetaleInsert_spIns.Parameters["@KujtesaOrderId"].Value = RowDetajet["KujtesaOrderId"];
                cmdDaljaMallitDetaleInsert_spIns.Parameters["@Barkodi"].Value = RowDetajet["Barkodi"];
                RowDetajet["Id"] = Convert.ToInt32(cmdDaljaMallitDetaleInsert_spIns.ExecuteScalar());
                RowDetajet.EndEdit();
            }

            foreach (DataRow RowPagesat in dsHeader.Tables["dtEkzekutimetEPageses"].Rows)
            {
                cmdEkzekutimiPagesesInsert_spIns.Parameters["@Vlera"].Value = RowPagesat["Vlera"];
                cmdEkzekutimiPagesesInsert_spIns.Parameters["@MenyraEPagesesId"].Value = RowPagesat["MenyraEPagesesId"];
                cmdEkzekutimiPagesesInsert_spIns.Parameters["@DaljaMallitId"].Value = RowPagesat["DaljaMallitId"];
                cmdEkzekutimiPagesesInsert_spIns.Parameters["@ShifraOperatorit"].Value = RowPagesat["ShifraOperatorit"];
                cmdEkzekutimiPagesesInsert_spIns.Parameters["@Paguar"].Value = RowPagesat["Paguar"];
                cmdEkzekutimiPagesesInsert_spIns.Parameters["@DhenjeKesh"].Value = RowPagesat["DhenjeKesh"];
                cmdEkzekutimiPagesesInsert_spIns.Parameters["@ValutaId"].Value = RowPagesat["ValutaId"];
                cmdEkzekutimiPagesesInsert_spIns.Parameters["@Kursi"].Value = RowPagesat["Kursi"];
                cmdEkzekutimiPagesesInsert_spIns.Parameters["@KodiVoucherit"].Value = RowPagesat["KodiVoucherit"];
                cmdEkzekutimiPagesesInsert_spIns.Parameters["@LlojiVoucherit"].Value = RowPagesat["LlojiVoucherit"];
                cmdEkzekutimiPagesesInsert_spIns.ExecuteNonQuery();
            }

            foreach (DataRow RowCCP in dsHeader.Tables["dtCCPFatura"].Rows)
            {
                cmdCCPFaturatInsert_spIns.Parameters["@DaljaMallitID"].Value = RowCCP["DaljaMallitID"];
                cmdCCPFaturatInsert_spIns.Parameters["@CCPKompaniaId"].Value = RowCCP["CCPKompaniaId"];
                cmdCCPFaturatInsert_spIns.Parameters["@RegjistruarNga"].Value = RowCCP["RegjistruarNga"];
                cmdCCPFaturatInsert_spIns.ExecuteNonQuery();
            }

            foreach (DataRow dr in dsHeader.Tables["dtPOSZbritjaMeKupon"].Rows)
            {
                cmdPOSZbritjaMeKuponInsert_spIns.Parameters["@DaljaMallitID"].Value = dr["DaljaMallitId"];
                cmdPOSZbritjaMeKuponInsert_spIns.Parameters["@Vlera"].Value = dr["Vlera"];
                cmdPOSZbritjaMeKuponInsert_spIns.Parameters["@KodiKuponit"].Value = dr["KodiKuponit"];
                cmdPOSZbritjaMeKuponInsert_spIns.Parameters["@POSKuponatPerZbritjeId"].Value = dr["POSKuponatPerZbritjeId"];
                cmdPOSZbritjaMeKuponInsert_spIns.ExecuteNonQuery();

                SqlConnection cnnServer = new SqlConnection(PublicClass.KoneksioniPrimar);

                SqlCommand cmdPOSKuponiMeZbritjeApliko_spIns = new SqlCommand("TOSHIBA.POSKuponiMeZbritjeApliko_sp", cnnServer);
                cmdPOSKuponiMeZbritjeApliko_spIns.CommandType = CommandType.StoredProcedure;

                cmdPOSKuponiMeZbritjeApliko_spIns.Parameters.Add("@POSKuponatPerZbritjeId", SqlDbType.Int);
                cmdPOSKuponiMeZbritjeApliko_spIns.Parameters["@POSKuponatPerZbritjeId"].Direction = ParameterDirection.Input;
                cmdPOSKuponiMeZbritjeApliko_spIns.Parameters.Add("@KodiKuponit", SqlDbType.VarChar);
                cmdPOSKuponiMeZbritjeApliko_spIns.Parameters["@KodiKuponit"].Direction = ParameterDirection.Input;

                cmdPOSKuponiMeZbritjeApliko_spIns.Parameters["@POSKuponatPerZbritjeId"].Value = dr["POSKuponatPerZbritjeId"];
                cmdPOSKuponiMeZbritjeApliko_spIns.Parameters["@KodiKuponit"].Value = dr["KodiKuponit"];

                cnnServer.Close();
                cnnServer.Open();
                cmdPOSKuponiMeZbritjeApliko_spIns.ExecuteNonQuery();
            }

            foreach (DataRow rowDaljaMallitDetaleAktivitetet in dsHeader.Tables["dtDaljaMallitDetaleAktivitetet"].Rows)
            {
                cmdDaljaMallitDetaleAktivitetetInsert_spIns.Parameters["@DaljaMallitDetaleId"].Value
                    = rowDaljaMallitDetaleAktivitetet["DaljaMallitDetaleId"];
                cmdDaljaMallitDetaleAktivitetetInsert_spIns.Parameters["@AktivitetiId"].Value
                    = rowDaljaMallitDetaleAktivitetet["AktivitetiId"];
                cmdDaljaMallitDetaleAktivitetetInsert_spIns.Parameters["@Zbritja"].Value
                    = rowDaljaMallitDetaleAktivitetet["Zbritja"];
                cmdDaljaMallitDetaleAktivitetetInsert_spIns.Parameters["@DaljaMallitId"].Value
                    = rowDaljaMallitDetaleAktivitetet["DaljaMallitId"];
                cmdDaljaMallitDetaleAktivitetetInsert_spIns.ExecuteNonQuery();
            }

            foreach (DataRow Row in dsHeader.Tables["dtHeader"].Rows)
            {
                cmdVerifikimiVlerave.Parameters["@DaljaMallitID"].Value = Row["Id"];
                cmdVerifikimiVlerave.ExecuteNonQuery();
            }

            foreach (DataRow drKuponat in dsHeader.Tables["dtKodetEKuponave"].Rows)
            {
                cmdKuponatInsert_spIns.Parameters["@KuponatPerZbritjeId"].Value = drKuponat["KuponatPerZbritjeId"];
                cmdKuponatInsert_spIns.Parameters["@KodiKuponit"].Value = drKuponat["KodiKuponit"];
                cmdKuponatInsert_spIns.Parameters["@DaljaMallitIdGjeneruar"].Value = Id;
                cmdKuponatInsert_spIns.ExecuteNonQuery();
            }

            foreach (DataRow drVoucherat in dsHeader.Tables["dtDaljaMallitVaucherat"].Rows)
            {
                cmdDaljaMallitVaucheratInsert_spIns.Parameters["@DaljaMallitID"].Value = Id;
                cmdDaljaMallitVaucheratInsert_spIns.Parameters["@Vlera"].Value = drVoucherat["Vlera"];
                cmdDaljaMallitVaucheratInsert_spIns.Parameters["@KodiVaucherit"].Value = drVoucherat["KodiVaucherit"];
                cmdDaljaMallitVaucheratInsert_spIns.Parameters["@Emri"].Value = drVoucherat["Emri"];
                cmdDaljaMallitVaucheratInsert_spIns.Parameters["@Mbiemri"].Value = drVoucherat["Mbiemri"];
                cmdDaljaMallitVaucheratInsert_spIns.Parameters["@Lloji"].Value = drVoucherat["Lloji"];
                cmdDaljaMallitVaucheratInsert_spIns.Parameters["@KuponatPerZbritjeLlojetEZbritjesId"].Value =
                    drVoucherat["KuponatPerZbritjeLlojetEZbritjesId"];
                cmdDaljaMallitVaucheratInsert_spIns.ExecuteNonQuery();
            }

            return Id;
        }

        public static long Sinkronizo(DataSet dsHeader, long Id,
            SqlConnection cnnServer, SqlConnection cnnLokal,
            SqlTransaction tranServer, SqlTransaction tranLokal)
        {
            SqlCommand cmdDaljaMallitInsert_spIns = new SqlCommand(
                "TOSHIBA.DaljaMallitInsertSinkronizimi_sp", cnnServer, tranServer);
            cmdDaljaMallitInsert_spIns.CommandType = CommandType.StoredProcedure;

            SqlCommand cmdDaljaMallitDetaleInsert_spIns = new SqlCommand(
                "TOSHIBA.POS_DaljaMallitDetaleInsert_sp", cnnServer, tranServer);
            cmdDaljaMallitDetaleInsert_spIns.CommandType = CommandType.StoredProcedure;

            SqlCommand cmdEkzekutimiPagesesInsert_spIns = new SqlCommand(
                "TOSHIBA.POS_EkzekutimiPagesesInsert_sp", cnnServer, tranServer);
            cmdEkzekutimiPagesesInsert_spIns.CommandType = CommandType.StoredProcedure;

            SqlCommand cmdDaljaMallitDetaleHistoriInsert_spIns = new SqlCommand(
                "TOSHIBA.POS_DaljaMallitDetaleHistoriInsert_sp", cnnServer, tranServer);
            cmdDaljaMallitDetaleHistoriInsert_spIns.CommandType = CommandType.StoredProcedure;

            SqlCommand cmdPOSKuponiMeZbritjeApliko_spIns = new SqlCommand(
                "TOSHIBA.POSKuponiMeZbritjeApliko_sp", cnnServer, tranServer);
            cmdPOSKuponiMeZbritjeApliko_spIns.CommandType = CommandType.StoredProcedure;

            SqlCommand cmdPOS_ZbritjaMeKuponInsert_spIns = new SqlCommand(
                "TOSHIBA.POS_ZbritjaMeKuponInsert_sp", cnnServer, tranServer);
            cmdPOS_ZbritjaMeKuponInsert_spIns.CommandType = CommandType.StoredProcedure;

            SqlCommand cmdCCPFaturatInsert_spIns = new SqlCommand(
                "TOSHIBA.POS_CCPFaturatInsert_sp", cnnServer, tranServer);
            cmdCCPFaturatInsert_spIns.CommandType = CommandType.StoredProcedure;

            SqlCommand cmdBleresitMeKartelaDetaleInsert_SpIns = new SqlCommand(
                "TOSHIBA.POS_BleresitMeKartelaDetaleInsert_Sp", cnnServer, tranServer);
            cmdBleresitMeKartelaDetaleInsert_SpIns.CommandType = CommandType.StoredProcedure;

            SqlCommand cmdBarazimiArkatareveFilloNderrimin = new SqlCommand(
                "TOSHIBA.POS_BarazimiArkatareveFilloNderrimin_sp", cnnServer, tranServer);
            cmdBarazimiArkatareveFilloNderrimin.CommandType = CommandType.StoredProcedure;

            SqlCommand cmdDaljaMallitDetaleAktivitetetInsert_spIns = new SqlCommand(
                "TOSHIBA.DaljaMallitDetaleAktivitetetInsert_sp", cnnServer, tranServer);
            cmdDaljaMallitDetaleAktivitetetInsert_spIns.CommandType = CommandType.StoredProcedure;

            SqlCommand cmdKuponatInsert_spIns = new SqlCommand(
                "TOSHIBA.KuponatUpdateInsert_sp", cnnServer, tranServer);
            cmdKuponatInsert_spIns.CommandType = CommandType.StoredProcedure;

            SqlCommand DaljaMallitVaucheratInsert_SpIns = new SqlCommand(
                "TOSHIBA.DaljaMallitVaucheratInsert_Sp", cnnServer, tranServer);
            DaljaMallitVaucheratInsert_SpIns.CommandType = CommandType.StoredProcedure;

            SqlCommand cmdDaljaMallitKonto = new SqlCommand(
                "TOSHIBA.POS_DaljaMallitKontimiGjenero_sp", cnnServer, tranServer);
            cmdDaljaMallitKonto.CommandType = CommandType.StoredProcedure;

            ///////////////////////////////////////////////

            //////////////////////////////////////////

            cmdBleresitMeKartelaDetaleInsert_SpIns.Parameters.Add("@SubjektiId", SqlDbType.Int);
            cmdBleresitMeKartelaDetaleInsert_SpIns.Parameters["@SubjektiId"].Direction = ParameterDirection.Input;
            cmdBleresitMeKartelaDetaleInsert_SpIns.Parameters.Add("@BleresitMeKartelaId", SqlDbType.Int);
            cmdBleresitMeKartelaDetaleInsert_SpIns.Parameters["@BleresitMeKartelaId"].Direction = ParameterDirection.Input;
            cmdBleresitMeKartelaDetaleInsert_SpIns.Parameters.Add("@DaljaMallitId", SqlDbType.BigInt);
            cmdBleresitMeKartelaDetaleInsert_SpIns.Parameters["@DaljaMallitId"].Direction = ParameterDirection.Input;

            cmdPOS_ZbritjaMeKuponInsert_spIns.Parameters.Add("@POSKuponatPerZbritjeId", SqlDbType.Int);
            cmdPOS_ZbritjaMeKuponInsert_spIns.Parameters["@POSKuponatPerZbritjeId"].Direction = ParameterDirection.Input;
            cmdPOS_ZbritjaMeKuponInsert_spIns.Parameters.Add("@Vlera", SqlDbType.Decimal);
            cmdPOS_ZbritjaMeKuponInsert_spIns.Parameters["@Vlera"].Direction = ParameterDirection.Input;
            cmdPOS_ZbritjaMeKuponInsert_spIns.Parameters.Add("@DaljaMallitId", SqlDbType.BigInt);
            cmdPOS_ZbritjaMeKuponInsert_spIns.Parameters["@DaljaMallitId"].Direction = ParameterDirection.Input;

            cmdDaljaMallitDetaleInsert_spIns.Parameters.Add("@DaljaMallitId", SqlDbType.BigInt);
            cmdDaljaMallitDetaleInsert_spIns.Parameters["@DaljaMallitId"].Direction = ParameterDirection.Input;
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
            cmdDaljaMallitDetaleInsert_spIns.Parameters.Add("@AplikimiVoucherit", SqlDbType.Bit);
            cmdDaljaMallitDetaleInsert_spIns.Parameters["@AplikimiVoucherit"].Direction = ParameterDirection.Input;
            cmdDaljaMallitDetaleInsert_spIns.Parameters.Add("@Barkodi", SqlDbType.VarChar);
            cmdDaljaMallitDetaleInsert_spIns.Parameters["@Barkodi"].Direction = ParameterDirection.Input;

            cmdDaljaMallitDetaleHistoriInsert_spIns.Parameters.Add("@DaljaMallitId", SqlDbType.BigInt);
            cmdDaljaMallitDetaleHistoriInsert_spIns.Parameters["@DaljaMallitId"].Direction = ParameterDirection.Input;
            cmdDaljaMallitDetaleHistoriInsert_spIns.Parameters.Add("@NR", SqlDbType.SmallInt);
            cmdDaljaMallitDetaleHistoriInsert_spIns.Parameters["@NR"].Direction = ParameterDirection.Input;
            cmdDaljaMallitDetaleHistoriInsert_spIns.Parameters.Add("@OrganizataId", SqlDbType.Int);
            cmdDaljaMallitDetaleHistoriInsert_spIns.Parameters["@OrganizataId"].Direction = ParameterDirection.Input;
            cmdDaljaMallitDetaleHistoriInsert_spIns.Parameters.Add("@ArtikulliId", SqlDbType.Int);
            cmdDaljaMallitDetaleHistoriInsert_spIns.Parameters["@ArtikulliId"].Direction = ParameterDirection.Input;
            cmdDaljaMallitDetaleHistoriInsert_spIns.Parameters.Add("@NjesiaID", SqlDbType.TinyInt);
            cmdDaljaMallitDetaleHistoriInsert_spIns.Parameters["@NjesiaID"].Direction = ParameterDirection.Input;
            cmdDaljaMallitDetaleHistoriInsert_spIns.Parameters.Add("@Sasia", SqlDbType.Decimal);
            cmdDaljaMallitDetaleHistoriInsert_spIns.Parameters["@Sasia"].Direction = ParameterDirection.Input;
            cmdDaljaMallitDetaleHistoriInsert_spIns.Parameters.Add("@Tvsh", SqlDbType.Decimal);
            cmdDaljaMallitDetaleHistoriInsert_spIns.Parameters["@Tvsh"].Direction = ParameterDirection.Input;
            cmdDaljaMallitDetaleHistoriInsert_spIns.Parameters.Add("@QmimiShitjes", SqlDbType.Decimal);
            cmdDaljaMallitDetaleHistoriInsert_spIns.Parameters["@QmimiShitjes"].Direction = ParameterDirection.Input;
            cmdDaljaMallitDetaleHistoriInsert_spIns.Parameters.Add("@Rabati", SqlDbType.Decimal);
            cmdDaljaMallitDetaleHistoriInsert_spIns.Parameters["@Rabati"].Direction = ParameterDirection.Input;
            cmdDaljaMallitDetaleHistoriInsert_spIns.Parameters.Add("@EkstraRabati", SqlDbType.Decimal);
            cmdDaljaMallitDetaleHistoriInsert_spIns.Parameters["@EkstraRabati"].Direction = ParameterDirection.Input;
            cmdDaljaMallitDetaleHistoriInsert_spIns.Parameters.Add("@QmimiFinal", SqlDbType.Decimal);
            cmdDaljaMallitDetaleHistoriInsert_spIns.Parameters["@QmimiFinal"].Direction = ParameterDirection.Input;

            cmdEkzekutimiPagesesInsert_spIns.Parameters.Add("@MenyraEPagesesId", SqlDbType.Int);
            cmdEkzekutimiPagesesInsert_spIns.Parameters["@MenyraEPagesesId"].Direction = ParameterDirection.Input;
            cmdEkzekutimiPagesesInsert_spIns.Parameters.Add("@DaljaMallitId", SqlDbType.BigInt);
            cmdEkzekutimiPagesesInsert_spIns.Parameters["@DaljaMallitId"].Direction = ParameterDirection.Input;
            cmdEkzekutimiPagesesInsert_spIns.Parameters.Add("@ShifraOperatorit", SqlDbType.Int);
            cmdEkzekutimiPagesesInsert_spIns.Parameters["@ShifraOperatorit"].Direction = ParameterDirection.Input;
            cmdEkzekutimiPagesesInsert_spIns.Parameters.Add("@Vlera", SqlDbType.Decimal);
            cmdEkzekutimiPagesesInsert_spIns.Parameters["@Vlera"].Direction = ParameterDirection.Input;
            cmdEkzekutimiPagesesInsert_spIns.Parameters.Add("@Paguar", SqlDbType.Decimal);
            cmdEkzekutimiPagesesInsert_spIns.Parameters["@Paguar"].Direction = ParameterDirection.Input;
            cmdEkzekutimiPagesesInsert_spIns.Parameters.Add("@DhenjeKesh", SqlDbType.Decimal);
            cmdEkzekutimiPagesesInsert_spIns.Parameters["@DhenjeKesh"].Direction = ParameterDirection.Input;
            cmdEkzekutimiPagesesInsert_spIns.Parameters.Add("@ValutaId", SqlDbType.Int);
            cmdEkzekutimiPagesesInsert_spIns.Parameters["@ValutaId"].Direction = ParameterDirection.Input;
            cmdEkzekutimiPagesesInsert_spIns.Parameters.Add("@Kursi", SqlDbType.Decimal);
            cmdEkzekutimiPagesesInsert_spIns.Parameters["@Kursi"].Direction = ParameterDirection.Input;
            cmdEkzekutimiPagesesInsert_spIns.Parameters.Add("@KodiVoucherit", SqlDbType.VarChar);
            cmdEkzekutimiPagesesInsert_spIns.Parameters["@KodiVoucherit"].Direction = ParameterDirection.Input;
            cmdEkzekutimiPagesesInsert_spIns.Parameters.Add("@LlojiVoucherit", SqlDbType.VarChar);
            cmdEkzekutimiPagesesInsert_spIns.Parameters["@LlojiVoucherit"].Direction = ParameterDirection.Input;

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
            cmdDaljaMallitInsert_spIns.Parameters.Add("@DataERegjistrimit", SqlDbType.DateTime);
            cmdDaljaMallitInsert_spIns.Parameters["@DataERegjistrimit"].Direction = ParameterDirection.Input;
            cmdDaljaMallitInsert_spIns.Parameters.Add("@TavolinaId", SqlDbType.Int);
            cmdDaljaMallitInsert_spIns.Parameters["@TavolinaId"].Direction = ParameterDirection.Input;
            cmdDaljaMallitInsert_spIns.Parameters.Add("@Validuar", SqlDbType.Bit);
            cmdDaljaMallitInsert_spIns.Parameters["@Validuar"].Direction = ParameterDirection.Input;
            cmdDaljaMallitInsert_spIns.Parameters.Add("@NumriATK", SqlDbType.VarChar);
            cmdDaljaMallitInsert_spIns.Parameters["@NumriATK"].Direction = ParameterDirection.Input;
            cmdDaljaMallitInsert_spIns.Parameters.Add("@NumriArkesGK", SqlDbType.VarChar);
            cmdDaljaMallitInsert_spIns.Parameters["@NumriArkesGK"].Direction = ParameterDirection.Input;
            cmdDaljaMallitInsert_spIns.Parameters.Add("@NumriFaturesManual", SqlDbType.VarChar);
            cmdDaljaMallitInsert_spIns.Parameters["@NumriFaturesManual"].Direction = ParameterDirection.Input;
            cmdDaljaMallitInsert_spIns.Parameters.Add("@DaljaMallitKorrektuarId", SqlDbType.BigInt);
            cmdDaljaMallitInsert_spIns.Parameters["@DaljaMallitKorrektuarId"].Direction = ParameterDirection.Input;
            cmdDaljaMallitInsert_spIns.Parameters.Add("@AplikacioniId", SqlDbType.Int);
            cmdDaljaMallitInsert_spIns.Parameters["@AplikacioniId"].Direction = ParameterDirection.Input;
            cmdDaljaMallitInsert_spIns.Parameters.Add("@KthimiMallitArsyejaId", SqlDbType.Int);
            cmdDaljaMallitInsert_spIns.Parameters["@KthimiMallitArsyejaId"].Direction = ParameterDirection.Input;
            cmdDaljaMallitInsert_spIns.Parameters.Add("@Personi", SqlDbType.VarChar);
            cmdDaljaMallitInsert_spIns.Parameters["@Personi"].Direction = ParameterDirection.Input;
            cmdDaljaMallitInsert_spIns.Parameters.Add("@Adresa", SqlDbType.VarChar);
            cmdDaljaMallitInsert_spIns.Parameters["@Adresa"].Direction = ParameterDirection.Input;
            cmdDaljaMallitInsert_spIns.Parameters.Add("@NumriPersonal", SqlDbType.VarChar);
            cmdDaljaMallitInsert_spIns.Parameters["@NumriPersonal"].Direction = ParameterDirection.Input;
            cmdDaljaMallitInsert_spIns.Parameters.Add("@NrTel", SqlDbType.VarChar);
            cmdDaljaMallitInsert_spIns.Parameters["@NrTel"].Direction = ParameterDirection.Input;
            cmdDaljaMallitInsert_spIns.Parameters.Add("@KuponiFiskalShtypur", SqlDbType.Bit);
            cmdDaljaMallitInsert_spIns.Parameters["@KuponiFiskalShtypur"].Direction = ParameterDirection.Input;

            cmdCCPFaturatInsert_spIns.Parameters.Add("@CCPKompaniaId", SqlDbType.Int);
            cmdCCPFaturatInsert_spIns.Parameters["@CCPKompaniaId"].Direction = ParameterDirection.Input;
            cmdCCPFaturatInsert_spIns.Parameters.Add("@DaljaMallitID", SqlDbType.BigInt);
            cmdCCPFaturatInsert_spIns.Parameters["@DaljaMallitID"].Direction = ParameterDirection.Input;
            cmdCCPFaturatInsert_spIns.Parameters.Add("@RegjistruarNga", SqlDbType.Int);
            cmdCCPFaturatInsert_spIns.Parameters["@RegjistruarNga"].Direction = ParameterDirection.Input;

            cmdDaljaMallitKonto.Parameters.Add("@DaljaMallitId", SqlDbType.BigInt);
            cmdDaljaMallitKonto.Parameters["@DaljaMallitId"].Direction = ParameterDirection.Input;

            cmdPOSKuponiMeZbritjeApliko_spIns.Parameters.Add("@POSKuponatPerZbritjeId", SqlDbType.Int);
            cmdPOSKuponiMeZbritjeApliko_spIns.Parameters["@POSKuponatPerZbritjeId"].Direction = ParameterDirection.Input;

            cmdBarazimiArkatareveFilloNderrimin.Parameters.Add("@OrganizataId", SqlDbType.Int);
            cmdBarazimiArkatareveFilloNderrimin.Parameters["@OrganizataId"].Direction = ParameterDirection.Input;
            cmdBarazimiArkatareveFilloNderrimin.Parameters.Add("@OperatoriId", SqlDbType.Int);
            cmdBarazimiArkatareveFilloNderrimin.Parameters["@OperatoriId"].Direction = ParameterDirection.Input;
            cmdBarazimiArkatareveFilloNderrimin.Parameters.Add("@LlojiDokumentitId", SqlDbType.Int);
            cmdBarazimiArkatareveFilloNderrimin.Parameters["@LlojiDokumentitId"].Direction = ParameterDirection.Input;
            cmdBarazimiArkatareveFilloNderrimin.Parameters.Add("@Komenti", SqlDbType.VarChar);
            cmdBarazimiArkatareveFilloNderrimin.Parameters["@Komenti"].Direction = ParameterDirection.Input;
            cmdBarazimiArkatareveFilloNderrimin.Parameters.Add("@BarazuarNga", SqlDbType.Int);
            cmdBarazimiArkatareveFilloNderrimin.Parameters["@BarazuarNga"].Direction = ParameterDirection.Input;
            cmdBarazimiArkatareveFilloNderrimin.Parameters.Add("@GjendjaFillestare", SqlDbType.Decimal);
            cmdBarazimiArkatareveFilloNderrimin.Parameters["@GjendjaFillestare"].Direction = ParameterDirection.Input;

            cmdDaljaMallitDetaleAktivitetetInsert_spIns.Parameters.Add("@DaljaMallitDetaleId", SqlDbType.Int);
            cmdDaljaMallitDetaleAktivitetetInsert_spIns.Parameters["@DaljaMallitDetaleId"].Direction = ParameterDirection.Input;
            cmdDaljaMallitDetaleAktivitetetInsert_spIns.Parameters.Add("@AktivitetiId", SqlDbType.BigInt);
            cmdDaljaMallitDetaleAktivitetetInsert_spIns.Parameters["@AktivitetiId"].Direction = ParameterDirection.Input;
            cmdDaljaMallitDetaleAktivitetetInsert_spIns.Parameters.Add("@Zbritja", SqlDbType.Decimal);
            cmdDaljaMallitDetaleAktivitetetInsert_spIns.Parameters["@Zbritja"].Direction = ParameterDirection.Input;
            cmdDaljaMallitDetaleAktivitetetInsert_spIns.Parameters.Add("@DaljaMallitId", SqlDbType.BigInt);
            cmdDaljaMallitDetaleAktivitetetInsert_spIns.Parameters["@DaljaMallitId"].Direction = ParameterDirection.Input;

            cmdKuponatInsert_spIns.Parameters.Add("@KuponatPerZbritjeId", SqlDbType.Int);
            cmdKuponatInsert_spIns.Parameters["@KuponatPerZbritjeId"].Direction = ParameterDirection.Input;
            cmdKuponatInsert_spIns.Parameters.Add("@KodiKuponit", SqlDbType.VarChar);
            cmdKuponatInsert_spIns.Parameters["@KodiKuponit"].Direction = ParameterDirection.Input;
            cmdKuponatInsert_spIns.Parameters.Add("@Aktivizuar", SqlDbType.Bit);
            cmdKuponatInsert_spIns.Parameters["@Aktivizuar"].Direction = ParameterDirection.Input;
            cmdKuponatInsert_spIns.Parameters.Add("@Aplikuar", SqlDbType.Bit);
            cmdKuponatInsert_spIns.Parameters["@Aplikuar"].Direction = ParameterDirection.Input;
            cmdKuponatInsert_spIns.Parameters.Add("@DaljaMallitIdGjeneruar", SqlDbType.BigInt);
            cmdKuponatInsert_spIns.Parameters["@DaljaMallitIdGjeneruar"].Direction = ParameterDirection.Input;
            cmdKuponatInsert_spIns.Parameters.Add("@DaljaMallitIdAplikuar", SqlDbType.BigInt);
            cmdKuponatInsert_spIns.Parameters["@DaljaMallitIdAplikuar"].Direction = ParameterDirection.Input;

            DaljaMallitVaucheratInsert_SpIns.Parameters.Add("@DaljaMallitID", SqlDbType.BigInt);
            DaljaMallitVaucheratInsert_SpIns.Parameters["@DaljaMallitID"].Direction = ParameterDirection.Input;
            DaljaMallitVaucheratInsert_SpIns.Parameters.Add("@Vlera", SqlDbType.Decimal);
            DaljaMallitVaucheratInsert_SpIns.Parameters["@Vlera"].Direction = ParameterDirection.Input;
            DaljaMallitVaucheratInsert_SpIns.Parameters.Add("@KodiVaucherit", SqlDbType.VarChar);
            DaljaMallitVaucheratInsert_SpIns.Parameters["@KodiVaucherit"].Direction = ParameterDirection.Input;
            DaljaMallitVaucheratInsert_SpIns.Parameters.Add("@Emri", SqlDbType.VarChar);
            DaljaMallitVaucheratInsert_SpIns.Parameters["@Emri"].Direction = ParameterDirection.Input;
            DaljaMallitVaucheratInsert_SpIns.Parameters.Add("@Mbiemri", SqlDbType.VarChar);
            DaljaMallitVaucheratInsert_SpIns.Parameters["@Mbiemri"].Direction = ParameterDirection.Input;
            DaljaMallitVaucheratInsert_SpIns.Parameters.Add("@Lloji", SqlDbType.VarChar);
            DaljaMallitVaucheratInsert_SpIns.Parameters["@Lloji"].Direction = ParameterDirection.Input;
            DaljaMallitVaucheratInsert_SpIns.Parameters.Add("@KuponatPerZbritjeLlojetEZbritjesId", SqlDbType.Int);
            DaljaMallitVaucheratInsert_SpIns.Parameters["@KuponatPerZbritjeLlojetEZbritjesId"]
                .Direction = ParameterDirection.Input;

            DataTable dtShitjaPerFshirje = new DataTable();
            dtShitjaPerFshirje.Columns.Add("DaljaMallitId", typeof(long));

            foreach (DataRow Row in dsHeader.Tables["dtHeader"].Rows)
            {
                cmdDaljaMallitInsert_spIns.Parameters["@OrganizataId"].Value = Row["OrganizataId"];
                cmdDaljaMallitInsert_spIns.Parameters["@Viti"].Value = Row["Viti"];
                cmdDaljaMallitInsert_spIns.Parameters["@Data"].Value = Row["Data"];
                cmdDaljaMallitInsert_spIns.Parameters["@NrFatures"].Value = Row["NrFatures"];
                cmdDaljaMallitInsert_spIns.Parameters["@DokumentiId"].Value = Row["DokumentiId"];
                cmdDaljaMallitInsert_spIns.Parameters["@NumriArkes"].Value = Row["NumriArkes"];
                cmdDaljaMallitInsert_spIns.Parameters["@ZbritjeNgaOperatori"].Value = Row["ZbritjeNgaOperatori"];
                cmdDaljaMallitInsert_spIns.Parameters["@SubjektiId"].Value = Row["SubjektiId"];
                cmdDaljaMallitInsert_spIns.Parameters["@IdLokal"].Value = Row["IdLokal"];
                cmdDaljaMallitInsert_spIns.Parameters["@RegjistruarNga"].Value = Row["RegjistruarNga"];
                cmdDaljaMallitInsert_spIns.Parameters["@IdLokal"].Value = Row["IdLokal"];
                cmdDaljaMallitInsert_spIns.Parameters["@DataERegjistrimit"].Value = Row["DataERegjistrimit"];
                cmdDaljaMallitInsert_spIns.Parameters["@TavolinaId"].Value = Row["TavolinaId"];
                cmdDaljaMallitInsert_spIns.Parameters["@Validuar"].Value = Row["Validuar"];
                cmdDaljaMallitInsert_spIns.Parameters["@NumriATK"].Value = Row["NumriATK"];
                cmdDaljaMallitInsert_spIns.Parameters["@NumriArkesGK"].Value = Row["NumriArkesGK"];
                cmdDaljaMallitInsert_spIns.Parameters["@NumriFaturesManual"].Value = Row["NumriFaturesManual"];
                cmdDaljaMallitInsert_spIns.Parameters["@DaljaMallitKorrektuarId"].Value = Row["DaljaMallitKorrektuarId"];
                cmdDaljaMallitInsert_spIns.Parameters["@AplikacioniId"].Value = Row["AplikacioniId"];
                cmdDaljaMallitInsert_spIns.Parameters["@KthimiMallitArsyejaId"].Value = Row["KthimiMallitArsyejaId"];
                cmdDaljaMallitInsert_spIns.Parameters["@Personi"].Value = Row["Personi"];
                cmdDaljaMallitInsert_spIns.Parameters["@Adresa"].Value = Row["Adresa"];
                cmdDaljaMallitInsert_spIns.Parameters["@NumriPersonal"].Value = Row["NumriPersonal"];
                cmdDaljaMallitInsert_spIns.Parameters["@NrTel"].Value = Row["NrTel"];
                cmdDaljaMallitInsert_spIns.Parameters["@KuponiFiskalShtypur"].Value = Row["KuponiFiskalShtypur"];

                long idTemp = Convert.ToInt64(Row["Id"]);
                Id = Convert.ToInt64(cmdDaljaMallitInsert_spIns.ExecuteScalar());
                Row["Id"] = Id;
                dtShitjaPerFshirje.Rows.Add(idTemp);
            }

            foreach (DataRow RowDetajet in dsHeader.Tables["dtDaljaDetalet"].Rows)
            {
                cmdDaljaMallitDetaleInsert_spIns.Parameters["@DaljaMallitId"].Value = RowDetajet["DaljaMallitId"];
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
                cmdDaljaMallitDetaleInsert_spIns.Parameters["@AplikimiVoucherit"].Value = RowDetajet["AplikimiVoucherit"];
                cmdDaljaMallitDetaleInsert_spIns.Parameters["@Barkodi"].Value = RowDetajet["Barkodi"];
                cmdDaljaMallitDetaleInsert_spIns.ExecuteNonQuery();
            }

            foreach (DataRow RowTeFshire in dsHeader.Tables["dtArtikujtEFshire"].Rows)
            {
                cmdDaljaMallitDetaleHistoriInsert_spIns.Parameters["@DaljaMallitId"].Value = RowTeFshire["DaljaMallitId"];
                cmdDaljaMallitDetaleHistoriInsert_spIns.Parameters["@OrganizataId"].Value = DBNull.Value;
                cmdDaljaMallitDetaleHistoriInsert_spIns.Parameters["@ArtikulliId"].Value = RowTeFshire["ArtikulliId"];
                cmdDaljaMallitDetaleHistoriInsert_spIns.Parameters["@NjesiaID"].Value = RowTeFshire["NjesiaID"];
                cmdDaljaMallitDetaleHistoriInsert_spIns.Parameters["@Nr"].Value = RowTeFshire["NR"];
                cmdDaljaMallitDetaleHistoriInsert_spIns.Parameters["@Sasia"].Value = RowTeFshire["Sasia"];
                cmdDaljaMallitDetaleHistoriInsert_spIns.Parameters["@Tvsh"].Value = RowTeFshire["Tvsh"];
                cmdDaljaMallitDetaleHistoriInsert_spIns.Parameters["@QmimiShitjes"].Value = RowTeFshire["QmimiShitjes"];
                cmdDaljaMallitDetaleHistoriInsert_spIns.Parameters["@Rabati"].Value = RowTeFshire["Rabati"];
                cmdDaljaMallitDetaleHistoriInsert_spIns.Parameters["@EkstraRabati"].Value = RowTeFshire["EkstraRabati"];
                cmdDaljaMallitDetaleHistoriInsert_spIns.Parameters["@QmimiFinal"].Value = RowTeFshire["QmimiShitjes"];
                cmdDaljaMallitDetaleHistoriInsert_spIns.ExecuteNonQuery();
            }

            foreach (DataRow RowPagesat in dsHeader.Tables["dtEkzekutimetEPageses"].Rows)
            {
                cmdEkzekutimiPagesesInsert_spIns.Parameters["@Vlera"].Value = RowPagesat["Vlera"];
                cmdEkzekutimiPagesesInsert_spIns.Parameters["@MenyraEPagesesId"].Value = RowPagesat["MenyraEPagesesId"];
                cmdEkzekutimiPagesesInsert_spIns.Parameters["@DaljaMallitId"].Value = RowPagesat["DaljaMallitId"];
                cmdEkzekutimiPagesesInsert_spIns.Parameters["@ShifraOperatorit"].Value = RowPagesat["ShifraOperatorit"];
                cmdEkzekutimiPagesesInsert_spIns.Parameters["@Paguar"].Value = RowPagesat["Paguar"];
                cmdEkzekutimiPagesesInsert_spIns.Parameters["@DhenjeKesh"].Value = RowPagesat["DhenjeKesh"];
                cmdEkzekutimiPagesesInsert_spIns.Parameters["@ValutaId"].Value = RowPagesat["ValutaId"];
                cmdEkzekutimiPagesesInsert_spIns.Parameters["@Kursi"].Value = RowPagesat["Kursi"] == DBNull.Value ||
                    decimal.Parse(RowPagesat["Kursi"].ToString()) <= 0 ?
                    1 : RowPagesat["Kursi"];

                cmdEkzekutimiPagesesInsert_spIns.Parameters["@KodiVoucherit"].Value = RowPagesat["KodiVoucherit"];
                cmdEkzekutimiPagesesInsert_spIns.Parameters["@LlojiVoucherit"].Value = RowPagesat["LlojiVoucherit"];
                cmdEkzekutimiPagesesInsert_spIns.ExecuteNonQuery();
            }

            foreach (DataRow RowCCP in dsHeader.Tables["dtCCPFatura"].Rows)
            {
                cmdCCPFaturatInsert_spIns.Parameters["@DaljaMallitID"].Value = RowCCP["DaljaMallitID"];
                cmdCCPFaturatInsert_spIns.Parameters["@CCPKompaniaId"].Value = RowCCP["CCPKompaniaId"];
                cmdCCPFaturatInsert_spIns.Parameters["@RegjistruarNga"].Value = RowCCP["RegjistruarNga"];
                cmdCCPFaturatInsert_spIns.ExecuteNonQuery();
            }

            foreach (DataRow Row in dsHeader.Tables["dtHeader"].Rows)
            {
                cmdDaljaMallitKonto.Parameters["@DaljaMallitID"].Value = Row["Id"];
                cmdDaljaMallitKonto.ExecuteNonQuery();
            }
            foreach (DataRow Row in dsHeader.Tables["dtPOSZbritjaMeKupon"].Rows)
            {
                cmdPOS_ZbritjaMeKuponInsert_spIns.Parameters["@DaljaMallitID"].Value = Row["DaljaMallitID"];
                cmdPOS_ZbritjaMeKuponInsert_spIns.Parameters["@Vlera"].Value = Row["Vlera"];
                cmdPOS_ZbritjaMeKuponInsert_spIns.Parameters["@POSKuponatPerZbritjeId"]
                    .Value = Row["POSKuponatPerZbritjeId"];
                cmdPOS_ZbritjaMeKuponInsert_spIns.ExecuteNonQuery();

                cmdPOSKuponiMeZbritjeApliko_spIns.Parameters["@POSKuponatPerZbritjeId"]
                    .Value = Row["POSKuponatPerZbritjeId"];
                if (PublicClass.PerdorKoneksionPrimar) cmdPOSKuponiMeZbritjeApliko_spIns.ExecuteNonQuery();
            }

            SqlCommand cmdFshijDaljaMallit = new SqlCommand("TOSHIBA.DaljaMallitDerguarNeServer_Sp", cnnLokal, tranLokal);
            cmdFshijDaljaMallit.CommandType = CommandType.StoredProcedure;
            cmdFshijDaljaMallit.Parameters.Add("@DaljaMallitId", SqlDbType.BigInt);
            cmdFshijDaljaMallit.Parameters["@DaljaMallitId"].Direction = ParameterDirection.Input;

            foreach (DataRow Row in dtShitjaPerFshirje.Rows)
            {
                cmdFshijDaljaMallit.Parameters["@DaljaMallitID"].Value = Row["DaljaMallitID"];
                cmdFshijDaljaMallit.ExecuteNonQuery();
            }

            foreach (DataRow rowDaljaMallitDetaleAktivitetet in dsHeader.Tables["dtDaljaMallitDetaleAktivitetet"].Rows)
            {
                cmdDaljaMallitDetaleAktivitetetInsert_spIns
                    .Parameters["@DaljaMallitDetaleId"].Value =
                    rowDaljaMallitDetaleAktivitetet["DaljaMallitDetaleId"];
                cmdDaljaMallitDetaleAktivitetetInsert_spIns
                    .Parameters["@AktivitetiId"].Value =
                    rowDaljaMallitDetaleAktivitetet["AktivitetiId"];
                cmdDaljaMallitDetaleAktivitetetInsert_spIns
                    .Parameters["@Zbritja"].Value 
                    = rowDaljaMallitDetaleAktivitetet["Zbritja"];
                cmdDaljaMallitDetaleAktivitetetInsert_spIns
                    .Parameters["@DaljaMallitId"].Value 
                    = rowDaljaMallitDetaleAktivitetet["DaljaMallitId"];
                cmdDaljaMallitDetaleAktivitetetInsert_spIns.ExecuteNonQuery();
            }

            foreach (DataRow drKuponat in dsHeader.Tables["dtKuponat"].Rows)
            {
                cmdKuponatInsert_spIns.Parameters["@KuponatPerZbritjeId"].Value
                    = drKuponat["KuponatPerZbritjeId"];
                cmdKuponatInsert_spIns.Parameters["@KodiKuponit"].Value 
                    = drKuponat["KodiKuponit"];
                cmdKuponatInsert_spIns.Parameters["@Aktivizuar"].Value 
                    = drKuponat["Aktivizuar"];
                cmdKuponatInsert_spIns.Parameters["@Aplikuar"].Value
                    = drKuponat["Aplikuar"];
                cmdKuponatInsert_spIns.Parameters["@DaljaMallitIdGjeneruar"].Value = 
                    drKuponat["DaljaMallitIdGjeneruar"];
                cmdKuponatInsert_spIns.Parameters["@DaljaMallitIdAplikuar"].Value =
                    drKuponat["DaljaMallitIdAplikuar"];
                cmdKuponatInsert_spIns.ExecuteNonQuery();
            }

            foreach (DataRow drVoucherat in dsHeader.Tables["dtDaljaMallitVaucherat"].Rows)
            {
                DaljaMallitVaucheratInsert_SpIns.Parameters["@DaljaMallitID"].Value = Id;
                DaljaMallitVaucheratInsert_SpIns.Parameters["@Vlera"].Value = drVoucherat["Vlera"];
                DaljaMallitVaucheratInsert_SpIns.Parameters["@KodiVaucherit"].Value = drVoucherat["KodiVaucherit"];
                DaljaMallitVaucheratInsert_SpIns.Parameters["@Emri"].Value = drVoucherat["Emri"];
                DaljaMallitVaucheratInsert_SpIns.Parameters["@Mbiemri"].Value = drVoucherat["Mbiemri"];
                DaljaMallitVaucheratInsert_SpIns.Parameters["@Lloji"].Value = drVoucherat["Lloji"];
                DaljaMallitVaucheratInsert_SpIns.Parameters["@KuponatPerZbritjeLlojetEZbritjesId"].Value 
                    = drVoucherat["KuponatPerZbritjeLlojetEZbritjesId"];
                DaljaMallitVaucheratInsert_SpIns.ExecuteNonQuery();
            }
            return Id;
        }


        public static void GetArtikujtPerPrinter(DataTable tabela, long daljaMallitId)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_POS_DaljaMallitDetaleSelect_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DaljaMallitId", daljaMallitId);
            da.Fill(tabela);
        }

        public static DataTable GetDaljet(DataTable shitjaERuajtur, long daljaId)
        {
            shitjaERuajtur.Clear();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_DaljaMallitSelect_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Id", daljaId);
            da.Fill(shitjaERuajtur);
            return shitjaERuajtur;
        }

        public static DataTable GetShitjaMeDetale(long daljaId, bool kerkoNeServer = true)
        {
            DataTable shitjaERuajtur = new DataTable();
            SqlDataAdapter da;
            if (kerkoNeServer)
                da = new SqlDataAdapter("TOSHIBA.DaljaMallitDetaleRaport_Sp", PublicClass.KoneksioniPrimar);
            else
                da = new SqlDataAdapter("TOSHIBA.DaljaMallitDetaleRaport_Sp", PublicClass.KoneksioniLokal);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DaljaId", daljaId);
            da.Fill(shitjaERuajtur);
            return shitjaERuajtur;
        }

        public static DataTable GetBankatPerFilial(bool kerkoneserver)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da;
            if (kerkoneserver)
                da = new SqlDataAdapter("TOSHIBA.OrganizataSelectBankat_sp", PublicClass.KoneksioniPrimar);
            else
                da = new SqlDataAdapter("TOSHIBA.OrganizataSelectBankat_sp", PublicClass.KoneksioniLokal);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@NjesiaID", PublicClass.OrganizataId);
            da.Fill(dt);
            return dt;
        }

        public static DataTable GetDaljetLokal(DataTable shitjaERuajtur, long daljaId)
        {
            shitjaERuajtur.Clear();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_DaljaMallitSelect_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Id", daljaId);
            da.Fill(shitjaERuajtur);
            return shitjaERuajtur;
        }

        public static DataTable GetArtikujt(DataTable tabela)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_ArtikulliSelectF1_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", PublicClass.OrganizataId);
            da.Fill(tabela);
            return tabela;
        }
        public static decimal GetZbritjenNeBazTeSasia(string ArtikulliId, int OrganizataId, decimal Sasia)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_ArtikujtMeLirimKontrollo_Sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@ArtikulliId", ArtikulliId);
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", OrganizataId);
            da.SelectCommand.Parameters.AddWithValue("@Sasia", Sasia);
            da.Fill(tabela);
            if (tabela.Rows.Count > 0)
            {
                decimal a = 0M;
                decimal.TryParse(tabela.Rows[0]["Zbritja"].ToString(), out a);
                return a;
            }
            else
                return 0;
        }
        public static DataTable GetCCPFaturat(DataTable tabela)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_CCPFaturatSelect_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.Fill(tabela);

            return tabela;
        }

        public static DataTable GetCCPFaturatLokale(DataTable tabela, string daljamallitid)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_CCPFaturatSelect_sp", PublicClass.KoneksioniLokal);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DaljaMallitId", daljamallitid);
            da.Fill(tabela);

            return tabela;
        }

        public static DataTable GetBaraziminEArkatareve(DataTable tabela, int operatoriId, int organizataId, DateTime? data = null)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.BarazimiArkatareveSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Data", data);
            da.SelectCommand.Parameters.AddWithValue("@OperatoriId", operatoriId);
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", organizataId);
            da.SelectCommand.Parameters.AddWithValue("@NderrimiMbyllur", false);
            da.Fill(tabela);

            return tabela;
        }

        public static int GetOperatorinQeDhaZbritje(string fjalekalimi)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.Mxh_OperatoretSelect_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@FjalekalimiPerZbritje", fjalekalimi);
            da.Fill(tabela);
            if (tabela.Rows.Count > 0)
                return Convert.ToInt32(tabela.Rows[0]["Id"]);
            else
                return 0;
        }


        public static void KrijotxtFajllinPerZRaport()
        {
            string path = @"C:\Temp\ZRaporti.txt";
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("Z,1,______,_,__;");
                }
            }
        }

        public static DataTable GetDaljaMallitDetaleAktivitetet(DataTable tabela, string daljaMallitId)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.DaljaMallitDetaleAktivitetetSelect_sp", PublicClass.KoneksioniLokal);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DaljaMallit", daljaMallitId);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetKuponat(DataTable tabela, long daljaMallitId)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.KuponatSelect_sp", PublicClass.KoneksioniLokal);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DaljaMallitId", daljaMallitId);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetPOSZbritjenMeKuponSipasKoditTeKuponit(string kodiKuponit, bool? aktivizuar = null, bool? aplikuar = null)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POSKuponatPerZbritjeKerkoKuponin_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@KodiKuponit", kodiKuponit);
            da.SelectCommand.Parameters.AddWithValue("@Aktivizuar", aktivizuar);
            da.SelectCommand.Parameters.AddWithValue("@Aplikuar", aplikuar);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetZbritjenMeKuponFilter(int kuponatPerZbritjeId)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POSKuponatPerZbritjeFilterSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@KuponatPerZbritjeId", kuponatPerZbritjeId);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetValutat(int? id = null, bool? valutaAktive = null)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.ValutatSelect_sp", PublicClass.KoneksioniLokal);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Id", id);
            da.SelectCommand.Parameters.AddWithValue("@ValutaAktive", valutaAktive);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetValutatServer(int? id = null, bool? valutaAktive = null)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ValutatSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Id", id);
            da.SelectCommand.Parameters.AddWithValue("@ValutaAktive", valutaAktive);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetValutatKartemonedhat(int? id = null, int? valutaId = null)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ValutatKartmonedhatSelect_Sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Id", id);
            da.SelectCommand.Parameters.AddWithValue("@ValutaId", valutaId);
            da.Fill(tabela);
            return tabela;
        }

        public static string GetNumrinEFaturesATK(int OrganizataId, string NumriFaturesLokal, out SqlTransaction tran, SqlConnection con)
        {
            try
            {
                con.Open();
                tran = con.BeginTransaction();
                DataTable tabela = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.GetNumrinEFaturesATK_Sp", con);
                da.SelectCommand.Transaction = tran;
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@OrganizataId", OrganizataId);
                da.SelectCommand.Parameters.AddWithValue("@NumriFaturesLokal", NumriFaturesLokal);
                da.Fill(tabela);
                if (tabela.Rows.Count > 0)
                    return tabela.Rows[0]["NumriFatures"].ToString();
                else
                    throw new Exception("Numrat serikë jane hargjuar! Kontaktoni administraten qendrore!");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("A network-related"))
                    throw new Exception("Nuk është e mundur të komunikohet me Server! Numri serik është e pamundur te mirret!");
                else
                    throw ex;
            }
        }

        public static DataTable GetDaljaMallitVaucherat(DataTable dt, long DaljaMallitId)
        {
            using (SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.DaljaMallitVaucheratSelect_Sp", PublicClass.KoneksioniLokal))
            {
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@DaljaMallitId", DaljaMallitId);
                da.SelectCommand.CommandTimeout = 10;
                da.Fill(dt);
            }
            return dt;
        }

        public static DataTable GetTatimet(DataTable dt, int? id = null, int? kategoria = null)
        {
            using (SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_TatimetSelect_Sp", PublicClass.KoneksioniLokal))
            {
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Id", id);
                da.SelectCommand.Parameters.AddWithValue("@Kategoria", kategoria);
                da.Fill(dt);
            }
            return dt;
        }

        public static DataTable GetBarazimetDetale(DataTable dt, int? id = null, long? barazimiId = null)
        {
            using (SqlDataAdapter da = new SqlDataAdapter("dbo.BarazimiArkatareveEREDetaletSelect_sp", PublicClass.KoneksioniPrimar))
            {
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Id", id);
                da.SelectCommand.Parameters.AddWithValue("@BarazimiArkatareveId", barazimiId);
                da.Fill(dt);
            }
            return dt;
        }

        public static DataTable GetBarazimiKartemonedhat(DataTable dt, int? id = null, long? barazimiDetaleId = null, int? valutaId = null)
        {
            using (SqlDataAdapter da = new SqlDataAdapter("dbo.BarazimiArkatareveKartmonedhatSelect_Sp", PublicClass.KoneksioniPrimar))
            {
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Id", id);
                da.SelectCommand.Parameters.AddWithValue("@BarazimiArkatareveDetaletId", barazimiDetaleId);
                da.SelectCommand.Parameters.AddWithValue("@ValutaId", valutaId);
                da.Fill(dt);
            }
            return dt;
        }

        public static DataTable GetDaljetEPambylluraPerOperatorin(DataTable dtFaturat, int? OrganizataId = null, int? OperatoriId = null, DateTime? dataFilter = null)
        {
            if (dtFaturat == null)
                dtFaturat = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("dbo.BarazimiArkatareveEREDaljetEHapuraSelect_Sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", OrganizataId);
            da.SelectCommand.Parameters.AddWithValue("@OperatoriId", OperatoriId);
            da.SelectCommand.Parameters.AddWithValue("@Data", null);
            da.Fill(dtFaturat);
            return dtFaturat;
        }

        public static void RuajBaraziminDetale(long barazimiId, int organizataId, int operatoriId)
        {
            using (SqlConnection cnnServer = new SqlConnection(PublicClass.KoneksioniPrimar))
            {
                SqlTransaction tranServer = default;
                cnnServer.Open();
                tranServer = cnnServer.BeginTransaction();

                using (SqlCommand cmdBarazimiArkatareveDetaleRuaj = new SqlCommand("dbo.POS_BarazimiArkatareveDetaleInsert_sp", cnnServer, tranServer)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmdBarazimiArkatareveDetaleRuaj.Parameters.Add("@BarazimiArkatareveId", SqlDbType.BigInt);
                    cmdBarazimiArkatareveDetaleRuaj.Parameters["@BarazimiArkatareveId"].Direction = ParameterDirection.Input;
                    cmdBarazimiArkatareveDetaleRuaj.Parameters.Add("@OrganizataId", SqlDbType.Int);
                    cmdBarazimiArkatareveDetaleRuaj.Parameters["@OrganizataId"].Direction = ParameterDirection.Input;
                    cmdBarazimiArkatareveDetaleRuaj.Parameters.Add("@OperatoriId", SqlDbType.Int);
                    cmdBarazimiArkatareveDetaleRuaj.Parameters["@OperatoriId"].Direction = ParameterDirection.Input;
                    try
                    {
                        cmdBarazimiArkatareveDetaleRuaj.Parameters["@BarazimiArkatareveId"].Value = barazimiId;
                        cmdBarazimiArkatareveDetaleRuaj.Parameters["@OrganizataId"].Value = organizataId;
                        cmdBarazimiArkatareveDetaleRuaj.Parameters["@OperatoriId"].Value = operatoriId;
                        cmdBarazimiArkatareveDetaleRuaj.ExecuteNonQuery();

                        tranServer.Commit();
                    }
                    catch (Exception)
                    {
                        tranServer.Rollback();
                        throw;
                    }
                    finally
                    {
                        cnnServer.Close();
                    }
                }
            }
        }

        public static void RuajKartemonedhat(DataTable dtBarazimiDetale, DataTable dtKartemonedhat, bool isAlreadySaved = false)
        {
            using (SqlConnection cnnServer = new SqlConnection(PublicClass.KoneksioniPrimar))
            {
                SqlTransaction tranServer = default;
                cnnServer.Open();
                tranServer = cnnServer.BeginTransaction();

                SqlCommand cmdBarazimiArkatareveUpdate_spUpd = new SqlCommand("dbo.BarazimiArkatareveDetaletUpdate_sp", cnnServer, tranServer);
                cmdBarazimiArkatareveUpdate_spUpd.CommandType = CommandType.StoredProcedure;

                SqlCommand cmdKartemonedhatInsert_spIns = new SqlCommand("dbo.BarazimiArkatareveKartmonedhatInsert_Sp", cnnServer, tranServer);
                cmdKartemonedhatInsert_spIns.CommandType = CommandType.StoredProcedure;

                SqlCommand cmdKartemonedhatUpdate_spUpd = new SqlCommand("dbo.BarazimiArkatareveKartmonedhatUpdate_Sp", cnnServer, tranServer);
                cmdKartemonedhatUpdate_spUpd.CommandType = CommandType.StoredProcedure;

                cmdKartemonedhatInsert_spIns.Parameters.Add("@BarazimiArkatareveDetaletId", SqlDbType.BigInt);
                cmdKartemonedhatInsert_spIns.Parameters["@BarazimiArkatareveDetaletId"].Direction = ParameterDirection.Input;
                cmdKartemonedhatInsert_spIns.Parameters.Add("@ValutaId", SqlDbType.Int);
                cmdKartemonedhatInsert_spIns.Parameters["@ValutaId"].Direction = ParameterDirection.Input;
                cmdKartemonedhatInsert_spIns.Parameters.Add("@Sasia", SqlDbType.Int);
                cmdKartemonedhatInsert_spIns.Parameters["@Sasia"].Direction = ParameterDirection.Input;
                cmdKartemonedhatInsert_spIns.Parameters.Add("@VleraKartmonedhes", SqlDbType.Decimal);
                cmdKartemonedhatInsert_spIns.Parameters["@VleraKartmonedhes"].Direction = ParameterDirection.Input;
                cmdKartemonedhatInsert_spIns.Parameters.Add("@Kursi", SqlDbType.Decimal);
                cmdKartemonedhatInsert_spIns.Parameters["@Kursi"].Direction = ParameterDirection.Input;

                cmdKartemonedhatUpdate_spUpd.Parameters.Add("@Id", SqlDbType.Int);
                cmdKartemonedhatUpdate_spUpd.Parameters["@Id"].Direction = ParameterDirection.Input;
                cmdKartemonedhatUpdate_spUpd.Parameters.Add("@BarazimiArkatareveDetaletId", SqlDbType.BigInt);
                cmdKartemonedhatUpdate_spUpd.Parameters["@BarazimiArkatareveDetaletId"].Direction = ParameterDirection.Input;
                cmdKartemonedhatUpdate_spUpd.Parameters.Add("@ValutaId", SqlDbType.Int);
                cmdKartemonedhatUpdate_spUpd.Parameters["@ValutaId"].Direction = ParameterDirection.Input;
                cmdKartemonedhatUpdate_spUpd.Parameters.Add("@Sasia", SqlDbType.Int);
                cmdKartemonedhatUpdate_spUpd.Parameters["@Sasia"].Direction = ParameterDirection.Input;
                cmdKartemonedhatUpdate_spUpd.Parameters.Add("@VleraKartmonedhes", SqlDbType.Decimal);
                cmdKartemonedhatUpdate_spUpd.Parameters["@VleraKartmonedhes"].Direction = ParameterDirection.Input;
                cmdKartemonedhatUpdate_spUpd.Parameters.Add("@Kursi", SqlDbType.Decimal);
                cmdKartemonedhatUpdate_spUpd.Parameters["@Kursi"].Direction = ParameterDirection.Input;

                cmdBarazimiArkatareveUpdate_spUpd.Parameters.Add("@Id", SqlDbType.BigInt);
                cmdBarazimiArkatareveUpdate_spUpd.Parameters["@Id"].Direction = ParameterDirection.Input;
                cmdBarazimiArkatareveUpdate_spUpd.Parameters.Add("@Vlera", SqlDbType.Decimal);
                cmdBarazimiArkatareveUpdate_spUpd.Parameters["@Vlera"].Direction = ParameterDirection.Input;
                cmdBarazimiArkatareveUpdate_spUpd.Parameters.Add("@Dorezuar", SqlDbType.Decimal);
                cmdBarazimiArkatareveUpdate_spUpd.Parameters["@Dorezuar"].Direction = ParameterDirection.Input;
                cmdBarazimiArkatareveUpdate_spUpd.Parameters.Add("@Diferenca", SqlDbType.Decimal);
                cmdBarazimiArkatareveUpdate_spUpd.Parameters["@Diferenca"].Direction = ParameterDirection.Input;
                cmdBarazimiArkatareveUpdate_spUpd.Parameters.Add("@RaportiFiskal", SqlDbType.Decimal);
                cmdBarazimiArkatareveUpdate_spUpd.Parameters["@RaportiFiskal"].Direction = ParameterDirection.Input;
                cmdBarazimiArkatareveUpdate_spUpd.Parameters.Add("@MenyraEPagesesId", SqlDbType.Int);
                cmdBarazimiArkatareveUpdate_spUpd.Parameters["@MenyraEPagesesId"].Direction = ParameterDirection.Input;
                cmdBarazimiArkatareveUpdate_spUpd.Parameters.Add("@Komenti", SqlDbType.VarChar);
                cmdBarazimiArkatareveUpdate_spUpd.Parameters["@Komenti"].Direction = ParameterDirection.Input;

                try
                {
                    int detaletId = 0;
                    //update rreshtin cash ne barazimin detale
                    foreach (DataRow dataRow in dtBarazimiDetale.Rows)
                    {
                        if (dataRow.RowState == DataRowState.Modified)
                        {
                            cmdBarazimiArkatareveUpdate_spUpd.Parameters["@Id"].Value = dataRow["Id"];
                            cmdBarazimiArkatareveUpdate_spUpd.Parameters["@Vlera"].Value = dataRow["Vlera"];
                            cmdBarazimiArkatareveUpdate_spUpd.Parameters["@Dorezuar"].Value = dataRow["Dorezuar"];
                            cmdBarazimiArkatareveUpdate_spUpd.Parameters["@Diferenca"].Value = dataRow["Diferenca"];
                            cmdBarazimiArkatareveUpdate_spUpd.Parameters["@RaportiFiskal"].Value = dataRow["RaportiFiskal"];
                            cmdBarazimiArkatareveUpdate_spUpd.Parameters["@MenyraEPagesesId"].Value = dataRow["MenyraEPagesesId"];
                            cmdBarazimiArkatareveUpdate_spUpd.Parameters["@Komenti"].Value = dataRow["Komenti"];
                            cmdBarazimiArkatareveUpdate_spUpd.ExecuteNonQuery();
                            detaletId = Convert.ToInt32(dataRow["Id"]);
                        }
                    }

                    //Ruaj kartemonedhat
                    foreach (DataRow dr in dtKartemonedhat.Rows)
                    {
                        if (isAlreadySaved)
                        {
                            cmdKartemonedhatUpdate_spUpd.Parameters["@Id"].Value = dr["Id"];
                            cmdKartemonedhatUpdate_spUpd.Parameters["@BarazimiArkatareveDetaletId"].Value = detaletId;
                            cmdKartemonedhatUpdate_spUpd.Parameters["@ValutaId"].Value = dr["ValutaId"];
                            cmdKartemonedhatUpdate_spUpd.Parameters["@Sasia"].Value = dr["Sasia"];
                            cmdKartemonedhatUpdate_spUpd.Parameters["@VleraKartmonedhes"].Value = dr["VleraKartmonedhes"];
                            cmdKartemonedhatUpdate_spUpd.Parameters["@Kursi"].Value = dr["Kursi"];
                            cmdKartemonedhatUpdate_spUpd.ExecuteNonQuery();
                        }
                        else
                        {
                            cmdKartemonedhatInsert_spIns.Parameters["@BarazimiArkatareveDetaletId"].Value = detaletId;
                            cmdKartemonedhatInsert_spIns.Parameters["@ValutaId"].Value = dr["ValutaId"];
                            cmdKartemonedhatInsert_spIns.Parameters["@Sasia"].Value = dr["Sasia"];
                            cmdKartemonedhatInsert_spIns.Parameters["@VleraKartmonedhes"].Value = dr["VleraKartmonedhes"];
                            cmdKartemonedhatInsert_spIns.Parameters["@Kursi"].Value = dr["Kursi"];
                            dr["Id"] = cmdKartemonedhatInsert_spIns.ExecuteScalar();
                            dr.EndEdit();
                        }
                    }

                    tranServer.Commit();
                }
                catch (Exception ex)
                {
                    tranServer.Rollback();
                    throw ex;
                }
                finally
                {
                    cnnServer.Close();
                }
            }
        }

        public async static System.Threading.Tasks.Task UpdateDaljaMallitVaucheratDergimiNeServer(long daljaMallitId)
        {
            using (var cnnLokal = new SqlConnection(PublicClass.KoneksioniLokal))
            {
                try
                {
                    await cnnLokal.OpenAsync().ConfigureAwait(false);
                    using (SqlCommand cmd = new SqlCommand("TOSHIBA.DaljaMallitVaucheratDerguarNeServerUpdate_Sp", cnnLokal)
                    {
                        CommandType = CommandType.StoredProcedure
                    })
                    {
                        cmd.Parameters.AddWithValue("@DaljaMallitId", daljaMallitId);
                        await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    cnnLokal.Close();
                }
            }
        }

    }
}
