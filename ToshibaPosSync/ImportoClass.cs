using System;
using System.Data;
using System.Data.SqlClient;
using ToshibaPos.SDK;

namespace ToshibaPosSinkronizimi
{
    public class ImportoClass
    {

        public ImportoClass()
        {

        }
        DataTable TabelatPerSinkronizim()
        {
            DataTable objektet = new DataTable();
            objektet.Columns.Add("Nr", typeof(int));
            objektet.Columns.Add("PershkrimiObjektit", typeof(string));
            objektet.Rows.Add(objektet.Rows.Count, "Brendet");
            objektet.Rows.Add(objektet.Rows.Count, "LlojetEArtikullit");
            objektet.Rows.Add(objektet.Rows.Count, "Akciza");
            objektet.Rows.Add(objektet.Rows.Count, "Tatimet");
            objektet.Rows.Add(objektet.Rows.Count, "Njesit");
            objektet.Rows.Add(objektet.Rows.Count, "Artikujt");
            objektet.Rows.Add(objektet.Rows.Count, "SubjektiLloji");
            objektet.Rows.Add(objektet.Rows.Count, "Subjektet");
            objektet.Rows.Add(objektet.Rows.Count, "Mxh_Filialet");
            objektet.Rows.Add(objektet.Rows.Count, "Mxh_GrupetEShfrytezuesve");
            objektet.Rows.Add(objektet.Rows.Count, "Mxh_Operatoret");
            objektet.Rows.Add(objektet.Rows.Count, "ArtikujtMeLirim");
            objektet.Rows.Add(objektet.Rows.Count, "ArkaOperatoret");
            objektet.Rows.Add(objektet.Rows.Count, "Barkodat");
            objektet.Rows.Add(objektet.Rows.Count, "StandardetEBarkodave");
            objektet.Rows.Add(objektet.Rows.Count, "Peshoret");
            objektet.Rows.Add(objektet.Rows.Count, "PLUPeshoret");
            objektet.Rows.Add(objektet.Rows.Count, "Cmimorja");
            objektet.Rows.Add(objektet.Rows.Count, "MenyratEPageses");
            objektet.Rows.Add(objektet.Rows.Count, "LlojetEDokumenteve");
            objektet.Rows.Add(objektet.Rows.Count, "Departamentet");
            objektet.Rows.Add(objektet.Rows.Count, "AmortizimiKategorite");
            objektet.Rows.Add(objektet.Rows.Count, "Konfigurimet");
            objektet.Rows.Add(objektet.Rows.Count, "Mxh_OrganizataDetalet");
            objektet.Rows.Add(objektet.Rows.Count, "CCPLlojetEKartelave");
            objektet.Rows.Add(objektet.Rows.Count, "POS_PlaniZbritjeve");
            objektet.Rows.Add(objektet.Rows.Count, "Bankat");
            objektet.Rows.Add(objektet.Rows.Count, "SubjektiBankat");
            objektet.Rows.Add(objektet.Rows.Count, "Arkat");
            objektet.Rows.Add(objektet.Rows.Count, "GrupetEMallrave");
            objektet.Rows.Add(objektet.Rows.Count, "Klasifikatoret");
            objektet.Rows.Add(objektet.Rows.Count, "RaportetFajllat");
            objektet.Rows.Add(objektet.Rows.Count, "CCPKartelatArtikujt");
            objektet.Rows.Add(objektet.Rows.Count, "CCPSubjektetGrupet");
            objektet.Rows.Add(objektet.Rows.Count, "DaljaMallitRreshtatEPare");
            objektet.Rows.Add(objektet.Rows.Count, "DaljeInternePersonat");
            objektet.Rows.Add(objektet.Rows.Count, "POSAktivitetetLlojet");
            objektet.Rows.Add(objektet.Rows.Count, "POSAktivitetet");
            objektet.Rows.Add(objektet.Rows.Count, "POSAktivitetetFilterLlojet");
            objektet.Rows.Add(objektet.Rows.Count, "POSAktivitetetLlojetEZbritjes");
            objektet.Rows.Add(objektet.Rows.Count, "POSAktivitetetOrganizatat");
            objektet.Rows.Add(objektet.Rows.Count, "POSAktivitetetZbritjaNeVlere");
            objektet.Rows.Add(objektet.Rows.Count, "POSAktivitetetFilter");
            objektet.Rows.Add(objektet.Rows.Count, "Mxh_OperatoretGrupetEMallrave");
            objektet.Rows.Add(objektet.Rows.Count, "CCPGrupet");
            objektet.Rows.Add(objektet.Rows.Count, "POSAktivitetetFilterCCPGrupet");
            objektet.Rows.Add(objektet.Rows.Count, "ArtikujtFoto");
            objektet.Rows.Add(objektet.Rows.Count, "Parapagimet");
            objektet.Rows.Add(objektet.Rows.Count, "ArtikujtPerberesOrganizatat");
            objektet.Rows.Add(objektet.Rows.Count, "AktivitetetDokument");
            objektet.Rows.Add(objektet.Rows.Count, "AktivitetetDokumentDetale");
            objektet.Rows.Add(objektet.Rows.Count, "Valutat");
            objektet.Rows.Add(objektet.Rows.Count, "POSAktivitetetKuponat");
            objektet.Rows.Add(objektet.Rows.Count, "CCPShperblimetArtikujt");
            objektet.Rows.Add(objektet.Rows.Count, "CCPFaturat");
            objektet.Rows.Add(objektet.Rows.Count, "CCPArtikujtEPerjashtuar");
            objektet.Rows.Add(objektet.Rows.Count, "POSKuponatPerZbritje");
            objektet.Rows.Add(objektet.Rows.Count, "ArtikujtPaZbritje");
            objektet.Rows.Add(objektet.Rows.Count, "CCPAlokimiBonuseve");
            objektet.Rows.Add(objektet.Rows.Count, "KthimiMallitArsyet");
            objektet.Rows.Add(objektet.Rows.Count, "DaljaMallitKushteEBleresit");
            objektet.Rows.Add(objektet.Rows.Count, "DaljaMallitKushteEBleresitArtikujt");

            return objektet;
        }

        public void ImportoShenimet()
        {
            string sqltabela = "";

            try
            {
                DataTable objektet = TabelatPerSinkronizim();

                SqlDataAdapter daPerGjenerimTeDbServer = new SqlDataAdapter();
                daPerGjenerimTeDbServer.SelectCommand = new SqlCommand("TOSHIBA.DataKohaSelect_Sp", new SqlConnection(PublicClass.KoneksioniPrimar)); //PB.DataKohaSelect_Sp
                daPerGjenerimTeDbServer.SelectCommand.Connection.Open();
                DateTime DataKohaNgaServeri = (DateTime)daPerGjenerimTeDbServer.SelectCommand.ExecuteScalar();
                daPerGjenerimTeDbServer.SelectCommand.Connection.Close();

                SqlDataAdapter daPerGjenerimTeDbLokal = new SqlDataAdapter();
                daPerGjenerimTeDbLokal.SelectCommand = new SqlCommand();
                daPerGjenerimTeDbLokal.SelectCommand.Connection = new SqlConnection(PublicClass.KoneksioniLokal);
                daPerGjenerimTeDbLokal.SelectCommand.Connection.Open();
                daPerGjenerimTeDbLokal.SelectCommand.CommandText = "TOSHIBA.POS_LokalDBKohaSinkronizimitMaxSelect_Sp";

                DateTime DataKohaSinkronizimitParaprak;

                if (daPerGjenerimTeDbLokal.SelectCommand.ExecuteScalar() != DBNull.Value)
                {
                    DataKohaSinkronizimitParaprak = (DateTime)daPerGjenerimTeDbLokal.SelectCommand.ExecuteScalar();
                }
                else
                {
                    DataKohaSinkronizimitParaprak = DataKohaNgaServeri;
                }

                daPerGjenerimTeDbLokal.SelectCommand.Connection.Close();

                int x = 0;
                int y = objektet.Rows.Count;

                foreach (DataRow row in objektet.Rows)
                {
                    x = x + 1;
                    decimal rez = Convert.ToDecimal(x) / Convert.ToDecimal(y) * 100.00M;

                    sqltabela = row["PershkrimiObjektit"].ToString();
                    SinkronizimiClass.Sync.AppendImportimiTextBox("Importimi:" + sqltabela, true);
                    switch (row["PershkrimiObjektit"].ToString())
                    {
                        case "GrupetEMallrave":
                            if (MeriTeGjitheRreshtat(row["PershkrimiObjektit"].ToString()))
                                POS_RuajGrupetEMallrave(DataKohaSinkronizimitParaprak.AddYears(-10));
                            else
                                POS_RuajGrupetEMallrave(DataKohaSinkronizimitParaprak);
                            break;
                        case "Tatimet":
                            if (MeriTeGjitheRreshtat(row["PershkrimiObjektit"].ToString()))
                                POS_RuajTatimet(DataKohaSinkronizimitParaprak.AddYears(-10));
                            else
                                POS_RuajTatimet(DataKohaSinkronizimitParaprak);
                            break;
                        case "Brendet":
                            if (MeriTeGjitheRreshtat(row["PershkrimiObjektit"].ToString()))
                                POS_RuajBrendet(DataKohaSinkronizimitParaprak.AddYears(-10));
                            else
                                POS_RuajBrendet(DataKohaSinkronizimitParaprak);
                            break;
                        case "LlojetEArtikullit":
                            if (MeriTeGjitheRreshtat(row["PershkrimiObjektit"].ToString()))
                                POS_RuajLlojetEArtikullit(DataKohaSinkronizimitParaprak.AddYears(-10));
                            else
                                POS_RuajLlojetEArtikullit(DataKohaSinkronizimitParaprak);
                            break;
                        case "Njesit":
                            if (MeriTeGjitheRreshtat(row["PershkrimiObjektit"].ToString()))
                                POS_RuajNjesit(DataKohaSinkronizimitParaprak.AddYears(-10));
                            else
                                POS_RuajNjesit(DataKohaSinkronizimitParaprak);
                            break;
                        case "Artikujt":
                            if (MeriTeGjitheRreshtat(row["PershkrimiObjektit"].ToString()))
                                POS_RuajArtikujt(DataKohaSinkronizimitParaprak.AddYears(-10));
                            else
                                POS_RuajArtikujt(DataKohaSinkronizimitParaprak);
                            break;
                        case "SubjektiLloji":
                            if (MeriTeGjitheRreshtat(row["PershkrimiObjektit"].ToString()))
                                POS_RuajSubjektiLloji(DataKohaSinkronizimitParaprak.AddYears(-10));
                            else
                                POS_RuajSubjektiLloji(DataKohaSinkronizimitParaprak);
                            break;
                        case "Subjektet":
                            if (MeriTeGjitheRreshtat(row["PershkrimiObjektit"].ToString()))
                                POS_RuajSubjektet(DataKohaSinkronizimitParaprak.AddYears(-10));
                            else
                                POS_RuajSubjektet(DataKohaSinkronizimitParaprak);
                            break;
                        case "Mxh_Filialet":
                            if (MeriTeGjitheRreshtat(row["PershkrimiObjektit"].ToString()))
                                POS_RuajMxh_Filialet(DataKohaSinkronizimitParaprak.AddYears(-10));
                            else
                                POS_RuajMxh_Filialet(DataKohaSinkronizimitParaprak);
                            break;
                        case "Mxh_GrupetEShfrytezuesve":
                            if (MeriTeGjitheRreshtat(row["PershkrimiObjektit"].ToString()))
                                POS_RuajMxh_GrupetEShfrytezuesve(DataKohaSinkronizimitParaprak.AddYears(-10));
                            else
                                POS_RuajMxh_GrupetEShfrytezuesve(DataKohaSinkronizimitParaprak);
                            break;
                        case "Mxh_Operatoret":
                            if (MeriTeGjitheRreshtat(row["PershkrimiObjektit"].ToString()))
                                POS_RuajMxh_Operatoret(DataKohaSinkronizimitParaprak.AddYears(-10));
                            else
                                POS_RuajMxh_Operatoret(DataKohaSinkronizimitParaprak);
                            break;
                        case "ArtikujtMeLirim":
                            if (MeriTeGjitheRreshtat(row["PershkrimiObjektit"].ToString()))
                                POS_RuajArtikujtMeLirim(DataKohaSinkronizimitParaprak.AddYears(-10));
                            else
                                POS_RuajArtikujtMeLirim(DataKohaSinkronizimitParaprak);
                            break;
                        case "Barkodat":
                            if (MeriTeGjitheRreshtat(row["PershkrimiObjektit"].ToString()))
                                POS_RuajBarkodat(DataKohaSinkronizimitParaprak.AddYears(-10));
                            else
                                POS_RuajBarkodat(DataKohaSinkronizimitParaprak);
                            break;
                        case "StandardetEBarkodave":
                            if (MeriTeGjitheRreshtat(row["PershkrimiObjektit"].ToString()))
                                POS_RuajStandardetEBarkodave(DataKohaSinkronizimitParaprak.AddYears(-10));
                            else
                                POS_RuajStandardetEBarkodave(DataKohaSinkronizimitParaprak);
                            break;
                        case "Peshoret":
                            if (MeriTeGjitheRreshtat(row["PershkrimiObjektit"].ToString()))
                                POS_RuajPeshoret(DataKohaSinkronizimitParaprak.AddYears(-10));
                            else
                                POS_RuajPeshoret(DataKohaSinkronizimitParaprak);
                            break;
                        case "PLUPeshoret":
                            if (MeriTeGjitheRreshtat(row["PershkrimiObjektit"].ToString()))
                                POS_RuajPLUPeshoret(DataKohaSinkronizimitParaprak.AddYears(-10));
                            else
                                POS_RuajPLUPeshoret(DataKohaSinkronizimitParaprak);
                            break;
                        case "Cmimorja":
                            if (MeriTeGjitheRreshtat(row["PershkrimiObjektit"].ToString()))
                                POS_RuajCmimorja(DataKohaSinkronizimitParaprak.AddYears(-10));
                            else
                                POS_RuajCmimorja(DataKohaSinkronizimitParaprak);
                            break;
                        case "MenyratEPageses":
                            if (MeriTeGjitheRreshtat(row["PershkrimiObjektit"].ToString()))
                                POS_RuajMenyratEPageses(DataKohaSinkronizimitParaprak.AddYears(-10));
                            else
                                POS_RuajMenyratEPageses(DataKohaSinkronizimitParaprak);
                            break;
                        case "LlojetEDokumenteve":
                            if (MeriTeGjitheRreshtat(row["PershkrimiObjektit"].ToString()))
                                POS_RuajLlojetEDokumenteve(DataKohaSinkronizimitParaprak.AddYears(-10));
                            else
                                POS_RuajLlojetEDokumenteve(DataKohaSinkronizimitParaprak);
                            break;
                        case "Departamentet":
                            if (MeriTeGjitheRreshtat(row["PershkrimiObjektit"].ToString()))
                                POS_RuajDepartamentet(DataKohaSinkronizimitParaprak.AddYears(-10));
                            else
                                POS_RuajDepartamentet(DataKohaSinkronizimitParaprak);
                            break;
                        case "Konfigurimet":
                            if (MeriTeGjitheRreshtat(row["PershkrimiObjektit"].ToString()))
                                POS_RuajKonfigurimet(DataKohaSinkronizimitParaprak.AddYears(-10));
                            else
                                POS_RuajKonfigurimet(DataKohaSinkronizimitParaprak);
                            break;
                        case "Mxh_OrganizataDetalet":
                            if (MeriTeGjitheRreshtat(row["PershkrimiObjektit"].ToString()))
                                POS_RuajMxh_OrganizataDetalet(DataKohaSinkronizimitParaprak.AddYears(-10));
                            else
                                POS_RuajMxh_OrganizataDetalet(DataKohaSinkronizimitParaprak);
                            break;
                        case "CCPLlojetEKartelave":
                            if (MeriTeGjitheRreshtat(row["PershkrimiObjektit"].ToString()))
                                POS_RuajCCPLlojetEKartelave(DataKohaSinkronizimitParaprak.AddYears(-10));
                            else
                                POS_RuajCCPLlojetEKartelave(DataKohaSinkronizimitParaprak);
                            break;
                        case "SubjektiBankat":
                            if (MeriTeGjitheRreshtat(row["PershkrimiObjektit"].ToString()))
                                POS_RuajSubjektiBankat(DataKohaSinkronizimitParaprak.AddYears(-10));
                            else
                                POS_RuajSubjektiBankat(DataKohaSinkronizimitParaprak);
                            break;
                        case "Bankat":
                            if (MeriTeGjitheRreshtat(row["PershkrimiObjektit"].ToString()))
                                POS_RuajBankat(DataKohaSinkronizimitParaprak.AddYears(-10));
                            else
                                POS_RuajBankat(DataKohaSinkronizimitParaprak);
                            break;
                        case "Arkat":
                            if (MeriTeGjitheRreshtat(row["PershkrimiObjektit"].ToString()))
                                POS_RuajArkat(DataKohaSinkronizimitParaprak.AddYears(-10));
                            else
                                POS_RuajArkat(DataKohaSinkronizimitParaprak);
                            break;
                        case "Klasifikatoret":
                            Klasifikatoret(DataKohaSinkronizimitParaprak);
                            break;
                        case "RaportetFajllat":
                            RaportetFajllat(DataKohaSinkronizimitParaprak);
                            break;
                        case "CCPKartelatArtikujt":
                            KartelatArtikujt(DataKohaSinkronizimitParaprak);
                            break;

                        //////AKTIVITETET//////                                
                        case "CCPSubjektetGrupet":
                            CCPSubjektetGrupet(DataKohaSinkronizimitParaprak);
                            break;
                        case "DaljaMallitRreshtatEPare":
                            DaljaMallitRreshtatEPare();
                            break;
                        case "DaljeInternePersonat":
                            DaljeInternePersonat(DataKohaSinkronizimitParaprak);
                            break;
                        case "POSAktivitetetLlojet":
                            POSAktivitetetLlojet();
                            break;
                        case "POSAktivitetet":
                            POSAktivitetet();
                            break;
                        case "POSAktivitetetFilterLlojet":
                            POSAktivitetetFilterLlojet();
                            break;
                        case "POSAktivitetetLlojetEZbritjes":
                            POSAktivitetetLlojetEZbritjes();
                            break;
                        case "POSAktivitetetOrganizatat":
                            POSAktivitetetOrganizatat();
                            break;
                        case "POSAktivitetetZbritjaNeVlere":
                            POSAktivitetetZbritjaNeVlere();
                            break;
                        case "POSAktivitetetFilter":
                            POSAktivitetetFilter();
                            break;
                        case "Mxh_OperatoretGrupetEMallrave":
                            Mxh_OperatoretGrupetEMallrave();
                            break;
                        case "CCPGrupet":
                            CCPGrupet();
                            break;
                        case "POSAktivitetetFilterCCPGrupet":
                            POSAktivitetetFilterCCPGrupet();
                            break;
                        case "ArtikujtFoto":
                            ArtikujtFoto(DataKohaSinkronizimitParaprak);
                            break;
                        //case "Parapagimet":
                        //    Parapagimet();
                        //    break;
                        //case "ArtikujtPerberesOrganizatat":
                        //    ArtikujtPerberesOrganizatat();
                        //    break;
                        case "AktivitetetDokument":
                            AktivitetetDokument();
                            break;
                        case "AktivitetetDokumentDetale":
                            AktivitetetDokumentDetale();
                            break;
                        case "Valutat":
                            Valutat();
                            break;
                        //case "POSAktivitetetKuponat":
                        //    POSAktivitetetKuponat();
                        //    break;
                        case "CCPShperblimetArtikujt":
                            CCPShperblimetArtikujt();
                            break;
                        case "CCPFaturat":
                            CCPFaturat();
                            break;
                        case "CCPArtikujtEPerjashtuar":
                            CCPArtikujtEPerjashtuar();
                            break;
                        //case "POSKuponatPerZbritje":
                        //    POSKuponatPerZbritje();
                        //    break;
                        case "ArtikujtPaZbritje":
                            ArtikujtPaZbritje();
                            break;
                        case "CCPAlokimiBonuseve":
                            CCPAlokimiBonuseve();
                            break;
                        case "KthimiMallitArsyet":
                            KthimiMallitArsyet();
                            break;
                        case "DaljaMallitKushteEBleresit":
                            DaljaMallitKushteEBleresit(DataKohaSinkronizimitParaprak);
                            break;
                        case "DaljaMallitKushteEBleresitArtikujt":
                            DaljaMallitKushteEBleresitArtikujt(DataKohaSinkronizimitParaprak);
                            break;
                    }
                }
                daPerGjenerimTeDbLokal.SelectCommand.Connection.Open();
                daPerGjenerimTeDbLokal.SelectCommand.CommandText = "TOSHIBA.POS_LokalDBKohaSinkronizimitInsert_Sp";
                daPerGjenerimTeDbLokal.SelectCommand.CommandType = CommandType.StoredProcedure;
                daPerGjenerimTeDbLokal.SelectCommand.Parameters.AddWithValue("@DataKoha", DataKohaNgaServeri);
                daPerGjenerimTeDbLokal.SelectCommand.ExecuteNonQuery();
                daPerGjenerimTeDbLokal.SelectCommand.Connection.Close();
            }
            catch (Exception ex)
            {
                string i = sqltabela;
                SinkronizimiClass.Sync.AppendImportimiTextBox(sqltabela + " Erro:" + ex.Message, true);
                throw new Exception(sqltabela + " Erro:" + ex.Message);
            }
            finally
            {

            }
        }
        #region Ruaj Shenimet Lokalisht 

