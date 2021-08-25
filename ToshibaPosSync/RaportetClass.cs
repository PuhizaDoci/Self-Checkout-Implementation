using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Reporting.WinForms;
using ToshibaPos.SDK;

namespace ToshibaPosSinkronizimi
{
    public class RaportetClass
    {

        public static class ConfigurationData
        {


            static string _gjuha, _email, _smtp, _userName, _pass;
            static int _port;

            public static string Gjuha { get { return _gjuha; } }
            public static string Email { get { return _email; } }
            public static string Smtp { get { return _smtp; } }
            public static int Port { get { return _port; } }
            public static string UserName { get { return _userName; } }
            public static string Pass { get { return _pass; } }

        }
        public static DataTable GetShitjaMeDetale(long daljaId)
        {
            DataTable shitjaERuajtur = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.DaljaMallitDetaleRaport_Sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DaljaId", daljaId);
            da.Fill(shitjaERuajtur);
            return shitjaERuajtur;
        }
        public static DataTable GetRaportin(string emriRaportitRDLC)
        {
            DataTable t = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.RaportetFajllatSelect_Sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Pershkrimi", emriRaportitRDLC);
            da.Fill(t);
            return t;
        }
 
        public static DataTable GetZonatOrientueseReport()
        {
            DataTable tabela = new DataTable();
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.Reg_ZonatOrientueseSelect_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetDaljaMallitPorosiaReport(long? PorosiaId)
        {
            DataTable tabela = new DataTable();
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.DaljaMallitPorosiaReport_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@PorosiaId", PorosiaId);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetProFormaExportReport(long? Id)
        {
            DataTable tabela = new DataTable();
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportProFormEksportSelect_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DaljaId", Id);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetLlagerListaImport(DataTable tabela, int subjektiId, int? organizataId, bool? statusi, DateTime? prej, DateTime? deri)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportLlagerListaPerImport_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@DataPrej", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DataDeri", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@SubjektiId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@Statusi", SqlDbType.Bit);

            if (prej.HasValue)
                da.SelectCommand.Parameters["@DataPrej"].Value = prej;
            else
                da.SelectCommand.Parameters["@DataPrej"].Value = DBNull.Value;

            if (deri.HasValue)
                da.SelectCommand.Parameters["@DataDeri"].Value = deri;
            else
                da.SelectCommand.Parameters["@DataDeri"].Value = DBNull.Value;

            if (subjektiId != 0)
                da.SelectCommand.Parameters["@SubjektiId"].Value = subjektiId;
            else
                da.SelectCommand.Parameters["@SubjektiId"].Value = DBNull.Value;


            if (organizataId != 0)
                da.SelectCommand.Parameters["@OrganizataId"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;

            if (statusi != null)
                da.SelectCommand.Parameters["@Statusi"].Value = statusi;
            else
                da.SelectCommand.Parameters["@Statusi"].Value = DBNull.Value;


            da.Fill(tabela);
            return tabela;
        }
        public static DataTable GetPagesatPerBankeReport_RS(DataTable tabela, int? Id, int? OrganizataId, int? DepartamentiId, DateTime? DataPrej, DateTime? DataDeri, int? Muaji, int? Viti, DataTable XhirollogariteEZgjedhura)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.Report_Rh_PagesatPerBank_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@PagesatId", Id);
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", OrganizataId);
            da.SelectCommand.Parameters.AddWithValue("@DepartamentiId", DepartamentiId);
            da.SelectCommand.Parameters.AddWithValue("@DataPrej", DataPrej);
            da.SelectCommand.Parameters.AddWithValue("@DataDeri", DataDeri);
            da.SelectCommand.Parameters.AddWithValue("@Muaji", Muaji);
            da.SelectCommand.Parameters.AddWithValue("@Viti", Viti);
            da.SelectCommand.Parameters.AddWithValue("@XhirollogaritEZgjedhura", XhirollogariteEZgjedhura);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetQarkullimiSipasOreve(DataTable tabela, int? organizataId, DateTime? prej)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportShitjaSipasOreve_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Data", prej);
            da.SelectCommand.Parameters.AddWithValue("@Organizata", organizataId);

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetFinTransferiDokumenteve(DataTable tabela, long Id)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.FinTransferiDokumenteveSelect_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Id", Id);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetFinTransferiDokumenteveDetale(DataTable tabela, long TransferiDokumentitId)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.FinTransferiDokumenteveDetaleReport_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@TransferiDokumentitId", TransferiDokumentitId);
            da.Fill(tabela);
            return tabela;
        }



        public static DataTable GetHyrjaMallitSintetikeShumaDetaleveReport(int? SubjektiId, int? OrganizataId, int? Viti, int? LlojiDokumentitId, bool? validuar, DateTime? PrejDates, DateTime? DeriMeDaten)
        {
            DataTable tabela = new DataTable();
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.HyrjaMallitSintetikeShumaDetaleveReport_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@SubjektiId", SubjektiId);
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", OrganizataId);
            da.SelectCommand.Parameters.AddWithValue("@Viti", Viti);
            da.SelectCommand.Parameters.AddWithValue("@LlojidokumentitID", LlojiDokumentitId);
            da.SelectCommand.Parameters.AddWithValue("@Validuar", validuar);
            da.SelectCommand.Parameters.AddWithValue("@PrejDates", PrejDates);
            da.SelectCommand.Parameters.AddWithValue("@DeriMeDaten", DeriMeDaten);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetKontratatShumeId(DataTable tabela, string listaartikulliid, long? Id, int? LlojiIKontrates, int? Departamenti, int? Banka, int? PersoneliId, int? PozitaId, int? PageFikse)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.Rh_KontrataReport_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@RreshtiIIdPerSHfletim", listaartikulliid == "" ? null : listaartikulliid);
            da.SelectCommand.Parameters.AddWithValue("@Id", Id);
            da.SelectCommand.Parameters.AddWithValue("@LlojiIKontrates", LlojiIKontrates);
            da.SelectCommand.Parameters.AddWithValue("@Departamenti", Departamenti);
            da.SelectCommand.Parameters.AddWithValue("@Banka", Banka);
            da.SelectCommand.Parameters.AddWithValue("@PersoneliId", PersoneliId);
            da.SelectCommand.Parameters.AddWithValue("@PozitaId", PozitaId);
            da.SelectCommand.Parameters.AddWithValue("@PageFikse", PageFikse);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetQarkullimiTotalSipasMenyresSePagesesReport(int? OrganizataId, DateTime? PrejDates, DateTime? DeriMeDaten, int? ShifraOperatorit)
        {
            DataTable tabela = new DataTable();
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.QarkullimiSipasOperaoritdheMenyrespageses_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", OrganizataId);
            da.SelectCommand.Parameters.AddWithValue("@PrejDates", PrejDates);
            da.SelectCommand.Parameters.AddWithValue("@DeriMeDaten", DeriMeDaten);
            da.SelectCommand.Parameters.AddWithValue("@ShifraOperatorit", ShifraOperatorit);
            da.Fill(tabela);
            return tabela;
        }
        public static DataTable GetQarkullimiSipasFilialaveCross_DataType(DataTable tabela, DataTable dtOrganizata, DataTable dtGrupi, DateTime prej, DateTime deri,
            DataTable dtBrendi, bool? SipasBrenditOseGrupit, bool QarkullimiPaTvsh)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.DaljaMallitSipasFilialave_DT_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Organizatat", dtOrganizata);
            da.SelectCommand.Parameters.AddWithValue("@GrupiMallit", dtGrupi);
            da.SelectCommand.Parameters.AddWithValue("@Brendi", dtBrendi);
            da.SelectCommand.Parameters.Add("@QarkullimiPaTVSH", SqlDbType.Bit);

            da.SelectCommand.Parameters.Add("@DataPrej", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DataDeri", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@SipasBrenditOseGrupit", SqlDbType.Int);

            da.SelectCommand.Parameters["@SipasBrenditOseGrupit"].Value = SipasBrenditOseGrupit;

            da.SelectCommand.Parameters["@DataPrej"].Value = prej;
            da.SelectCommand.Parameters["@DataDeri"].Value = deri;
            da.SelectCommand.Parameters["@QarkullimiPaTVSH"].Value = QarkullimiPaTvsh;

            da.SelectCommand.CommandTimeout = 6000;
            da.Fill(tabela);
            return tabela;
        }
        public static DataTable GetQarkullimiSipasFilialaveCross(DataTable tabela, Int32? organizataId, Int64? grupiId, DateTime prej, DateTime deri, Int32? BrendiId,
            bool? SipasBrenditOseGrupit, bool QarkullimiPaTvsh)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.QarkullimiSipasFilialave_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@DataPrej", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DataDeri", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@GrupiMallitId", SqlDbType.BigInt);
            da.SelectCommand.Parameters.Add("@BrendiId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@SipasBrenditOseGrupit", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@QarkullimiPaTvsh", SqlDbType.Bit);

            da.SelectCommand.Parameters["@OrganizataId"].Value = organizataId;
            da.SelectCommand.Parameters["@SipasBrenditOseGrupit"].Value = SipasBrenditOseGrupit;


            if (grupiId != 0)
                da.SelectCommand.Parameters["@GrupiMallitId"].Value = grupiId;
            else
                da.SelectCommand.Parameters["@GrupiMallitId"].Value = DBNull.Value;


            da.SelectCommand.Parameters["@BrendiId"].Value = BrendiId;


            da.SelectCommand.Parameters["@DataPrej"].Value = prej;
            da.SelectCommand.Parameters["@DataDeri"].Value = deri;
            da.SelectCommand.Parameters["@QarkullimiPaTvsh"].Value = QarkullimiPaTvsh;
            da.SelectCommand.CommandTimeout = 6000;
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetDaljaMallitDetaleSipasBleresitReport(DataTable tabela, int? organizataId, Int64? grupiId
            , DateTime? prej, DateTime? deri, int? BrendiId, int? ArtikulliId, decimal? RabatiPrej
                , decimal? RabatiDeri, int? BleresiId)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.DaljaMallitDetaleSipasBleresitReport_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", organizataId);
            da.SelectCommand.Parameters.AddWithValue("@SubjektiId", BleresiId);
            da.SelectCommand.Parameters.AddWithValue("@ArtikulliId", ArtikulliId);
            da.SelectCommand.Parameters.AddWithValue("@RabatiPrej", RabatiPrej);
            da.SelectCommand.Parameters.AddWithValue("@RabatiDeri", RabatiDeri);
            da.SelectCommand.Parameters.AddWithValue("@BrendiId", BrendiId);
            da.SelectCommand.Parameters.AddWithValue("@GrupiMallitId", grupiId);
            da.SelectCommand.Parameters.AddWithValue("@DataPrej", prej);
            da.SelectCommand.Parameters.AddWithValue("@DataDeri", deri);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetStokuHyrjetDaljetDetaleReport(DataTable tabela, int? organizataId, Int64? grupiId
        , DateTime? prej, DateTime? deri, int? BrendiId, int? ArtikulliId, bool? ShfaqeKolonenPerQmimTeShitjes)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.StokuHyrjaDaljaReport_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", organizataId);
            da.SelectCommand.Parameters.AddWithValue("@ArtikulliId", ArtikulliId);
            da.SelectCommand.Parameters.AddWithValue("@BrendiId", BrendiId);
            da.SelectCommand.Parameters.AddWithValue("@GrupiMallitId", grupiId);
            da.SelectCommand.Parameters.AddWithValue("@DataPrej", prej);
            da.SelectCommand.Parameters.AddWithValue("@DataDeri", deri);
            da.SelectCommand.Parameters.AddWithValue("@ShfaqeKolonenPerQmimTeShitjes", ShfaqeKolonenPerQmimTeShitjes);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetStokuDetaleMeNumraSerikReport(DataTable tabela, int? organizataId, Int64? grupiId
            , DateTime? prej, DateTime? deri, int? BrendiId, int? ArtikulliId, DateTime? DataSkadimit)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.StokuMeNumraSerikDetaleReport_Sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", organizataId);
            da.SelectCommand.Parameters.AddWithValue("@ArtikulliId", ArtikulliId);
            da.SelectCommand.Parameters.AddWithValue("@BrendiId", BrendiId);
            da.SelectCommand.Parameters.AddWithValue("@GrupiMallitId", grupiId);
            da.SelectCommand.Parameters.AddWithValue("@DataPrej", prej);
            da.SelectCommand.Parameters.AddWithValue("@DataDeri", deri);
            da.SelectCommand.Parameters.AddWithValue("@DataSkadimit", DataSkadimit);

            da.Fill(tabela);
            return tabela;
        }
        public static DataTable GetKartelaSipasArtikullitdheNumraveSerikReport(DataTable tabela, int? organizataId, Int64? grupiId
            , DateTime? prej, DateTime? deri, int? BrendiId, int? ArtikulliId, string NumriSerik)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.KartelaSipasNumraveSerikDetaleReport_Sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", organizataId);
            da.SelectCommand.Parameters.AddWithValue("@ArtikulliId", ArtikulliId);
            da.SelectCommand.Parameters.AddWithValue("@BrendiId", BrendiId);
            da.SelectCommand.Parameters.AddWithValue("@GrupiMallitId", grupiId);
            da.SelectCommand.Parameters.AddWithValue("@DataPrej", prej);
            da.SelectCommand.Parameters.AddWithValue("@DataDeri", deri);
            da.SelectCommand.Parameters.AddWithValue("@NumriSerik", NumriSerik == "" ? null : NumriSerik);
            da.Fill(tabela);
            return tabela;
        }
        public static DataTable QarkullimiSipasPeshoresReport(int? organizataId, Int64? grupiId, DateTime? prej, DateTime? deri, int? SubjektiId, int? PeshorjaId)
        {
            DataTable t = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportQarkullimiSipasPeshores_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", organizataId);
            da.SelectCommand.Parameters.AddWithValue("@PeshorjaId", PeshorjaId == 0 ? null : PeshorjaId);
            da.SelectCommand.Parameters.AddWithValue("@SubjektiId", SubjektiId == 0 ? null : SubjektiId);
            //da.SelectCommand.Parameters.AddWithValue("@GrupiMallitId", grupiId);
            da.SelectCommand.Parameters.AddWithValue("@DataPrej", prej);
            da.SelectCommand.Parameters.AddWithValue("@DataDeri", deri);
            da.Fill(t);
            return t;
        }

