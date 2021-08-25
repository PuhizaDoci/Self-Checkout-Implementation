using ToshibaPos.SDK;
using System;
using System.Data;
using System.Windows.Forms;
using ToshibaPOS.DAL;

namespace ToshibaPosSinkronizimi
{
    public class Tabelat
    {
        public DataTable Daljet { get; set; }
        public DataTable DaljetDetalet { get; set; }
        public DataTable MenyratEPageses { get; set; }
        public DataTable EkzekutimetEPageses { get; set; }
        public DataTable DetaletEFshire { get; set; }
        public DataTable CCPFaturat { get; set; }
        public DataTable POSZbritjaMeKupon { get; set; }
        public DataTable dtDaljaMallitDetaleAktivitetet { get; set; }
        public DataTable dtKuponat { get; set; }
        public DataTable dtDaljaMallitVaucherat { get; set; }
        public BindingSource BsDaljaDetalet { get; set; }
        public BindingSource BsHeader { get; set; }
        public BindingSource BsMenyratEPageses { get; set; }
        public DataSet DsHeader { get; set; }
        public decimal CashBack { get; set; }
        public bool ShitjeTrueKthimFalse { get; set; } = true;
        private decimal _TotalRabati = 0;
        public Tabelat()
        {

            Daljet = new DataTable("dtHeader");
            DaljetDetalet = new DataTable("dtDaljaDetalet");
            MenyratEPageses = new DataTable("dtEkzekutimetEPageses");
            EkzekutimetEPageses = new DataTable("dtEkzekutimetEPageses");
            DetaletEFshire = new DataTable("dtDetaletEFshire");
            CCPFaturat = new DataTable("dtCCPFatura");
            POSZbritjaMeKupon = new DataTable("dtPOSZbritjaMeKupon");
            dtDaljaMallitDetaleAktivitetet = new DataTable("dtDaljaMallitDetaleAktivitetet");
            dtKuponat = new DataTable("dtKuponat");
            dtDaljaMallitVaucherat = new DataTable("dtDaljaMallitVaucherat");

            BsDaljaDetalet = new BindingSource();
            BsHeader = new BindingSource();
            BsMenyratEPageses = new BindingSource();

            DsHeader = new DataSet();

            SetDefaultValues();
            SetConstraints();

            AddTablesToDataSet();

            BsHeader.DataSource = Daljet;
            BsMenyratEPageses.DataSource = MenyratEPageses;
            MenyratEPageses.DefaultView.Sort = "Renditja asc";

            int index = BsMenyratEPageses.Find("Id", "22");

            if (index >= 0)
            {
                BsMenyratEPageses.Position = index;
            }

            BsDaljaDetalet.DataSource = DaljetDetalet;

        }

        private void AddTablesToDataSet()
        {
            DsHeader.Tables.Add(Daljet);
            DsHeader.Tables.Add(DaljetDetalet);
            DsHeader.Tables.Add(EkzekutimetEPageses);
            DsHeader.Tables.Add(CCPFaturat);
            DsHeader.Tables.Add(DetaletEFshire);
            DsHeader.Tables.Add(POSZbritjaMeKupon);
            DsHeader.Tables.Add(dtDaljaMallitDetaleAktivitetet);
            DsHeader.Tables.Add(dtKuponat);
            DsHeader.Tables.Add(dtDaljaMallitVaucherat);
        }