        private void ArtikujtFoto(DateTime DataKohaSinkronizimitParaprak)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.ArtikujtFotoPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DeriMeOren", DataKohaSinkronizimitParaprak);
            da.Fill(tabela);
            if (tabela.Rows.Count > 0)
                try
                {
                    SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
                    cnn.Open();
                    SqlCommand cmdArtikujtFotoUpdateInsert_spUpd = new SqlCommand("TOSHIBA.ArtikujtFotoUpdateInsert_sp", cnn);
                    cmdArtikujtFotoUpdateInsert_spUpd.CommandType = CommandType.StoredProcedure;
                    cmdArtikujtFotoUpdateInsert_spUpd.Parameters.AddWithValue("@ArtikujtFotoType", tabela);
                    cmdArtikujtFotoUpdateInsert_spUpd.ExecuteNonQuery();
                    cnn.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
        }
        private void Parapagimet()
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.ParapagimetPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.Fill(tabela);

            try
            {
                SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
                cnn.Open();
                SqlCommand cmdParapagimetUpdateInsert_spUpd = new SqlCommand("TOSHIBA.ParapagimetUpdateInsert_sp", cnn);
                cmdParapagimetUpdateInsert_spUpd.CommandType = CommandType.StoredProcedure;
                cmdParapagimetUpdateInsert_spUpd.Parameters.AddWithValue("@ParapagimetType", tabela);
                cmdParapagimetUpdateInsert_spUpd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool MeriTeGjitheRreshtat(string emritabeles)
        {
            DataTable tabela1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select top 1 * from " + emritabeles, PublicClass.KoneksioniLokal);
            if (emritabeles == "Cmimorja")
                da = new SqlDataAdapter("Select top 1 * from " + emritabeles + " where OrganizataId=" + PublicClass.OrganizataId, PublicClass.KoneksioniLokal);

            da.Fill(tabela1);
            if (tabela1.Rows.Count > 0)
            {
                return false;
            }
            else
                return true;
        }
        private void POS_RuajTatimet(DateTime DataKohaSinkronizimitParaprak)
        {
            DataTable tabela1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_TatimetPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DeriMeOren", DataKohaSinkronizimitParaprak);
            da.Fill(tabela1);

            SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
            cnn.Open();
            SqlCommand cmdPOS_TatimetUpdateInsert_spUpd = new SqlCommand("TOSHIBA.POS_TatimetUpdateInsert_sp", cnn);
            cmdPOS_TatimetUpdateInsert_spUpd.CommandType = CommandType.StoredProcedure;
            cmdPOS_TatimetUpdateInsert_spUpd.Parameters.Add("@Id", SqlDbType.TinyInt);
            cmdPOS_TatimetUpdateInsert_spUpd.Parameters["@Id"].Direction = ParameterDirection.Input;
            cmdPOS_TatimetUpdateInsert_spUpd.Parameters.Add("@Pershkrimi", SqlDbType.VarChar);
            cmdPOS_TatimetUpdateInsert_spUpd.Parameters["@Pershkrimi"].Direction = ParameterDirection.Input;
            cmdPOS_TatimetUpdateInsert_spUpd.Parameters.Add("@Vlera", SqlDbType.Decimal);
            cmdPOS_TatimetUpdateInsert_spUpd.Parameters["@Vlera"].Direction = ParameterDirection.Input;
            cmdPOS_TatimetUpdateInsert_spUpd.Parameters.Add("@Statusi", SqlDbType.Bit);
            cmdPOS_TatimetUpdateInsert_spUpd.Parameters["@Statusi"].Direction = ParameterDirection.Input;
            cmdPOS_TatimetUpdateInsert_spUpd.Parameters.Add("@Kategoria", SqlDbType.VarChar);
            cmdPOS_TatimetUpdateInsert_spUpd.Parameters["@Kategoria"].Direction = ParameterDirection.Input;
            cmdPOS_TatimetUpdateInsert_spUpd.Parameters.Add("@DataERegjistrimit", SqlDbType.DateTime);
            cmdPOS_TatimetUpdateInsert_spUpd.Parameters["@DataERegjistrimit"].Direction = ParameterDirection.Input;
            try
            {

                foreach (DataRow Row in tabela1.Rows)
                {
                    cmdPOS_TatimetUpdateInsert_spUpd.Parameters["@Id"].Value = Row["Id"];
                    cmdPOS_TatimetUpdateInsert_spUpd.Parameters["@Pershkrimi"].Value = Row["Pershkrimi"];
                    cmdPOS_TatimetUpdateInsert_spUpd.Parameters["@Vlera"].Value = Row["Vlera"];
                    cmdPOS_TatimetUpdateInsert_spUpd.Parameters["@Statusi"].Value = Row["Statusi"];
                    cmdPOS_TatimetUpdateInsert_spUpd.Parameters["@Kategoria"].Value = Row["Kategoria"];
                    cmdPOS_TatimetUpdateInsert_spUpd.Parameters["@DataERegjistrimit"].Value = Row["DataERegjistrimit"];
                    cmdPOS_TatimetUpdateInsert_spUpd.ExecuteNonQuery();

                }
                cnn.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void POS_RuajSubjektiLloji(DateTime DataKohaSinkronizimitParaprak)
        {
            DataTable tabela1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_SubjektiLlojiPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DeriMeOren", DataKohaSinkronizimitParaprak);
            da.Fill(tabela1);
            SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
            cnn.Open();
            SqlCommand cmdPOS_SubjektiLlojiUpdateInsert_spUpd = new SqlCommand("TOSHIBA.POS_SubjektiLlojiUpdateInsert_sp", cnn);
            cmdPOS_SubjektiLlojiUpdateInsert_spUpd.CommandType = CommandType.StoredProcedure;
            cmdPOS_SubjektiLlojiUpdateInsert_spUpd.Parameters.Add("@Id", SqlDbType.Int);
            cmdPOS_SubjektiLlojiUpdateInsert_spUpd.Parameters["@Id"].Direction = ParameterDirection.Input;
            cmdPOS_SubjektiLlojiUpdateInsert_spUpd.Parameters.Add("@Shkurtesa", SqlDbType.VarChar);
            cmdPOS_SubjektiLlojiUpdateInsert_spUpd.Parameters["@Shkurtesa"].Direction = ParameterDirection.Input;
            cmdPOS_SubjektiLlojiUpdateInsert_spUpd.Parameters.Add("@Pershkrimi", SqlDbType.VarChar);
            cmdPOS_SubjektiLlojiUpdateInsert_spUpd.Parameters["@Pershkrimi"].Direction = ParameterDirection.Input;
            cmdPOS_SubjektiLlojiUpdateInsert_spUpd.Parameters.Add("@DataERegjistrimit", SqlDbType.DateTime);
            cmdPOS_SubjektiLlojiUpdateInsert_spUpd.Parameters["@DataERegjistrimit"].Direction = ParameterDirection.Input;



            try
            {

                foreach (DataRow Row in tabela1.Rows)
                {
                    cmdPOS_SubjektiLlojiUpdateInsert_spUpd.Parameters["@Id"].Value = Row["Id"];
                    cmdPOS_SubjektiLlojiUpdateInsert_spUpd.Parameters["@Shkurtesa"].Value = Row["Shkurtesa"];
                    cmdPOS_SubjektiLlojiUpdateInsert_spUpd.Parameters["@Pershkrimi"].Value = Row["Pershkrimi"];
                    cmdPOS_SubjektiLlojiUpdateInsert_spUpd.Parameters["@DataERegjistrimit"].Value = Row["DataERegjistrimit"];
                    cmdPOS_SubjektiLlojiUpdateInsert_spUpd.ExecuteNonQuery();

                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void POS_RuajSubjektet(DateTime DataKohaSinkronizimitParaprak)
        {
            DataTable tabela1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_SubjektetPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DeriMeOren", DataKohaSinkronizimitParaprak);
            da.Fill(tabela1);

            SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
            cnn.Open();
            SqlCommand cmdPOS_SubjektetUpdateInsert_spUpd = new SqlCommand("TOSHIBA.POS_SubjektetUpdateInsert_sp", cnn);
            cmdPOS_SubjektetUpdateInsert_spUpd.CommandType = CommandType.StoredProcedure;
            cmdPOS_SubjektetUpdateInsert_spUpd.Parameters.AddWithValue("@SubjektetTP", tabela1);
            try
            {
                if (tabela1.Rows.Count > 0)
                    cmdPOS_SubjektetUpdateInsert_spUpd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void POS_RuajStandardetEBarkodave(DateTime DataKohaSinkronizimitParaprak)
        {
            DataTable tabela1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_StandardetEBarkodavePerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DeriMeOren", DataKohaSinkronizimitParaprak);
            da.Fill(tabela1);

            SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
            cnn.Open();
            SqlCommand cmdPOS_StandardetEBarkodaveUpdateInsert_spUpd = new SqlCommand("TOSHIBA.POS_StandardetEBarkodaveUpdateInsert_sp", cnn);
            cmdPOS_StandardetEBarkodaveUpdateInsert_spUpd.CommandType = CommandType.StoredProcedure;


            cmdPOS_StandardetEBarkodaveUpdateInsert_spUpd.Parameters.Add("@Id", SqlDbType.Int);
            cmdPOS_StandardetEBarkodaveUpdateInsert_spUpd.Parameters["@Id"].Direction = ParameterDirection.Input;
            cmdPOS_StandardetEBarkodaveUpdateInsert_spUpd.Parameters.Add("@Tipi", SqlDbType.VarChar);
            cmdPOS_StandardetEBarkodaveUpdateInsert_spUpd.Parameters["@Tipi"].Direction = ParameterDirection.Input;
            cmdPOS_StandardetEBarkodaveUpdateInsert_spUpd.Parameters.Add("@DataERegjistrimit", SqlDbType.DateTime);
            cmdPOS_StandardetEBarkodaveUpdateInsert_spUpd.Parameters["@DataERegjistrimit"].Direction = ParameterDirection.Input;



            try
            {

                foreach (DataRow Row in tabela1.Rows)
                {
                    cmdPOS_StandardetEBarkodaveUpdateInsert_spUpd.Parameters["@Id"].Value = Row["Id"];
                    cmdPOS_StandardetEBarkodaveUpdateInsert_spUpd.Parameters["@Tipi"].Value = Row["Tipi"];
                    cmdPOS_StandardetEBarkodaveUpdateInsert_spUpd.Parameters["@DataERegjistrimit"].Value = Row["DataERegjistrimit"];
                    cmdPOS_StandardetEBarkodaveUpdateInsert_spUpd.ExecuteNonQuery();

                }
                cnn.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void POS_RuajPLUPeshoret(DateTime DataKohaSinkronizimitParaprak)
        {
            DataTable tabela1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_PLUPeshoretPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DeriMeOren", DataKohaSinkronizimitParaprak);
            da.Fill(tabela1);

            SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
            cnn.Open();
            SqlCommand cmdPOS_PLUPeshoretUpdateInsert_spUpd = new SqlCommand("TOSHIBA.POS_PLUPeshoretUpdateInsert_sp", cnn);
            cmdPOS_PLUPeshoretUpdateInsert_spUpd.CommandType = CommandType.StoredProcedure;
            cmdPOS_PLUPeshoretUpdateInsert_spUpd.Parameters.AddWithValue("@PLUPeshoretTp", tabela1);
            try
            {
                if (tabela1.Rows.Count > 0)
                    cmdPOS_PLUPeshoretUpdateInsert_spUpd.ExecuteNonQuery();

                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void POS_RuajArtikujtMeLirim(DateTime DataKohaSinkronizimitParaprak)
        {
            DataTable tabela1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_ArtikujtMeLirimPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DeriMeOren", DataKohaSinkronizimitParaprak);
            da.Fill(tabela1);
            SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
            cnn.Open();
            SqlCommand cmdPOS_ArtikujtMeLirimUpdateInsert_spUpd = new SqlCommand("TOSHIBA.POS_ArtikujtMeLirimUpdateInsert_sp", cnn);
            cmdPOS_ArtikujtMeLirimUpdateInsert_spUpd.CommandType = CommandType.StoredProcedure;
            cmdPOS_ArtikujtMeLirimUpdateInsert_spUpd.Parameters.AddWithValue("@ArtikujtMeLirimTP", tabela1);
            try
            {

                if (tabela1.Rows.Count > 0)
                    cmdPOS_ArtikujtMeLirimUpdateInsert_spUpd.ExecuteNonQuery();

                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void POS_RuajArtikujt(DateTime DataKohaSinkronizimitParaprak)
        {
            DataTable tabela1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_ArtikujtPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DeriMeOren", DataKohaSinkronizimitParaprak);
            da.Fill(tabela1);

            if (!tabela1.Columns.Contains("IdImportimit"))
            {
                tabela1.Columns.Add("tabela1", typeof(string));
            }

            SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
            cnn.Open();

            SqlCommand cmdPOS_ArtikujtUpdateInsert_spUpd = new SqlCommand("TOSHIBA.POS_ArtikujtUpdateInsert_sp", cnn);
            cmdPOS_ArtikujtUpdateInsert_spUpd.CommandType = CommandType.StoredProcedure;
            cmdPOS_ArtikujtUpdateInsert_spUpd.CommandTimeout = 20 * 60;
            cmdPOS_ArtikujtUpdateInsert_spUpd.Parameters.AddWithValue("@ArtikujtTP", tabela1);

            try
            {
                if (tabela1.Rows.Count > 0)
                    cmdPOS_ArtikujtUpdateInsert_spUpd.ExecuteNonQuery();

                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void POS_RuajBarkodat(DateTime DataKohaSinkronizimitParaprak)
        {
            DataTable tabela1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_BarkodatPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DeriMeOren", DataKohaSinkronizimitParaprak);
            da.Fill(tabela1);
            SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
            cnn.Open();
            SqlCommand cmdPOS_BarkodatUpdateInsert_spUpd = new SqlCommand("TOSHIBA.POS_BarkodatUpdateInsert_sp", cnn);
            cmdPOS_BarkodatUpdateInsert_spUpd.CommandType = CommandType.StoredProcedure;
            cmdPOS_BarkodatUpdateInsert_spUpd.Parameters.AddWithValue("@BarkodatTP", tabela1);
            try
            {
                if (tabela1.Rows.Count > 0)
                    cmdPOS_BarkodatUpdateInsert_spUpd.ExecuteNonQuery();

                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void POS_RuajBrendet(DateTime DataKohaSinkronizimitParaprak)
        {
            DataTable tabela1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_BrendetPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DeriMeOren", DataKohaSinkronizimitParaprak);
            da.Fill(tabela1);
            SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
            cnn.Open();
            SqlCommand cmdPOS_BrendetUpdateInsert_spUpd = new SqlCommand("TOSHIBA.POS_BrendetUpdateInsert_sp", cnn);
            cmdPOS_BrendetUpdateInsert_spUpd.CommandType = CommandType.StoredProcedure;

            cmdPOS_BrendetUpdateInsert_spUpd.Parameters.Add("@Id", SqlDbType.Int);
            cmdPOS_BrendetUpdateInsert_spUpd.Parameters["@Id"].Direction = ParameterDirection.Input;
            cmdPOS_BrendetUpdateInsert_spUpd.Parameters.Add("@Pershkrimi", SqlDbType.VarChar);
            cmdPOS_BrendetUpdateInsert_spUpd.Parameters["@Pershkrimi"].Direction = ParameterDirection.Input;
            cmdPOS_BrendetUpdateInsert_spUpd.Parameters.Add("@DataERegjistrimit", SqlDbType.DateTime);
            cmdPOS_BrendetUpdateInsert_spUpd.Parameters["@DataERegjistrimit"].Direction = ParameterDirection.Input;


            try
            {
                foreach (DataRow Row in tabela1.Rows)
                {
                    cmdPOS_BrendetUpdateInsert_spUpd.Parameters["@Id"].Value = Row["Id"];
                    cmdPOS_BrendetUpdateInsert_spUpd.Parameters["@Pershkrimi"].Value = Row["Pershkrimi"];
                    cmdPOS_BrendetUpdateInsert_spUpd.Parameters["@DataERegjistrimit"].Value = Row["DataERegjistrimit"];
                    cmdPOS_BrendetUpdateInsert_spUpd.ExecuteNonQuery();

                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void POS_RuajCCPLlojetEKartelave(DateTime DataKohaSinkronizimitParaprak)
        {
            DataTable tabela1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_CCPLlojetEKartelavePerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DeriMeOren", DataKohaSinkronizimitParaprak);
            da.Fill(tabela1);


            SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
            cnn.Open();
            SqlCommand cmdPOS_CCPLlojetEKartelaveUpdateInsert_spUpd = new SqlCommand("TOSHIBA.POS_CCPLlojetEKartelaveUpdateInsert_sp", cnn);
            cmdPOS_CCPLlojetEKartelaveUpdateInsert_spUpd.CommandType = CommandType.StoredProcedure;


            cmdPOS_CCPLlojetEKartelaveUpdateInsert_spUpd.Parameters.Add("@Id", SqlDbType.Int);
            cmdPOS_CCPLlojetEKartelaveUpdateInsert_spUpd.Parameters["@Id"].Direction = ParameterDirection.Input;
            cmdPOS_CCPLlojetEKartelaveUpdateInsert_spUpd.Parameters.Add("@Pershkrimi", SqlDbType.VarChar);
            cmdPOS_CCPLlojetEKartelaveUpdateInsert_spUpd.Parameters["@Pershkrimi"].Direction = ParameterDirection.Input;
            cmdPOS_CCPLlojetEKartelaveUpdateInsert_spUpd.Parameters.Add("@TipiKarteles", SqlDbType.VarChar);
            cmdPOS_CCPLlojetEKartelaveUpdateInsert_spUpd.Parameters["@TipiKarteles"].Direction = ParameterDirection.Input;
            cmdPOS_CCPLlojetEKartelaveUpdateInsert_spUpd.Parameters.Add("@Statusi", SqlDbType.Bit);
            cmdPOS_CCPLlojetEKartelaveUpdateInsert_spUpd.Parameters["@Statusi"].Direction = ParameterDirection.Input;
            cmdPOS_CCPLlojetEKartelaveUpdateInsert_spUpd.Parameters.Add("@DataERegjistrimit", SqlDbType.DateTime);
            cmdPOS_CCPLlojetEKartelaveUpdateInsert_spUpd.Parameters["@DataERegjistrimit"].Direction = ParameterDirection.Input;

            try
            {

                foreach (DataRow Row in tabela1.Rows)
                {
                    cmdPOS_CCPLlojetEKartelaveUpdateInsert_spUpd.Parameters["@Id"].Value = Row["Id"];
                    cmdPOS_CCPLlojetEKartelaveUpdateInsert_spUpd.Parameters["@Pershkrimi"].Value = Row["Pershkrimi"];
                    cmdPOS_CCPLlojetEKartelaveUpdateInsert_spUpd.Parameters["@TipiKarteles"].Value = Row["TipiKarteles"];
                    cmdPOS_CCPLlojetEKartelaveUpdateInsert_spUpd.Parameters["@Statusi"].Value = Row["Statusi"];
                    cmdPOS_CCPLlojetEKartelaveUpdateInsert_spUpd.Parameters["@DataERegjistrimit"].Value = Row["DataERegjistrimit"];
                    cmdPOS_CCPLlojetEKartelaveUpdateInsert_spUpd.ExecuteNonQuery();

                }
                cnn.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void POS_RuajCmimorja(DateTime DataKohaSinkronizimitParaprak)
        {
            DataTable tabela1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_CmimorjaPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", PublicClass.OrganizataId);
            da.SelectCommand.Parameters.AddWithValue("@DataERegjistrimit", DataKohaSinkronizimitParaprak);
            da.Fill(tabela1);

            if (PublicClass.AplikacioniId == 3184)
                foreach (DataRow dr in tabela1.Rows)
                {
                    dr["TVSH"] = 8;
                }

            if (tabela1.Rows.Count > 0)
            {
                SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
                cnn.Open();
                SqlCommand cmdPOS_CmimorjaInsertUpdate_spUpd = new SqlCommand("TOSHIBA.POS_CmimorjaInsertUpdate_sp", cnn);
                cmdPOS_CmimorjaInsertUpdate_spUpd.CommandType = CommandType.StoredProcedure;
                cmdPOS_CmimorjaInsertUpdate_spUpd.Parameters.AddWithValue("@CmimorjaTP", tabela1);
                try
                {
                    cmdPOS_CmimorjaInsertUpdate_spUpd.ExecuteNonQuery();
                    cnn.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        private void POS_RuajDepartamentet(DateTime DataKohaSinkronizimitParaprak)
        {
            DataTable tabela1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_DepartamentetPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DeriMeOren", DataKohaSinkronizimitParaprak);
            da.Fill(tabela1);


            SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
            cnn.Open();
            SqlCommand cmdPOS_DepartamentetUpdateInsert_spUpd = new SqlCommand("TOSHIBA.POS_DepartamentetUpdateInsert_sp", cnn);
            cmdPOS_DepartamentetUpdateInsert_spUpd.CommandType = CommandType.StoredProcedure;


            cmdPOS_DepartamentetUpdateInsert_spUpd.Parameters.Add("@Id", SqlDbType.Int);
            cmdPOS_DepartamentetUpdateInsert_spUpd.Parameters["@Id"].Direction = ParameterDirection.Input;
            cmdPOS_DepartamentetUpdateInsert_spUpd.Parameters.Add("@Pershkrimi", SqlDbType.VarChar);
            cmdPOS_DepartamentetUpdateInsert_spUpd.Parameters["@Pershkrimi"].Direction = ParameterDirection.Input;
            cmdPOS_DepartamentetUpdateInsert_spUpd.Parameters.Add("@Plani", SqlDbType.Image);
            cmdPOS_DepartamentetUpdateInsert_spUpd.Parameters["@Plani"].Direction = ParameterDirection.Input;
            cmdPOS_DepartamentetUpdateInsert_spUpd.Parameters.Add("@DataERegjistrimit", SqlDbType.DateTime);
            cmdPOS_DepartamentetUpdateInsert_spUpd.Parameters["@DataERegjistrimit"].Direction = ParameterDirection.Input;

            try
            {

                foreach (DataRow Row in tabela1.Rows)
                {
                    cmdPOS_DepartamentetUpdateInsert_spUpd.Parameters["@Id"].Value = Row["Id"];
                    cmdPOS_DepartamentetUpdateInsert_spUpd.Parameters["@Pershkrimi"].Value = Row["Pershkrimi"];
                    cmdPOS_DepartamentetUpdateInsert_spUpd.Parameters["@Pershkrimi"].Value = Row["Pershkrimi"];
                    cmdPOS_DepartamentetUpdateInsert_spUpd.Parameters["@Plani"].Value = Row["Plani"];
                    cmdPOS_DepartamentetUpdateInsert_spUpd.Parameters["@DataERegjistrimit"].Value = Row["DataERegjistrimit"];
                    cmdPOS_DepartamentetUpdateInsert_spUpd.ExecuteNonQuery();

                }
                cnn.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void POS_RuajGrupetEMallrave(DateTime DataKohaSinkronizimitParaprak)
        {
            DataTable tabela1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_GrupetEMallravePerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DeriMeOren", DataKohaSinkronizimitParaprak);
            da.Fill(tabela1);

            SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
            cnn.Open();
            SqlCommand cmdPOS_GrupetEMallraveUpdateInsert_spUpd = new SqlCommand("TOSHIBA.POS_GrupetEMallraveUpdateInsert_sp", cnn);
            cmdPOS_GrupetEMallraveUpdateInsert_spUpd.CommandType = CommandType.StoredProcedure;
            cmdPOS_GrupetEMallraveUpdateInsert_spUpd.Parameters.AddWithValue("GrupetEMallraveTP", tabela1);
            try
            {
                if (tabela1.Rows.Count > 0)
                {
                    cmdPOS_GrupetEMallraveUpdateInsert_spUpd.ExecuteNonQuery();
                    cnn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void POS_RuajKonfigurimet(DateTime DataKohaSinkronizimitParaprak)
        {
            DataTable tabela1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_KonfigurimetPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DeriMeOren", DataKohaSinkronizimitParaprak);
            da.Fill(tabela1);


            SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
            cnn.Open();
            SqlCommand cmdPOS_KonfigurimetUpdateInsert_spUpd = new SqlCommand("TOSHIBA.POS_KonfigurimetUpdateInsert_sp", cnn);
            cmdPOS_KonfigurimetUpdateInsert_spUpd.CommandType = CommandType.StoredProcedure;


            cmdPOS_KonfigurimetUpdateInsert_spUpd.Parameters.Add("@Id", SqlDbType.Int);
            cmdPOS_KonfigurimetUpdateInsert_spUpd.Parameters["@Id"].Direction = ParameterDirection.Input;
            cmdPOS_KonfigurimetUpdateInsert_spUpd.Parameters.Add("@Pershkrimi", SqlDbType.VarChar);
            cmdPOS_KonfigurimetUpdateInsert_spUpd.Parameters["@Pershkrimi"].Direction = ParameterDirection.Input;
            cmdPOS_KonfigurimetUpdateInsert_spUpd.Parameters.Add("@Statusi", SqlDbType.Bit);
            cmdPOS_KonfigurimetUpdateInsert_spUpd.Parameters["@Statusi"].Direction = ParameterDirection.Input;
            cmdPOS_KonfigurimetUpdateInsert_spUpd.Parameters.Add("@Tipi", SqlDbType.VarChar);
            cmdPOS_KonfigurimetUpdateInsert_spUpd.Parameters["@Tipi"].Direction = ParameterDirection.Input;
            cmdPOS_KonfigurimetUpdateInsert_spUpd.Parameters.Add("@Vlera", SqlDbType.VarChar);
            cmdPOS_KonfigurimetUpdateInsert_spUpd.Parameters["@Vlera"].Direction = ParameterDirection.Input;
            cmdPOS_KonfigurimetUpdateInsert_spUpd.Parameters.Add("@DataERegjistrimit", SqlDbType.DateTime);
            cmdPOS_KonfigurimetUpdateInsert_spUpd.Parameters["@DataERegjistrimit"].Direction = ParameterDirection.Input;

            try
            {

                foreach (DataRow Row in tabela1.Rows)
                {
                    cmdPOS_KonfigurimetUpdateInsert_spUpd.Parameters["@Id"].Value = Row["Id"];
                    cmdPOS_KonfigurimetUpdateInsert_spUpd.Parameters["@Pershkrimi"].Value = Row["Pershkrimi"];
                    cmdPOS_KonfigurimetUpdateInsert_spUpd.Parameters["@Statusi"].Value = Row["Statusi"];
                    cmdPOS_KonfigurimetUpdateInsert_spUpd.Parameters["@Tipi"].Value = Row["Tipi"];
                    cmdPOS_KonfigurimetUpdateInsert_spUpd.Parameters["@Vlera"].Value = Row["Vlera"];
                    cmdPOS_KonfigurimetUpdateInsert_spUpd.Parameters["@DataERegjistrimit"].Value = Row["DataERegjistrimit"];
                    cmdPOS_KonfigurimetUpdateInsert_spUpd.ExecuteNonQuery();

                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void POS_RuajLlojetEArtikullit(DateTime DataKohaSinkronizimitParaprak)
        {
            DataTable tabela1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_LlojetEArtikullitPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DeriMeOren", DataKohaSinkronizimitParaprak);
            da.Fill(tabela1);

            SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
            cnn.Open();
            SqlCommand cmdPOS_LlojetEArtikullitUpdateInsert_spUpd = new SqlCommand("TOSHIBA.POS_LlojetEArtikullitUpdateInsert_sp", cnn);
            cmdPOS_LlojetEArtikullitUpdateInsert_spUpd.CommandType = CommandType.StoredProcedure;

            cmdPOS_LlojetEArtikullitUpdateInsert_spUpd.Parameters.Add("@Id", SqlDbType.Int);
            cmdPOS_LlojetEArtikullitUpdateInsert_spUpd.Parameters["@Id"].Direction = ParameterDirection.Input;
            cmdPOS_LlojetEArtikullitUpdateInsert_spUpd.Parameters.Add("@Pershkrimi", SqlDbType.VarChar);
            cmdPOS_LlojetEArtikullitUpdateInsert_spUpd.Parameters["@Pershkrimi"].Direction = ParameterDirection.Input;
            cmdPOS_LlojetEArtikullitUpdateInsert_spUpd.Parameters.Add("@Statusi", SqlDbType.Bit);
            cmdPOS_LlojetEArtikullitUpdateInsert_spUpd.Parameters["@Statusi"].Direction = ParameterDirection.Input;
            cmdPOS_LlojetEArtikullitUpdateInsert_spUpd.Parameters.Add("@LejonStokunNegative", SqlDbType.Bit);
            cmdPOS_LlojetEArtikullitUpdateInsert_spUpd.Parameters["@LejonStokunNegative"].Direction = ParameterDirection.Input;
            cmdPOS_LlojetEArtikullitUpdateInsert_spUpd.Parameters.Add("@DataERegjistrimit", SqlDbType.DateTime);
            cmdPOS_LlojetEArtikullitUpdateInsert_spUpd.Parameters["@DataERegjistrimit"].Direction = ParameterDirection.Input;

            try
            {
                foreach (DataRow Row in tabela1.Rows)
                {
                    cmdPOS_LlojetEArtikullitUpdateInsert_spUpd.Parameters["@Id"].Value = Row["Id"];
                    cmdPOS_LlojetEArtikullitUpdateInsert_spUpd.Parameters["@Pershkrimi"].Value = Row["Pershkrimi"];
                    cmdPOS_LlojetEArtikullitUpdateInsert_spUpd.Parameters["@Statusi"].Value = Row["Statusi"];
                    cmdPOS_LlojetEArtikullitUpdateInsert_spUpd.Parameters["@LejonStokunNegative"].Value = Row["LejonStokunNegative"];
                    cmdPOS_LlojetEArtikullitUpdateInsert_spUpd.Parameters["@DataERegjistrimit"].Value = Row["DataERegjistrimit"];
                    cmdPOS_LlojetEArtikullitUpdateInsert_spUpd.ExecuteNonQuery();

                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void POS_RuajLlojetEDokumenteve(DateTime DataKohaSinkronizimitParaprak)
        {
            DataTable tabela1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_LlojetEDokumentevePerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DeriMeOren", DataKohaSinkronizimitParaprak);
            da.Fill(tabela1);


            SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
            cnn.Open();
            SqlCommand cmdPOS_LlojetEDokumenteveUpdateInsert_spUpd = new SqlCommand("TOSHIBA.POS_LlojetEDokumenteveUpdateInsert_sp", cnn);
            cmdPOS_LlojetEDokumenteveUpdateInsert_spUpd.CommandType = CommandType.StoredProcedure;

            cmdPOS_LlojetEDokumenteveUpdateInsert_spUpd.Parameters.Add("@Id", SqlDbType.Int);
            cmdPOS_LlojetEDokumenteveUpdateInsert_spUpd.Parameters["@Id"].Direction = ParameterDirection.Input;
            cmdPOS_LlojetEDokumenteveUpdateInsert_spUpd.Parameters.Add("@Pershkrimi", SqlDbType.VarChar);
            cmdPOS_LlojetEDokumenteveUpdateInsert_spUpd.Parameters["@Pershkrimi"].Direction = ParameterDirection.Input;
            cmdPOS_LlojetEDokumenteveUpdateInsert_spUpd.Parameters.Add("@Tipi", SqlDbType.VarChar);
            cmdPOS_LlojetEDokumenteveUpdateInsert_spUpd.Parameters["@Tipi"].Direction = ParameterDirection.Input;
            cmdPOS_LlojetEDokumenteveUpdateInsert_spUpd.Parameters.Add("@PrindiID", SqlDbType.Int);
            cmdPOS_LlojetEDokumenteveUpdateInsert_spUpd.Parameters["@PrindiID"].Direction = ParameterDirection.Input;
            cmdPOS_LlojetEDokumenteveUpdateInsert_spUpd.Parameters.Add("@Shkurtesa", SqlDbType.VarChar);
            cmdPOS_LlojetEDokumenteveUpdateInsert_spUpd.Parameters["@Shkurtesa"].Direction = ParameterDirection.Input;
            cmdPOS_LlojetEDokumenteveUpdateInsert_spUpd.Parameters.Add("@Shenja", SqlDbType.Int);
            cmdPOS_LlojetEDokumenteveUpdateInsert_spUpd.Parameters["@Shenja"].Direction = ParameterDirection.Input;
            cmdPOS_LlojetEDokumenteveUpdateInsert_spUpd.Parameters.Add("@DokumentIJashtem", SqlDbType.Bit);
            cmdPOS_LlojetEDokumenteveUpdateInsert_spUpd.Parameters["@DokumentIJashtem"].Direction = ParameterDirection.Input;
            cmdPOS_LlojetEDokumenteveUpdateInsert_spUpd.Parameters.Add("@Tatimi", SqlDbType.Decimal);
            cmdPOS_LlojetEDokumenteveUpdateInsert_spUpd.Parameters["@Tatimi"].Direction = ParameterDirection.Input;
            cmdPOS_LlojetEDokumenteveUpdateInsert_spUpd.Parameters.Add("@DataERegjistrimit", SqlDbType.DateTime);
            cmdPOS_LlojetEDokumenteveUpdateInsert_spUpd.Parameters["@DataERegjistrimit"].Direction = ParameterDirection.Input;
            cmdPOS_LlojetEDokumenteveUpdateInsert_spUpd.Parameters.Add("@TrackingTipi", SqlDbType.VarChar);
            cmdPOS_LlojetEDokumenteveUpdateInsert_spUpd.Parameters["@TrackingTipi"].Direction = ParameterDirection.Input;



            try
            {

                foreach (DataRow Row in tabela1.Rows)
                {
                    cmdPOS_LlojetEDokumenteveUpdateInsert_spUpd.Parameters["@Id"].Value = Row["Id"];
                    cmdPOS_LlojetEDokumenteveUpdateInsert_spUpd.Parameters["@Pershkrimi"].Value = Row["Pershkrimi"];
                    cmdPOS_LlojetEDokumenteveUpdateInsert_spUpd.Parameters["@Tipi"].Value = Row["Tipi"];
                    cmdPOS_LlojetEDokumenteveUpdateInsert_spUpd.Parameters["@PrindiID"].Value = Row["PrindiID"];
                    cmdPOS_LlojetEDokumenteveUpdateInsert_spUpd.Parameters["@Shkurtesa"].Value = Row["Shkurtesa"];
                    cmdPOS_LlojetEDokumenteveUpdateInsert_spUpd.Parameters["@Shenja"].Value = Row["Shenja"];
                    cmdPOS_LlojetEDokumenteveUpdateInsert_spUpd.Parameters["@DokumentIJashtem"].Value = Row["DokumentIJashtem"];
                    cmdPOS_LlojetEDokumenteveUpdateInsert_spUpd.Parameters["@Tatimi"].Value = Row["Tatimi"];
                    cmdPOS_LlojetEDokumenteveUpdateInsert_spUpd.Parameters["@DataERegjistrimit"].Value = Row["DataERegjistrimit"];
                    cmdPOS_LlojetEDokumenteveUpdateInsert_spUpd.Parameters["@TrackingTipi"].Value = Row["TrackingTipi"];
                    cmdPOS_LlojetEDokumenteveUpdateInsert_spUpd.ExecuteNonQuery();

                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        private void POS_RuajMenyratEPageses(DateTime DataKohaSinkronizimitParaprak)
        {
            DataTable tabela1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_MenyratEPagesesPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DeriMeOren", DataKohaSinkronizimitParaprak);
            da.Fill(tabela1);


            SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
            cnn.Open();
            SqlCommand cmdPOS_MenyratEPagesesUpdateInsert_spUpd = new SqlCommand("TOSHIBA.POS_MenyratEPagesesUpdateInsert_sp", cnn);
            cmdPOS_MenyratEPagesesUpdateInsert_spUpd.CommandType = CommandType.StoredProcedure;


            cmdPOS_MenyratEPagesesUpdateInsert_spUpd.Parameters.Add("@Id", SqlDbType.Int);
            cmdPOS_MenyratEPagesesUpdateInsert_spUpd.Parameters["@Id"].Direction = ParameterDirection.Input;
            cmdPOS_MenyratEPagesesUpdateInsert_spUpd.Parameters.Add("@Shkurtesa", SqlDbType.VarChar);
            cmdPOS_MenyratEPagesesUpdateInsert_spUpd.Parameters["@Shkurtesa"].Direction = ParameterDirection.Input;
            cmdPOS_MenyratEPagesesUpdateInsert_spUpd.Parameters.Add("@Pershkrimi", SqlDbType.VarChar);
            cmdPOS_MenyratEPagesesUpdateInsert_spUpd.Parameters["@Pershkrimi"].Direction = ParameterDirection.Input;
            cmdPOS_MenyratEPagesesUpdateInsert_spUpd.Parameters.Add("@Provizioni", SqlDbType.Decimal);
            cmdPOS_MenyratEPagesesUpdateInsert_spUpd.Parameters["@Provizioni"].Direction = ParameterDirection.Input;
            cmdPOS_MenyratEPagesesUpdateInsert_spUpd.Parameters.Add("@Tipi", SqlDbType.VarChar);
            cmdPOS_MenyratEPagesesUpdateInsert_spUpd.Parameters["@Tipi"].Direction = ParameterDirection.Input;
            cmdPOS_MenyratEPagesesUpdateInsert_spUpd.Parameters.Add("@Renditja", SqlDbType.Int);
            cmdPOS_MenyratEPagesesUpdateInsert_spUpd.Parameters["@Renditja"].Direction = ParameterDirection.Input;
            cmdPOS_MenyratEPagesesUpdateInsert_spUpd.Parameters.Add("@PershkrimiAnglisht", SqlDbType.VarChar);
            cmdPOS_MenyratEPagesesUpdateInsert_spUpd.Parameters["@PershkrimiAnglisht"].Direction = ParameterDirection.Input;
            cmdPOS_MenyratEPagesesUpdateInsert_spUpd.Parameters.Add("@DataERegjistrimit", SqlDbType.DateTime);
            cmdPOS_MenyratEPagesesUpdateInsert_spUpd.Parameters["@DataERegjistrimit"].Direction = ParameterDirection.Input;
            cmdPOS_MenyratEPagesesUpdateInsert_spUpd.Parameters.Add("@ParaqitetNePos", SqlDbType.Bit);
            cmdPOS_MenyratEPagesesUpdateInsert_spUpd.Parameters["@ParaqitetNePos"].Direction = ParameterDirection.Input;
            cmdPOS_MenyratEPagesesUpdateInsert_spUpd.Parameters.Add("@PagesMeBonus", SqlDbType.Bit);
            cmdPOS_MenyratEPagesesUpdateInsert_spUpd.Parameters["@PagesMeBonus"].Direction = ParameterDirection.Input;

            try
            {
                foreach (DataRow Row in tabela1.Rows)
                {
                    cmdPOS_MenyratEPagesesUpdateInsert_spUpd.Parameters["@Id"].Value = Row["Id"];
                    cmdPOS_MenyratEPagesesUpdateInsert_spUpd.Parameters["@Shkurtesa"].Value = Row["Shkurtesa"];
                    cmdPOS_MenyratEPagesesUpdateInsert_spUpd.Parameters["@Pershkrimi"].Value = Row["Pershkrimi"];
                    cmdPOS_MenyratEPagesesUpdateInsert_spUpd.Parameters["@Provizioni"].Value = Row["Provizioni"];
                    cmdPOS_MenyratEPagesesUpdateInsert_spUpd.Parameters["@Tipi"].Value = Row["Tipi"];
                    cmdPOS_MenyratEPagesesUpdateInsert_spUpd.Parameters["@Tipi"].Value = Row["Tipi"];
                    cmdPOS_MenyratEPagesesUpdateInsert_spUpd.Parameters["@Renditja"].Value = Row["Renditja"];
                    cmdPOS_MenyratEPagesesUpdateInsert_spUpd.Parameters["@PershkrimiAnglisht"].Value = Row["PershkrimiAnglisht"];
                    cmdPOS_MenyratEPagesesUpdateInsert_spUpd.Parameters["@DataERegjistrimit"].Value = Row["DataERegjistrimit"];
                    cmdPOS_MenyratEPagesesUpdateInsert_spUpd.Parameters["@ParaqitetNePos"].Value = Row["ParaqitetNePos"];
                    cmdPOS_MenyratEPagesesUpdateInsert_spUpd.Parameters["@PagesMeBonus"].Value = Row["PagesMeBonus"];
                    cmdPOS_MenyratEPagesesUpdateInsert_spUpd.ExecuteNonQuery();
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void POS_RuajMxh_Filialet(DateTime DataKohaSinkronizimitParaprak)
        {

            DataTable tabela1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_FilialetPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Id", PublicClass.OrganizataId);
            da.Fill(tabela1);


            SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);

            SqlCommand cmdPOS_Mxh_FilialetUpdateInsert_spUpd = new SqlCommand("TOSHIBA.POS_Mxh_FilialetUpdateInsert_sp", cnn);
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.CommandType = CommandType.StoredProcedure;


            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters.Add("@Id", SqlDbType.Int);
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@Id"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters.Add("@Pershkrimi", SqlDbType.VarChar);
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@Pershkrimi"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters.Add("@LogoBardhEZi", SqlDbType.Image);
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@LogoBardhEZi"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters.Add("@Logo", SqlDbType.Image);
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@Logo"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters.Add("@OrganizataId", SqlDbType.Int);
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@OrganizataId"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters.Add("@Tipi", SqlDbType.Int);
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@Tipi"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters.Add("@EmriServerit", SqlDbType.VarChar);
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@EmriServerit"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters.Add("@EmriDatabazes", SqlDbType.VarChar);
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@EmriDatabazes"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters.Add("@Statusi", SqlDbType.Bit);
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@Statusi"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters.Add("@FilialaVepruese", SqlDbType.Bit);
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@FilialaVepruese"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters.Add("@LinkServeri", SqlDbType.VarChar);
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@LinkServeri"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters.Add("@Sinkronizohet", SqlDbType.Bit);
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@Sinkronizohet"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters.Add("@SinkronizohetNga", SqlDbType.Int);
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@SinkronizohetNga"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters.Add("@Lloji", SqlDbType.Char);
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@Lloji"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters.Add("@MundesoTavolinatEHapura", SqlDbType.Bit);
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@MundesoTavolinatEHapura"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters.Add("@StatusiProjektit", SqlDbType.Bit);
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@StatusiProjektit"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters.Add("@TvshPerProjekt", SqlDbType.Decimal);
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@TvshPerProjekt"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters.Add("@DataERegjistrimit", SqlDbType.DateTime);
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@DataERegjistrimit"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters.Add("@KaCmime", SqlDbType.Bit);
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@KaCmime"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters.Add("@PrefixNeNrTeFatures", SqlDbType.VarChar);
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@PrefixNeNrTeFatures"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters.Add("@Renditja", SqlDbType.Int);
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@Renditja"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters.Add("@PershkrimiShkurter", SqlDbType.VarChar);
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@PershkrimiShkurter"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters.Add("@PrindiId", SqlDbType.VarChar);
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@PrindiId"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters.Add("@NrTerminalit", SqlDbType.VarChar);
            cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@NrTerminalit"].Direction = ParameterDirection.Input;

            try
            {
                cnn.Open();
                foreach (DataRow Row in tabela1.Rows)
                {
                    cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@Id"].Value = Row["Id"];
                    cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@Pershkrimi"].Value = Row["Pershkrimi"];
                    cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@LogoBardhEZi"].Value = Row["LogoBardhEZi"];
                    cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@Logo"].Value = Row["Logo"];
                    cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@OrganizataId"].Value = Row["OrganizataId"];
                    cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@Tipi"].Value = Row["Tipi"];
                    cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@EmriServerit"].Value = Row["EmriServerit"];
                    cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@EmriDatabazes"].Value = Row["EmriDatabazes"];
                    cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@Statusi"].Value = Row["Statusi"];
                    cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@FilialaVepruese"].Value = Row["FilialaVepruese"];
                    cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@LinkServeri"].Value = Row["LinkServeri"];
                    cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@Sinkronizohet"].Value = Row["Sinkronizohet"];
                    cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@SinkronizohetNga"].Value = Row["SinkronizohetNga"];
                    cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@Lloji"].Value = Row["Lloji"];
                    cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@MundesoTavolinatEHapura"].Value = Row["MundesoTavolinatEHapura"];
                    cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@StatusiProjektit"].Value = Row["StatusiProjektit"];
                    cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@TvshPerProjekt"].Value = Row["TvshPerProjekt"];
                    cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@DataERegjistrimit"].Value = Row["DataERegjistrimit"];
                    cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@KaCmime"].Value = Row["KaCmime"];
                    cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@PrefixNeNrTeFatures"].Value = Row["PrefixNeNrTeFatures"];
                    cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@Renditja"].Value = Row["Renditja"];
                    cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@PershkrimiShkurter"].Value = Row["PershkrimiShkurter"];
                    cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@PrindiId"].Value = Row["PrindiId"];
                    cmdPOS_Mxh_FilialetUpdateInsert_spUpd.Parameters["@NrTerminalit"].Value = Row["NrTerminalit"];
                    cmdPOS_Mxh_FilialetUpdateInsert_spUpd.ExecuteNonQuery();

                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        private void POS_RuajMxh_GrupetEShfrytezuesve(DateTime DataKohaSinkronizimitParaprak)
        {
            DataTable tabela1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_Mxh_GrupetEShfrytezuesvePerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DeriMeOren", DataKohaSinkronizimitParaprak);
            da.Fill(tabela1);


            SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
            cnn.Open();
            SqlCommand cmdPOS_Mxh_GrupetEShfrytezuesveUpdateInsert_spUpd = new SqlCommand("TOSHIBA.POS_Mxh_GrupetEShfrytezuesveUpdateInsert_sp", cnn);
            cmdPOS_Mxh_GrupetEShfrytezuesveUpdateInsert_spUpd.CommandType = CommandType.StoredProcedure;


            cmdPOS_Mxh_GrupetEShfrytezuesveUpdateInsert_spUpd.Parameters.Add("@Id", SqlDbType.Int);
            cmdPOS_Mxh_GrupetEShfrytezuesveUpdateInsert_spUpd.Parameters["@Id"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_GrupetEShfrytezuesveUpdateInsert_spUpd.Parameters.Add("@Pershkrimi", SqlDbType.VarChar);
            cmdPOS_Mxh_GrupetEShfrytezuesveUpdateInsert_spUpd.Parameters["@Pershkrimi"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_GrupetEShfrytezuesveUpdateInsert_spUpd.Parameters.Add("@Tipi", SqlDbType.VarChar);
            cmdPOS_Mxh_GrupetEShfrytezuesveUpdateInsert_spUpd.Parameters["@Tipi"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_GrupetEShfrytezuesveUpdateInsert_spUpd.Parameters.Add("@DataERegjistrimit", SqlDbType.DateTime);
            cmdPOS_Mxh_GrupetEShfrytezuesveUpdateInsert_spUpd.Parameters["@DataERegjistrimit"].Direction = ParameterDirection.Input;

            try
            {
                foreach (DataRow Row in tabela1.Rows)
                {
                    cmdPOS_Mxh_GrupetEShfrytezuesveUpdateInsert_spUpd.Parameters["@Id"].Value = Row["Id"];
                    cmdPOS_Mxh_GrupetEShfrytezuesveUpdateInsert_spUpd.Parameters["@Pershkrimi"].Value = Row["Pershkrimi"];
                    cmdPOS_Mxh_GrupetEShfrytezuesveUpdateInsert_spUpd.Parameters["@Tipi"].Value = Row["Tipi"];
                    cmdPOS_Mxh_GrupetEShfrytezuesveUpdateInsert_spUpd.Parameters["@DataERegjistrimit"].Value = Row["DataERegjistrimit"];
                    cmdPOS_Mxh_GrupetEShfrytezuesveUpdateInsert_spUpd.ExecuteNonQuery();

                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void POS_RuajMxh_Operatoret(DateTime DataKohaSinkronizimitParaprak)
        {
            DataTable tabela1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_Mxh_OperatoretPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DeriMeOren", DataKohaSinkronizimitParaprak);
            //da.SelectCommand.Parameters.AddWithValue("@OrganizataId", PublicClass.OrganizataId);
            da.Fill(tabela1);

            SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
            cnn.Open();
            SqlCommand cmdPOS_Mxh_OperatoretUpdateInsert_spUpd = new SqlCommand("TOSHIBA.POS_Mxh_OperatoretUpdateInsert_sp", cnn);
            cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.CommandType = CommandType.StoredProcedure;


            cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters.Add("@Id", SqlDbType.Int);
            cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters["@Id"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters.Add("@Emri", SqlDbType.VarChar);
            cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters["@Emri"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters.Add("@Mbiemri", SqlDbType.VarChar);
            cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters["@Mbiemri"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters.Add("@GrupiId", SqlDbType.Int);
            cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters["@GrupiId"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters.Add("@Operatori", SqlDbType.VarChar);
            cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters["@Operatori"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters.Add("@Statusi", SqlDbType.Bit);
            cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters["@Statusi"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters.Add("@OrganizataId", SqlDbType.Int);
            cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters["@OrganizataId"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters.Add("@Pass", SqlDbType.VarChar);
            cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters["@Pass"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters.Add("@DataEKrijimit", SqlDbType.DateTime);
            cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters["@DataEKrijimit"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters.Add("@Email", SqlDbType.VarChar);
            cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters["@Email"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters.Add("@FjalekalimiPerZbritje", SqlDbType.VarChar);
            cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters["@FjalekalimiPerZbritje"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters.Add("@ShifraOperatorit", SqlDbType.VarChar);
            cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters["@ShifraOperatorit"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters.Add("@DataERegjistrimit", SqlDbType.DateTime);
            cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters["@DataERegjistrimit"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters.Add("@HapjaEDokumenteve", SqlDbType.Bit);
            cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters["@HapjaEDokumenteve"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters.Add("@SektoriId", SqlDbType.Int);
            cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters["@SektoriId"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters.Add("@Tel", SqlDbType.VarChar);
            cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters["@Tel"].Direction = ParameterDirection.Input;



            try
            {

                foreach (DataRow Row in tabela1.Rows)
                {
                    cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters["@Id"].Value = Row["Id"];
                    cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters["@Emri"].Value = Row["Emri"];
                    cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters["@Mbiemri"].Value = Row["Mbiemri"];
                    cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters["@GrupiId"].Value = Row["GrupiId"];
                    cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters["@Operatori"].Value = Row["Operatori"];
                    cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters["@Statusi"].Value = Row["Statusi"];
                    cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters["@OrganizataId"].Value = Row["OrganizataId"];
                    cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters["@Pass"].Value = Row["Pass"];
                    cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters["@DataEKrijimit"].Value = Row["DataEKrijimit"];
                    cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters["@Email"].Value = Row["Email"];
                    cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters["@FjalekalimiPerZbritje"].Value = Row["FjalekalimiPerZbritje"];
                    cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters["@ShifraOperatorit"].Value = Row["ShifraOperatorit"];
                    cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters["@DataERegjistrimit"].Value = Row["DataERegjistrimit"];
                    cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters["@HapjaEDokumenteve"].Value = Row["HapjaEDokumenteve"];
                    cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters["@SektoriId"].Value = Row["SektoriId"];
                    cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.Parameters["@Tel"].Value = Row["Tel"];
                    cmdPOS_Mxh_OperatoretUpdateInsert_spUpd.ExecuteNonQuery();

                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void POS_RuajMxh_OrganizataDetalet(DateTime DataKohaSinkronizimitParaprak)
        {
            DataTable tabela1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_Mxh_OrganizataDetaletPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DeriMeOren", DataKohaSinkronizimitParaprak);
            da.Fill(tabela1);


            SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
            cnn.Open();
            SqlCommand cmdPOS_Mxh_OrganizataDetaletUpdateInsert_spUpd = new SqlCommand("TOSHIBA.POS_Mxh_OrganizataDetaletUpdateInsert_sp", cnn);
            cmdPOS_Mxh_OrganizataDetaletUpdateInsert_spUpd.CommandType = CommandType.StoredProcedure;


            cmdPOS_Mxh_OrganizataDetaletUpdateInsert_spUpd.Parameters.Add("@Id", SqlDbType.Int);
            cmdPOS_Mxh_OrganizataDetaletUpdateInsert_spUpd.Parameters["@Id"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_OrganizataDetaletUpdateInsert_spUpd.Parameters.Add("@Gjuha", SqlDbType.VarChar);
            cmdPOS_Mxh_OrganizataDetaletUpdateInsert_spUpd.Parameters["@Gjuha"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_OrganizataDetaletUpdateInsert_spUpd.Parameters.Add("@Email", SqlDbType.VarChar);
            cmdPOS_Mxh_OrganizataDetaletUpdateInsert_spUpd.Parameters["@Email"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_OrganizataDetaletUpdateInsert_spUpd.Parameters.Add("@Smtp", SqlDbType.VarChar);
            cmdPOS_Mxh_OrganizataDetaletUpdateInsert_spUpd.Parameters["@Smtp"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_OrganizataDetaletUpdateInsert_spUpd.Parameters.Add("@Porti", SqlDbType.VarChar);
            cmdPOS_Mxh_OrganizataDetaletUpdateInsert_spUpd.Parameters["@Porti"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_OrganizataDetaletUpdateInsert_spUpd.Parameters.Add("@UserName", SqlDbType.VarChar);
            cmdPOS_Mxh_OrganizataDetaletUpdateInsert_spUpd.Parameters["@UserName"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_OrganizataDetaletUpdateInsert_spUpd.Parameters.Add("@Pass", SqlDbType.VarChar);
            cmdPOS_Mxh_OrganizataDetaletUpdateInsert_spUpd.Parameters["@Pass"].Direction = ParameterDirection.Input;
            cmdPOS_Mxh_OrganizataDetaletUpdateInsert_spUpd.Parameters.Add("@DataERegjistrimit", SqlDbType.DateTime);
            cmdPOS_Mxh_OrganizataDetaletUpdateInsert_spUpd.Parameters["@DataERegjistrimit"].Direction = ParameterDirection.Input;


            try
            {

                foreach (DataRow Row in tabela1.Rows)
                {
                    cmdPOS_Mxh_OrganizataDetaletUpdateInsert_spUpd.Parameters["@Id"].Value = Row["Id"];
                    cmdPOS_Mxh_OrganizataDetaletUpdateInsert_spUpd.Parameters["@Gjuha"].Value = Row["Gjuha"];
                    cmdPOS_Mxh_OrganizataDetaletUpdateInsert_spUpd.Parameters["@Email"].Value = Row["Email"];
                    cmdPOS_Mxh_OrganizataDetaletUpdateInsert_spUpd.Parameters["@Smtp"].Value = Row["Smtp"];
                    cmdPOS_Mxh_OrganizataDetaletUpdateInsert_spUpd.Parameters["@Porti"].Value = Row["Porti"];
                    cmdPOS_Mxh_OrganizataDetaletUpdateInsert_spUpd.Parameters["@UserName"].Value = Row["UserName"];
                    cmdPOS_Mxh_OrganizataDetaletUpdateInsert_spUpd.Parameters["@Pass"].Value = Row["Pass"];
                    cmdPOS_Mxh_OrganizataDetaletUpdateInsert_spUpd.Parameters["@DataERegjistrimit"].Value = Row["DataERegjistrimit"];
                    cmdPOS_Mxh_OrganizataDetaletUpdateInsert_spUpd.ExecuteNonQuery();

                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void POS_RuajNjesit(DateTime DataKohaSinkronizimitParaprak)
        {

            DataTable tabela1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_NjesitPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DeriMeOren", DataKohaSinkronizimitParaprak);
            da.Fill(tabela1);


            SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);

            SqlCommand cmdPOS_NjesitUpdateInsert_spUpd = new SqlCommand("TOSHIBA.POS_NjesitUpdateInsert_sp", cnn);
            cmdPOS_NjesitUpdateInsert_spUpd.CommandType = CommandType.StoredProcedure;


            cmdPOS_NjesitUpdateInsert_spUpd.Parameters.Add("@Id", SqlDbType.TinyInt);
            cmdPOS_NjesitUpdateInsert_spUpd.Parameters["@Id"].Direction = ParameterDirection.Input;
            cmdPOS_NjesitUpdateInsert_spUpd.Parameters.Add("@Pershkrimi", SqlDbType.VarChar);
            cmdPOS_NjesitUpdateInsert_spUpd.Parameters["@Pershkrimi"].Direction = ParameterDirection.Input;
            cmdPOS_NjesitUpdateInsert_spUpd.Parameters.Add("@Njesia", SqlDbType.VarChar);
            cmdPOS_NjesitUpdateInsert_spUpd.Parameters["@Njesia"].Direction = ParameterDirection.Input;
            cmdPOS_NjesitUpdateInsert_spUpd.Parameters.Add("@DataERegjistrimit", SqlDbType.DateTime);
            cmdPOS_NjesitUpdateInsert_spUpd.Parameters["@DataERegjistrimit"].Direction = ParameterDirection.Input;

            try
            {
                if (cnn.State != ConnectionState.Open)
                {
                    cnn.Open();
                }
                foreach (DataRow Row in tabela1.Rows)
                {
                    cmdPOS_NjesitUpdateInsert_spUpd.Parameters["@Id"].Value = Row["Id"];
                    cmdPOS_NjesitUpdateInsert_spUpd.Parameters["@Pershkrimi"].Value = Row["Pershkrimi"];
                    cmdPOS_NjesitUpdateInsert_spUpd.Parameters["@Njesia"].Value = Row["Njesia"];
                    cmdPOS_NjesitUpdateInsert_spUpd.Parameters["@DataERegjistrimit"].Value = Row["DataERegjistrimit"];
                    cmdPOS_NjesitUpdateInsert_spUpd.ExecuteNonQuery();

                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void POS_RuajPeshoret(DateTime DataKohaSinkronizimitParaprak)
        {
            DataTable tabela1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_PeshoretPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DeriMeOren", DataKohaSinkronizimitParaprak);
            da.Fill(tabela1);



            SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
            cnn.Open();
            SqlCommand cmdPOS_PeshoretUpdateInsert_spUpd = new SqlCommand("TOSHIBA.POS_PeshoretUpdateInsert_sp", cnn);
            cmdPOS_PeshoretUpdateInsert_spUpd.CommandType = CommandType.StoredProcedure;


            cmdPOS_PeshoretUpdateInsert_spUpd.Parameters.Add("@Id", SqlDbType.Int);
            cmdPOS_PeshoretUpdateInsert_spUpd.Parameters["@Id"].Direction = ParameterDirection.Input;
            cmdPOS_PeshoretUpdateInsert_spUpd.Parameters.Add("@Shifra", SqlDbType.Int);
            cmdPOS_PeshoretUpdateInsert_spUpd.Parameters["@Shifra"].Direction = ParameterDirection.Input;
            cmdPOS_PeshoretUpdateInsert_spUpd.Parameters.Add("@Pershkrimi", SqlDbType.VarChar);
            cmdPOS_PeshoretUpdateInsert_spUpd.Parameters["@Pershkrimi"].Direction = ParameterDirection.Input;
            cmdPOS_PeshoretUpdateInsert_spUpd.Parameters.Add("@Statusi", SqlDbType.Bit);
            cmdPOS_PeshoretUpdateInsert_spUpd.Parameters["@Statusi"].Direction = ParameterDirection.Input;
            cmdPOS_PeshoretUpdateInsert_spUpd.Parameters.Add("@DataERegjistrimit", SqlDbType.DateTime);
            cmdPOS_PeshoretUpdateInsert_spUpd.Parameters["@DataERegjistrimit"].Direction = ParameterDirection.Input;

            try
            {
                foreach (DataRow Row in tabela1.Rows)
                {
                    cmdPOS_PeshoretUpdateInsert_spUpd.Parameters["@Id"].Value = Row["Id"];
                    cmdPOS_PeshoretUpdateInsert_spUpd.Parameters["@Shifra"].Value = Row["Shifra"];
                    cmdPOS_PeshoretUpdateInsert_spUpd.Parameters["@Pershkrimi"].Value = Row["Pershkrimi"];
                    cmdPOS_PeshoretUpdateInsert_spUpd.Parameters["@Statusi"].Value = Row["Statusi"];
                    cmdPOS_PeshoretUpdateInsert_spUpd.Parameters["@DataERegjistrimit"].Value = Row["DataERegjistrimit"];
                    cmdPOS_PeshoretUpdateInsert_spUpd.ExecuteNonQuery();

                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void POS_RuajSubjektiBankat(DateTime DataKohaSinkronizimitParaprak)
        {
            DataTable tabela1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.SubjektiBankatPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DeriMeOren", DataKohaSinkronizimitParaprak);
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", PublicClass.OrganizataId);
            da.Fill(tabela1);


            SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
            cnn.Open();
            SqlCommand cmdSubjektiBankatUpdate_spUpd = new SqlCommand("TOSHIBA.SubjektiBankatUpdateInsert_sp", cnn);
            cmdSubjektiBankatUpdate_spUpd.CommandType = CommandType.StoredProcedure;
            cmdSubjektiBankatUpdate_spUpd.Parameters.AddWithValue("@SubjektiBankatType", tabela1);

            try
            {
                cmdSubjektiBankatUpdate_spUpd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void POS_RuajBankat(DateTime DataKohaSinkronizimitParaprak)
        {
            DataTable tabela1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.BankatPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DeriMeOren", DataKohaSinkronizimitParaprak);
            da.Fill(tabela1);


            SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
            cnn.Open();
            SqlCommand cmdBankatUpdate_spUpd = new SqlCommand("TOSHIBA.BankatUpdateInsert_sp", cnn);
            cmdBankatUpdate_spUpd.CommandType = CommandType.StoredProcedure;
            cmdBankatUpdate_spUpd.Parameters.Add("@BankatType", tabela1);
            try
            {
                cmdBankatUpdate_spUpd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void POS_RuajArkat(DateTime DataKohaSinkronizimitParaprak)
        {
            DataTable tabela1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_ArkatPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@NrArkes", PublicClass.Arka.NrArkes);
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", PublicClass.OrganizataId);
            da.Fill(tabela1);
            if (tabela1.Rows.Count < 1) { return; }
            DataRow Row = tabela1.Rows[0];

            SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
            cnn.Open();

            SqlCommand cmdArkatInsert_spIns = new SqlCommand("TOSHIBA.POS_ArkatInsertUpdate_sp", cnn);
            cmdArkatInsert_spIns.CommandType = CommandType.StoredProcedure;

            try
            {
                cmdArkatInsert_spIns.Parameters.AddWithValue("@ArkatTp", tabela1);
                cmdArkatInsert_spIns.ExecuteNonQuery();

                ArkatClass A = new ArkatClass();

                //Ky rresht i vendos vlerat qe nuk i sjell databaza nga klasa publike egzistuese
                //ne klasen qe do te ruhet ne disk

                FunctionClass.CopyClass(PublicClass.Arka, A);

                A.Id = Convert.ToInt32(Row["Id"]);
                A.NrArkes = Convert.ToInt16(Row["NrArkes"]);
                A.HostName = Row["HostName"].ToString();
                A.FLinkCode = Row["FLinkCode"].ToString();
                A.PGMCode = Row["PGMCode"].ToString();
                A.NumriArkesGK = Row["NumriArkesGK"].ToString();
                A.VerzioniIArkes = Row["VerzioniIArkes"].ToString();
                if (Row["ShtypjaAutomatikeZRaport"].ToString() != "")
                    A.ShtypjaAutomatikeZRaport = Convert.ToBoolean(Row["ShtypjaAutomatikeZRaport"].ToString());
                if (Row["KohaEShtypjesSeZRaportit"].ToString() != "")
                    A.KohaEShtypjesSeZRaportit = Convert.ToDateTime(Row["KohaEShtypjesSeZRaportit"].ToString());
                A.LejoKerkiminmeEmer = Convert.ToBoolean(Row["LejoKerkiminmeEmer"].ToString());
                A.AplikocmiminMeShumiceKurarrihetPaketimi = Convert.ToBoolean(Row["AplikocmiminMeShumiceKurarrihetPaketimi"].ToString());
                A.LejoStokunNegative = Convert.ToBoolean(Row["LejoStokunNegative"].ToString());
                A.LejoZbritjenNeArke = Convert.ToBoolean(Row["LejoZbritjenNeArke"].ToString());
                A.LejoNDerrimineCmimit = Convert.ToBoolean(Row["LejoNDerrimineCmimit"].ToString());
                A.ShtypKopjenEKuponitFiskal = Convert.ToBoolean(Row["ShtypKopjenEKuponitFiskal"].ToString());
                A.KerkoPassWordPerAplikiminEZbritjes = Convert.ToBoolean(Row["KerkoPassWordPerAplikiminEZbritjes"].ToString());
                A.LejoRabatPerTeGjitheArtikujt = Convert.ToBoolean(Row["LejoRabatPerTeGjitheArtikujt"].ToString());
                A.LejoZbritjeNeTotalVler = Convert.ToBoolean(Row["LejoZbritjeNeTotalVler"].ToString());
                if (Row["RegjimiPunesOffline"].ToString() != "")
                    A.RegjimiPunesOffline = Convert.ToBoolean(Row["RegjimiPunesOffline"].ToString());
                A.IntervaliImportimitSekonda = Convert.ToInt32(Row["IntervaliImportimitSekonda"].ToString());
                A.IntervaliDergimitSekonda = Convert.ToInt32(Row["IntervaliDergimitSekonda"].ToString());
                if (Row["OperatoriAutomatikId"].ToString() != "")
                    A.OperatoriAutomatikId = Convert.ToInt32(Row["OperatoriAutomatikId"].ToString());
                if (Row["KaTeDrejtTePunojOffline"].ToString() != "")
                    A.KaTeDrejtTePunojOffline = Convert.ToBoolean(Row["KaTeDrejtTePunojOffline"].ToString());
                if (Row["ShteguFiskal"].ToString() != "")
                    A.ShteguFiskal = (Row["ShteguFiskal"].ToString());
                if (Row["TipiPrinterit"].ToString() != "")
                    A.TipiPrinterit = (Row["TipiPrinterit"].ToString());
                if (Row["Porti"].ToString() != "")
                    A.Porti = (Row["Porti"].ToString());
                if (Row["PrintonDirekt"].ToString() != "")
                    A.PrintonDirekt = Convert.ToBoolean(Row["PrintonDirekt"].ToString());
                if (Row["HapPortinNjeHere"].ToString() != "")
                    A.HapPortinNjeHere = Convert.ToBoolean(Row["HapPortinNjeHere"].ToString());
                if (Row["NeTestim"].ToString() != "")
                    A.NeTestim = Convert.ToBoolean(Row["NeTestim"].ToString());
                if (Row["TouchScreen"].ToString() != "")
                    A.TouchScreen = Convert.ToBoolean(Row["TouchScreen"].ToString());
                if (Row["LejoNdryshiminESasise"].ToString() != "")
                    A.LejoNdryshiminESasise = Convert.ToBoolean(Row["LejoNdryshiminESasise"]);
                if (Row["LejoFshirjenEArtikujve"].ToString() != "")
                    A.LejoFshirjenEArtikujve = Convert.ToBoolean(Row["LejoFshirjenEArtikujve"]);
                if (Row["TransferetEnable"].ToString() != "")
                    A.TransferetEnable = Convert.ToBoolean(Row["TransferetEnable"]);
                if (Row["ShperblimetEnable"].ToString() != "")
                    A.ShperblimetEnable = Convert.ToBoolean(Row["ShperblimetEnable"]);
                if (Row["SubjektiDetaleEnable"].ToString() != "")
                    A.SubjektiDetaleEnable = Convert.ToBoolean(Row["SubjektiDetaleEnable"]);

                if (PublicClass.Arka != null && PublicClass.Arka != A)
                {
                    A.WriteToDisc();
                    PublicClass.Arka = A;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }
        private void Klasifikatoret(DateTime DataKohaSinkronizimitParaprak)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.KlasifikatoretPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.Fill(tabela);
            try
            {
                SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
                cnn.Open();
                SqlCommand cmdKlasifikatoretUpdateInsert_spUpd = new SqlCommand("TOSHIBA.KlasifikatoretUpdateInsert_sp", cnn);
                cmdKlasifikatoretUpdateInsert_spUpd.CommandType = CommandType.StoredProcedure;
                cmdKlasifikatoretUpdateInsert_spUpd.Parameters.AddWithValue("@KlasifikatoretType", tabela);
                cmdKlasifikatoretUpdateInsert_spUpd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void DaljeInternePersonat(DateTime DataKohaSinkronizimitParaprak)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.DaljeInternePersonatPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", PublicClass.OrganizataId);
            da.Fill(tabela);
            try
            {
                SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
                cnn.Open();
                SqlCommand cmdKlasifikatoretUpdateInsert_spUpd = new SqlCommand("TOSHIBA.DaljeInternePersonatUpdateInsert_sp", cnn);
                cmdKlasifikatoretUpdateInsert_spUpd.CommandType = CommandType.StoredProcedure;
                cmdKlasifikatoretUpdateInsert_spUpd.Parameters.AddWithValue("@DaljeInternePersonatType", tabela);
                cmdKlasifikatoretUpdateInsert_spUpd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void RaportetFajllat(DateTime DataKohaSinkronizimitParaprak)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.RaportetFajllatPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", PublicClass.OrganizataId);
            da.Fill(tabela);
            try
            {
                SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
                cnn.Open();
                SqlCommand cmdRS_KlasifikatoretPerKuzhineDeleteInsert_spUpd = new SqlCommand("TOSHIBA.RaportetFajllatInsert_sp", cnn);
                cmdRS_KlasifikatoretPerKuzhineDeleteInsert_spUpd.CommandType = CommandType.StoredProcedure;
                cmdRS_KlasifikatoretPerKuzhineDeleteInsert_spUpd.Parameters.AddWithValue("@RaportetFajllatTable", tabela);
                cmdRS_KlasifikatoretPerKuzhineDeleteInsert_spUpd.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void KartelatArtikujt(DateTime DataKohaSinkronizimitParaprak)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.PMPKartelatArtikujtPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            //da.SelectCommand.Parameters.AddWithValue("@OrganizataId", PublicClass.OrganizataId);
            da.Fill(tabela);

            SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
            cnn.Open();

            SqlCommand CCPKartelatArtikujtUpdate_sp = new SqlCommand("TOSHIBA.PMPCCPKartelatArtikujtUpdateInsert_sp", cnn);
            CCPKartelatArtikujtUpdate_sp.CommandType = CommandType.StoredProcedure;
            try
            {
                if (tabela.Rows.Count > 0)
                {
                    CCPKartelatArtikujtUpdate_sp.Parameters.AddWithValue("@KartelatArtikujtTP", tabela);
                    CCPKartelatArtikujtUpdate_sp.ExecuteNonQuery();
                    cnn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void DaljaMallitRreshtatEPare()
        {
            SqlDataAdapter daa = new SqlDataAdapter("TOSHIBA.DaljaMallitKaShenimeSelect_sp", PublicClass.KoneksioniLokal);
            DataTable dt = new DataTable();
            daa.Fill(dt);
            if (dt != null && dt.Rows.Count == 0)
            {
                DataTable tabela = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.DaljaMallitPerDatabazTeReSelect_Sp", PublicClass.KoneksioniPrimar);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@OrganizataId", PublicClass.OrganizataId);
                da.SelectCommand.Parameters.AddWithValue("@NumriArkes", PublicClass.Arka.NrArkes);
                da.Fill(tabela);

                SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
                cnn.Open();

                SqlCommand CCPKartelatArtikujtUpdate_sp = new SqlCommand("TOSHIBA.DaljaMallitRreshtatEPareInsert_Sp", cnn);
                CCPKartelatArtikujtUpdate_sp.CommandType = CommandType.StoredProcedure;
                try
                {
                    if (tabela.Rows.Count > 0)
                    {
                        CCPKartelatArtikujtUpdate_sp.Parameters.AddWithValue("@Tabela", tabela);
                        CCPKartelatArtikujtUpdate_sp.ExecuteNonQuery();
                        cnn.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        private void POSAktivitetetLlojet()
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POSAktivitetetLlojetPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.Fill(tabela);

            try
            {
                SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
                cnn.Open();

                SqlCommand cmdAktivitetetLlojetUpdateInsert_spUpd = new SqlCommand("TOSHIBA.POSAktivitetetLlojetUpdateInsert_sp", cnn);
                cmdAktivitetetLlojetUpdateInsert_spUpd.CommandType = CommandType.StoredProcedure;
                cmdAktivitetetLlojetUpdateInsert_spUpd.Parameters.AddWithValue("@AktivitetetLlojetType", tabela);

                if (tabela.Rows.Count > 0)
                    cmdAktivitetetLlojetUpdateInsert_spUpd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void POSAktivitetetFilterLlojet()
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POSAktivitetetFilterLlojetPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.Fill(tabela);

            try
            {
                SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
                cnn.Open();

                SqlCommand cmdPOSAktivitetetFilterLlojetUpdateInsert_spUpd = new SqlCommand("TOSHIBA.POSAktivitetetFilterLlojetUpdateInsert_sp", cnn);
                cmdPOSAktivitetetFilterLlojetUpdateInsert_spUpd.CommandType = CommandType.StoredProcedure;
                cmdPOSAktivitetetFilterLlojetUpdateInsert_spUpd.Parameters.AddWithValue("@POSAktivitetetFilterLlojetType", tabela);

                if (tabela.Rows.Count > 0)
                    cmdPOSAktivitetetFilterLlojetUpdateInsert_spUpd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void POSAktivitetet()
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POSAktivitetetPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", PublicClass.OrganizataId);
            da.Fill(tabela);

            try
            {
                SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
                cnn.Open();

                SqlCommand cmdPOSAktivitetetUpdateInsert_spUpd = new SqlCommand("TOSHIBA.POSAktivitetetUpdateInsert_sp", cnn);
                cmdPOSAktivitetetUpdateInsert_spUpd.CommandType = CommandType.StoredProcedure;
                cmdPOSAktivitetetUpdateInsert_spUpd.Parameters.AddWithValue("@POSAktivitetetPerSinkronizimType", tabela);
                cmdPOSAktivitetetUpdateInsert_spUpd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void POSAktivitetetLlojetEZbritjes()
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POSAktivitetetLlojetEZbritjesPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.Fill(tabela);

            try
            {
                SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
                cnn.Open();

                SqlCommand cmdPOSAktivitetetLlojetEZbritjesUpdateInsert_spUpd = new SqlCommand("TOSHIBA.POSAktivitetetLlojetEZbritjesUpdateInsert_sp", cnn);
                cmdPOSAktivitetetLlojetEZbritjesUpdateInsert_spUpd.CommandType = CommandType.StoredProcedure;
                cmdPOSAktivitetetLlojetEZbritjesUpdateInsert_spUpd.Parameters.AddWithValue("@POSAktivitetetLlojetEZbritjesType", tabela);

                if (tabela.Rows.Count > 0)
                    cmdPOSAktivitetetLlojetEZbritjesUpdateInsert_spUpd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void POSAktivitetetOrganizatat()
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POSAktivitetetOrganizatatPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", PublicClass.OrganizataId);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.Fill(tabela);

            try
            {
                SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
                cnn.Open();

                SqlCommand cmdPOSAktivitetetOrganizatatUpdateInsert_spUpd = new SqlCommand("TOSHIBA.POSAktivitetetOrganizatatUpdateInsert_sp", cnn);
                cmdPOSAktivitetetOrganizatatUpdateInsert_spUpd.CommandType = CommandType.StoredProcedure;
                cmdPOSAktivitetetOrganizatatUpdateInsert_spUpd.Parameters.AddWithValue("@POSAktivitetetOrganizatatType", tabela);
                cmdPOSAktivitetetOrganizatatUpdateInsert_spUpd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void POSAktivitetetZbritjaNeVlere()
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POSAktivitetetZbritjaNeVlerePerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", PublicClass.OrganizataId);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.Fill(tabela);

            try
            {
                SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
                cnn.Open();

                SqlCommand cmdPOSAktivitetetZbritjaNeVlereUpdateInsert_spUpd = new SqlCommand("TOSHIBA.POSAktivitetetZbritjaNeVlereUpdateInsert_sp", cnn);
                cmdPOSAktivitetetZbritjaNeVlereUpdateInsert_spUpd.CommandType = CommandType.StoredProcedure;
                cmdPOSAktivitetetZbritjaNeVlereUpdateInsert_spUpd.Parameters.AddWithValue("@POSAktivitetetZbritjaNeVlereType", tabela);
                cmdPOSAktivitetetZbritjaNeVlereUpdateInsert_spUpd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void POSAktivitetetFilter()
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POSAktivitetetFilterPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", PublicClass.OrganizataId);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.Fill(tabela);

            try
            {
                SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
                cnn.Open();

                SqlCommand cmdPOSAktivitetetFilterUpdateInsert_spUpd = new SqlCommand("TOSHIBA.POSAktivitetetFilterUpdateInsert_sp", cnn);
                cmdPOSAktivitetetFilterUpdateInsert_spUpd.CommandType = CommandType.StoredProcedure;
                cmdPOSAktivitetetFilterUpdateInsert_spUpd.Parameters.AddWithValue("@POSAktivitetetFilterType", tabela);
                cmdPOSAktivitetetFilterUpdateInsert_spUpd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Mxh_OperatoretGrupetEMallrave()
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.Mxh_OperatoretGrupetEMallravePerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.Fill(tabela);

            try
            {
                SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
                cnn.Open();

                SqlCommand cmdMxh_OperatoretGrupetEMallraveUpdateInsert_spUpd = new SqlCommand("TOSHIBA.Mxh_OperatoretGrupetEMallraveUpdateInsert_sp", cnn);
                cmdMxh_OperatoretGrupetEMallraveUpdateInsert_spUpd.CommandType = CommandType.StoredProcedure;
                cmdMxh_OperatoretGrupetEMallraveUpdateInsert_spUpd.Parameters.AddWithValue("@Mxh_OperatoretGrupetEMallraveType", tabela);

                cmdMxh_OperatoretGrupetEMallraveUpdateInsert_spUpd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CCPGrupet()
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.CCPGrupetPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.Fill(tabela);

            try
            {
                SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
                cnn.Open();

                SqlCommand cmdCCPGrupetUpdateInsert_spUpd = new SqlCommand("TOSHIBA.CCPGrupetUpdateInsert_sp", cnn);
                cmdCCPGrupetUpdateInsert_spUpd.CommandType = CommandType.StoredProcedure;
                cmdCCPGrupetUpdateInsert_spUpd.Parameters.AddWithValue("@CCPGrupetType", tabela);

                cmdCCPGrupetUpdateInsert_spUpd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void POSAktivitetetFilterCCPGrupet()
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POSAktivitetetFilterCCPGrupetPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", PublicClass.OrganizataId);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.Fill(tabela);

            try
            {
                SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
                cnn.Open();

                SqlCommand cmdPOSAktivitetetFilterCCPGrupetUpdateInsert_spUpd = new SqlCommand("TOSHIBA.POSAktivitetetFilterCCPGrupetUpdateInsert_sp", cnn);
                cmdPOSAktivitetetFilterCCPGrupetUpdateInsert_spUpd.CommandType = CommandType.StoredProcedure;
                cmdPOSAktivitetetFilterCCPGrupetUpdateInsert_spUpd.Parameters.AddWithValue("@POSAktivitetetFilterCCPGrupetType", tabela);

                cmdPOSAktivitetetFilterCCPGrupetUpdateInsert_spUpd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ArtikujtPerberesOrganizatat()
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.ArtikujtPerberesOrganizatatPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", PublicClass.OrganizataId);
            da.Fill(tabela);

            try
            {
                SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
                cnn.Open();

                SqlCommand cmdArtikujtPerberesOrganizatatDeleteInsert_spDelIns = new SqlCommand("TOSHIBA.ArtikujtPerberesOrganizatatDeleteInsert_sp", cnn);
                cmdArtikujtPerberesOrganizatatDeleteInsert_spDelIns.CommandType = CommandType.StoredProcedure;
                cmdArtikujtPerberesOrganizatatDeleteInsert_spDelIns.Parameters.AddWithValue("@ArtikujtPerberesOrganizatatType", tabela);
                cmdArtikujtPerberesOrganizatatDeleteInsert_spDelIns.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void AktivitetetDokument()
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.AktivitetetDokumentPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", PublicClass.OrganizataId);
            da.Fill(tabela);

            try
            {
                SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
                cnn.Open();

                SqlCommand cmdAktivitetetDokumentUpdateInsert_spUpdIns = new SqlCommand("TOSHIBA.AktivitetetDokumentUpdateInsert_sp", cnn);
                cmdAktivitetetDokumentUpdateInsert_spUpdIns.CommandType = CommandType.StoredProcedure;
                cmdAktivitetetDokumentUpdateInsert_spUpdIns.Parameters.AddWithValue("@AktivitetetDokumentType", tabela);
                cmdAktivitetetDokumentUpdateInsert_spUpdIns.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void AktivitetetDokumentDetale()
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.AktivitetetDokumentDetalePerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", PublicClass.OrganizataId);
            da.Fill(tabela);

            try
            {
                SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
                cnn.Open();

                SqlCommand cmdAktivitetetDokumentDetaleUpdateInsert_spUpdIns = new SqlCommand("TOSHIBA.AktivitetetDokumentDetaleUpdateInsert_sp", cnn);
                cmdAktivitetetDokumentDetaleUpdateInsert_spUpdIns.CommandType = CommandType.StoredProcedure;
                cmdAktivitetetDokumentDetaleUpdateInsert_spUpdIns.Parameters.AddWithValue("@AktivitetetDokumentDetaleType", tabela);
                cmdAktivitetetDokumentDetaleUpdateInsert_spUpdIns.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Valutat()
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.ValutatPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.Fill(tabela);

            try
            {
                SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
                cnn.Open();

                SqlCommand cmdValutatUpdateInsert_spUpdIns = new SqlCommand("TOSHIBA.ValutatUpdateInsert_sp", cnn);
                cmdValutatUpdateInsert_spUpdIns.CommandType = CommandType.StoredProcedure;
                cmdValutatUpdateInsert_spUpdIns.Parameters.AddWithValue("@ValutatType", tabela);
                cmdValutatUpdateInsert_spUpdIns.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void POSAktivitetetKuponat()
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POSAktivitetetKuponatPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", PublicClass.OrganizataId);
            da.Fill(tabela);

            try
            {
                SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
                cnn.Open();

                SqlCommand cmdUpdateInsert_spUpdIns = new SqlCommand("TOSHIBA.POSAktivitetetKuponatUpdateInsert_sp", cnn);
                cmdUpdateInsert_spUpdIns.CommandType = CommandType.StoredProcedure;
                cmdUpdateInsert_spUpdIns.Parameters.AddWithValue("@POSAktivitetetKuponatType", tabela);
                cmdUpdateInsert_spUpdIns.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CCPAlokimiBonuseve()
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.CCPAlokimiBonusevePerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.Fill(tabela);

            try
            {
                SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
                cnn.Open();

                SqlCommand cmdUpdateInsert_spUpdIns = new SqlCommand("TOSHIBA.CCPAlokimiBonuseveUpdateInsert_sp", cnn);
                cmdUpdateInsert_spUpdIns.CommandType = CommandType.StoredProcedure;
                cmdUpdateInsert_spUpdIns.Parameters.AddWithValue("@CCPAlokimiBonuseveType", tabela);
                cmdUpdateInsert_spUpdIns.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CCPShperblimetArtikujt()
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.CCPShperblimetArtikujtPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.Fill(tabela);

            try
            {
                SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
                cnn.Open();

                SqlCommand cmdUpdateInsert_spUpdIns = new SqlCommand("TOSHIBA.CCPShperblimetArtikujtUpdateInsert_sp", cnn);
                cmdUpdateInsert_spUpdIns.CommandType = CommandType.StoredProcedure;
                cmdUpdateInsert_spUpdIns.Parameters.AddWithValue("@CCPShperblimetArtikujtType", tabela);
                cmdUpdateInsert_spUpdIns.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CCPFaturat()
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.CCPFaturatPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.Fill(tabela);

            try
            {
                SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
                cnn.Open();

                SqlCommand cmdUpdateInsert_spUpdIns = new SqlCommand("TOSHIBA.CCPFaturatUpdateInsert_sp", cnn);
                cmdUpdateInsert_spUpdIns.CommandType = CommandType.StoredProcedure;
                cmdUpdateInsert_spUpdIns.Parameters.AddWithValue("@CCPFaturatType", tabela);
                cmdUpdateInsert_spUpdIns.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CCPArtikujtEPerjashtuar()
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.CCPArtikujtEPerjashtuarPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.Fill(tabela);

            try
            {
                SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
                cnn.Open();

                SqlCommand cmdUpdateInsert_spUpdIns = new SqlCommand("TOSHIBA.CCPArtikujtEPerjashtuarUpdateInsert_sp", cnn);
                cmdUpdateInsert_spUpdIns.CommandType = CommandType.StoredProcedure;
                cmdUpdateInsert_spUpdIns.Parameters.AddWithValue("@CCPArtikujtEPerjashtuarType", tabela);
                cmdUpdateInsert_spUpdIns.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void POSKuponatPerZbritje()
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POSKuponatPerZbritjePerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@OrganizataId", PublicClass.OrganizataId);
            da.Fill(tabela);
            SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
            try
            {
                cnn.Open();
                SqlCommand cmdUpdateInsert_spUpdIns = new SqlCommand("TOSHIBA.POSKuponatPerZbritjeUpdateInsert_sp", cnn);
                cmdUpdateInsert_spUpdIns.CommandType = CommandType.StoredProcedure;
                cmdUpdateInsert_spUpdIns.Parameters.AddWithValue("@POSKuponatPerZbritjeType", tabela);
                cmdUpdateInsert_spUpdIns.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }
        private void ArtikujtPaZbritje()
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.ArtikujtPaZbritjePerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.Fill(tabela);

            SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);

            try
            {
                cnn.Open();
                SqlCommand cmdUpdateInsert_spUpdIns = new SqlCommand("TOSHIBA.ArtikujtPaZbritjeUpdateInsert_sp", cnn);
                cmdUpdateInsert_spUpdIns.CommandType = CommandType.StoredProcedure;
                cmdUpdateInsert_spUpdIns.Parameters.AddWithValue("@ArtikujtPaZbritjeType", tabela);
                cmdUpdateInsert_spUpdIns.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }
        private void DaljaMallitKushteEBleresit(DateTime DataKohaSinkronizimitParaprak)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_DaljaMallitKushteEBleresitPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DataERegjistrimit", DataKohaSinkronizimitParaprak);
            da.Fill(tabela);

            SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);

            try
            {
                cnn.Open();
                SqlCommand cmdUpdateInsert_spUpdIns = new SqlCommand("TOSHIBA.POS_DaljaMallitKushteEBleresitUpdateInsert_sp", cnn);
                cmdUpdateInsert_spUpdIns.CommandType = CommandType.StoredProcedure;
                cmdUpdateInsert_spUpdIns.Parameters.AddWithValue("@DaljaMallitKushteEBleresitTP", tabela);
                cmdUpdateInsert_spUpdIns.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }
        private void DaljaMallitKushteEBleresitArtikujt(DateTime DataKohaSinkronizimitParaprak)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.POS_DaljaMallitKushteEBleresitArtikujtPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@DataERegjistrimit", DataKohaSinkronizimitParaprak);
            da.Fill(tabela);

            SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);

            try
            {
                cnn.Open();
                SqlCommand cmdUpdateInsert_spUpdIns = new SqlCommand("TOSHIBA.POS_DaljaMallitKushteEBleresitArtikujtUpdateInsert_sp", cnn);
                cmdUpdateInsert_spUpdIns.CommandType = CommandType.StoredProcedure;
                cmdUpdateInsert_spUpdIns.Parameters.AddWithValue("@DaljaMallitKushteEBleresitArtikujtTP", tabela);
                cmdUpdateInsert_spUpdIns.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }
        private void KthimiMallitArsyet()
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.KthimiMallitArsyetPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.Fill(tabela);

            SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);

            try
            {
                cnn.Open();
                SqlCommand cmdUpdateInsert_spUpdIns = new SqlCommand("TOSHIBA.KthimiMallitArsyetUpdateInsert_sp", cnn);
                cmdUpdateInsert_spUpdIns.CommandType = CommandType.StoredProcedure;
                cmdUpdateInsert_spUpdIns.Parameters.AddWithValue("@KthimiMallitArsyetType", tabela);
                cmdUpdateInsert_spUpdIns.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }
        private void CCPSubjektetGrupet(DateTime DataKohaSinkronizimitParaprak)
        {
            DataTable tabela = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("TOSHIBA.CCPSubjektetGrupetPerSinkronizimSelect_sp", PublicClass.KoneksioniPrimar);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.Fill(tabela);
            if (tabela.Rows.Count < 1)
                return;
            SqlConnection cnn = new SqlConnection(PublicClass.KoneksioniLokal);
            cnn.Open();
            SqlCommand PMPSkenimetArsyejetUpdate_sp = new SqlCommand("TOSHIBA.CCPSubjektetGrupetUpdateInsert_sp", cnn);
            PMPSkenimetArsyejetUpdate_sp.CommandType = CommandType.StoredProcedure;
            try
            {
                if (tabela.Rows.Count > 0)
                {
                    PMPSkenimetArsyejetUpdate_sp.Parameters.AddWithValue("@CCPSubjektetGrupetTP", tabela);
                    PMPSkenimetArsyejetUpdate_sp.ExecuteNonQuery();
                }
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