        public static DataTable GetProdhimiArtikujtEProdhuarReport(DataTable tabela, int? organizataId, Int64? grupiId
                , DateTime? prej, DateTime? deri, int? BrendiId, int? ArtikulliId)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ProdhimiILendesSePareReport_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", organizataId);
            da.SelectCommand.Parameters.AddWithValue("@ArtikulliId", ArtikulliId);
            da.SelectCommand.Parameters.AddWithValue("@BrendiId", BrendiId);
            da.SelectCommand.Parameters.AddWithValue("@GrupiMallitId", grupiId);
            da.SelectCommand.Parameters.AddWithValue("@DataPrej", prej);
            da.SelectCommand.Parameters.AddWithValue("@DataDeri", deri);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetProdhimiIArtikujtEProdhuarDheShitjaReport(DataTable tabela, int? organizataId, Int64? grupiId
                , DateTime? prej, DateTime? deri, int? BrendiId, int? ArtikulliId)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ProdhimiIArtikujtEProdhuarDheShitjaReport_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", organizataId);
            da.SelectCommand.Parameters.AddWithValue("@ArtikulliId", ArtikulliId);
            da.SelectCommand.Parameters.AddWithValue("@BrendiId", BrendiId);
            da.SelectCommand.Parameters.AddWithValue("@GrupiMallitId", grupiId);
            da.SelectCommand.Parameters.AddWithValue("@DataPrej", prej);
            da.SelectCommand.Parameters.AddWithValue("@DataDeri", deri);
            da.Fill(tabela);
            return tabela;
        }
        public static DataTable GetKoShitjaSipasProjekteve(int? OrganizataId)
        {
            DataTable tabela = new DataTable();
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.KOShitjetReport_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", OrganizataId);
            da.Fill(tabela);
            return tabela;
        }
        public static DataTable GetKartelaArtikullitReport(int? OrganizataId, DateTime? prej, DateTime? deri, int? ArtikulliId, string Lloji = "")
        {
            //DbCommand Comm = GenericDataAccess.CreateCommand();
            //Comm.CommandText = "[dbo].[KartelaEArtikullitReport_sp]";
            //SqlParameter ORganizataId_P = new SqlParameter("@OrganizataId", OrganizataId);
            //SqlParameter ArtikulliId_P = new SqlParameter("@ArtikulliId", ArtikulliId);
            //SqlParameter PrejDates_P = new SqlParameter("@PrejDates", prej);
            //SqlParameter DeriMeDaten_P = new SqlParameter("@DeriMeDaten", deri);
            //SqlParameter Lloji_P = new SqlParameter("@Lloji", Lloji);
            //Comm.Parameters.Add(ORganizataId_P);
            //Comm.Parameters.Add(ArtikulliId_P);
            //Comm.Parameters.Add(PrejDates_P);
            //Comm.Parameters.Add(DeriMeDaten_P);
            //Comm.Parameters.Add(Lloji_P);
            //return GenericDataAccess.ExecuteSelectCommand(Comm);


            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("dbo.KartelaEArtikullitReport_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@PrejDates", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DerimeDaten", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@ArtikulliId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@Lloji", SqlDbType.VarChar);

            if (OrganizataId != 0)
                da.SelectCommand.Parameters["@OrganizataId"].Value = OrganizataId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;

            if (ArtikulliId != 0)
                da.SelectCommand.Parameters["@ArtikulliId"].Value = ArtikulliId;
            else
                da.SelectCommand.Parameters["@ArtikulliId"].Value = DBNull.Value;

            if (Lloji != "")
                da.SelectCommand.Parameters["@Lloji"].Value = Lloji;
            else
                da.SelectCommand.Parameters["@Lloji"].Value = DBNull.Value;


            da.SelectCommand.Parameters["@PrejDates"].Value = prej;
            da.SelectCommand.Parameters["@DerimeDaten"].Value = deri;

            da.Fill(tabela);
            return tabela;


        }

        public static DataTable GetDokumentetEHapura()
        {
            DataTable tabela = new DataTable();
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.Reg_DokumentetQeDuhetTeMbyllen", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.Fill(tabela);
            return tabela;
        }
        public static DataTable ShitjaSipasAgjentit(DataTable tabela, Int32 organizataId, Int32 AgjentiID, DateTime prej, DateTime deri)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.DaljaMallitSipasAgjentit_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@PrejDates", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DerimeDaten", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@AgjentiShitjesID", SqlDbType.Int);

            if (organizataId != 0)
                da.SelectCommand.Parameters["@OrganizataId"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;

            if (AgjentiID != 0)
                da.SelectCommand.Parameters["@AgjentiShitjesID"].Value = AgjentiID;
            else
                da.SelectCommand.Parameters["@AgjentiShitjesID"].Value = DBNull.Value;



            da.SelectCommand.Parameters["@PrejDates"].Value = prej;
            da.SelectCommand.Parameters["@DerimeDaten"].Value = deri;

            da.SelectCommand.CommandTimeout = 6000;
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetStokuSipasFilialesCross(DataTable tabela, Int32 organizataId, Int32 grupiId, Int32 BrendiId, DateTime prej, DateTime deri)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.QarkullimiSipasFilialaveStoqetTeGjitheArtikujt_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@DataPrej", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DataDeri", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@GrupiMallitId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@BrendiId", SqlDbType.Int);

            if (organizataId != 0)
                da.SelectCommand.Parameters["@OrganizataId"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;

            if (grupiId != 0)
                da.SelectCommand.Parameters["@GrupiMallitId"].Value = grupiId;
            else
                da.SelectCommand.Parameters["@GrupiMallitId"].Value = DBNull.Value;

            if (BrendiId != 0)
                da.SelectCommand.Parameters["@BrendiId"].Value = BrendiId;
            else
                da.SelectCommand.Parameters["@BrendiId"].Value = DBNull.Value;


            da.SelectCommand.Parameters["@DataPrej"].Value = prej;
            da.SelectCommand.Parameters["@DataDeri"].Value = deri;

            da.SelectCommand.CommandTimeout = 6000;
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetOrePuneDitoreReport_Sp(DataTable tabela, string Organizata, int viti, int muaji, int personeliid)
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.Rh_OrePuneDitoreReport_Sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@OrganizataId";
            if (Organizata.ToString() == "")
                param.Value = DBNull.Value;
            else
                param.Value = Organizata;
            da.SelectCommand.Parameters.Add(param);

            SqlParameter param1 = new SqlParameter();
            param1.ParameterName = "@Viti";
            if (viti == 0)
                param1.Value = DBNull.Value;
            else
                param1.Value = viti;
            da.SelectCommand.Parameters.Add(param1);

            SqlParameter param2 = new SqlParameter();
            param2.ParameterName = "@Muaji";
            if (muaji == 0)
                param2.Value = DBNull.Value;
            else
                param2.Value = muaji;
            da.SelectCommand.Parameters.Add(param2);

            SqlParameter param3 = new SqlParameter();
            param3.ParameterName = "@PersoneliId";
            if (personeliid == 0)
                param3.Value = DBNull.Value;
            else
                param3.Value = personeliid;
            da.SelectCommand.Parameters.Add(param3);

            da.Fill(tabela);
            return tabela;
        }


        public static DataTable GetOrePuneTeAkumuluaraMujoreReport(DataTable tabela, int Organizata, int viti, int muaji, int personeliid, int DepartamentiID)
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.OrePuneAkumuluaraMujoreReport_Sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@OrganizataId";
            if (Organizata == 0)
                param.Value = DBNull.Value;
            else
                param.Value = Organizata;
            da.SelectCommand.Parameters.Add(param);

            SqlParameter param1 = new SqlParameter();
            param1.ParameterName = "@Viti";
            if (viti == 0)
                param1.Value = DBNull.Value;
            else
                param1.Value = viti;
            da.SelectCommand.Parameters.Add(param1);

            SqlParameter param2 = new SqlParameter();
            param2.ParameterName = "@Muaji";
            if (muaji == 0)
                param2.Value = DBNull.Value;
            else
                param2.Value = muaji;
            da.SelectCommand.Parameters.Add(param2);

            //SqlParameter param3 = new SqlParameter();
            //param3.ParameterName = "@PersoneliId";
            //if (personeliid == 0)
            //    param3.Value = DBNull.Value;
            //else
            //    param3.Value = personeliid;
            //da.SelectCommand.Parameters.Add(param3);

            SqlParameter param4 = new SqlParameter();
            param4.ParameterName = "@DepartamentiId";
            if (DepartamentiID == 0)
                param4.Value = DBNull.Value;
            else
                param4.Value = DepartamentiID;
            da.SelectCommand.Parameters.Add(param4);

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetOrePuneTeAkumuluaraVjetoreReport(DataTable tabela, int Organizata, int viti, int personeliid, int DepartamentiID)
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.OrePuneAkumuluaraVjetoreReport_Sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@OrganizataId";
            if (Organizata == 0)
                param.Value = DBNull.Value;
            else
                param.Value = Organizata;
            da.SelectCommand.Parameters.Add(param);

            SqlParameter param1 = new SqlParameter();
            param1.ParameterName = "@Viti";
            if (viti == 0)
                param1.Value = DBNull.Value;
            else
                param1.Value = viti;
            da.SelectCommand.Parameters.Add(param1);

            SqlParameter param3 = new SqlParameter();
            param3.ParameterName = "@DepartamentiId";
            if (DepartamentiID == 0)
                param3.Value = DBNull.Value;
            else
                param3.Value = DepartamentiID;
            da.SelectCommand.Parameters.Add(param3);

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetOrePunePerPagesReport_Sp(DataTable tabela, string Organizata, int viti, int muaji, int personeliid)
        {
            SqlDataAdapter da = new SqlDataAdapter("dbo.Rh_OrePunePerPagesReport_Sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@OrganizataId";
            if (Organizata.ToString() == "")
                param.Value = DBNull.Value;
            else
                param.Value = Organizata;
            da.SelectCommand.Parameters.Add(param);

            SqlParameter param1 = new SqlParameter()
            {
                ParameterName = "@Viti"
            };
            if (viti == 0)
                param1.Value = DBNull.Value;
            else
                param1.Value = viti;
            da.SelectCommand.Parameters.Add(param1);

            SqlParameter param2 = new SqlParameter()
            {
                ParameterName = "@Muaji"
            };
            if (muaji == 0)
                param2.Value = DBNull.Value;
            else
                param2.Value = muaji;
            da.SelectCommand.Parameters.Add(param2);

            SqlParameter param3 = new SqlParameter()
            {
                ParameterName = "@PersoneliId"
            };
            if (personeliid == 0)
                param3.Value = DBNull.Value;
            else
                param3.Value = personeliid;
            da.SelectCommand.Parameters.Add(param3);


            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetKartelenIdentifikueseRaport(DataTable tabela, int id)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.Rh_PersoneliSelect_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@Id", SqlDbType.Int);
            da.SelectCommand.Parameters["@Id"].Value = id;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetNdalesatEHapura_AvansReport(DataTable tabela, int avansId)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.Rh_NdalesatEHapura_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@AvansId", SqlDbType.Int);
            da.SelectCommand.Parameters["@AvansId"].Value = avansId;
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetAvansetReport(DataTable tabela, int avansId)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.Rh_Report_Avans_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@AvansId", SqlDbType.Int);
            da.SelectCommand.Parameters["@AvansId"].Value = avansId;
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetKontratenReport(DataTable tabela, int PersoneliId)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.Rh_KontrataSelect_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@PersoneliId", SqlDbType.Int);
            da.SelectCommand.Parameters["@PersoneliId"].Value = PersoneliId;
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetPranimiMallitVerifikoSasineReport(DataTable tabela, long Id)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.PranimiMallitVerifikoSasineReport_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@Id", SqlDbType.BigInt);
            da.SelectCommand.Parameters["@Id"].Value = Id;
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetParashikiminELinjave(DataTable tabela)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ProjektetLinjatSelect_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetNdalesatReport(DataTable tabela, int Id)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.Rh_BonusetNdalesatSelect_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@Id", SqlDbType.Int);
            da.SelectCommand.Parameters["@Id"].Value = Id;
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetPagesatPerBankeReport(DataTable tabela, int Id)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.Report_Rh_PagesatPerBank_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@PagesatId", SqlDbType.Int);
            da.SelectCommand.Parameters["@PagesatId"].Value = Id;
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetPagesatPerBankeSubReport(DataTable tabela, int OrganizataId)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportBankatSubjektet_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@SubjektiId", SqlDbType.Int);
            da.SelectCommand.Parameters["@SubjektiId"].Value = OrganizataId;
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetPushimetReport(DataTable tabela, int Id)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.Rh_PushimetSelect_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@Id", SqlDbType.Int);
            da.SelectCommand.Parameters["@Id"].Value = Id;
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetPersoneliReport(DataTable tabela)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.Rh_PersoneliSelect_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetPagatMujoreReport(DataTable tabela, int OrganizataId, int Muaji, int Viti, int? DepartamentiId)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.Rh_ReportPagesat_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", OrganizataId);
            da.SelectCommand.Parameters.AddWithValue("@Viti", Viti);
            da.SelectCommand.Parameters.AddWithValue("@Muaji", Muaji);
            da.SelectCommand.Parameters.AddWithValue("@DepartamentiId", DepartamentiId);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetPagatVjetoreReport(DataTable tabela, int OrganizataId, int Viti)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.Rh_ReportPagesat_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters["@OrganizataId"].Value = OrganizataId;
            da.SelectCommand.Parameters.Add("@Viti", SqlDbType.Int);
            da.SelectCommand.Parameters["@Viti"].Value = Viti;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetLibrinEDaljesInterneRaport(DataTable tabela, int filialaId, DateTime prej, DateTime deri)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.LibriiDaljeveInterne_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FiljalaId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@PrejDates", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DeriMeDaten", SqlDbType.DateTime);

            if (filialaId != 0)
                da.SelectCommand.Parameters["@FiljalaId"].Value = filialaId;
            else
                da.SelectCommand.Parameters["@FiljalaId"].Value = DBNull.Value;

            da.SelectCommand.Parameters["@PrejDates"].Value = prej;
            da.SelectCommand.Parameters["@DeriMeDaten"].Value = deri;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetFinLibriShitjesPerExcelATK_Sp(DateTime dataPrej, DateTime dataDeri, DataTable Organizatat)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("dbo.FinLibriShitjesPerExcelATK_Sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@PrejDates", dataPrej);
            da.SelectCommand.Parameters.AddWithValue("@DeriMedaten", dataDeri);
            da.SelectCommand.Parameters.AddWithValue("@Organizatat", Organizatat);
            da.Fill(tabela);
            return tabela;
        }
        public static DataTable GetLibrinEShitjesRaport(DataTable dtOrganizatat, DateTime prej, DateTime deri, DataTable dtLlojetETransaksioneve, bool SipasArtikujve, DataTable dtSubjektet)
        {
            DataTable tabela = new DataTable();
            string emriprocedures = "";
            if (!SipasArtikujve)
            {
                emriprocedures = "dbo.FinReportLibriIShitjesiAvancuar";
            }
            else
            {
                emriprocedures = "dbo.FinLibriShitjesDetale_Sp";
            }
            SqlDataAdapter da = new SqlDataAdapter(emriprocedures, PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DataPrej", prej.Date);
            da.SelectCommand.Parameters.AddWithValue("@DataDeri", deri.Date);
            da.SelectCommand.Parameters.AddWithValue("@Organizatat", dtOrganizatat);
            da.SelectCommand.Parameters.AddWithValue("@LlojetETransLibriBlerjes", dtLlojetETransaksioneve);
            da.SelectCommand.Parameters.AddWithValue("@Subjektet", dtSubjektet);
            da.SelectCommand.CommandTimeout = 3000;
            da.Fill(tabela);
            return tabela;
        }


        public static DataTable GetLibrinEBlerjesRaport(DataTable dtOrganizatat, DateTime prej, DateTime deri, DataTable dtLlojetETransaksioneve, bool SipasArtikujve, DataTable Subjektet)
        {
            DataTable tabela = new DataTable();
            string emristoreprocedures = "";
            if (!SipasArtikujve)
            {
                emristoreprocedures = "dbo.FinReportLibriIBlerjesiAvancuar_sp";
            }
            else
            {
                emristoreprocedures = "dbo.FinReportLibriIBlerjesDetale_sp";
            }
            SqlDataAdapter da = new SqlDataAdapter(emristoreprocedures, PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DataPrej", prej.Date);
            da.SelectCommand.Parameters.AddWithValue("@DataDeri", deri.Date);
            da.SelectCommand.Parameters.AddWithValue("@Organizatat", dtOrganizatat);
            da.SelectCommand.Parameters.AddWithValue("@LlojetETransLibriBlerjes", dtLlojetETransaksioneve);
            da.SelectCommand.Parameters.AddWithValue("@Subjektet", Subjektet);
            da.SelectCommand.CommandTimeout = 3000;
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetLibrinEBlerjesMeKontoRaport(DataTable tabela, string filialaId, DateTime prej, DateTime deri, bool LibriNotave, bool LibriMallrave, bool LibriShpenzimeveCH, bool LibriExporteve, bool LibriInvestimeve)
        {
            tabela.Clear();
            //SqlDataAdapter da = new SqlDataAdapter("FinReportLibriIBlerjes", PublicClass.Koneksioni);
            SqlDataAdapter da = new SqlDataAdapter("dbo.FinReportLibriIBlerjesiAvancuarMeKonto_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@OrganizatatShumeId", SqlDbType.VarChar);
            da.SelectCommand.Parameters.Add("@DataPrej", SqlDbType.Date);
            da.SelectCommand.Parameters.Add("@DataDeri", SqlDbType.Date);
            da.SelectCommand.Parameters.Add("@LibriNotave", SqlDbType.Bit);
            da.SelectCommand.Parameters.Add("@LibriMallrave", SqlDbType.Bit);
            da.SelectCommand.Parameters.Add("@LibriShpenzimeve", SqlDbType.Bit);
            da.SelectCommand.Parameters.Add("@LibriExporteve", SqlDbType.Bit);
            da.SelectCommand.Parameters.Add("@LibriInvestimeve", SqlDbType.Bit);

            da.SelectCommand.Parameters["@LibriNotave"].Value = LibriNotave;
            da.SelectCommand.Parameters["@LibriMallrave"].Value = LibriMallrave;
            da.SelectCommand.Parameters["@LibriShpenzimeve"].Value = LibriShpenzimeveCH;
            da.SelectCommand.Parameters["@LibriExporteve"].Value = LibriExporteve;
            da.SelectCommand.Parameters["@LibriInvestimeve"].Value = LibriInvestimeve;
            da.SelectCommand.Parameters["@OrganizatatShumeId"].Value = filialaId;
            da.SelectCommand.Parameters["@DataPrej"].Value = prej.Date;
            da.SelectCommand.Parameters["@DataDeri"].Value = deri.Date;


            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetKompenzimetReport(DataTable tabela, long kompenzimiId)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.FinReportKompenzimet_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@KompenzimiID", SqlDbType.Int);
            da.SelectCommand.Parameters["@KompenzimiID"].Value = kompenzimiId;

            da.Fill(tabela);
            return tabela;
        }



        public static DataTable GetLlagerListaRaport(DataTable tabela, int? subjektiId, long? grupiId, int? organizataId, bool? statusi, int? K16, int? K17, int? K18, int? K19, int? K20)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportLlagerListaSelect_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@Organizata", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@SubjektiId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@Aktive", SqlDbType.Bit);
            da.SelectCommand.Parameters.Add("@ShifraGrupit", SqlDbType.BigInt);
            da.SelectCommand.Parameters.Add("@K16", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@K17", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@K18", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@K19", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@K20", SqlDbType.Int);

            if (subjektiId != null)
                da.SelectCommand.Parameters["@SubjektiId"].Value = subjektiId;
            else
                da.SelectCommand.Parameters["@SubjektiId"].Value = DBNull.Value;

            if (organizataId != null)
                da.SelectCommand.Parameters["@Organizata"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@Organizata"].Value = DBNull.Value;

            if (statusi != null)
                da.SelectCommand.Parameters["@Aktive"].Value = statusi;
            else
                da.SelectCommand.Parameters["@Aktive"].Value = DBNull.Value;

            if (grupiId != 0)
                da.SelectCommand.Parameters["@ShifraGrupit"].Value = grupiId;
            else
                da.SelectCommand.Parameters["@ShifraGrupit"].Value = DBNull.Value;

            if (K16 != 0)
                da.SelectCommand.Parameters["@K16"].Value = K16;
            else
                da.SelectCommand.Parameters["@K16"].Value = DBNull.Value;

            if (K17 != 0)
                da.SelectCommand.Parameters["@K17"].Value = K17;
            else
                da.SelectCommand.Parameters["@K17"].Value = DBNull.Value;

            if (K18 != 0)
                da.SelectCommand.Parameters["@K18"].Value = K18;
            else
                da.SelectCommand.Parameters["@K18"].Value = DBNull.Value;

            if (K19 != 0)
                da.SelectCommand.Parameters["@K19"].Value = K19;
            else
                da.SelectCommand.Parameters["@K19"].Value = DBNull.Value;

            if (K20 != 0)
                da.SelectCommand.Parameters["@K20"].Value = K20;
            else
                da.SelectCommand.Parameters["@K20"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetKOSaldotReport(DataTable tabela, int? organizataId, DateTime? data)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.KOSaldotReport_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@Data", SqlDbType.Date);

            if (organizataId != null)
                da.SelectCommand.Parameters["@OrganizataId"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;

            if (data != null)
                da.SelectCommand.Parameters["@Data"].Value = data;
            else
                da.SelectCommand.Parameters["@Data"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }


        public static DataTable GetRegTopListaEDiferencave(DataTable tabela, int? RegjistrimiID, int? SektoriId, string ShifraGrupit, int? TopLista)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.Reg_Top100MinusSelect_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@RegjistrimiId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@NumriIArtikujve", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@SektoriId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@ShifraGrupit", SqlDbType.VarChar);

            da.SelectCommand.Parameters["@RegjistrimiId"].Value = RegjistrimiID;
            da.SelectCommand.Parameters["@SektoriId"].Value = SektoriId;
            da.SelectCommand.Parameters["@NumriIArtikujve"].Value = TopLista;

            if (ShifraGrupit != "")
                da.SelectCommand.Parameters["@ShifraGrupit"].Value = ShifraGrupit;
            else
                da.SelectCommand.Parameters["@ShifraGrupit"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetCmimorjaMeFotoDataTable(DataTable tabela, int brendiId, int organizataId,
             decimal? mazhaPrej, decimal? mazhaDeri, decimal? stokuPrej, decimal? stokuDeri, int FurnitoriID, DateTime prej
                   , string shifraGrupitMallit, Int32? ArtikulliID)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.CmimorjaMeFotoReport_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.AddWithValue("@GrupiMallitShifra", shifraGrupitMallit);
            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@BrendiId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@MazhaPrej", SqlDbType.Decimal);
            da.SelectCommand.Parameters.Add("@MazhaDeri", SqlDbType.Decimal);
            da.SelectCommand.Parameters.Add("@StokuPrej", SqlDbType.Decimal);
            da.SelectCommand.Parameters.Add("@StokuDeri", SqlDbType.Decimal);
            da.SelectCommand.Parameters.Add("@SubjektiID", SqlDbType.Decimal);
            da.SelectCommand.Parameters.Add("@Data", SqlDbType.Date);
            da.SelectCommand.Parameters.Add("@ArtikulliID", SqlDbType.Int);

            da.SelectCommand.Parameters["@ArtikulliID"].Value = ArtikulliID;
            da.SelectCommand.Parameters["@Data"].Value = prej;

            if (FurnitoriID != 0)
                da.SelectCommand.Parameters["@SubjektiID"].Value = FurnitoriID;
            else
                da.SelectCommand.Parameters["@SubjektiID"].Value = DBNull.Value;

            if (mazhaPrej.HasValue)
                da.SelectCommand.Parameters["@MazhaPrej"].Value = mazhaPrej;
            else
                da.SelectCommand.Parameters["@MazhaPrej"].Value = DBNull.Value;

            if (mazhaDeri.HasValue)
                da.SelectCommand.Parameters["@MazhaDeri"].Value = mazhaDeri;
            else
                da.SelectCommand.Parameters["@MazhaDeri"].Value = DBNull.Value;


            if (stokuPrej.HasValue)
                da.SelectCommand.Parameters["@StokuPrej"].Value = stokuPrej;
            else
                da.SelectCommand.Parameters["@StokuPrej"].Value = DBNull.Value;

            if (stokuDeri.HasValue)
                da.SelectCommand.Parameters["@StokuDeri"].Value = stokuDeri;
            else
                da.SelectCommand.Parameters["@StokuDeri"].Value = DBNull.Value;

            if (organizataId != 0)
                da.SelectCommand.Parameters["@OrganizataId"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;

            if (brendiId != 0)
                da.SelectCommand.Parameters["@BrendiId"].Value = brendiId;
            else
                da.SelectCommand.Parameters["@BrendiId"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }


        public static DataTable GetStoqetMomentaleRaport(DataTable tabela, int brendiId, int organizataId,
      decimal? mazhaPrej, decimal? mazhaDeri, decimal? stokuPrej, decimal? stokuDeri, int FurnitoriID, DateTime prej
            , string shifraGrupitMallit, Int32? ArtikulliID)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportStoqetMomentaleSelect_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = 60 * 5;

            da.SelectCommand.Parameters.AddWithValue("@GrupiMallitShifra", shifraGrupitMallit);
            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@BrendiId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@MazhaPrej", SqlDbType.Decimal);
            da.SelectCommand.Parameters.Add("@MazhaDeri", SqlDbType.Decimal);
            da.SelectCommand.Parameters.Add("@StokuPrej", SqlDbType.Decimal);
            da.SelectCommand.Parameters.Add("@StokuDeri", SqlDbType.Decimal);
            da.SelectCommand.Parameters.Add("@SubjektiID", SqlDbType.Decimal);
            da.SelectCommand.Parameters.Add("@Data", SqlDbType.Date);
            da.SelectCommand.Parameters.Add("@ArtikulliID", SqlDbType.Int);

            da.SelectCommand.Parameters["@ArtikulliID"].Value = ArtikulliID;
            da.SelectCommand.Parameters["@Data"].Value = prej;

            if (FurnitoriID != 0)
                da.SelectCommand.Parameters["@SubjektiID"].Value = FurnitoriID;
            else
                da.SelectCommand.Parameters["@SubjektiID"].Value = DBNull.Value;

            if (mazhaPrej.HasValue)
                da.SelectCommand.Parameters["@MazhaPrej"].Value = mazhaPrej;
            else
                da.SelectCommand.Parameters["@MazhaPrej"].Value = DBNull.Value;

            if (mazhaDeri.HasValue)
                da.SelectCommand.Parameters["@MazhaDeri"].Value = mazhaDeri;
            else
                da.SelectCommand.Parameters["@MazhaDeri"].Value = DBNull.Value;


            if (stokuPrej.HasValue)
                da.SelectCommand.Parameters["@StokuPrej"].Value = stokuPrej;
            else
                da.SelectCommand.Parameters["@StokuPrej"].Value = DBNull.Value;

            if (stokuDeri.HasValue)
                da.SelectCommand.Parameters["@StokuDeri"].Value = stokuDeri;
            else
                da.SelectCommand.Parameters["@StokuDeri"].Value = DBNull.Value;

            if (organizataId != 0)
                da.SelectCommand.Parameters["@OrganizataId"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;

            if (brendiId != 0)
                da.SelectCommand.Parameters["@BrendiId"].Value = brendiId;
            else
                da.SelectCommand.Parameters["@BrendiId"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }


        public static DataTable GetShitjetsipasMazhesRaport(DataTable tabela, int brendiId, int organizataId,
     decimal? mazhaPrej, decimal? mazhaDeri, DateTime prej, DateTime Deri, long? GrupiMallitId)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.DaljaMallitShitjasipasMazhes_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.AddWithValue("@GrupiMallitId", GrupiMallitId);
            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@BrendiId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@MazhaPrej", SqlDbType.Decimal);
            da.SelectCommand.Parameters.Add("@MazhaDeri", SqlDbType.Decimal);
            da.SelectCommand.Parameters.Add("@PrejDates", SqlDbType.Date);
            da.SelectCommand.Parameters.Add("@DeriMeDaten", SqlDbType.Date);

            da.SelectCommand.Parameters["@PrejDates"].Value = prej;
            da.SelectCommand.Parameters["@DeriMeDaten"].Value = Deri;

            if (mazhaPrej.HasValue)
                da.SelectCommand.Parameters["@MazhaPrej"].Value = mazhaPrej;
            else
                da.SelectCommand.Parameters["@MazhaPrej"].Value = DBNull.Value;

            if (mazhaDeri.HasValue)
                da.SelectCommand.Parameters["@MazhaDeri"].Value = mazhaDeri;
            else
                da.SelectCommand.Parameters["@MazhaDeri"].Value = DBNull.Value;


            if (organizataId != 0)
                da.SelectCommand.Parameters["@OrganizataId"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;

            if (brendiId != 0)
                da.SelectCommand.Parameters["@BrendiId"].Value = brendiId;
            else
                da.SelectCommand.Parameters["@BrendiId"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }


        public static DataTable GetStoqetMinimaleRaport(DataTable tabela, int brendiId, int organizataId, int FurnitoriID, int GrupiID)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.StoqetMinimale_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@BrendiId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@GrupiID", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@SubjektiID", SqlDbType.Int);

            if (FurnitoriID != 0)
                da.SelectCommand.Parameters["@SubjektiID"].Value = FurnitoriID;
            else
                da.SelectCommand.Parameters["@SubjektiID"].Value = DBNull.Value;

            if (organizataId != 0)
                da.SelectCommand.Parameters["@OrganizataId"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;

            if (brendiId != 0)
                da.SelectCommand.Parameters["@BrendiId"].Value = brendiId;
            else
                da.SelectCommand.Parameters["@BrendiId"].Value = DBNull.Value;

            if (GrupiID != 0)
                da.SelectCommand.Parameters["@GrupiID"].Value = GrupiID;
            else
                da.SelectCommand.Parameters["@GrupiID"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetAfatiPagesesRaport(DataTable tabela, int subjektiId, DateTime prejDates, DateTime deriMeDaten)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.FinPagesatSipasDatesReport_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@SubjektiId", SqlDbType.Int);

            if (subjektiId != 0)
                da.SelectCommand.Parameters["@SubjektiId"].Value = subjektiId;
            else
                da.SelectCommand.Parameters["@SubjektiId"].Value = DBNull.Value;

            da.SelectCommand.Parameters.Add("@PrejDates", SqlDbType.DateTime);
            da.SelectCommand.Parameters["@PrejDates"].Value = prejDates;
            da.SelectCommand.Parameters.Add("@DeriMeDaten", SqlDbType.DateTime);
            da.SelectCommand.Parameters["@DeriMeDaten"].Value = deriMeDaten;
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetAmortiziminAnalitikRaport(DataTable tabela, int filialaId, DateTime data)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.FinReportAmortizimiPaisjeveAnalitike_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FiljalaId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@Data", SqlDbType.DateTime);

            if (filialaId != 0)
                da.SelectCommand.Parameters["@FiljalaId"].Value = filialaId;
            else
                da.SelectCommand.Parameters["@FiljalaId"].Value = DBNull.Value;


            da.SelectCommand.Parameters["@Data"].Value = data;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetListaEAseteveSipasViteveRaport(DataTable tabela)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ListaeAseteveSipasViteve_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetBilancinEGjendjes(DataTable tabela, DateTime prej, DateTime deri, int OrganizataID)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.FinBilanciGjendjesReport_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandTimeout = 300;

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@Prejdates", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DerimeDaten", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@OrganizataID", SqlDbType.Int);

            if (OrganizataID != 0)
                da.SelectCommand.Parameters["@OrganizataID"].Value = OrganizataID;
            else
                da.SelectCommand.Parameters["@OrganizataID"].Value = DBNull.Value;

            da.SelectCommand.Parameters["@Prejdates"].Value = prej;
            da.SelectCommand.Parameters["@DerimeDaten"].Value = deri;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetBilancinEGjendjesMujore(DataTable tabela, DateTime prej, DateTime deri, int OrganizataID)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.FinBilanciGjendjesMujorReport_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandTimeout = 300;

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@Prejdates", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DerimeDaten", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@OrganizataID", SqlDbType.Int);

            if (OrganizataID != 0)
                da.SelectCommand.Parameters["@OrganizataID"].Value = OrganizataID;
            else
                da.SelectCommand.Parameters["@OrganizataID"].Value = DBNull.Value;

            da.SelectCommand.Parameters["@Prejdates"].Value = prej;
            da.SelectCommand.Parameters["@DerimeDaten"].Value = deri;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetBilancinESuksesit(DataTable tabela, DateTime prej, DateTime deri, DataTable dtOrganizatat)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.FinBilanciSuksesit_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@Prejdates", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DerimeDaten", SqlDbType.DateTime);
            da.SelectCommand.Parameters.AddWithValue("@Organizatat", dtOrganizatat);
            da.SelectCommand.Parameters["@Prejdates"].Value = prej;
            da.SelectCommand.Parameters["@DerimeDaten"].Value = deri;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetBilancinESuksesitAnalitik(DataTable tabela, DateTime prej, DateTime deri, DataTable dtOrganizatat)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.FinBilanciSuksesitAnalitik_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@Prejdates", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DerimeDaten", SqlDbType.DateTime);

            da.SelectCommand.Parameters.AddWithValue("@Organizatat", dtOrganizatat);

            da.SelectCommand.Parameters["@Prejdates"].Value = prej;
            da.SelectCommand.Parameters["@DerimeDaten"].Value = deri;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetBilancinESuksesitAnalitik2(DataTable tabela, DateTime prej, DateTime deri, DataTable dtOrganizatat)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.FinBilanciSuksesitAnalitikVer2_sp", PublicClass.Koneksioni);

            DataTable dtOrg = new DataTable();
            dtOrg = dtOrganizatat.Clone();
            foreach (DataRow item in dtOrganizatat.Rows)
            {
                string a = item["Statusi"].ToString();
                if (item["Statusi"].ToString() == "True")
                    dtOrg.ImportRow(item);
            }
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@Prejdates", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DerimeDaten", SqlDbType.DateTime);
            da.SelectCommand.Parameters.AddWithValue("@Organizatat", dtOrg);

            da.SelectCommand.Parameters["@Prejdates"].Value = prej;
            da.SelectCommand.Parameters["@DerimeDaten"].Value = deri;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetBilancinESuksesitAnalitikMujor(DataTable tabela, DateTime prej, DateTime deri, DataTable dtOrganizatat)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.FinBilanciSuksesitAnalitiMujor_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@Prejdates", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DerimeDaten", SqlDbType.DateTime);
            da.SelectCommand.Parameters.AddWithValue("@Organizatat", dtOrganizatat);

            da.SelectCommand.Parameters["@Prejdates"].Value = prej;
            da.SelectCommand.Parameters["@DerimeDaten"].Value = deri;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetNdalesatBonusetRaport(DataTable tabela, int Id)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.Rh_NdalesatBonusetReport_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@Id", SqlDbType.Int);

            if (Id != 0)
                da.SelectCommand.Parameters["@Id"].Value = Id;
            else
                da.SelectCommand.Parameters["@Id"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }



        public static DataTable GetKushtetEBlersve(DataTable tabela, int subjektiId)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportDaljaMallitKushteEBleresit_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@SubjektiId", SqlDbType.Int);

            if (subjektiId != 0)
                da.SelectCommand.Parameters["@SubjektiId"].Value = subjektiId;
            else
                da.SelectCommand.Parameters["@SubjektiId"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetBilancinEGjendjesAnalitike(DataTable tabela, int filialaId, DateTime prej, DateTime deri)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.FinBilanciGjendjesAnalitikReport_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@PrejDates", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DeriMeDaten", SqlDbType.DateTime);

            if (filialaId != 0)
                da.SelectCommand.Parameters["@OrganizataId"].Value = filialaId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;


            da.SelectCommand.Parameters["@PrejDates"].Value = prej;
            da.SelectCommand.Parameters["@DeriMeDaten"].Value = deri;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetBilanciVertetues(DataTable tabela, DataTable Organizatat, DateTime prej, DateTime deri)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.FinBrutoBilanciReport_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Organizatat", Organizatat);
            da.SelectCommand.Parameters.AddWithValue("@PrejDates", prej);
            da.SelectCommand.Parameters.AddWithValue("@DeriMeDaten", deri);
            da.SelectCommand.CommandTimeout = 6000;
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetKartelaEKontos(DataTable tabela, int? filialaId, DateTime? prej, DateTime? deri, string shifra)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.FinKartelaKontosReport_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Filiala", filialaId);
            da.SelectCommand.Parameters.AddWithValue("@PrejDates", prej);
            da.SelectCommand.Parameters.AddWithValue("@DerimeDaten", deri);
            da.SelectCommand.Parameters.AddWithValue("@KontoShifra", shifra);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetLibriMadh(DataTable tabela, int filialaId, DateTime prej, DateTime deri)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.FinLibriMadhReport_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = 1000;
            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@PrejDates", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DeriMeDaten", SqlDbType.DateTime);

            if (filialaId != 0)
                da.SelectCommand.Parameters["@OrganizataId"].Value = filialaId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;


            da.SelectCommand.Parameters["@PrejDates"].Value = prej;
            da.SelectCommand.Parameters["@DeriMeDaten"].Value = deri;


            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetKontoKalimtareKontrollo(DataTable tabela, int kontoId, DateTime prej, DateTime deri)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.FinKontotKalimtareKontrollo_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@PrejDates", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DeriMeDaten", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@KontoId", SqlDbType.Int);

            if (kontoId != 0)
                da.SelectCommand.Parameters["@KontoId"].Value = kontoId;
            else
                da.SelectCommand.Parameters["@KontoId"].Value = DBNull.Value;

            da.SelectCommand.Parameters["@PrejDates"].Value = prej;
            da.SelectCommand.Parameters["@DeriMeDaten"].Value = deri;
            da.SelectCommand.CommandTimeout = 6000;
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetLibrinEInvestimeveRaport(DataTable tabela, int filialaId, DateTime prej, DateTime deri, bool smartFin)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.FinReportLibriIInvestimeve_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FilialaId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@DataPrej", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DataDeri", SqlDbType.DateTime);
            //da.SelectCommand.Parameters.Add("@KuBITFin", SqlDbType.Bit);

            if (filialaId != 0)
                da.SelectCommand.Parameters["@FilialaId"].Value = filialaId;
            else
                da.SelectCommand.Parameters["@FilialaId"].Value = DBNull.Value;


            da.SelectCommand.Parameters["@DataPrej"].Value = prej;
            da.SelectCommand.Parameters["@DataDeri"].Value = deri;
            //da.SelectCommand.Parameters["@KuBITFin"].Value = smartFin;

            da.Fill(tabela);
            return tabela;
        }


        public static DataTable GetLibrinEShpenzimeveRaport(DataTable tabela, int filialaId, DateTime prej, DateTime deri, bool smartFin)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.FinReportLibriIShpenzimeve_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FilialaId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@DataPrej", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DataDeri", SqlDbType.DateTime);
            //da.SelectCommand.Parameters.Add("@KuBITFin", SqlDbType.Bit);

            if (filialaId != 0)
                da.SelectCommand.Parameters["@FilialaId"].Value = filialaId;
            else
                da.SelectCommand.Parameters["@FilialaId"].Value = DBNull.Value;


            da.SelectCommand.Parameters["@DataPrej"].Value = prej;
            da.SelectCommand.Parameters["@DataDeri"].Value = deri;
            //da.SelectCommand.Parameters["@KuBITFin"].Value = smartFin;

            da.Fill(tabela);
            return tabela;
        }


        public static DataTable GetPasqyratEBilancitTeGjendjes(DataTable tabela, DateTime dataPrej, DateTime dataDeri)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.FinPasqyraEBilancitTeGjendjes_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@PrejDates", SqlDbType.DateTime);
            da.SelectCommand.Parameters["@PrejDates"].Value = dataPrej;

            da.SelectCommand.Parameters.Add("@DeriMeDaten", SqlDbType.DateTime);
            da.SelectCommand.Parameters["@DeriMeDaten"].Value = dataDeri;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetKartelenESubjektit(DataTable tabela, int filialaId, int subjektiId,
          int kontoId, DateTime prej, DateTime deri, bool kartelasipaskesteve, bool KartelaMeValutBlerese, bool SipasArtikujve, bool SipasDatesSeFatures, bool paraqitGjendjenFillestare)
        {
            string Procedura = "dbo.FinKartelaESubjekteveReport_sp";
            tabela.Clear();
            if (SipasArtikujve)
                Procedura = "dbo.FinKartelaESubjekteveSipasArtikujveReport_sp";

            SqlDataAdapter da = new SqlDataAdapter(Procedura, PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FilialaId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@DataPrej", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DataDeri", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@SubjektiId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@KontoId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@KartelaMeValuteBlerese", SqlDbType.Bit);
            da.SelectCommand.Parameters.Add("@KartelaSipasKesteve", SqlDbType.Bit);
            da.SelectCommand.Parameters.Add("@ParaqitGjendjenFillestare", SqlDbType.Bit);
            da.SelectCommand.Parameters.AddWithValue("@SipasDatesSeFatures", SipasDatesSeFatures);

            if (filialaId != 0)
                da.SelectCommand.Parameters["@FilialaId"].Value = filialaId;
            else
                da.SelectCommand.Parameters["@FilialaId"].Value = DBNull.Value;

            if (subjektiId != 0)
                da.SelectCommand.Parameters["@SubjektiId"].Value = subjektiId;
            else
                da.SelectCommand.Parameters["@SubjektiId"].Value = DBNull.Value;

            if (kontoId != 0)
                da.SelectCommand.Parameters["@KontoId"].Value = kontoId;
            else
                da.SelectCommand.Parameters["@KontoId"].Value = DBNull.Value;

            da.SelectCommand.Parameters["@KartelaMeValuteBlerese"].Value = KartelaMeValutBlerese;
            da.SelectCommand.Parameters["@DataPrej"].Value = prej;
            da.SelectCommand.Parameters["@DataDeri"].Value = deri;
            da.SelectCommand.Parameters["@KartelaSipasKesteve"].Value = kartelasipaskesteve;
            da.SelectCommand.Parameters["@ParaqitGjendjenFillestare"].Value = paraqitGjendjenFillestare;

            da.SelectCommand.CommandTimeout = 6000;
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetKartelenESubjektitSipasPlanit(DataTable tabela, int? OrganizataId, DateTime? Data, int? SubjektiId)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.FinKartelaSubjektitSipasKOKontratesKestet_Sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@OrganizataID", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@Data", SqlDbType.Date);
            da.SelectCommand.Parameters.Add("@SubjektiId", SqlDbType.Int);

            if (OrganizataId != 0)
                da.SelectCommand.Parameters["@OrganizataId"].Value = OrganizataId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;

            da.SelectCommand.Parameters["@Data"].Value = Data;

            if (SubjektiId != 0)
                da.SelectCommand.Parameters["@SubjektiId"].Value = SubjektiId;
            else
                da.SelectCommand.Parameters["@SubjektiId"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;

        }

        public static DataTable GetLlogaritPA(DataTable tabela, string llogaritPagueshmeTatueshme, int kontoId, DateTime DeriMedaten)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportLlogaritEArketueshmeDhePagueshme_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@ArketueshmePagueshme", SqlDbType.Char);
            da.SelectCommand.Parameters.Add("@KontoId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@DeriMeDaten", SqlDbType.Date);

            da.SelectCommand.Parameters["@DeriMeDaten"].Value = DeriMedaten;
            da.SelectCommand.Parameters["@ArketueshmePagueshme"].Value = llogaritPagueshmeTatueshme;
            if (kontoId != 0)
                da.SelectCommand.Parameters["@KontoId"].Value = kontoId;
            else
                da.SelectCommand.Parameters["@KontoId"].Value = DBNull.Value;



            da.SelectCommand.CommandTimeout = 6000;
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetLlogaritArketueshme(DataTable tabela, int Organizata, DateTime Prejdates, DateTime DeriMedaten, int Vjetersia, int AgjentiID,
            //int? K16, int? K17, int? K18, int? K19, int? K20,
            int? AgjentiShitjesId, string ArketueshmePagueshme, int? kontoId, bool? StatusiSubjektit = null,
            DataTable dtK16 = null, DataTable dtK17 = null, DataTable dtK18 = null, DataTable dtK19 = null, DataTable dtK20 = null)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.FinLlogariteArketueshme_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@StatusiSubjektit", StatusiSubjektit);
            da.SelectCommand.Parameters.Add("@OrganizataID", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@Prejdates", SqlDbType.Date);
            da.SelectCommand.Parameters.Add("@DerimeDaten", SqlDbType.Date);
            da.SelectCommand.Parameters.Add("@Vjetersia", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@AgjentiID", SqlDbType.Int);
            //da.SelectCommand.Parameters.Add("@K16", SqlDbType.Int);
            //da.SelectCommand.Parameters.Add("@K17", SqlDbType.Int);
            //da.SelectCommand.Parameters.Add("@K18", SqlDbType.Int);
            //da.SelectCommand.Parameters.Add("@K19", SqlDbType.Int);
            //da.SelectCommand.Parameters.Add("@K20", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@AgjentiShitjesId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@ArketueshmePagueshme", SqlDbType.VarChar);
            da.SelectCommand.Parameters.Add("@KontoId", SqlDbType.Int);

            da.SelectCommand.Parameters["@Prejdates"].Value = Prejdates;
            da.SelectCommand.Parameters["@DerimeDaten"].Value = DeriMedaten;
            da.SelectCommand.Parameters["@Vjetersia"].Value = Vjetersia;
            da.SelectCommand.Parameters["@ArketueshmePagueshme"].Value = ArketueshmePagueshme;

            //if (K16 != 0)
            //    da.SelectCommand.Parameters["@K16"].Value = K16;
            //else
            //    da.SelectCommand.Parameters["@K16"].Value = DBNull.Value;

            //if (K17 != 0)
            //    da.SelectCommand.Parameters["@K17"].Value = K17;
            //else
            //    da.SelectCommand.Parameters["@K17"].Value = DBNull.Value;

            //if (K18 != 0)
            //    da.SelectCommand.Parameters["@K18"].Value = K18;
            //else
            //    da.SelectCommand.Parameters["@K18"].Value = DBNull.Value;

            //if (K19 != 0)
            //    da.SelectCommand.Parameters["@K19"].Value = K19;
            //else
            //    da.SelectCommand.Parameters["@K19"].Value = DBNull.Value;

            //if (K20 != 0)
            //    da.SelectCommand.Parameters["@K20"].Value = K20;
            //else
            //    da.SelectCommand.Parameters["@K20"].Value = DBNull.Value;

            if (Organizata != 0)
                da.SelectCommand.Parameters["@OrganizataID"].Value = Organizata;
            else
                da.SelectCommand.Parameters["@OrganizataID"].Value = DBNull.Value;

            if (AgjentiID != 0)
                da.SelectCommand.Parameters["@AgjentiID"].Value = AgjentiID;
            else
                da.SelectCommand.Parameters["@AgjentiID"].Value = DBNull.Value;

            if (AgjentiShitjesId != 0)
                da.SelectCommand.Parameters["@AgjentiShitjesId"].Value = AgjentiShitjesId;
            else
                da.SelectCommand.Parameters["@AgjentiShitjesId"].Value = DBNull.Value;

            if (kontoId != 0)
                da.SelectCommand.Parameters["@KontoId"].Value = kontoId;
            else
                da.SelectCommand.Parameters["@KontoId"].Value = DBNull.Value;

            da.SelectCommand.Parameters.AddWithValue("@Klasifikatori16", dtK16);
            da.SelectCommand.Parameters.AddWithValue("@Klasifikatori17", dtK17);
            da.SelectCommand.Parameters.AddWithValue("@Klasifikatori18", dtK18);
            da.SelectCommand.Parameters.AddWithValue("@Klasifikatori19", dtK19);
            da.SelectCommand.Parameters.AddWithValue("@Klasifikatori20", dtK20);

            da.SelectCommand.CommandTimeout = 6000;
            da.Fill(tabela);
            return tabela;
        }
        public static DataTable GetLlogaritArketueshmePagueshmeSintetike(DataTable tabela, DateTime Prejdates, DateTime DeriMedaten, String ArketueshmePagueshme, int AgjentiID, bool KartelaMeValutBlerese,
                //int? K16, int? K17, int? K18, int? K19, int? K20, 
                int OrganizataID, int? kontoId, int? AgjentiShitjesId, bool? StatusiSubjektit = null, decimal? saldoPrej = null, decimal? saldoDeri = null,
                DataTable dtK16 = null, DataTable dtK17 = null, DataTable dtK18 = null, DataTable dtK19 = null, DataTable dtK20 = null)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.FinLlogariteArketueshmePagueshmeSintetike_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@StatusiSubjektit", StatusiSubjektit);
            da.SelectCommand.Parameters.Add("@Prejdates", SqlDbType.Date);
            da.SelectCommand.Parameters.Add("@DerimeDaten", SqlDbType.Date);
            da.SelectCommand.Parameters.Add("@ArketueshmePagueshme", SqlDbType.Char);
            da.SelectCommand.Parameters.Add("@AgjentiID", SqlDbType.Int);
            //da.SelectCommand.Parameters.Add("@K16", SqlDbType.Int);
            //da.SelectCommand.Parameters.Add("@K17", SqlDbType.Int);
            //da.SelectCommand.Parameters.Add("@K18", SqlDbType.Int);
            //da.SelectCommand.Parameters.Add("@K19", SqlDbType.Int);
            //da.SelectCommand.Parameters.Add("@K20", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@OrganizataID", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@KartelaMeValuteBlerese", SqlDbType.Bit);
            da.SelectCommand.Parameters.Add("@KontoId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@AgjentiShitjesId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@SaldoPrej", SqlDbType.Decimal);
            da.SelectCommand.Parameters.Add("@SaldoDeri", SqlDbType.Decimal);

            if (AgjentiID != 0)
                da.SelectCommand.Parameters["@AgjentiID"].Value = AgjentiID;
            else
                da.SelectCommand.Parameters["@AgjentiID"].Value = DBNull.Value;

            if (AgjentiShitjesId != 0)
                da.SelectCommand.Parameters["@AgjentiShitjesId"].Value = AgjentiShitjesId;
            else
                da.SelectCommand.Parameters["@AgjentiShitjesId"].Value = DBNull.Value;

            da.SelectCommand.Parameters["@Prejdates"].Value = Prejdates;
            da.SelectCommand.Parameters["@DerimeDaten"].Value = DeriMedaten;
            da.SelectCommand.Parameters["@ArketueshmePagueshme"].Value = ArketueshmePagueshme;
            da.SelectCommand.Parameters["@KartelaMeValuteBlerese"].Value = KartelaMeValutBlerese;

            if (OrganizataID != 0)
                da.SelectCommand.Parameters["@OrganizataID"].Value = OrganizataID;
            else
                da.SelectCommand.Parameters["@OrganizataID"].Value = DBNull.Value;

            //if (K16 != 0)
            //    da.SelectCommand.Parameters["@K16"].Value = K16;
            //else
            //    da.SelectCommand.Parameters["@K16"].Value = DBNull.Value;

            //if (K17 != 0)
            //    da.SelectCommand.Parameters["@K17"].Value = K17;
            //else
            //    da.SelectCommand.Parameters["@K17"].Value = DBNull.Value;

            //if (K18 != 0)
            //    da.SelectCommand.Parameters["@K18"].Value = K18;
            //else
            //    da.SelectCommand.Parameters["@K18"].Value = DBNull.Value;

            //if (K19 != 0)
            //    da.SelectCommand.Parameters["@K19"].Value = K19;
            //else
            //    da.SelectCommand.Parameters["@K19"].Value = DBNull.Value;

            //if (K20 != 0)
            //    da.SelectCommand.Parameters["@K20"].Value = K20;
            //else
            //    da.SelectCommand.Parameters["@K20"].Value = DBNull.Value;

            if (kontoId != 0)
                da.SelectCommand.Parameters["@KontoId"].Value = kontoId;
            else
                da.SelectCommand.Parameters["@KontoId"].Value = DBNull.Value;

            da.SelectCommand.Parameters["@SaldoPrej"].Value = saldoPrej;
            da.SelectCommand.Parameters["@SaldoDeri"].Value = saldoDeri;

            da.SelectCommand.Parameters.AddWithValue("@Klasifikatori16", dtK16);
            da.SelectCommand.Parameters.AddWithValue("@Klasifikatori17", dtK17);
            da.SelectCommand.Parameters.AddWithValue("@Klasifikatori18", dtK18);
            da.SelectCommand.Parameters.AddWithValue("@Klasifikatori19", dtK19);
            da.SelectCommand.Parameters.AddWithValue("@Klasifikatori20", dtK20);

            da.SelectCommand.CommandTimeout = 6000;
            da.Fill(tabela);
            return tabela;
        }
        public static DataTable GetPagesatSipasDates(DataTable tabela, int Organizata, string BleresOseFurnitore, DateTime? PrejDates, DateTime? DeriMedaten, string EmriStoreprocedures, int VjetersiaNeDite, int? SubjektiId = null,
            //int? K16 = null, int? K17 = null, int? K18 = null, int? K19 = null, int? K20 = null, 
            DataTable dtK16 = null, DataTable dtK17 = null, DataTable dtK18 = null, DataTable dtK19 = null,
            DataTable dtK20 = null)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter(EmriStoreprocedures, PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@DataPrej", SqlDbType.Date);
            da.SelectCommand.Parameters.Add("@DataDeri", SqlDbType.Date);
            da.SelectCommand.Parameters.Add("@BleresOseFurnitore", SqlDbType.VarChar);
            da.SelectCommand.Parameters.Add("@VjetersiaDite", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@SubjektiId", SqlDbType.Int);
            //da.SelectCommand.Parameters.Add("@K16", SqlDbType.Int);
            //da.SelectCommand.Parameters.Add("@K17", SqlDbType.Int);
            //da.SelectCommand.Parameters.Add("@K18", SqlDbType.Int);
            //da.SelectCommand.Parameters.Add("@K19", SqlDbType.Int);
            //da.SelectCommand.Parameters.Add("@K20", SqlDbType.Int);

            da.SelectCommand.Parameters["@BleresOseFurnitore"].Value = BleresOseFurnitore;
            da.SelectCommand.Parameters["@VjetersiaDite"].Value = VjetersiaNeDite;

            if (PrejDates != null)
                da.SelectCommand.Parameters["@DataPrej"].Value = PrejDates;
            else
                da.SelectCommand.Parameters["@DataPrej"].Value = DBNull.Value;

            if (DeriMedaten != null)
                da.SelectCommand.Parameters["@DataDeri"].Value = DeriMedaten;
            else
                da.SelectCommand.Parameters["@DataDeri"].Value = DBNull.Value;


            if (SubjektiId != null)
                da.SelectCommand.Parameters["@SubjektiId"].Value = SubjektiId;
            else
                da.SelectCommand.Parameters["@SubjektiId"].Value = DBNull.Value;


            if (Organizata != 0)
                da.SelectCommand.Parameters["@OrganizataId"].Value = Organizata;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;

            //if (K16 != 0 && K16 != null)
            //    da.SelectCommand.Parameters["@K16"].Value = K16;
            //else
            //    da.SelectCommand.Parameters["@K16"].Value = DBNull.Value;

            //if (K17 != 0 && K17 != null)
            //    da.SelectCommand.Parameters["@K17"].Value = K17;
            //else
            //    da.SelectCommand.Parameters["@K17"].Value = DBNull.Value;

            //if (K18 != 0 && K18 != null)
            //    da.SelectCommand.Parameters["@K18"].Value = K18;
            //else
            //    da.SelectCommand.Parameters["@K18"].Value = DBNull.Value;

            //if (K19 != 0 && K19 != null)
            //    da.SelectCommand.Parameters["@K19"].Value = K19;
            //else
            //    da.SelectCommand.Parameters["@K19"].Value = DBNull.Value;

            //if (K20 != 0 && K20 != null)
            //    da.SelectCommand.Parameters["@K20"].Value = K20;
            //else
            //    da.SelectCommand.Parameters["@K20"].Value = DBNull.Value;

            da.SelectCommand.Parameters.AddWithValue("@Klasifikatori16", dtK16);
            da.SelectCommand.Parameters.AddWithValue("@Klasifikatori17", dtK17);
            da.SelectCommand.Parameters.AddWithValue("@Klasifikatori18", dtK18);
            da.SelectCommand.Parameters.AddWithValue("@Klasifikatori19", dtK19);
            da.SelectCommand.Parameters.AddWithValue("@Klasifikatori20", dtK20);



            da.SelectCommand.CommandTimeout = 3600;
            da.Fill(tabela);
            return tabela;
        }
        public static DataTable GetLlogaritePagueshme(DataTable tabela, int Organizata, DateTime DeriMedaten, int Vjetersia, int AgjentiID, int? K16, int? K17, int? K18, int? K19, int? K20)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.FinLlogaritePagueshme_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@OrganizataID", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@Data", SqlDbType.Date);
            da.SelectCommand.Parameters.Add("@Vjetersia", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@AgjentiID", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@K16", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@K17", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@K18", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@K19", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@K20", SqlDbType.Int);
            da.SelectCommand.Parameters["@Data"].Value = DeriMedaten;
            da.SelectCommand.Parameters["@Vjetersia"].Value = Vjetersia;
            if (Organizata != 0)
                da.SelectCommand.Parameters["@OrganizataID"].Value = Organizata;
            else
                da.SelectCommand.Parameters["@OrganizataID"].Value = DBNull.Value;

            if (AgjentiID != 0)
                da.SelectCommand.Parameters["@AgjentiID"].Value = AgjentiID;
            else
                da.SelectCommand.Parameters["@AgjentiID"].Value = DBNull.Value;

            if (K16 != 0)
                da.SelectCommand.Parameters["@K16"].Value = K16;
            else
                da.SelectCommand.Parameters["@K16"].Value = DBNull.Value;

            if (K17 != 0)
                da.SelectCommand.Parameters["@K17"].Value = K17;
            else
                da.SelectCommand.Parameters["@K17"].Value = DBNull.Value;

            if (K18 != 0)
                da.SelectCommand.Parameters["@K18"].Value = K18;
            else
                da.SelectCommand.Parameters["@K18"].Value = DBNull.Value;

            if (K19 != 0)
                da.SelectCommand.Parameters["@K19"].Value = K19;
            else
                da.SelectCommand.Parameters["@K19"].Value = DBNull.Value;

            if (K20 != 0)
                da.SelectCommand.Parameters["@K20"].Value = K20;
            else
                da.SelectCommand.Parameters["@K20"].Value = DBNull.Value;

            da.SelectCommand.CommandTimeout = 6000;
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetKartelenEKontosSintetike(DataTable tabela, int kontoId, DateTime dataPrej, DateTime DataDeri)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.FinRaportKartelaKontosSintetike_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@KontoId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@DataPrej", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DataDeri", SqlDbType.DateTime);
            da.SelectCommand.Parameters["@KontoId"].Value = kontoId;


            da.SelectCommand.Parameters["@DataPrej"].Value = dataPrej;
            da.SelectCommand.Parameters["@DataDeri"].Value = DataDeri;

            da.Fill(tabela);
            return tabela;
        }


        public static DataTable GetPasqyratERrjedhesParase(DataTable tabela, DateTime dataPrej, DateTime dataDeri)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.FinPasqyraERrjedhesSeParase_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@PrejDates", SqlDbType.DateTime);
            da.SelectCommand.Parameters["@PrejDates"].Value = dataPrej;

            da.SelectCommand.Parameters.Add("@DeriMeDaten", SqlDbType.DateTime);
            da.SelectCommand.Parameters["@DeriMeDaten"].Value = dataDeri;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetPasqyratETeArdhurave(DataTable tabela, DateTime dataPrej, DateTime dataDeri)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.FinPasqyraETeArdhurave_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@PrejDates", SqlDbType.DateTime);
            da.SelectCommand.Parameters["@PrejDates"].Value = dataPrej;

            da.SelectCommand.Parameters.Add("@DeriMeDaten", SqlDbType.DateTime);
            da.SelectCommand.Parameters["@DeriMeDaten"].Value = dataDeri;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetShitjaSipasMuajveDheOrganizataveReport(DataTable tabela, int viti)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.FinShitjaSipasMuajveDheOrganizatave_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@Viti", SqlDbType.Int);
            da.SelectCommand.Parameters["@Viti"].Value = viti;
            da.SelectCommand.CommandTimeout = 220;
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetQarkullimiSipasSektoritRaport(DataTable tabela, int? sektoriId, int? organizataId, DateTime? prej, DateTime? deri)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportQarkullimiSipasSektorit_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@SektoriId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@DataPrej", SqlDbType.Date);
            da.SelectCommand.Parameters.Add("@DataDeri", SqlDbType.Date);

            if (organizataId != null)
                da.SelectCommand.Parameters["@OrganizataId"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;

            if (sektoriId != null)
                da.SelectCommand.Parameters["@SektoriId"].Value = sektoriId;
            else
                da.SelectCommand.Parameters["@SektoriId"].Value = DBNull.Value;

            if (prej != null)
                da.SelectCommand.Parameters["@DataPrej"].Value = prej;
            else
                da.SelectCommand.Parameters["@DataPrej"].Value = DBNull.Value;

            if (deri != null)
                da.SelectCommand.Parameters["@DataDeri"].Value = deri;
            else
                da.SelectCommand.Parameters["@DataDeri"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }



        public static DataTable GetDaljaMallitSipasGrupeveDheArtikujveRaport(DataTable tabela, int? niveliGrupit, int? grupiId, int? organizataId, DateTime? prej, DateTime? deri)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportDaljaMallitSipasGrupeveDheArtikujve_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@ShifraGrupit", SqlDbType.VarChar);
            da.SelectCommand.Parameters.Add("@DataPrej", SqlDbType.Date);
            da.SelectCommand.Parameters.Add("@DataDeri", SqlDbType.Date);
            da.SelectCommand.Parameters.Add("@NiveliGrupit", SqlDbType.Int);

            if (organizataId != null)
                da.SelectCommand.Parameters["@OrganizataId"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;

            if (grupiId != null)
                da.SelectCommand.Parameters["@ShifraGrupit"].Value = grupiId;
            else
                da.SelectCommand.Parameters["@ShifraGrupit"].Value = DBNull.Value;

            if (prej != null)
                da.SelectCommand.Parameters["@DataPrej"].Value = prej;
            else
                da.SelectCommand.Parameters["@DataPrej"].Value = DBNull.Value;

            if (deri != null)
                da.SelectCommand.Parameters["@DataDeri"].Value = deri;
            else
                da.SelectCommand.Parameters["@DataDeri"].Value = DBNull.Value;

            if (niveliGrupit != 0)
                da.SelectCommand.Parameters["@NiveliGrupit"].Value = niveliGrupit;
            else
                da.SelectCommand.Parameters["@NiveliGrupit"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }


        public static DataTable GetDaljaMallitSipasGrupeveRaport(DataTable tabela, int? niveliGrupit, int? grupiId, int? organizataId, DateTime? prej, DateTime? deri)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportDaljaMallitSipasGrupeve_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@ShifraGrupit", SqlDbType.VarChar);
            da.SelectCommand.Parameters.Add("@DataPrej", SqlDbType.Date);
            da.SelectCommand.Parameters.Add("@DataDeri", SqlDbType.Date);
            da.SelectCommand.Parameters.Add("@NiveliGrupit", SqlDbType.Int);

            if (organizataId != null)
                da.SelectCommand.Parameters["@OrganizataId"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;

            if (grupiId != null)
                da.SelectCommand.Parameters["@ShifraGrupit"].Value = grupiId;
            else
                da.SelectCommand.Parameters["@ShifraGrupit"].Value = DBNull.Value;

            if (prej != null)
                da.SelectCommand.Parameters["@DataPrej"].Value = prej;
            else
                da.SelectCommand.Parameters["@DataPrej"].Value = DBNull.Value;

            if (deri != null)
                da.SelectCommand.Parameters["@DataDeri"].Value = deri;
            else
                da.SelectCommand.Parameters["@DataDeri"].Value = DBNull.Value;

            if (niveliGrupit != 0)
                da.SelectCommand.Parameters["@NiveliGrupit"].Value = niveliGrupit;
            else
                da.SelectCommand.Parameters["@NiveliGrupit"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetHyrjetImportRaport(DataTable tabela, long Id)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportHyrjaMallitImport_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@HyrjaId", SqlDbType.BigInt);
            da.SelectCommand.Parameters["@HyrjaId"].Value = Id;
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetHyrjetImportNdrlyshimiQmimitRaport(DataTable tabela, long Id)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.HyrjaEMallitNdryshimiQmimitReport_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@HyrjaId", SqlDbType.BigInt);
            da.SelectCommand.Parameters["@HyrjaId"].Value = Id;
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetHyrjaMallitImportTotalet(DataTable tabela, long HyrjaId)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportHyrjaMallitImportTotalet_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@HyrjaId", SqlDbType.BigInt);
            da.SelectCommand.Parameters["@HyrjaId"].Value = HyrjaId;
            //da.SelectCommand.Parameters.Add("@SektoriId", SqlDbType.Int);
            //da.SelectCommand.Parameters["@SektoriId"].Value = sektoriId;
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetHyrjaMallitImportShpenzimet(DataTable tabela, long HyrjaId)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.HyrjaEMallitImportShpenzimetSelect_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@HyrjaId", SqlDbType.BigInt);
            da.SelectCommand.Parameters["@HyrjaId"].Value = HyrjaId;
            //da.SelectCommand.Parameters.Add("@SektoriId", SqlDbType.Int);
            //da.SelectCommand.Parameters["@SektoriId"].Value = sektoriId;
            da.Fill(tabela);
            return tabela;
        }


        public static DataTable GetOretPuneRaport(DataTable tabela, int personeliId, DateTime? prej, DateTime? deri)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.HD_HyrjeDaljeTotalReport_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@PersoneliId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@DataPrej", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DataDeri", SqlDbType.DateTime);


            if (prej.HasValue)
                da.SelectCommand.Parameters["@DataPrej"].Value = prej;
            else
                da.SelectCommand.Parameters["@DataPrej"].Value = DBNull.Value;

            if (deri.HasValue)
                da.SelectCommand.Parameters["@DataDeri"].Value = deri;
            else
                da.SelectCommand.Parameters["@DataDeri"].Value = DBNull.Value;

            if (personeliId != 0)
                da.SelectCommand.Parameters["@PersoneliId"].Value = personeliId;
            else
                da.SelectCommand.Parameters["@PersoneliId"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetKartelenIdentifikueseRaport(DataTable tabela)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.Rh_PersoneliSelect_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetHyrjetDaljetRaport(DataTable tabela, int personeliId, DateTime? kohaPrej, DateTime? kohaDeri, DateTime? prej, DateTime? deri)
        {

            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.HD_HyrjeDaljeReport_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@PersoneliId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@DataPrej", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DataDeri", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@VonesaHyrjes", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@VonesaPauze", SqlDbType.DateTime);


            if (prej.HasValue)
                da.SelectCommand.Parameters["@DataPrej"].Value = prej;
            else
                da.SelectCommand.Parameters["@DataPrej"].Value = DBNull.Value;

            if (deri.HasValue)
                da.SelectCommand.Parameters["@DataDeri"].Value = deri;
            else
                da.SelectCommand.Parameters["@DataDeri"].Value = DBNull.Value;

            if (kohaPrej.HasValue)
                da.SelectCommand.Parameters["@VonesaHyrjes"].Value = prej;
            else
                da.SelectCommand.Parameters["@VonesaHyrjes"].Value = DBNull.Value;

            if (kohaDeri.HasValue)
                da.SelectCommand.Parameters["@VonesaPauze"].Value = deri;
            else
                da.SelectCommand.Parameters["@VonesaPauze"].Value = DBNull.Value;


            if (personeliId != 0)
                da.SelectCommand.Parameters["@PersoneliId"].Value = personeliId;
            else
                da.SelectCommand.Parameters["@PersoneliId"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetHyrjetDaljetMujoreRaport(DataTable tabela, int personeliId, int viti, int muaji)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.HD_HyrjeDaljePersoniPivotReport_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@PersoneliId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@Viti", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@Muaji", SqlDbType.Int);

            if (viti != 0)
                da.SelectCommand.Parameters["@Viti"].Value = viti;
            else
                da.SelectCommand.Parameters["@Viti"].Value = DBNull.Value;

            if (muaji != 0)
                da.SelectCommand.Parameters["@Muaji"].Value = muaji;
            else
                da.SelectCommand.Parameters["@Muaji"].Value = DBNull.Value;

            if (personeliId != 0)
                da.SelectCommand.Parameters["@PersoneliId"].Value = personeliId;
            else
                da.SelectCommand.Parameters["@PersoneliId"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetHyrjaMallitSubReportTVSH_Sp(DataTable tabela, long id)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportHyrjaMallitSubReportTVSH_Sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@HyrjaId", id);
            da.Fill(tabela);
            return tabela;
        }


        public static DataTable GetCCPBonusatSelect_sp(DataTable tabela, int id)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.CCPBonusetSelect_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Id", id);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetRaportetFooter(DataTable tabela, string EmriRaportit)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.RaportetFooterSelect_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@EmriRaportit", EmriRaportit);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetCCPKompaniteSelect_sp(DataTable tabela, int id)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.CCPKompaniteSelect_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Id", id);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetPerformancaShfrytezuesit(DataTable tabela, int shfrytezuesiId, int organizataId,
          DateTime? prej, DateTime? deri)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportPerformancaShfrytezuesveHyrjeSelect_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@ShfrytezuesiId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@DataPrej", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DataDeri", SqlDbType.DateTime);


            if (prej.HasValue)
                da.SelectCommand.Parameters["@DataPrej"].Value = prej;
            else
                da.SelectCommand.Parameters["@DataPrej"].Value = DBNull.Value;

            if (deri.HasValue)
                da.SelectCommand.Parameters["@DataDeri"].Value = deri;
            else
                da.SelectCommand.Parameters["@DataDeri"].Value = DBNull.Value;

            if (shfrytezuesiId != 0)
                da.SelectCommand.Parameters["@ShfrytezuesiId"].Value = shfrytezuesiId;
            else
                da.SelectCommand.Parameters["@ShfrytezuesiId"].Value = DBNull.Value;


            if (organizataId != 0)
                da.SelectCommand.Parameters["@OrganizataId"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;


            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetBlerjetMbi500Raport(DataTable tabela,
         DateTime? prej, DateTime? deri)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportBlerjetMbi500_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@DataPrej", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DataDeri", SqlDbType.DateTime);


            if (prej.HasValue)
                da.SelectCommand.Parameters["@DataPrej"].Value = prej;
            else
                da.SelectCommand.Parameters["@DataPrej"].Value = DBNull.Value;

            if (deri.HasValue)
                da.SelectCommand.Parameters["@DataDeri"].Value = deri;
            else
                da.SelectCommand.Parameters["@DataDeri"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }



        public static DataTable GetParashikimiBuxhetit(DataTable tabela, int OrganizataID)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ProjektetPBDetaleKorrektimSelect_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@OrganizataID", SqlDbType.Int);

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetParashikimiBuxhetitDiferencat(DataTable tabela, int OrganizataID)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ProjektetKorrektimetReport_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@OrganizataID", SqlDbType.Int);
            da.SelectCommand.Parameters["@OrganizataID"].Value = PublicClass.OrganizataId;
            da.Fill(tabela);
            return tabela;
        }



        public static DataTable GetNivelizimetRaport(DataTable tabela, int organizataId,
          DateTime? prej, DateTime? deri)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.S_ReportNivelizimet_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@DataPrej", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DataDeri", SqlDbType.DateTime);


            if (prej.HasValue)
                da.SelectCommand.Parameters["@DataPrej"].Value = prej;
            else
                da.SelectCommand.Parameters["@DataPrej"].Value = DBNull.Value;

            if (deri.HasValue)
                da.SelectCommand.Parameters["@DataDeri"].Value = deri;
            else
                da.SelectCommand.Parameters["@DataDeri"].Value = DBNull.Value;


            if (organizataId != 0)
                da.SelectCommand.Parameters["@OrganizataId"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetLibriBlerjesRaportS(DataTable tabela, int organizataId,
      DateTime? prej, DateTime? deri, bool Vendore)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.S_ReportLibriBlerjes_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@DataPrej", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DataDeri", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@TipiIHyrjeve", SqlDbType.VarChar);
            if (Vendore == true)
            {
                da.SelectCommand.Parameters["@TipiIHyrjeve"].Value = "H";
            }
            else
            {
                da.SelectCommand.Parameters["@TipiIHyrjeve"].Value = "HI";
            }
            if (prej.HasValue)
                da.SelectCommand.Parameters["@DataPrej"].Value = prej;
            else
                da.SelectCommand.Parameters["@DataPrej"].Value = DBNull.Value;

            if (deri.HasValue)
                da.SelectCommand.Parameters["@DataDeri"].Value = deri;
            else
                da.SelectCommand.Parameters["@DataDeri"].Value = DBNull.Value;


            if (organizataId != 0)
                da.SelectCommand.Parameters["@OrganizataId"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }
        public static DataTable GetFleteShkarkimet(DataTable tabela, int organizataId,
                                                    DateTime? prej, DateTime? deri)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportFletShkarkimiDoganorDetale_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@DataPrej", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DataDeri", SqlDbType.DateTime);


            if (prej.HasValue)
                da.SelectCommand.Parameters["@DataPrej"].Value = prej;
            else
                da.SelectCommand.Parameters["@DataPrej"].Value = DBNull.Value;

            if (deri.HasValue)
                da.SelectCommand.Parameters["@DataDeri"].Value = deri;
            else
                da.SelectCommand.Parameters["@DataDeri"].Value = DBNull.Value;


            if (organizataId != 0)
                da.SelectCommand.Parameters["@OrganizataId"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }


        public static DataTable GetLibriDoganor(DataTable tabela,
                                                    DateTime? prej, DateTime? deri)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.DepoDoganoreHyrjeDaljeReport_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            //da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@DataPrej", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DataDeri", SqlDbType.DateTime);


            if (prej.HasValue)
                da.SelectCommand.Parameters["@DataPrej"].Value = prej;
            else
                da.SelectCommand.Parameters["@DataPrej"].Value = DBNull.Value;

            if (deri.HasValue)
                da.SelectCommand.Parameters["@DataDeri"].Value = deri;
            else
                da.SelectCommand.Parameters["@DataDeri"].Value = DBNull.Value;


            //if (organizataId != 0)
            //    da.SelectCommand.Parameters["@OrganizataId"].Value = organizataId;
            //else
            //    da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetStoqetDoganore(DataTable tabela, int organizataId,
                                                    int artikulliid, int subjektiId, DateTime Data)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportStokuNeDeponDoganore_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@ArtikulliId", SqlDbType.VarChar);
            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@SubjektiId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@Data", SqlDbType.Date);


            da.SelectCommand.Parameters["@Data"].Value = Data;


            if (organizataId != 0)
                da.SelectCommand.Parameters["@OrganizataId"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;

            if (artikulliid != 0)
                da.SelectCommand.Parameters["@ArtikulliId"].Value = artikulliid;
            else
                da.SelectCommand.Parameters["@ArtikulliId"].Value = DBNull.Value;


            if (subjektiId != 0)
                da.SelectCommand.Parameters["@SubjektiId"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@SubjektiId"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }
        public static DataTable GetLibriTregtarRaportS(DataTable tabela,
    DateTime? prej, DateTime? deri, int? OrganizataId)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.S_ReportLibriTregtar_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@DataPrej", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DataDeri", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);

            da.SelectCommand.Parameters["@OrganizataId"].Value = OrganizataId;

            if (prej.HasValue)
                da.SelectCommand.Parameters["@DataPrej"].Value = prej;
            else
                da.SelectCommand.Parameters["@DataPrej"].Value = DBNull.Value;

            if (deri.HasValue)
                da.SelectCommand.Parameters["@DataDeri"].Value = deri;
            else
                da.SelectCommand.Parameters["@DataDeri"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetLibriShitjesRaportS(DataTable tabela, int organizataId,
          DateTime? prej, DateTime? deri)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.S_ReportLibriShitjes_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@DataPrej", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DataDeri", SqlDbType.DateTime);


            if (prej.HasValue)
                da.SelectCommand.Parameters["@DataPrej"].Value = prej;
            else
                da.SelectCommand.Parameters["@DataPrej"].Value = DBNull.Value;

            if (deri.HasValue)
                da.SelectCommand.Parameters["@DataDeri"].Value = deri;
            else
                da.SelectCommand.Parameters["@DataDeri"].Value = DBNull.Value;


            if (organizataId != 0)
                da.SelectCommand.Parameters["@OrganizataId"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }



        public static DataTable GetPagesatSipasDates(DataTable tabela, DateTime data)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.Report_PagesatSipasDates_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@Data", SqlDbType.DateTime);

            da.SelectCommand.Parameters["@Data"].Value = data;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetDaljaInterne(DataTable tabela, int arsyejaId, int organizataId, int dokumentiId,
            int artikulliId, int departamentiId, DateTime? prej, DateTime? deri, int DestinimiId)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportDaljeInterne_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@DokumentiId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@ArsyejaId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@ArtikulliId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@DepartamentiId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@DataPrej", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DataDeri", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DestinimiId", SqlDbType.Int);


            if (prej.HasValue)
                da.SelectCommand.Parameters["@DataPrej"].Value = prej;
            else
                da.SelectCommand.Parameters["@DataPrej"].Value = DBNull.Value;

            if (deri.HasValue)
                da.SelectCommand.Parameters["@DataDeri"].Value = deri;
            else
                da.SelectCommand.Parameters["@DataDeri"].Value = DBNull.Value;

            if (arsyejaId != 0)
                da.SelectCommand.Parameters["@ArsyejaId"].Value = arsyejaId;
            else
                da.SelectCommand.Parameters["@ArsyejaId"].Value = DBNull.Value;

            if (DestinimiId != 0)
                da.SelectCommand.Parameters["@DestinimiId"].Value = DestinimiId;
            else
                da.SelectCommand.Parameters["@DestinimiId"].Value = DBNull.Value;

            if (organizataId != 0)
                da.SelectCommand.Parameters["@OrganizataId"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;

            if (artikulliId != 0)
                da.SelectCommand.Parameters["@ArtikulliId"].Value = artikulliId;
            else
                da.SelectCommand.Parameters["@ArtikulliId"].Value = DBNull.Value;

            if (departamentiId != 0)
                da.SelectCommand.Parameters["@DepartamentiId"].Value = departamentiId;
            else
                da.SelectCommand.Parameters["@DepartamentiId"].Value = DBNull.Value;

            if (dokumentiId != 0)
                da.SelectCommand.Parameters["@DokumentiId"].Value = dokumentiId;
            else
                da.SelectCommand.Parameters["@DokumentiId"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetShitjaSipasDietveReport(DataTable tabela, int organizataId, DateTime? prej, DateTime? deri)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportShitjaSipasDiteve_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@Organizata", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@PrejDates", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DeriMeDaten", SqlDbType.DateTime);


            if (prej.HasValue)
                da.SelectCommand.Parameters["@PrejDates"].Value = prej;
            else
                da.SelectCommand.Parameters["@PrejDates"].Value = DBNull.Value;

            if (deri.HasValue)
                da.SelectCommand.Parameters["@DeriMeDaten"].Value = deri;
            else
                da.SelectCommand.Parameters["@DeriMeDaten"].Value = DBNull.Value;

            if (organizataId != 0)
                da.SelectCommand.Parameters["@Organizata"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@Organizata"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }


        public static DataTable GetShitjaMeZbritjeReport(DataTable tabela, int organizataId, DateTime? prej, DateTime? deri)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.DaljaMallitShitjasipasZbritjes_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@OrganizataID", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@PrejDates", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DerimeDaten", SqlDbType.DateTime);


            if (prej.HasValue)
                da.SelectCommand.Parameters["@PrejDates"].Value = prej;
            else
                da.SelectCommand.Parameters["@PrejDates"].Value = DBNull.Value;

            if (deri.HasValue)
                da.SelectCommand.Parameters["@DerimeDaten"].Value = deri;
            else
                da.SelectCommand.Parameters["@DerimeDaten"].Value = DBNull.Value;

            if (organizataId != 0)
                da.SelectCommand.Parameters["@OrganizataID"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@OrganizataID"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetShitjaSipasDatesDheMenyresSePagesesReport(DataTable tabela, int organizataId, DateTime? prej, DateTime? deri)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.QarkullimiSipasMenyresSePageses_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@DataPrej", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DataDeri", SqlDbType.DateTime);


            if (prej.HasValue)
                da.SelectCommand.Parameters["@DataPrej"].Value = prej;
            else
                da.SelectCommand.Parameters["@DataPrej"].Value = DBNull.Value;

            if (deri.HasValue)
                da.SelectCommand.Parameters["@DataDeri"].Value = deri;
            else
                da.SelectCommand.Parameters["@DataDeri"].Value = DBNull.Value;

            if (organizataId != 0)
                da.SelectCommand.Parameters["@OrganizataId"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }
        public static DataTable GetShitjaSipasOperatoreveReport(DataTable tabela, int organizataId, DateTime? prej, DateTime? deri)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.QarkullimiTotalSipasOperatoreve_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@DataPrej", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DataDeri", SqlDbType.DateTime);


            if (prej.HasValue)
                da.SelectCommand.Parameters["@DataPrej"].Value = prej;
            else
                da.SelectCommand.Parameters["@DataPrej"].Value = DBNull.Value;

            if (deri.HasValue)
                da.SelectCommand.Parameters["@DataDeri"].Value = deri;
            else
                da.SelectCommand.Parameters["@DataDeri"].Value = DBNull.Value;

            if (organizataId != 0)
                da.SelectCommand.Parameters["@OrganizataId"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetShitjaSipasNrteFaturaveReport(DataTable tabela, int organizataId, DateTime? prej, DateTime? deri, int? Operatori)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.QarkullimiSipasOperatoritDetale_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@DataPrej", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DataDeri", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@ShifraOperatorit", SqlDbType.Int);


            if (prej.HasValue)
                da.SelectCommand.Parameters["@DataPrej"].Value = prej;
            else
                da.SelectCommand.Parameters["@DataPrej"].Value = DBNull.Value;

            if (deri.HasValue)
                da.SelectCommand.Parameters["@DataDeri"].Value = deri;
            else
                da.SelectCommand.Parameters["@DataDeri"].Value = DBNull.Value;

            if (organizataId != 0)
                da.SelectCommand.Parameters["@OrganizataId"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;

            if (Operatori != 0)
                da.SelectCommand.Parameters["@ShifraOperatorit"].Value = Operatori;
            else
                da.SelectCommand.Parameters["@ShifraOperatorit"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetShitjaMePakiceReport
            (
            DataTable tabela
            , int? OrganizataId = null
            , long? DaljaId = null
            , int? DokumentiId = null
            , int? ArtikulliId = null
            , long? NrFatures = null
            , int? Viti = null
            )
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportShitjaSipasParagonave_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", OrganizataId);
            da.SelectCommand.Parameters.AddWithValue("@DaljaId", DaljaId);
            da.SelectCommand.Parameters.AddWithValue("@DokumentiId", DokumentiId);
            da.SelectCommand.Parameters.AddWithValue("@ArtikulliId", ArtikulliId);
            da.SelectCommand.Parameters.AddWithValue("@Viti", Viti);
            if (NrFatures.ToString().Length > 12)
            {
                da.SelectCommand.Parameters.AddWithValue("@IdLokal", NrFatures);
            }
            else
            {
                da.SelectCommand.Parameters.AddWithValue("@NrFatures", NrFatures);
            }


            da.Fill(tabela);
            return tabela;
        }
        public static DataTable GetDaljaInterne(DataTable tabela, int organizataId
            , DateTime? prej, DateTime? deri, int DestinimiId)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.DaljeInterneSipasDestinimitReport_Cros", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@FilialaId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@DataPrej", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DataDeri", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DestinimiId", SqlDbType.Int);


            if (prej.HasValue)
                da.SelectCommand.Parameters["@DataPrej"].Value = prej;
            else
                da.SelectCommand.Parameters["@DataPrej"].Value = DBNull.Value;

            if (deri.HasValue)
                da.SelectCommand.Parameters["@DataDeri"].Value = deri;
            else
                da.SelectCommand.Parameters["@DataDeri"].Value = DBNull.Value;

            if (DestinimiId != 0)
                da.SelectCommand.Parameters["@DestinimiId"].Value = DestinimiId;
            else
                da.SelectCommand.Parameters["@DestinimiId"].Value = DBNull.Value;

            if (organizataId != 0)
                da.SelectCommand.Parameters["@FilialaId"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@FilialaId"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }


        public static DataTable GetListenESubjekteveRaport(DataTable tabela, bool? statusi, int llojiSubjektitId)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportSubjektet_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@Statusi", SqlDbType.Bit);
            da.SelectCommand.Parameters.Add("@LlojiSubjektit", SqlDbType.Int);


            if (llojiSubjektitId != 0)
                da.SelectCommand.Parameters["@LlojiSubjektit"].Value = llojiSubjektitId;
            else
                da.SelectCommand.Parameters["@LlojiSubjektit"].Value = DBNull.Value;

            if (statusi.HasValue)
                da.SelectCommand.Parameters["@Statusi"].Value = statusi;
            else
                da.SelectCommand.Parameters["@Statusi"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }


        public static DataTable GetListenEArtikujve(DataTable tabela, int brendiId, int subjektiId,
      int njesiaId, string grupiMallitShifra, bool? statusi)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportListaEArtikujve_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@BrendiId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@GrupiMallitShifra", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@Njesia", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@SubjektiId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@Statusi", SqlDbType.Bit);


            if (subjektiId != 0)
                da.SelectCommand.Parameters["@SubjektiId"].Value = subjektiId;
            else
                da.SelectCommand.Parameters["@SubjektiId"].Value = DBNull.Value;

            if (brendiId != 0)
                da.SelectCommand.Parameters["@BrendiId"].Value = brendiId;
            else
                da.SelectCommand.Parameters["@BrendiId"].Value = DBNull.Value;

            if (njesiaId != 0)
                da.SelectCommand.Parameters["@Njesia"].Value = njesiaId;
            else
                da.SelectCommand.Parameters["@Njesia"].Value = DBNull.Value;


            if (grupiMallitShifra != "")
                da.SelectCommand.Parameters["@GrupiMallitShifra"].Value = grupiMallitShifra;
            else
                da.SelectCommand.Parameters["@GrupiMallitShifra"].Value = DBNull.Value;

            if (statusi.HasValue)
                da.SelectCommand.Parameters["@Statusi"].Value = statusi;
            else
                da.SelectCommand.Parameters["@Statusi"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }


        public static DataTable GetSubjektet(DataTable tabela, int LlojiISubjektitID, int VendiID, bool? Statusi)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.SubjektetSelect_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@LlojiISubjektitID", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@Statusi", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@VendiID", SqlDbType.Int);

            if (LlojiISubjektitID != 0)
                da.SelectCommand.Parameters["@LlojiISubjektitID"].Value = LlojiISubjektitID;
            else
                da.SelectCommand.Parameters["@LlojiISubjektitID"].Value = DBNull.Value;

            if (VendiID != 0)
                da.SelectCommand.Parameters["@VendiID"].Value = VendiID;
            else
                da.SelectCommand.Parameters["@VendiID"].Value = DBNull.Value;

            if (Statusi.HasValue)
                da.SelectCommand.Parameters["@Statusi"].Value = Statusi;
            else
                da.SelectCommand.Parameters["@Statusi"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetStoqetMeTevjetraMeDite(DataTable tabela, int? brendiId, int? subjektiId,
        int? njesiaId, long? GrupiMallitId, int? OrganizataId, int? dite)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.VjetersiaEStoqeveDepoDoganoreReport_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@VjetersiaNeDite", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@GrupiMallitId", SqlDbType.BigInt);
            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@SubjektiId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@BrendiId", SqlDbType.Int);


            da.SelectCommand.Parameters["@VjetersiaNeDite"].Value = dite;
            da.SelectCommand.Parameters["@GrupiMallitId"].Value = GrupiMallitId;
            da.SelectCommand.Parameters["@OrganizataId"].Value = OrganizataId;
            da.SelectCommand.Parameters["@SubjektiId"].Value = subjektiId;
            da.SelectCommand.Parameters["@BrendiId"].Value = brendiId;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetStoqetMeTevjetraMeDiteDepoDoganore(DataTable tabela, int? brendiId, int? subjektiId,
        int? njesiaId, long? GrupiMallitId, int? OrganizataId, int dite)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.StoqetMeTevjetraMeDiteSelect_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@MeTeVjetraSeDite", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@GrupiMallitId", SqlDbType.BigInt);
            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@SubjektiId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@BrendiId", SqlDbType.Int);


            da.SelectCommand.Parameters["@MeTeVjetraSeDite"].Value = dite;
            da.SelectCommand.Parameters["@GrupiMallitId"].Value = GrupiMallitId;
            da.SelectCommand.Parameters["@OrganizataId"].Value = OrganizataId;
            da.SelectCommand.Parameters["@SubjektiId"].Value = subjektiId;
            da.SelectCommand.Parameters["@BrendiId"].Value = brendiId;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetBlerjetSipasArtikujve(int? OrganizataId, int? LlojiDokumentitId, int? SubjektiId, int? ArtikulliId, DateTime? PrejDates, DateTime? DeriMeDaten, bool? Validuar, int? GrupiMallitId, int? llojiArtikullitId)
        {
            DataTable tabela = new DataTable();
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportBlerjetSipasArtikujve_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.AddWithValue("@SubjektiId", SubjektiId);
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", OrganizataId);
            da.SelectCommand.Parameters.AddWithValue("@ArtikulliId", ArtikulliId);
            da.SelectCommand.Parameters.AddWithValue("@LlojiIDokumentitId", LlojiDokumentitId);
            da.SelectCommand.Parameters.AddWithValue("@Validuar", Validuar);
            da.SelectCommand.Parameters.AddWithValue("@PrejDates", PrejDates);
            da.SelectCommand.Parameters.AddWithValue("@DeriMeDaten", DeriMeDaten);
            da.SelectCommand.Parameters.AddWithValue("@GrupiMallitId", GrupiMallitId);
            da.SelectCommand.Parameters.AddWithValue("@LlojiArtikullitId", llojiArtikullitId);

            da.Fill(tabela);
            return tabela;
        }
        public static DataTable GetTransferatSipasArtikujve(int? OrganizataDergueseId, int? OrganizataPranueseId, int? LlojiDokumentitId, int? ArtikulliId, DateTime? PrejDates, DateTime? DeriMeDaten, bool? Validuar, int? GrupiMallitId, int? llojiArtikullitId)
        {
            DataTable tabela = new DataTable();
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.TransferiMallitSipasArtikujveReport_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.AddWithValue("@OrganizataDergueseId", OrganizataDergueseId);
            da.SelectCommand.Parameters.AddWithValue("@OrganizataPranueseId", OrganizataPranueseId);
            da.SelectCommand.Parameters.AddWithValue("@ArtikulliId", ArtikulliId);
            da.SelectCommand.Parameters.AddWithValue("@LlojiIDokumentitId", LlojiDokumentitId);
            da.SelectCommand.Parameters.AddWithValue("@Validuar", Validuar);
            da.SelectCommand.Parameters.AddWithValue("@PrejDates", PrejDates);
            da.SelectCommand.Parameters.AddWithValue("@DeriMeDaten", DeriMeDaten);
            da.SelectCommand.Parameters.AddWithValue("@GrupiMallitId", GrupiMallitId);
            da.SelectCommand.Parameters.AddWithValue("@LlojiArtikullitId", llojiArtikullitId);

            da.Fill(tabela);
            return tabela;

        }
        public static DataTable GetTransferatSipasDokumenteve(int? OrganizataDergueseId, int? OrganizataPranueseId, int? LlojiDokumentitId, int? ArtikulliId, DateTime? PrejDates, DateTime? DeriMeDaten, bool? Validuar, int? GrupiMallitId, int? llojiArtikullitId)
        {
            DataTable tabela = new DataTable();
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.TransferiMallitSintetikReport_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.AddWithValue("@OrganizataDergueseId", OrganizataDergueseId);
            da.SelectCommand.Parameters.AddWithValue("@OrganizataPranueseId", OrganizataPranueseId);
            da.SelectCommand.Parameters.AddWithValue("@ArtikulliId", ArtikulliId);
            da.SelectCommand.Parameters.AddWithValue("@LlojiIDokumentitId", LlojiDokumentitId);
            da.SelectCommand.Parameters.AddWithValue("@Validuar", Validuar);
            da.SelectCommand.Parameters.AddWithValue("@PrejDates", PrejDates);
            da.SelectCommand.Parameters.AddWithValue("@DeriMeDaten", DeriMeDaten);
            da.SelectCommand.Parameters.AddWithValue("@GrupiMallitId", GrupiMallitId);
            da.SelectCommand.Parameters.AddWithValue("@LlojiArtikullitId", llojiArtikullitId);

            da.Fill(tabela);
            return tabela;
        }
        public static DataTable GetListaEPunetoreveRaport(DataTable tabela, int? punetoriId, int? departamentiId)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.HD_PersoneliSelect_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Id", punetoriId);
            da.SelectCommand.Parameters.AddWithValue("@DepartamentiId", departamentiId);
            da.Fill(tabela);
            return tabela;
        }


        public static DataTable GetOretPuneTotalRaport(DataTable tabela, int punetoriId,
        DateTime? prej, DateTime? deri)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.HyrjeDaljeTotalReport_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@PersoneliId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@DataPrej", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DataDeri", SqlDbType.DateTime);


            if (prej.HasValue)
                da.SelectCommand.Parameters["@DataPrej"].Value = prej;
            else
                da.SelectCommand.Parameters["@DataPrej"].Value = DBNull.Value;

            if (deri.HasValue)
                da.SelectCommand.Parameters["@DataDeri"].Value = deri;
            else
                da.SelectCommand.Parameters["@DataDeri"].Value = DBNull.Value;

            if (punetoriId != 0)
                da.SelectCommand.Parameters["@PersoneliId"].Value = punetoriId;
            else
                da.SelectCommand.Parameters["@PersoneliId"].Value = DBNull.Value;


            da.Fill(tabela);
            return tabela;
        }



        public static DataTable GetBlerjetSipasFurnitorit(DataTable tabela, int? organizataId, int? llojiDocId, int? subjektiId,
      DateTime? prej, DateTime? deri)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportBlerjetSipasFurnitorit_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@SubjektiId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@LlojiDokumentitId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@PrejData", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DeriData", SqlDbType.DateTime);


            if (prej.HasValue)
                da.SelectCommand.Parameters["@PrejData"].Value = prej;
            else
                da.SelectCommand.Parameters["@PrejData"].Value = DBNull.Value;

            if (deri.HasValue)
                da.SelectCommand.Parameters["@DeriData"].Value = deri;
            else
                da.SelectCommand.Parameters["@DeriData"].Value = DBNull.Value;

            if (subjektiId != 0)
                da.SelectCommand.Parameters["@SubjektiId"].Value = subjektiId;
            else
                da.SelectCommand.Parameters["@SubjektiId"].Value = DBNull.Value;

            if (llojiDocId != 0)
                da.SelectCommand.Parameters["@LlojiDokumentitId"].Value = llojiDocId;
            else
                da.SelectCommand.Parameters["@LlojiDokumentitId"].Value = DBNull.Value;

            if (organizataId != 0)
                da.SelectCommand.Parameters["@OrganizataId"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;


            da.Fill(tabela);
            return tabela;
        }


        public static DataTable GetBlerjetSipasFurnitoritPerMuaj(DataTable tabela, int? organizataId, int? subjektiId,
     DateTime? prej, DateTime? deri)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.HyrjaMallitSipasMuajveDheSubjekteve_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@SubjektiID", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@Prejdates", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DeriMeDaten", SqlDbType.DateTime);


            if (prej.HasValue)
                da.SelectCommand.Parameters["@Prejdates"].Value = prej;
            else
                da.SelectCommand.Parameters["@Prejdates"].Value = DBNull.Value;

            if (deri.HasValue)
                da.SelectCommand.Parameters["@DeriMeDaten"].Value = deri;
            else
                da.SelectCommand.Parameters["@DeriMeDaten"].Value = DBNull.Value;

            if (subjektiId != 0)
                da.SelectCommand.Parameters["@SubjektiID"].Value = subjektiId;
            else
                da.SelectCommand.Parameters["@SubjektiID"].Value = DBNull.Value;

            if (organizataId != 0)
                da.SelectCommand.Parameters["@OrganizataId"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;


            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetFleteDergesatDetaleRaport(DataTable tabela, int organizataId, int subjektiId,
        DateTime? prej, DateTime? deri, int bleresiID, int ArtikulliId, string Shoferi, int MenyraPagesesID, Int64 GrupiID, int PranuesiID, int KategoriaID,
        string Punishtja)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportFletDergesatDetale_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@SubjektiId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@bleresiID", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@DataPrej", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DataDeri", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@ArtikulliId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@Shoferi", SqlDbType.VarChar);
            da.SelectCommand.Parameters.Add("@MenyraEPagesesId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@GrupiIMallitId", SqlDbType.BigInt);
            da.SelectCommand.Parameters.Add("@PranuesiID", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@Kategoria", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@Punishtja", SqlDbType.VarChar);

            if (Punishtja != "")
                da.SelectCommand.Parameters["@Punishtja"].Value = Punishtja;
            else
                da.SelectCommand.Parameters["@Punishtja"].Value = DBNull.Value;

            if (KategoriaID != 0)
                da.SelectCommand.Parameters["@Kategoria"].Value = KategoriaID;
            else
                da.SelectCommand.Parameters["@Kategoria"].Value = DBNull.Value;

            if (MenyraPagesesID != 0)
                da.SelectCommand.Parameters["@MenyraEPagesesId"].Value = MenyraPagesesID;
            else
                da.SelectCommand.Parameters["@MenyraEPagesesId"].Value = DBNull.Value;

            if (ArtikulliId != 0)
                da.SelectCommand.Parameters["@ArtikulliId"].Value = ArtikulliId;
            else
                da.SelectCommand.Parameters["@ArtikulliId"].Value = DBNull.Value;

            if (PranuesiID != 0)
                da.SelectCommand.Parameters["@PranuesiID"].Value = PranuesiID;
            else
                da.SelectCommand.Parameters["@PranuesiID"].Value = DBNull.Value;

            if (Shoferi != "")
                da.SelectCommand.Parameters["@Shoferi"].Value = Shoferi;
            else
                da.SelectCommand.Parameters["@Shoferi"].Value = DBNull.Value;

            if (prej.HasValue)
                da.SelectCommand.Parameters["@DataPrej"].Value = prej;
            else
                da.SelectCommand.Parameters["@DataPrej"].Value = DBNull.Value;

            if (deri.HasValue)
                da.SelectCommand.Parameters["@DataDeri"].Value = deri;
            else
                da.SelectCommand.Parameters["@DataDeri"].Value = DBNull.Value;

            if (subjektiId != 0)
                da.SelectCommand.Parameters["@SubjektiId"].Value = subjektiId;
            else
                da.SelectCommand.Parameters["@SubjektiId"].Value = DBNull.Value;

            if (bleresiID != 0)
                da.SelectCommand.Parameters["@bleresiID"].Value = bleresiID;
            else
                da.SelectCommand.Parameters["@bleresiID"].Value = DBNull.Value;

            if (organizataId != 0)
                da.SelectCommand.Parameters["@OrganizataId"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;


            if (GrupiID != 0)
                da.SelectCommand.Parameters["@GrupiIMallitId"].Value = GrupiID;
            else
                da.SelectCommand.Parameters["@GrupiIMallitId"].Value = DBNull.Value;


            da.Fill(tabela);
            return tabela;
        }


        public static DataTable GetFleteDergesatSipasPranuesitRaport(DataTable tabela, int organizataId, DateTime? prej, DateTime? deri, int PranuesiID, int KategoriaID, int bleresiID)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.FleteDergesaPranuesitReport_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@PrejDates", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DeriMeDaten", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@PranuesiID", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@KategoriaID", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@SubjektiID", SqlDbType.Int);

            if (bleresiID != 0)
                da.SelectCommand.Parameters["@SubjektiID"].Value = bleresiID;
            else
                da.SelectCommand.Parameters["@SubjektiID"].Value = DBNull.Value;

            if (KategoriaID != 0)
                da.SelectCommand.Parameters["@KategoriaID"].Value = KategoriaID;
            else
                da.SelectCommand.Parameters["@KategoriaID"].Value = DBNull.Value;

            if (PranuesiID != 0)
                da.SelectCommand.Parameters["@PranuesiID"].Value = PranuesiID;
            else
                da.SelectCommand.Parameters["@PranuesiID"].Value = DBNull.Value;

            if (prej.HasValue)
                da.SelectCommand.Parameters["@PrejDates"].Value = prej;
            else
                da.SelectCommand.Parameters["@PrejDates"].Value = DBNull.Value;

            if (deri.HasValue)
                da.SelectCommand.Parameters["@DeriMeDaten"].Value = deri;
            else
                da.SelectCommand.Parameters["@DeriMeDaten"].Value = DBNull.Value;

            if (organizataId != 0)
                da.SelectCommand.Parameters["@OrganizataId"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;



            da.Fill(tabela);
            return tabela;
        }
        public static DataTable GetFleteDergesatSipasPranuesitTotalRaport(DataTable tabela, int organizataId, DateTime? prej, DateTime? deri, int PranuesiID, int KategoriaID, int bleresiID)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.FleteDergesaPranuesitSintetikeReport_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@PrejDates", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DeriMeDaten", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@PranuesiID", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@KategoriaID", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@SubjektiID", SqlDbType.Int);

            if (bleresiID != 0)
                da.SelectCommand.Parameters["@SubjektiID"].Value = bleresiID;
            else

                da.SelectCommand.Parameters["@SubjektiID"].Value = DBNull.Value;
            if (KategoriaID != 0)
                da.SelectCommand.Parameters["@KategoriaID"].Value = KategoriaID;
            else
                da.SelectCommand.Parameters["@KategoriaID"].Value = DBNull.Value;

            if (PranuesiID != 0)
                da.SelectCommand.Parameters["@PranuesiID"].Value = PranuesiID;
            else
                da.SelectCommand.Parameters["@PranuesiID"].Value = DBNull.Value;

            if (prej.HasValue)
                da.SelectCommand.Parameters["@PrejDates"].Value = prej;
            else
                da.SelectCommand.Parameters["@PrejDates"].Value = DBNull.Value;

            if (deri.HasValue)
                da.SelectCommand.Parameters["@DeriMeDaten"].Value = deri;
            else
                da.SelectCommand.Parameters["@DeriMeDaten"].Value = DBNull.Value;

            if (organizataId != 0)
                da.SelectCommand.Parameters["@OrganizataId"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;



            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetBlerjeDestinuara(DataTable tabela, int? organizataId, int? subjektiId,
     DateTime? prej, DateTime? deri, int? bleresiID)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportBlerjetEDestinuara_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@SubjektiId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@bleresiID", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@DataPrej", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DataDeri", SqlDbType.DateTime);


            if (prej.HasValue)
                da.SelectCommand.Parameters["@DataPrej"].Value = prej;
            else
                da.SelectCommand.Parameters["@DataDeri"].Value = DBNull.Value;

            if (deri.HasValue)
                da.SelectCommand.Parameters["@DataPrej"].Value = deri;
            else
                da.SelectCommand.Parameters["@DataDeri"].Value = DBNull.Value;

            if (subjektiId != 0)
                da.SelectCommand.Parameters["@SubjektiId"].Value = subjektiId;
            else
                da.SelectCommand.Parameters["@SubjektiId"].Value = DBNull.Value;

            if (bleresiID != 0)
                da.SelectCommand.Parameters["@bleresiID"].Value = bleresiID;
            else
                da.SelectCommand.Parameters["@bleresiID"].Value = DBNull.Value;

            if (organizataId != 0)
                da.SelectCommand.Parameters["@OrganizataId"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;


            da.Fill(tabela);
            return tabela;
        }


        public static DataTable GetListaeArtikujveSipasFurnitorit(DataTable tabela, int organizataId, int subjektiId,
        DateTime? prej, DateTime? deri)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportListaEArtikujvesipasFurnitorit_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@SubjektiId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@PrejDates", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@Derimedaten", SqlDbType.DateTime);


            if (prej.HasValue)
                da.SelectCommand.Parameters["@PrejDates"].Value = prej;
            else
                da.SelectCommand.Parameters["@PrejDates"].Value = DBNull.Value;

            if (deri.HasValue)
                da.SelectCommand.Parameters["@Derimedaten"].Value = deri;
            else
                da.SelectCommand.Parameters["@Derimedaten"].Value = DBNull.Value;

            if (subjektiId != 0)
                da.SelectCommand.Parameters["@SubjektiId"].Value = subjektiId;
            else
                da.SelectCommand.Parameters["@SubjektiId"].Value = DBNull.Value;


            if (organizataId != 0)
                da.SelectCommand.Parameters["@OrganizataId"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;


            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetShitjetSipasKlientitPerMuaj(DataTable tabela, int organizataId, DateTime? prej, DateTime? deri)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.DaljaMallitSipasKlientitPerMuaj", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@Prej", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@Deri", SqlDbType.DateTime);


            if (prej.HasValue)
                da.SelectCommand.Parameters["@Prej"].Value = prej;
            else
                da.SelectCommand.Parameters["@Prej"].Value = DBNull.Value;

            if (deri.HasValue)
                da.SelectCommand.Parameters["@Deri"].Value = deri;
            else
                da.SelectCommand.Parameters["@Deri"].Value = DBNull.Value;

            if (organizataId != 0)
                da.SelectCommand.Parameters["@OrganizataId"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }


        public static DataTable GetListaKryesoreERegjistrimit(int? organizataId, long? RegjistrimiId)
        {
            DataTable tabela = new DataTable();
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.Reg_ListaKryesoreSelect_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", organizataId);
            da.SelectCommand.Parameters.AddWithValue("@RegjistrimiId", RegjistrimiId);

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetRegjistriminParcial(long id)
        {
            DataTable tabela = new DataTable();
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.Reg_RegjistrimiParcialReport_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Id", id);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetShitjetPerMuajRaport(DataTable tabela, int organizataId, DateTime? prej, DateTime? deri)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.DaljaMallitSipasKlientitPerMuaj", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@Prej", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@Deri", SqlDbType.DateTime);


            if (prej.HasValue)
                da.SelectCommand.Parameters["@Prej"].Value = prej;
            else
                da.SelectCommand.Parameters["@Prej"].Value = DBNull.Value;

            if (deri.HasValue)
                da.SelectCommand.Parameters["@Deri"].Value = deri;
            else
                da.SelectCommand.Parameters["@Deri"].Value = DBNull.Value;

            if (organizataId != 0)
                da.SelectCommand.Parameters["@OrganizataId"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetShpenzimetEDestiniuara(DataTable tabela, int organizataId, DateTime? prej, DateTime? deri, int ShpenzimeteDestinuara, int DestinimiID)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.FinShpenzimetEDestinuara_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@DataPrej", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DataDeri", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@ShpenzimeteDestinuara", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@DestinimiID", SqlDbType.Int);

            da.SelectCommand.Parameters["@ShpenzimeteDestinuara"].Value = ShpenzimeteDestinuara;

            if (DestinimiID > 0)
                da.SelectCommand.Parameters["@DestinimiID"].Value = DestinimiID;
            else
                da.SelectCommand.Parameters["@DestinimiID"].Value = DBNull.Value;

            if (prej.HasValue)
                da.SelectCommand.Parameters["@DataPrej"].Value = prej;
            else
                da.SelectCommand.Parameters["@DataPrej"].Value = DBNull.Value;

            if (deri.HasValue)
                da.SelectCommand.Parameters["@DataDeri"].Value = deri;
            else
                da.SelectCommand.Parameters["@DataDeri"].Value = DBNull.Value;

            if (organizataId != 0)
                da.SelectCommand.Parameters["@OrganizataId"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetShitjetSipasArtikullitDheDateMeOre(DataTable tabela, int organizataId, DateTime? prej, DateTime? deri)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.DaljaMallitSipasArtikullitDheDatesMeOreReport_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@Prej", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@Deri", SqlDbType.DateTime);


            if (prej.HasValue)
                da.SelectCommand.Parameters["@Prej"].Value = prej;
            else
                da.SelectCommand.Parameters["@Prej"].Value = DBNull.Value;

            if (deri.HasValue)
                da.SelectCommand.Parameters["@Deri"].Value = deri;
            else
                da.SelectCommand.Parameters["@Deri"].Value = DBNull.Value;

            if (organizataId != 0)
                da.SelectCommand.Parameters["@OrganizataId"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }



        public static DataTable GetShitjetVjetoreParameterForm(DataTable tabela, int organizataId, DateTime? prej, DateTime? deri)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.DaljaMallitSipasArtikullitDheVItitSasiaCros", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@Prej", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@Deri", SqlDbType.DateTime);


            if (prej.HasValue)
                da.SelectCommand.Parameters["@Prej"].Value = prej;
            else
                da.SelectCommand.Parameters["@Prej"].Value = DBNull.Value;

            if (deri.HasValue)
                da.SelectCommand.Parameters["@Deri"].Value = deri;
            else
                da.SelectCommand.Parameters["@Deri"].Value = DBNull.Value;

            if (organizataId != 0)
                da.SelectCommand.Parameters["@OrganizataId"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }


        public static DataTable GetDaljetSipasBlersveSintetike(DataTable tabela, int organizataId, int subjektiId, int llojiDoc,
        DateTime? prej, DateTime? deri)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportDaljaMallitSipasBleresveSintetike_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@SubjektiId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@DataPrej", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DataDeri", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@LlojiDokumentitId", SqlDbType.Int);


            if (prej.HasValue)
                da.SelectCommand.Parameters["@DataPrej"].Value = prej;
            else
                da.SelectCommand.Parameters["@DataPrej"].Value = DBNull.Value;

            if (deri.HasValue)
                da.SelectCommand.Parameters["@DataDeri"].Value = deri;
            else
                da.SelectCommand.Parameters["@DataDeri"].Value = DBNull.Value;

            if (subjektiId != 0)
                da.SelectCommand.Parameters["@SubjektiId"].Value = subjektiId;
            else
                da.SelectCommand.Parameters["@SubjektiId"].Value = DBNull.Value;

            if (llojiDoc != 0)
                da.SelectCommand.Parameters["@LlojiDokumentitId"].Value = llojiDoc;
            else
                da.SelectCommand.Parameters["@LlojiDokumentitId"].Value = DBNull.Value;

            if (organizataId != 0)
                da.SelectCommand.Parameters["@OrganizataId"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;


            da.Fill(tabela);
            return tabela;
        }


        public static DataTable GetArtikujtEFshire(DataTable tabela, int operatoriId, DateTime prej, DateTime deri)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportFshirjaEArtikujveNeArka_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@DataPrej", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DataDeri", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@OperatoriId", SqlDbType.Int);

            da.SelectCommand.Parameters["@DataPrej"].Value = prej;
            da.SelectCommand.Parameters["@DataDeri"].Value = deri;
            if (operatoriId != 0)
                da.SelectCommand.Parameters["@OperatoriID"].Value = operatoriId;
            else
                da.SelectCommand.Parameters["@OperatoriID"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetFaturatDetaleRaport(DataTable tabela, int filialaId, int furnitoriId,
            int sektoriId, DataTable llojiIPozitesTabela, DateTime prej, DateTime deri)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.BO_FaturatReportSelect_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FilialaId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@FurnitoriId", SqlDbType.Int);
            //da.SelectCommand.Parameters.Add("@SektoriId", SqlDbType.Int);
            da.SelectCommand.Parameters.AddWithValue("@LlojiIPozites", llojiIPozitesTabela);
            da.SelectCommand.Parameters.Add("@PrejDates", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DeriMeDaten", SqlDbType.DateTime);

            if (filialaId != 0)
                da.SelectCommand.Parameters["@FilialaId"].Value = filialaId;
            else
                da.SelectCommand.Parameters["@FilialaId"].Value = DBNull.Value;

            if (furnitoriId != 0)
                da.SelectCommand.Parameters["@FurnitoriId"].Value = furnitoriId;
            else
                da.SelectCommand.Parameters["@FurnitoriId"].Value = DBNull.Value;

            //if (sektoriId != 0)
            //    da.SelectCommand.Parameters["@SektoriId"].Value = sektoriId;
            //else
            //    da.SelectCommand.Parameters["@SektoriId"].Value = DBNull.Value;



            da.SelectCommand.Parameters["@PrejDates"].Value = prej;
            da.SelectCommand.Parameters["@DeriMeDaten"].Value = deri;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetArtikujtPerberesRaport(DataTable tabela, int Id)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ArtikujtPerberesSelect_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@ArtikulliIPerberId", SqlDbType.BigInt);

            if (Id != -1)
                da.SelectCommand.Parameters["@ArtikulliIPerberId"].Value = Id;
            else
                da.SelectCommand.Parameters["@ArtikulliIPerberId"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetCCPBonusKartelaRaport(DataTable tabela, string shifra)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.CCPKompaniaSelect_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@Shifra", SqlDbType.VarChar);
            da.SelectCommand.Parameters["@Shifra"].Value = shifra;
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetKatalloguOrganizatatRaport(DataTable tabela, int Id)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.KatalloguOrganizatatSelect_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@KatalloguId", SqlDbType.Int);
            da.SelectCommand.Parameters["@KatalloguId"].Value = Id;
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetFaturatSipasVleresRaport(DataTable tabela, decimal? vleraPrej = null, decimal? vleraDeri = null, DateTime? prejDates = null, DateTime? deriMeDaten = null, int? dokumentiId = null)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportFaturatSipasVleres_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@PrejVleres", vleraPrej);
            da.SelectCommand.Parameters.AddWithValue("@DeriMeVleren", vleraDeri);
            da.SelectCommand.Parameters.AddWithValue("@PrejDates", prejDates);
            da.SelectCommand.Parameters.AddWithValue("@DeriMeDaten", deriMeDaten);
            da.SelectCommand.Parameters.AddWithValue("@DokumentiID", dokumentiId);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetPasqyrimetESkeneristavRaport(DataTable tabela, int regjistrimiId)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.Reg_PasqyraESkeneristaveSelectReport_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@RegjistrimiId", SqlDbType.BigInt);
            da.SelectCommand.Parameters["@RegjistrimiId"].Value = regjistrimiId;
            //da.SelectCommand.Parameters.Add("@SektoriId", SqlDbType.Int);
            //da.SelectCommand.Parameters["@SektoriId"].Value = sektoriId;
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetRegjistrimiRaport(DataTable tabela, int regjistrimiId)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.Reg_RegjistrimiSelect_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@Id", SqlDbType.BigInt);
            da.SelectCommand.Parameters["@Id"].Value = regjistrimiId;
            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters["@OrganizataId"].Value = PublicClass.OrganizataId;
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetPasqyratERegjistrimitRaport(DataTable tabela, int regjistrimiId, string ShifraGrupit)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.Reg_ReportDiferencaSipasSektoritSelect_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@RegjistrimiId", SqlDbType.BigInt);
            da.SelectCommand.Parameters["@RegjistrimiId"].Value = regjistrimiId;
            da.SelectCommand.Parameters.Add("@ShifraGrupit", SqlDbType.VarChar);

            if (ShifraGrupit != "")
                da.SelectCommand.Parameters["@ShifraGrupit"].Value = ShifraGrupit;
            else
                da.SelectCommand.Parameters["@ShifraGrupit"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }


        public static DataTable GetHyrjetNdryshimiIQmimitRaport(DataTable tabela, long Id)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.HyrjaEMallitNdryshimiQmimitReport_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@HyrjaId", SqlDbType.BigInt);
            da.SelectCommand.Parameters["@HyrjaId"].Value = Id;
            //da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            //da.SelectCommand.Parameters["@OrganizataId"].Value = PublicClass.OrganizataId;
            da.Fill(tabela);
            return tabela;
        }



        public static DataTable GetReg_RinumrimiReport_Sp(DataTable tabela, long RinumrimiId)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.Reg_RinumrimiReport_Sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@RinumrimiId", RinumrimiId);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetHyrjetRaport(DataTable tabela, long Id)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportHyrjaMallit_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@HyrjaId", SqlDbType.BigInt);
            da.SelectCommand.Parameters["@HyrjaId"].Value = Id;
            da.Fill(tabela);
            return tabela;
        }
        public static DataTable GetHyrjaMallitSintetikeReport(int? SubjektiId, int? OrganizataId, int? LlojiDokumentitId, bool? validuar, DateTime? PrejDates, DateTime? DeriMeDaten)
        {
            DataTable tabela = new DataTable();
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.HyrjaMallitSintetikReport_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@SubjektiId", SubjektiId);
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", OrganizataId);
            da.SelectCommand.Parameters.AddWithValue("@LlojidokumentitID", LlojiDokumentitId);
            da.SelectCommand.Parameters.AddWithValue("@Validuar", validuar);
            da.SelectCommand.Parameters.AddWithValue("@DataPrej", PrejDates);
            da.SelectCommand.Parameters.AddWithValue("@DataDeri", DeriMeDaten);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetKthimiMallitFatura(DataTable tabela, long Id)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportHyrjaMallit_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@HyrjaId", SqlDbType.BigInt);
            da.SelectCommand.Parameters["@HyrjaId"].Value = Id;
            da.Fill(tabela);
            return tabela;
        }
        public static DataTable GetKatalloguRaport(DataTable tabela, long Id)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportKatalloguSelect_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@KatalloguId", SqlDbType.BigInt);
            da.SelectCommand.Parameters["@KatalloguId"].Value = Id;
            da.Fill(tabela);
            return tabela;
        }


        public static DataTable GetKartelaEBorxhitRaport(DataTable tabela, int? organizataId,
            long? kontrataId, bool raportSintetik, DateTime dataDeri)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter(raportSintetik ? "dbo.KOKontratatKartelaEBorxhitSintetikRaport_sp" : "dbo.KOKontratatKartelaEBorxhitRaport_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            organizataId = organizataId == 0 ? null : organizataId;
            kontrataId = kontrataId == 0 ? null : kontrataId;
            //da.SelectCommand.Parameters.Add("@SubjektiId", SqlDbType.Int);
            //da.SelectCommand.Parameters["@SubjektiId"].Value = subjektiId;
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", organizataId);
            da.SelectCommand.Parameters.AddWithValue("@DataDeri", dataDeri);

            if (raportSintetik == false)
                da.SelectCommand.Parameters.AddWithValue("@kontrataId", kontrataId);

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetFaturatDetaleRaport(DataTable tabela, long Id)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.BO_FaturatReportSelect_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FaturatId", SqlDbType.BigInt);

            if (Id != 0)
                da.SelectCommand.Parameters["@FaturatId"].Value = Id;
            else
                da.SelectCommand.Parameters["@FaturatId"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }


        public static DataTable GetSubjektiBankatDetaleRaport(DataTable tabela, int subjektiId)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.SubjektiBankatSelect_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@SubjektiId", SqlDbType.Int);
            da.SelectCommand.Parameters["@SubjektiId"].Value = subjektiId;
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetShitjaMeShumiceSipasLinjave(DataTable tabela, long DaljaId)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportShitjaMeShumiceSipasLinjaveSelect_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@DaljaId", SqlDbType.BigInt);
            da.SelectCommand.Parameters["@DaljaId"].Value = DaljaId;
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetPLURaport(DataTable tabela, int PeshorjaId, int SubjektiId)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.PLUPeshoretReportSelect_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@PeshorjaId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@SubjektiId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);

            da.SelectCommand.Parameters["@OrganizataId"].Value = PublicClass.OrganizataId;

            if (PeshorjaId != 0)
                da.SelectCommand.Parameters["@PeshorjaId"].Value = PeshorjaId;
            else
                da.SelectCommand.Parameters["@PeshorjaId"].Value = DBNull.Value;

            if (SubjektiId != 0)
                da.SelectCommand.Parameters["@SubjektiId"].Value = SubjektiId;
            else
                da.SelectCommand.Parameters["@SubjektiId"].Value = DBNull.Value;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetPozitatSekondareSipasPlanit(DataTable tabela, int DyqaniId)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.BO_PozitatSekondareSipasPlanit_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@DyqaniId", SqlDbType.BigInt);
            da.SelectCommand.Parameters["@DyqaniId"].Value = DyqaniId;

            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetStoqetPasiveSipasBlerjesRaport(DataTable tabela, int organizataId, DateTime? prejDates)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportStoqetPasiveNeBlerje_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@PrejDates", SqlDbType.DateTime);

            if (organizataId != 0)
                da.SelectCommand.Parameters["@OrganizataId"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;

            if (prejDates.HasValue)
                da.SelectCommand.Parameters["@PrejDates"].Value = prejDates;
            else
                da.SelectCommand.Parameters["@PrejDates"].Value = DBNull.Value;

            da.SelectCommand.CommandTimeout = 6000;
            da.Fill(tabela);
            return tabela;
        }


        public static DataTable GetStoqetPasiveSipasShitjesRaport(DataTable tabela, int organizataId, DateTime? prejDates, decimal shitjaMePakSe)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportStoqetPasiveNeShitje_Sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@PrejDates", SqlDbType.DateTime);

            da.SelectCommand.Parameters.Add("@ShitjaMePakSe", SqlDbType.Decimal);

            if (organizataId != 0)
                da.SelectCommand.Parameters["@OrganizataId"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;

            if (prejDates.HasValue)
                da.SelectCommand.Parameters["@PrejDates"].Value = prejDates;
            else
                da.SelectCommand.Parameters["@PrejDates"].Value = DBNull.Value;

            da.SelectCommand.Parameters["@ShitjaMePakSe"].Value = shitjaMePakSe;

            da.SelectCommand.CommandTimeout = 6000;
            da.Fill(tabela);
            return tabela;
        }
        public static DataTable GetTopShitjetRaport(DataTable tabela, int organizataId, int grupiMallitId, bool? sortuar, DateTime prejDates, DateTime deriMeDaten, DataTable dtBrendet, bool? GrupoSipasOrganizates)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportTopListaEShitjes_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@OrganizataId", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@GrupiMallit", SqlDbType.VarChar);
            da.SelectCommand.Parameters.Add("@Sortimi", SqlDbType.Char);
            da.SelectCommand.Parameters.Add("@PrejDates", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@DeriMeDaten", SqlDbType.DateTime);
            da.SelectCommand.Parameters.Add("@GrupoSipasOrganizates", SqlDbType.Bit);
            da.SelectCommand.Parameters.AddWithValue("@Brendi", dtBrendet);

            da.SelectCommand.Parameters["@GrupoSipasOrganizates"].Value = GrupoSipasOrganizates;

            if (organizataId != 0)
                da.SelectCommand.Parameters["@OrganizataId"].Value = organizataId;
            else
                da.SelectCommand.Parameters["@OrganizataId"].Value = DBNull.Value;

            if (grupiMallitId != 0)
                da.SelectCommand.Parameters["@GrupiMallit"].Value = grupiMallitId;
            else
                da.SelectCommand.Parameters["@GrupiMallit"].Value = DBNull.Value;

            if (sortuar.Value == true)
                da.SelectCommand.Parameters["@Sortimi"].Value = 'S';
            else
                da.SelectCommand.Parameters["@Sortimi"].Value = DBNull.Value;

            da.SelectCommand.Parameters["@PrejDates"].Value = prejDates;
            da.SelectCommand.Parameters["@DeriMeDaten"].Value = deriMeDaten;
            da.SelectCommand.CommandTimeout = 6000;
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetOfertaRaport(long Id)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportOfertaSelect_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@OfertaId", Id);
            da.Fill(tabela);
            return tabela;
        }
        public static DataTable GetOfertaMeFotoRaport(long Id)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("dbo.OfertaMeFotoReport_Sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@OfertaId", Id);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetOfertaInfoShteseRaport(long Id)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("dbo.OfertaInfoShteseReportSelect_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@IDOferta", Id);
            da.Fill(tabela);
            return tabela;
        }


        public static DataTable GetPorosiaRaport(long Id)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("dbo.PorosiaSelectReport_Sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Porosia", Id);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetPranimiRaport(long Id)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("dbo.PranimiMallitSelectReport_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@PranimiMallitId", Id);
            da.Fill(tabela);
            return tabela;
        }



        public static DataTable GetLejimStokuRaport(long Id)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportLejimStoku_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@LejimStokuId", Id);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetPranimiRaport(int viti, string nrPranimit)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("dbo.PranimiMallitSelectReport_sp", PublicClass.Koneksioni);

            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@Viti", SqlDbType.Int);
            da.SelectCommand.Parameters.Add("@NrPranimit", SqlDbType.VarChar);
            da.SelectCommand.Parameters.Add("@Validuar", SqlDbType.Bit);

            da.SelectCommand.Parameters["@Viti"].Value = viti;
            da.SelectCommand.Parameters["@Validuar"].Value = true;
            da.SelectCommand.Parameters["@NrPranimit"].Value = nrPranimit;

            da.Fill(tabela);
            return tabela;
        }
        public static DataTable GetProdhimiRaport(long Id)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportProdhimiSelect_Sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Id", Id);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetKOKontratAneksetReport_sp(int headerId)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("dbo.KOKontratAneksetReport_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@KontratAneksiId", headerId);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetPagesatReport(DataTable tabela)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.Rh_ReportPagesatRG_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetFletePagesaPerPagaKalkulimiTatimit_sp(DataTable tabela, int Id)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("dbo.FletePagesaPerPagaKalkulimiTatimit_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@PagesaDetaleId", Id);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetStoqetMomentaleReportWeb(int? OrganizataId = null)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("dbo.ReportStoqetMomentaleNeCmimoreWeb_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", OrganizataId);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetWebDaljaMallitDimenzionetERaportit(long daljaMallitId)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("dbo.WebDaljaMallitDimenzionetERaportit_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DaljaMallitId", daljaMallitId);
            da.Fill(tabela);
            return tabela;
        }


        public static DataTable GetSubjektet(DataTable Tabela, int? Id = null, string NrFiskal = null)
        {

            Tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.SubjektetSelect_sp", PublicClass.KoneksioniLokal);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Id", Id);
            da.SelectCommand.Parameters.AddWithValue("@NrFiskal", NrFiskal);
            da.SelectCommand.CommandTimeout = 2;
            da.Fill(Tabela);
            return Tabela;
        }
        public static bool GetRaportin(ReportViewer rpt, string Raporti, string Pathi)
        {

            DataTable dtRaporti = GetRaportin(Raporti);
            if (dtRaporti.Rows.Count == 0)
            {
                rpt.LocalReport.ReportEmbeddedResource = Pathi;
                return true;
            }
            else if (dtRaporti.Rows.Count == 1)
            {
                if (RaportetClass.EksportoRaportinFajll(Raporti))
                {
                    string shteku = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]) + '\\' +
                                                         Raporti;
                    rpt.LocalReport.ReportPath = shteku;
                    rpt.LocalReport.EnableHyperlinks = true;
                    return true;
                }
            }
            else if (dtRaporti.Rows.Count > 1)
            {
                return false;
            }

            return true;
        }
        public static bool EksportoRaportinFajll(string emriRaportitRDLC, int? id = null)
        {
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.RaportetFajllatSelect_Sp", PublicClass.KoneksioniLokal);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Pershkrimi", emriRaportitRDLC);
            da.SelectCommand.Parameters.AddWithValue("@Id", id);
            DataTable t = new DataTable();
            da.Fill(t);
            if (t.Rows.Count > 0)
            {
                string shteku = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]) + '\\' + emriRaportitRDLC;
                byte[] buffer = (byte[])t.Rows[0]["Raporti"];
                //if (File.Exists(shteku))
                //{
                //    if (Convert.ToBoolean(t.Rows[0]["Freskuar"]))
                //    {
                File.Delete(shteku);
                File.WriteAllBytes(shteku, buffer);
                //    }
                //}
                //else
                //{
                //    File.WriteAllBytes(shteku, buffer);
                //}
                return true;

            }

            return false;
        }
        public static DataTable GetShitjaMeDetale(DataTable dt, long daljaId)
        {
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.DaljaMallitDetaleRaport_Sp", PublicClass.KoneksioniLokal);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DaljaId", daljaId);
            da.Fill(dt);
            return dt;
        }
        public static DataTable GetTransaksionetF1(bool? vetemmarket = null)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.ShitjetPer10DiteSelect_sp", PublicClass.KoneksioniLokal);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@VetemMarketi", vetemmarket);
            da.SelectCommand.CommandTimeout = 10;
            da.Fill(tabela);
            return tabela;
        }
        public static DataTable GetBankatPerFilial()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.OrganizataSelectBankat_sp", PublicClass.KoneksioniLokal);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@NjesiaID", PublicClass.OrganizataId);
            da.Fill(dt);
            return dt;
        }
        public static DataTable GetEkzekutimiPageses(long DaljaMallitId)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_EkzekutimiPagesesSelect_sp", PublicClass.KoneksioniLokal);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DaljaMallitId", DaljaMallitId);
            da.Fill(tabela);
            return tabela;
        }
    }
}