        private void SetDefaultValues()
        {
            Daljet = new DataTable("dtHeader");
            Daljet.Columns.Add("Id", typeof(long));
            Daljet.Columns["Id"].AutoIncrement = true;
            Daljet.Columns["Id"].AutoIncrementSeed = -1;
            Daljet.Columns["Id"].AutoIncrementStep = -1;

            Daljet.Columns.Add("OrganizataId", typeof(int));
            Daljet.Columns["OrganizataId"].DefaultValue = PublicClass.OrganizataId;

            Daljet.Columns.Add("Viti", typeof(int));
            Daljet.Columns.Add("Data", typeof(DateTime));
            Daljet.Columns.Add("NrFatures", typeof(int));
            Daljet.Columns.Add("DokumentiId", typeof(int));
            Daljet.Columns["DokumentiId"].DefaultValue = 20;

            Daljet.Columns.Add("NumriArkes", typeof(int));

            Daljet.Columns.Add("ZbritjeNgaOperatori", typeof(int));
            Daljet.Columns.Add("SubjektiId", typeof(int));
            Daljet.Columns.Add("IdLokal", typeof(string));

            Daljet.Columns.Add("RegjistruarNga", typeof(int));
            Daljet.Columns["RegjistruarNga"].DefaultValue = PublicClass.OperatoriId;
            Daljet.Columns.Add("DataERegjistrimit", typeof(DateTime));

            //Daljet.Columns.Add("RFID", typeof(string));
            //Daljet.Columns.Add("RFIDCCP", typeof(string));

            Daljet.Columns.Add("TavolinaId", typeof(int));
            Daljet.Columns.Add("Validuar", typeof(bool));
            Daljet.Columns.Add("NumriATK", typeof(string));
            Daljet.Columns.Add("NumriArkesGK", typeof(string));
            Daljet.Columns.Add("NumriFaturesManual", typeof(string));
            Daljet.Columns.Add("ServerId", typeof(long));
            Daljet.Columns.Add("DaljaMallitKorrektuarId", typeof(long));
            Daljet.Columns.Add("AplikacioniId", typeof(int));
            Daljet.Columns.Add("KthimiMallitArsyejaId", typeof(int));
            Daljet.Columns.Add("Personi", typeof(string));
            Daljet.Columns.Add("Adresa", typeof(string));
            Daljet.Columns.Add("NumriPersonal", typeof(string));
            Daljet.Columns.Add("NrTel", typeof(string));
            Daljet.Columns.Add("Koment", typeof(string));
            Daljet.Columns.Add("KuponiFiskalShtypur", typeof(bool));

            BsHeader.AddNew();
            BsHeader.EndEdit();


            DaljetDetalet = new DataTable("dtDaljaDetalet");

            DaljetDetalet.Columns.Add("NR", typeof(int));
            DaljetDetalet.Columns["NR"].AutoIncrement = true;
            DaljetDetalet.Columns["NR"].AutoIncrementSeed = 1;
            DaljetDetalet.Columns["NR"].AutoIncrementStep = 1;

            DaljetDetalet.Columns.Add("DaljaMallitId", typeof(long));
            DaljetDetalet.Columns.Add("OrganizataId", typeof(int));
            DaljetDetalet.Columns["OrganizataId"].DefaultValue = PublicClass.OrganizataId;
            DaljetDetalet.Columns.Add("ArtikulliId", typeof(int));

            DaljetDetalet.Columns.Add("Shifra", typeof(string));
            DaljetDetalet.Columns.Add("Barkodi", typeof(string));
            DaljetDetalet.Columns.Add("Pershkrimi", typeof(string));
            DaljetDetalet.Columns.Add("Njesia", typeof(string));
            DaljetDetalet.Columns.Add("NjesiaID", typeof(int));
            DaljetDetalet.Columns.Add("Paketimi", typeof(decimal));
            DaljetDetalet.Columns.Add("Stoku", typeof(decimal));
            DaljetDetalet.Columns.Add("Sasia", typeof(decimal));
            DaljetDetalet.Columns.Add("Tvsh", typeof(decimal));
            DaljetDetalet.Columns.Add("QmimiRregullt", typeof(decimal));
            DaljetDetalet.Columns.Add("QmimiShumices", typeof(decimal));
            DaljetDetalet.Columns.Add("QmimiShitjes", typeof(decimal));
            DaljetDetalet.Columns.Add("Rabati", typeof(decimal));
            DaljetDetalet.Columns.Add("EkstraRabati", typeof(decimal));
            DaljetDetalet.Columns.Add("QmimiFinal", typeof(decimal));
            DaljetDetalet.Columns.Add("VleraMeTVSH", typeof(decimal));
            DaljetDetalet.Columns.Add("ArtikullPaZbritje", typeof(bool));
            DaljetDetalet.Columns.Add("StatusiQmimitId", typeof(int));
            DaljetDetalet.Columns["Stoku"].DefaultValue = 0;
            DaljetDetalet.Columns.Add("KaLirim", typeof(bool));
            DaljetDetalet.DefaultView.Sort = "Nr asc";
            DaljetDetalet.Columns.Add("AplikimiVoucherit", typeof(bool));

            DetaletEFshire = new DataTable("dtArtikujtEFshire");
            DetaletEFshire.Columns.Add("DaljaMallitId", typeof(long));
            DetaletEFshire.Columns.Add("OrganizataId", typeof(int));
            DetaletEFshire.Columns.Add("ArtikulliId", typeof(int));
            DetaletEFshire.Columns.Add("NjesiaID", typeof(int));
            DetaletEFshire.Columns.Add("NR", typeof(int));
            DetaletEFshire.Columns.Add("Sasia", typeof(decimal));
            DetaletEFshire.Columns.Add("Tvsh", typeof(decimal));
            DetaletEFshire.Columns.Add("QmimiShitjes", typeof(decimal));
            DetaletEFshire.Columns.Add("Rabati", typeof(decimal));
            DetaletEFshire.Columns.Add("EkstraRabati", typeof(decimal));
            DetaletEFshire.Columns.Add("QmimiFinal", typeof(decimal));

            EkzekutimetEPageses = new DataTable("dtEkzekutimetEPageses");

            EkzekutimetEPageses.Columns.Add("Id", typeof(int));
            EkzekutimetEPageses.Columns.Add("MenyraEPagesesId", typeof(int));
            EkzekutimetEPageses.Columns.Add("DaljaMallitID", typeof(long));
            EkzekutimetEPageses.Columns.Add("MenyraEPageses", typeof(string));
            EkzekutimetEPageses.Columns.Add("Vlera", typeof(decimal));
            EkzekutimetEPageses.Columns.Add("Paguar", typeof(decimal));
            EkzekutimetEPageses.Columns.Add("ShifraOperatorit", typeof(int));
            EkzekutimetEPageses.Columns.Add("DhenjeKesh", typeof(decimal));
            EkzekutimetEPageses.Columns["ShifraOperatorit"].DefaultValue = PublicClass.OperatoriId;
            EkzekutimetEPageses.Columns.Add("KodiVoucherit", typeof(string));
            EkzekutimetEPageses.Columns.Add("LlojiVoucherit", typeof(string));


            POSZbritjaMeKupon = new DataTable("dtPOSZbritjaMeKupon");
            POSZbritjaMeKupon.Columns.Add("Id", typeof(int));
            POSZbritjaMeKupon.Columns.Add("POSKuponatPerZbritjeId", typeof(int));
            POSZbritjaMeKupon.Columns.Add("KodiKuponit", typeof(string));
            POSZbritjaMeKupon.Columns.Add("Vlera", typeof(decimal));
            POSZbritjaMeKupon.Columns.Add("DaljaMallitId", typeof(long));



            CCPFaturat = new DataTable("dtCCPFatura");
            CCPFaturat.Columns.Add("Id", typeof(int));
            CCPFaturat.Columns.Add("CCPKompaniaId", typeof(int));
            CCPFaturat.Columns.Add("DaljaMallitId", typeof(long));
            CCPFaturat.Columns.Add("CCPBonusiId", typeof(int));
            CCPFaturat.Columns.Add("RegjistruarNga", typeof(int));

            MenyratEPageses = ToshibaPosClass.GetMenyratEPageses(new DataTable());
            MenyratEPageses.TableName = ("dtMenyratEPageses");


            Daljet.Columns["Id"].AutoIncrement = true;
            Daljet.Columns["Id"].AutoIncrementStep = -1;
            Daljet.Columns["Id"].AutoIncrementSeed = -1;

            DaljetDetalet.Columns["Nr"].AutoIncrement = true;
            DaljetDetalet.Columns["Nr"].AutoIncrementSeed = 1;
            //Daljet.Columns["NumriArkes"].DefaultValue = PublicClass.Arka.NrArkes;

            EkzekutimetEPageses.Columns["ShifraOperatorit"].DefaultValue = PublicClass.ShifraEOperatorit;
            CCPFaturat.Columns["RegjistruarNga"].DefaultValue = PublicClass.OperatoriId;

            DaljetDetalet.Columns.Add("RabatiNgaShfrytezuesiOseNgaKartela", typeof(decimal));
            DaljetDetalet.Columns["RabatiNgaShfrytezuesiOseNgaKartela"].DefaultValue = 0;

            dtDaljaMallitDetaleAktivitetet = new DataTable("dtDaljaMallitDetaleAktivitetet");
            dtDaljaMallitDetaleAktivitetet.Columns.Add("Id", typeof(int));
            dtDaljaMallitDetaleAktivitetet.Columns.Add("DaljaMallitDetaleId", typeof(int));
            dtDaljaMallitDetaleAktivitetet.Columns.Add("AktivitetiId", typeof(long));
            dtDaljaMallitDetaleAktivitetet.Columns.Add("Zbritja", typeof(decimal));
            dtDaljaMallitDetaleAktivitetet.Columns.Add("DaljaMallitId", typeof(long));

            dtKuponat = new DataTable("dtKuponat");
            dtKuponat.Columns.Add("Id", typeof(int));
            dtKuponat.Columns.Add("KuponatPerZbritjeId", typeof(int));
            dtKuponat.Columns.Add("KodiKuponit", typeof(string));
            dtKuponat.Columns.Add("Aktivizuar", typeof(bool));
            dtKuponat.Columns.Add("Aplikuar", typeof(bool));
            dtKuponat.Columns.Add("DaljaMallitIdGjeneruar", typeof(long));
            dtKuponat.Columns.Add("DaljaMallitIdAplikuar", typeof(long));

            dtDaljaMallitVaucherat = new DataTable("dtDaljaMallitVaucherat");
            dtDaljaMallitVaucherat.Columns.Add("Id", typeof(int));
            dtDaljaMallitVaucherat.Columns["Id"].AutoIncrement = true;
            dtDaljaMallitVaucherat.Columns["Id"].AutoIncrementStep = -1;
            dtDaljaMallitVaucherat.Columns["Id"].AutoIncrementSeed = -1;
            dtDaljaMallitVaucherat.Columns.Add("DaljaMallitID", typeof(long));
            dtDaljaMallitVaucherat.Columns.Add("Vlera", typeof(decimal));
            dtDaljaMallitVaucherat.Columns.Add("KodiVaucherit", typeof(string));
            dtDaljaMallitVaucherat.Columns.Add("Emri", typeof(string));
            dtDaljaMallitVaucherat.Columns.Add("Mbiemri", typeof(string));
            dtDaljaMallitVaucherat.Columns.Add("Lloji", typeof(string));
        }

