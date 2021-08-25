using ToshibaPos.SDK;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ToshibaPosSinkronizimi
{
    public class DaljaMallitSyncCL
    {
        public TextBox txtInto { get; set; } = new TextBox();
        public DaljaMallitSyncCL()
        {

        }
        public void Sinkronizo()
        {
            GetDaljetLokale();
            foreach (DataRow dr in dtDaljaMallit.Rows)
            {
                Set(dr);
                if (ServerId == null)
                {
                    if (!AEshteEgzistonFaturaNeServer())
                    {
                        SinkronizoMeServer();
                    }
                }
            }
        }
        void SinkronizoMeServer()
        {

            switch (DokumentiId)
            {
                case 45://FletëDergesë Malli
                    SinkronizoFletedergesen();
                    break;
                case 46://Korrektim FletëDergesë Malli
                    SinkronizoFletedergesen();
                    break;
                case 54://FletëDergesë pa TVSH
                    SinkronizoFletedergesen();
                    break;
                case 55://Korrektim FletëDergesë pa TVSH
                    SinkronizoFletedergesen();
                    break;
                case 61://61	Perdorim Intern
                    SinkronizoDaljenInterne();
                    break;
                case 62://62	Furrë Perdorimi Intern
                    SinkronizoDaljenInterne();
                    break;
                case 65://65	Korrektim Perdorim Intern
                    SinkronizoDaljenInterne();
                    break;
                case 66://66	Korrektim Furrë Perdorimi Intern
                    SinkronizoDaljenInterne();
                    break;
                case 811://811	Faturë Interne
                    SinkronizoDaljenInterne();
                    break;
                case 812://812	Korrektim Faturë Interne
                    SinkronizoDaljenInterne();
                    break;
                //case 965://965	Perdorim Intern Restorant
                //    SinkronizoDaljenInterne();
                //    break;
                default:
                    SinkronizoFaturen();
                    break;
            }
        }
        void SinkronizoFaturen()
        {
        }
        void SinkronizoDaljenInterne()
        {

        }
        void SinkronizoFletedergesen()
        {

        }
        public long Id { get; set; }
        public int OrganizataId { get; set; }
        public int Viti { get; set; }
        public DateTime Data { get; set; }
        public int NrFatures { get; set; }
        public int DokumentiId { get; set; }
        public int RegjistruarNga { get; set; }
        public int NumriArkes { get; set; }
        public DateTime DataERegjistrimit { get; set; }
        public int? SubjektiId { get; set; }
        public bool ShitjeEPerjashtuar { get; set; }
        public string Koment { get; set; }
        public bool? Xhirollogari { get; set; }
        public bool Sinkronizuar { get; set; }
        public bool NeTransfer { get; set; }
        public int DepartamentiId { get; set; }
        public bool? Validuar { get; set; }
        public int AfatiPageses { get; set; }
        public long? DaljaMallitKorrektuarId { get; set; }
        public string NrDuditX3 { get; set; }
        public DateTime? DataFatures { get; set; }
        public bool RaportDoganor { get; set; }
        public decimal Kursi { get; set; }
        public int? ValutaId { get; set; }
        public bool KuponiFiskalShtypur { get; set; }
        public int? K6 { get; set; }
        public int? K7 { get; set; }
        public int? K8 { get; set; }
        public int? K9 { get; set; }
        public int? K10 { get; set; }
        public DateTime? DataValidimit { get; set; }
        public string DaljaMallitImportuarId { get; set; }
        public string IdLokal { get; set; }
        public long? FaturaKomulativeId { get; set; }
        public int? TrackingId { get; set; }
        public string NumriFaturesManual { get; set; }
        public int? ZbritjeNgaOperatori { get; set; }
        //public string RFID { get; set; }
        //public string RFIDCCP { get; set; }
        public long? BarazimiId { get; set; }
        public long? ServerId { get; set; }
        public string ReferencaID { get; set; }
        public int? KartelaId { get; set; }
        public bool MeVete { get; set; }
        public string NumriATK { get; set; }
        public string NumriArkesGK { get; set; }
        public string Personi { get; set; }
        public string Adresa { get; set; }
        public string NumriPersonal { get; set; }
        public string NrTel { get; set; }
        DataTable dtDaljaMallit { get; set; }
        void Set(DataRow dr)
        {
            if (!string.IsNullOrEmpty(dr["Id"].ToString()))
                Id = (long)dr["Id"];
            if (!string.IsNullOrEmpty(dr["OrganizataId"].ToString()))
                OrganizataId = (int)dr["OrganizataId"];
            if (!string.IsNullOrEmpty(dr["Viti"].ToString()))
                Viti = (int)dr["Viti"];
            if (!string.IsNullOrEmpty(dr["Data"].ToString()))
                Data = (DateTime)dr["Data"];
            if (!string.IsNullOrEmpty(dr["NrFatures"].ToString()))
                NrFatures = (int)dr["NrFatures"];
            if (!string.IsNullOrEmpty(dr["DokumentiId"].ToString()))
                DokumentiId = (int)dr["DokumentiId"];
            if (!string.IsNullOrEmpty(dr["RegjistruarNga"].ToString()))
                RegjistruarNga = (int)dr["RegjistruarNga"];
            if (!string.IsNullOrEmpty(dr["NumriArkes"].ToString()))
                NumriArkes = (int)dr["NumriArkes"];
            if (!string.IsNullOrEmpty(dr["DataERegjistrimit"].ToString()))
                DataERegjistrimit = (DateTime)dr["DataERegjistrimit"];
            if (!string.IsNullOrEmpty(dr["SubjektiId"].ToString()))
                SubjektiId = (int)dr["SubjektiId"];
            if (!string.IsNullOrEmpty(dr["ShitjeEPerjashtuar"].ToString()))
                ShitjeEPerjashtuar = Convert.ToBoolean(dr["ShitjeEPerjashtuar"]);
            if (!string.IsNullOrEmpty(dr["Koment"].ToString()))
                Koment = (string)dr["Koment"];
            if (!string.IsNullOrEmpty(dr["Xhirollogari"].ToString()))
                Xhirollogari = Convert.ToBoolean(dr["Xhirollogari"]);
            if (!string.IsNullOrEmpty(dr["Sinkronizuar"].ToString()))
                Sinkronizuar = Convert.ToBoolean(dr["Sinkronizuar"]);
            if (!string.IsNullOrEmpty(dr["NeTransfer"].ToString()))
                NeTransfer = Convert.ToBoolean(dr["NeTransfer"]);
            if (!string.IsNullOrEmpty(dr["DepartamentiId"].ToString()))
                DepartamentiId = (int)dr["DepartamentiId"];
            if (!string.IsNullOrEmpty(dr["Validuar"].ToString()))
                Validuar = Convert.ToBoolean(dr["Validuar"]);
            if (!string.IsNullOrEmpty(dr["AfatiPageses"].ToString()))
                AfatiPageses = (int)dr["AfatiPageses"];
            if (!string.IsNullOrEmpty(dr["DaljaMallitKorrektuarId"].ToString()))
                DaljaMallitKorrektuarId = (long)dr["DaljaMallitKorrektuarId"];
            if (!string.IsNullOrEmpty(dr["NrDuditX3"].ToString()))
                NrDuditX3 = (string)dr["NrDuditX3"];
            if (!string.IsNullOrEmpty(dr["DataFatures"].ToString()))
                DataFatures = (DateTime)dr["DataFatures"];
            if (!string.IsNullOrEmpty(dr["RaportDoganor"].ToString()))
                RaportDoganor = Convert.ToBoolean(dr["RaportDoganor"].ToString());
            if (!string.IsNullOrEmpty(dr["Kursi"].ToString()))
                Kursi = (decimal)dr["Kursi"];
            if (!string.IsNullOrEmpty(dr["ValutaId"].ToString()))
                ValutaId = (int)dr["ValutaId"];
            if (!string.IsNullOrEmpty(dr["KuponiFiskalShtypur"].ToString()))
                KuponiFiskalShtypur = Convert.ToBoolean(dr["KuponiFiskalShtypur"].ToString());
            if (!string.IsNullOrEmpty(dr["K6"].ToString()))
                K6 = (int)dr["K6"];
            if (!string.IsNullOrEmpty(dr["K7"].ToString()))
                K7 = (int)dr["K7"];
            if (!string.IsNullOrEmpty(dr["K8"].ToString()))
                K8 = (int)dr["K8"];
            if (!string.IsNullOrEmpty(dr["K9"].ToString()))
                K9 = (int)dr["K9"];
            if (!string.IsNullOrEmpty(dr["K10"].ToString()))
                K10 = (int)dr["K10"];
            if (!string.IsNullOrEmpty(dr["DataValidimit"].ToString()))
                DataValidimit = (DateTime)dr["DataValidimit"];
            if (!string.IsNullOrEmpty(dr["DaljaMallitImportuarId"].ToString()))
                DaljaMallitImportuarId = (string)dr["DaljaMallitImportuarId"];
            if (!string.IsNullOrEmpty(dr["IdLokal"].ToString()))
                IdLokal = (string)dr["IdLokal"];
            if (!string.IsNullOrEmpty(dr["FaturaKomulativeId"].ToString()))
                FaturaKomulativeId = (long)dr["FaturaKomulativeId"];
            if (!string.IsNullOrEmpty(dr["TrackingId"].ToString()))
                TrackingId = (int)dr["TrackingId"];
            if (!string.IsNullOrEmpty(dr["NumriFaturesManual"].ToString()))
                NumriFaturesManual = (string)dr["NumriFaturesManual"];
            if (!string.IsNullOrEmpty(dr["ZbritjeNgaOperatori"].ToString()))
                ZbritjeNgaOperatori = (int)dr["ZbritjeNgaOperatori"];
            //if (!string.IsNullOrEmpty(dr["RFID"].ToString()))
            //    RFID = (string)dr["RFID"];
            //if (!string.IsNullOrEmpty(dr["RFIDCCP"].ToString()))
            //    RFIDCCP = (string)dr["RFIDCCP"];
            if (!string.IsNullOrEmpty(dr["BarazimiId"].ToString()))
                BarazimiId = (long)dr["BarazimiId"];
            if (!string.IsNullOrEmpty(dr["ServerId"].ToString()))
                ServerId = (long)dr["ServerId"];
            if (!string.IsNullOrEmpty(dr["ReferencaID"].ToString()))
                ReferencaID = dr["ReferencaID"].ToString();
            if (!string.IsNullOrEmpty(dr["KartelaId"].ToString()))
                KartelaId = Convert.ToInt32(dr["KartelaId"].ToString());
            if (!string.IsNullOrEmpty(dr["MeVete"].ToString()))
                MeVete = Convert.ToBoolean(dr["MeVete"].ToString());
            if (!string.IsNullOrEmpty(dr["NumriATK"].ToString()))
                NumriATK = dr["NumriATK"].ToString();
            if (!string.IsNullOrEmpty(dr["NumriArkesGK"].ToString()))
                NumriArkesGK = dr["NumriArkesGK"].ToString();
            if (!string.IsNullOrEmpty(dr["Personi"].ToString()))
                Personi = dr["Personi"].ToString();
            if (!string.IsNullOrEmpty(dr["Adresa"].ToString()))
                Adresa = dr["Adresa"].ToString();
            if (!string.IsNullOrEmpty(dr["NumriPersonal"].ToString()))
                NumriPersonal = dr["NumriPersonal"].ToString();
            if (!string.IsNullOrEmpty(dr["NrTel"].ToString()))
                NrTel = dr["NrTel"].ToString();
        }
        void GetDaljetLokale()
        {
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.DaljaMallitPerSinkronizimSelect_sp", PublicClass.KoneksioniLokal);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Sinkronizuar", false);
            da.SelectCommand.Parameters.AddWithValue("@Validuar", true);
            da.Fill(dtDaljaMallit);
        }
        bool AEshteEgzistonFaturaNeServer()
        {
            SqlConnection cnnServerKryesore = new SqlConnection(PublicClass.KoneksioniPrimar);
            try
            {
                cnnServerKryesore.Open();
                string a = "";
                SqlCommand cmd = new SqlCommand("TOSHIBA.DaljaMallitVerifikoFaturenLokale_Sp", cnnServerKryesore);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdLokal", IdLokal);
                cmd.Parameters.AddWithValue("@organizataid", OrganizataId);
                long ser;
                long.TryParse(cmd.ExecuteScalar().ToString(), out ser);
                if (ser > 0)
                {
                    ServerId = ser;
                    EgzistonNeServer();
                    return true;
                }
                else
                    return false;

            }
            catch (Exception ex)
            {
                AppendDergimiTextBox(ex.Message, false);
                throw ex;
            }
            finally
            {
                cnnServerKryesore.Close();
            }
        }
        public void AppendDergimiTextBox(string value, bool OK)
        {

            if (txtInto.InvokeRequired)
            {
                txtInto.Invoke((MethodInvoker)delegate
                {
                    txtInto.Text = value;
                });
                if (OK)
                    txtInto.ForeColor = Color.White;
                else
                    txtInto.ForeColor = Color.Red;
            }
            else
            {
                txtInto.Text = value;

                if (OK)
                    txtInto.ForeColor = Color.White;
                else
                    txtInto.ForeColor = Color.Red;

                txtInto.Refresh();
            }

        }
        void EgzistonNeServer()
        {
            string b = PublicClass.KoneksioniLokal.ToString();
            SqlConnection cnnLokal = new SqlConnection(b);
            try
            {
                cnnLokal.Open();
                string a = "";
                SqlCommand cmdDaljaMallitIdServerUpdateUpdate_spUpd = new SqlCommand("TOSHIBA.DaljaMallitIdServerUpdate_sp", cnnLokal);
                cmdDaljaMallitIdServerUpdateUpdate_spUpd.CommandType = CommandType.StoredProcedure;
                cmdDaljaMallitIdServerUpdateUpdate_spUpd.Parameters.AddWithValue("@DaljaMallitIdServer", ServerId);
                cmdDaljaMallitIdServerUpdateUpdate_spUpd.Parameters.AddWithValue("@DaljaMallitIdLokal", Id);
                cmdDaljaMallitIdServerUpdateUpdate_spUpd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                AppendDergimiTextBox(ex.Message, false);
                throw ex;
            }
            finally
            {
                cnnLokal.Close();
            }
        }
        DataTable GetDaljetDetaletLokale()
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_DaljaMallitDetaleSelect_sp", PublicClass.KoneksioniLokal);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DaljaMallitId", Id);
            da.Fill(tabela);
            return tabela;
        }
    }
}