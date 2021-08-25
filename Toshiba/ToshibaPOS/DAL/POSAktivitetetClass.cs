using System;
using System.Data;
using System.Data.SqlClient;
using ToshibaPos.SDK;

namespace ToshibaPOS.DAL
{
    public class POSAktivitetetClass
    {
        public static DataTable GetPOSAktivitetet(
            DataTable dtPOSAktivitetet = null, long? Id = null,
            DateTime? PrejDates = null,
            DateTime? DeriMeDaten = null,
            DateTime? PrejOres = null,
            DateTime? DeriMeOren = null,
            string Pershkrimi = null,
            bool? Statusi = null,
            int? organizataId = null,
            int? llojiDokumentitId = null,
            int? nrDokumentit = null,
            int? viti = null,
            DateTime? data = null,
            bool? perfshinKatallogun = null,
            bool? AND = null,
            bool? Aktiv = null)
        {
            if (dtPOSAktivitetet == null)
                dtPOSAktivitetet = new DataTable();
            dtPOSAktivitetet.Clear(); SqlDataAdapter da = new SqlDataAdapter("dbo.POSAktivitetetSelect_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (Id > 0) da.SelectCommand.Parameters.AddWithValue("@Id", Id);
            da.SelectCommand.Parameters.AddWithValue("@PrejDates", PrejDates);
            da.SelectCommand.Parameters.AddWithValue("@DeriMeDaten", DeriMeDaten);
            da.SelectCommand.Parameters.AddWithValue("@PrejOres", PrejOres);
            da.SelectCommand.Parameters.AddWithValue("@DeriMeOren", DeriMeOren);
            da.SelectCommand.Parameters.AddWithValue("@Pershkrimi", Pershkrimi);
            da.SelectCommand.Parameters.AddWithValue("@Statusi", Statusi);
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", organizataId);
            da.SelectCommand.Parameters.AddWithValue("@LlojiIDokumentitId", llojiDokumentitId);
            da.SelectCommand.Parameters.AddWithValue("@NrDokumentit", nrDokumentit);
            da.SelectCommand.Parameters.AddWithValue("@Viti", viti);
            da.SelectCommand.Parameters.AddWithValue("@Data", data);
            da.SelectCommand.Parameters.AddWithValue("@PerfshinKatallogun", perfshinKatallogun);
            da.SelectCommand.Parameters.AddWithValue("@DHE", AND);
            da.SelectCommand.Parameters.AddWithValue("@Aktiv", Aktiv);
            da.Fill(dtPOSAktivitetet);

            return dtPOSAktivitetet;
        }

        public static DataTable GetPOSAktivitetetFilter(
            DataTable dtPOSAktivitetetFilter = null, int? Id = null,
            long? AktivitetiId = null,
            int? FilteriId = null,
            string Shifra = null,
            bool? Aktiv = null,
            int? llojiDokumentitId = null)
        {
            if (dtPOSAktivitetetFilter == null)
                dtPOSAktivitetetFilter = new DataTable();
            dtPOSAktivitetetFilter.Clear(); SqlDataAdapter da = new SqlDataAdapter("dbo.POSAktivitetetFilterSelect_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (Id > 0) da.SelectCommand.Parameters.AddWithValue("@Id", Id);
            da.SelectCommand.Parameters.AddWithValue("@AktivitetiId", AktivitetiId);
            da.SelectCommand.Parameters.AddWithValue("@FilteriId", FilteriId);
            da.SelectCommand.Parameters.AddWithValue("@Shifra", Shifra);
            da.SelectCommand.Parameters.AddWithValue("@Aktiv", Aktiv);
            da.SelectCommand.Parameters.AddWithValue("@LlojiDokumentitId", llojiDokumentitId);
            da.Fill(dtPOSAktivitetetFilter);

            return dtPOSAktivitetetFilter;
        }

        public static DataTable GetPOSAktivitetetZbritjaNeVlere(
            DataTable dtPOSAktivitetetZbritjaNeVlere = null, int? Id = null,
            long? AktivitetiId = null,
            int? LlojiIZbrijtesId = null,
            decimal? Vlera = null,
            decimal? Zbritja = null,
            string Komenti = null,
            bool? Aktiv = null,
            int? llojiDokumentitId = null)
        {
            if (dtPOSAktivitetetZbritjaNeVlere == null)
                dtPOSAktivitetetZbritjaNeVlere = new DataTable();
            dtPOSAktivitetetZbritjaNeVlere.Clear(); SqlDataAdapter da = new SqlDataAdapter("dbo.POSAktivitetetZbritjaNeVlereSelect_sp", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (Id > 0) da.SelectCommand.Parameters.AddWithValue("@Id", Id);
            da.SelectCommand.Parameters.AddWithValue("@AktivitetiId", AktivitetiId);
            da.SelectCommand.Parameters.AddWithValue("@LlojiIZbrijtesId", LlojiIZbrijtesId);
            da.SelectCommand.Parameters.AddWithValue("@Vlera", Vlera);
            da.SelectCommand.Parameters.AddWithValue("@Zbritja", Zbritja);
            da.SelectCommand.Parameters.AddWithValue("@Komenti", Komenti);
            da.SelectCommand.Parameters.AddWithValue("@Aktiv", Aktiv);
            da.SelectCommand.Parameters.AddWithValue("@LlojiDokumentitId", llojiDokumentitId);
            da.Fill(dtPOSAktivitetetZbritjaNeVlere);

            return dtPOSAktivitetetZbritjaNeVlere;
        }

        public static DataTable ArtikujteSkenuarPlotesoDetalet(DataTable dtPOSAktivitetet)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("[dbo].[POSAktivitetetPlotesoDetalet_sp]", PublicClass.Koneksioni);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@POSAktivitetet", dtPOSAktivitetet);
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", PublicClass.OrganizataId);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable ArtikujteSkenuarPlotesoDetaletVoucher(DataTable dtPOSKodetEKuponave)
        {


            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("[dbo].[POSKuponatPerZbritjePlotesoDetaletSelect_sp]", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.CommandTimeout = 2;
            da.SelectCommand.Parameters.AddWithValue("@POSAktivitetet", dtPOSKodetEKuponave);
            da.Fill(tabela);
            return tabela;
        }

        public static DataTable GetDaljaMallitDetaleAktivitetet(DataTable tabela, string daljaMallitId)
        {
            tabela.Clear();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.DaljaMallitDetaleAktivitetetSelect_sp", PublicClass.KoneksioniLokal);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DaljaMallit", daljaMallitId);
            return tabela;
        }
    }
}