        private void SetConstraints()
        {
            EkzekutimetEPageses.Constraints.Add(new ForeignKeyConstraint("FK_EkzekutimetEPagesave_DaljaMallitId",
                Daljet.Columns["Id"], EkzekutimetEPageses.Columns["DaljaMallitId"])
            {
                DeleteRule = Rule.Cascade,
                UpdateRule = Rule.Cascade
            });


            DaljetDetalet.Constraints.Add(new ForeignKeyConstraint("FK_DaljaMallitDetale_DaljaMallitId",
                Daljet.Columns["Id"], DaljetDetalet.Columns["DaljaMallitId"])
            {
                DeleteRule = Rule.Cascade,
                UpdateRule = Rule.Cascade
            });


            CCPFaturat.Constraints.Add(new ForeignKeyConstraint("FK_CCPFaturat_DaljaMallitId", Daljet.Columns["Id"],
                    CCPFaturat.Columns["DaljaMallitId"])
            { DeleteRule = Rule.Cascade, UpdateRule = Rule.Cascade });


            DetaletEFshire.Constraints.Add(new ForeignKeyConstraint("FK_DetaletEFshire_DaljaMallitId", Daljet.Columns["Id"],
                    DetaletEFshire.Columns["DaljaMallitId"])
            { DeleteRule = Rule.Cascade, UpdateRule = Rule.Cascade });


            POSZbritjaMeKupon.Constraints.Add(new ForeignKeyConstraint("FK_POSZbritjaMeKupon_DaljaMallitId", Daljet.Columns["Id"],
                    POSZbritjaMeKupon.Columns["DaljaMallitId"])
            { DeleteRule = Rule.Cascade, UpdateRule = Rule.Cascade });

            POSZbritjaMeKupon.Columns["POSKuponatPerZbritjeId"].Unique = true;

            dtDaljaMallitDetaleAktivitetet.Constraints.Add(new ForeignKeyConstraint("FK_DaljaMallitDetaleAktivitetet_DaljaMallitId",
                Daljet.Columns["Id"], dtDaljaMallitDetaleAktivitetet.Columns["DaljaMallitId"])
            {
                DeleteRule = Rule.Cascade,
                UpdateRule = Rule.Cascade
            });

            dtKuponat.Constraints.Add(new ForeignKeyConstraint("FK_dtKuponat_DaljaMallitIdGjeneruar",
                Daljet.Columns["Id"], dtKuponat.Columns["DaljaMallitIdGjeneruar"])
            {
                DeleteRule = Rule.Cascade,
                UpdateRule = Rule.Cascade
            });

            dtKuponat.Constraints.Add(new ForeignKeyConstraint("FK_dtKuponat_DaljaMallitIdAplikuar",
                Daljet.Columns["Id"], dtKuponat.Columns["DaljaMallitIdAplikuar"])
            {
                DeleteRule = Rule.Cascade,
                UpdateRule = Rule.Cascade
            });

            dtDaljaMallitVaucherat.Constraints.Add(new ForeignKeyConstraint("FK_KodetEKuponave_DaljaMallitIdAplikuar",
            Daljet.Columns["Id"], dtDaljaMallitVaucherat.Columns["DaljaMallitID"])
            {
                DeleteRule = Rule.Cascade,
                UpdateRule = Rule.Cascade
            });

            DsHeader.EnforceConstraints = true;
        }
    }
}
