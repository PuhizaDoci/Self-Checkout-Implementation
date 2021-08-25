using ToshibaPos.SDK;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ToshibaPosSinkronizimi
{
    public static class SinkronizimiClass
    {

        public static void Starto(bool StartoSinkronizimin)
        { 
            ArkatClass G = new ArkatClass();
            G.LoadFromDiscToPublicClassArka();
            if (string.IsNullOrEmpty(PublicClass.Arka.Databaza))
            {
                G.LoadFromRegedit();
                if (!string.IsNullOrEmpty(G.Databaza))
                {
                    PublicClass.Arka.WriteToDisc();
                    PublicClass.Arka = G;

                    OrganizataClass O = new OrganizataClass();
                    O.GetFromDB(G.OrganizataId);
                    O.WriteToDisc();

                    ConfigurationDataClass C = new ConfigurationDataClass();
                    C.GetFromDB();
                    C.WriteToDisc();
                    PublicClass.ConfigurationData = C;
                }
            }
            //else
            //{
            if (string.IsNullOrEmpty(PublicClass.Arka.Databaza))
            {
                LidhjaForm lf = new LidhjaForm();
                if (lf.ShowDialog() != DialogResult.OK)
                {
                    Application.Exit();
                    return;
                }
            }
            //}

            if (G.ADuhetTeRuehtArkaNeServer(PublicClass.Arka.OrganizataId, PublicClass.Arka.NrArkes))
            {
                PublicClass.Arka.Ruaj(false);
            }

            OrganizataClass OA = new OrganizataClass(null);
            OA.LoadFromDisc();
            if (PublicClass.Organizata.Id == null || PublicClass.Organizata.Id == 0)
            {
                OA = new OrganizataClass(PublicClass.Arka.OrganizataId);
                OA.WriteToDisc();
                OA.LoadFromDisc();
            }

            ConfigurationDataClass AC = new ConfigurationDataClass();
            AC.LoadFromDisc();
            if (PublicClass.ConfigurationData.Gjuha == null || PublicClass.ConfigurationData.Gjuha == "")
            {
                AC.GetFromDB();
                AC.WriteToDisc();
                AC.LoadFromDisc();
            }

            DBLocalClass a = new DBLocalClass();
            if (a.TestoKoneksionin(PublicClass.Arka.KoneksioniPrimar))
                a.FreskoDatabazen();

            if (StartoSinkronizimin)
                Sync.Starto();
        }

        public static SinkronizimetTimerat Sync = new SinkronizimetTimerat();

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
    }
}
