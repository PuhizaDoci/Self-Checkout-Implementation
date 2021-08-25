using NLog;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using ToshibaPOS.DAL;

namespace ToshibaPOS
{
    public class FaturaBLL
    {
        public FaturaBLL()
        {
            dtDaljaMallitDetaleAktivitetet = 
                POSAktivitetetClass.GetDaljaMallitDetaleAktivitetet(new DataTable(), "-1");
            ds = new DataSet();
            DaljaMallitCL = new CL.DaljaMallitCL();

            dtDaljaMallitDetale = new DataTable();
            dtDaljaMallitDetale = Tools.ConvertClassToDataTable.
                KonevtoKlasenNeDataTable(typeof(CL.DaljaMallitDetaleCL));
            dtDaljaMallitDetale.Columns["Id"].AutoIncrement = true;
            dtDaljaMallitDetale.Columns["Id"].AutoIncrementSeed = -1;
            dtDaljaMallitDetale.Columns["Id"].AutoIncrementStep = -1;

            dtEkzekutimiPageses = new DataTable();
            dtEkzekutimiPageses = Tools.ConvertClassToDataTable.
                KonevtoKlasenNeDataTable(typeof(CL.EkzekutimiPagesesCL));
            dtEkzekutimiPageses.Columns["Id"].AutoIncrement = true;
            dtEkzekutimiPageses.Columns["Id"].AutoIncrementSeed = -1;
            dtEkzekutimiPageses.Columns["Id"].AutoIncrementStep = -1;

            dtDaljaMallit = new DataTable();
            dtDaljaMallit = Tools.ConvertClassToDataTable.
                KonevtoKlasenNeDataTable(typeof(CL.DaljaMallitCL));

            ds.Tables.Add(dtDaljaMallitDetale);
            ds.Tables.Add(dtEkzekutimiPageses);
            ds.Tables.Add(dtDaljaMallit);

            bsHeader.DataSource = dtDaljaMallit;
            dtDaljaMallitDetale.Columns["DaljaMallitId"].DefaultValue = DaljaMallitCL.Id;
            dtEkzekutimiPageses.Columns["DaljaMallitId"].DefaultValue = DaljaMallitCL.Id;

            dtDaljaMallitDetale.Columns.Add("VleraPaTvsh", typeof(decimal), 
                "Sasia*(((QmimiShitjes*(1-Rabati/100))*(1-EkstraRabati/100))/(1+TVSH/100))");
            dtDaljaMallitDetale.Columns.Add("VleraMeTvsh", typeof(decimal),
                "Sasia*(((QmimiShitjes*(1-Rabati/100))*(1-EkstraRabati/100)))");
            dtDaljaMallitDetale.Columns.Add("VleraTvsh", typeof(decimal),
                "VleraMeTvsh-VleraPaTvsh");

        }

        public DataSet ds { get; set; }
        public CL.DaljaMallitCL DaljaMallitCL { get; set; }
        public DataTable dtDaljaMallit { get; set; }
        public DataTable dtDaljaMallitDetale { get; set; }
        public DataTable dtEkzekutimiPageses { get; set; }
        public CL.DaljaMallitDetaleCL ArtikulliIFunditIShtuar { get; set; }

        DataTable dtDaljaMallitDetaleAktivitetet = new DataTable();
        BindingSource bsHeader = new BindingSource();
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public string ShtoArtikullin(string Barkodi, int largimi)
        {
            ArtikulliIFunditIShtuar = new CL.DaljaMallitDetaleCL(Barkodi);
            if (ArtikulliIFunditIShtuar.ArtikulliId > 0)
            {
                DataRow drIRi = dtDaljaMallitDetale.NewRow();
                drIRi["Nr"] = dtDaljaMallitDetale.Rows.Count + 1;
                drIRi["Shifra"] = ArtikulliIFunditIShtuar.Shifra;
                drIRi["Emertimi"] = ArtikulliIFunditIShtuar.Emertimi;
                drIRi["ShifraProdhuesit"] = ArtikulliIFunditIShtuar.ShifraProdhuesit;
                drIRi["PershkrimiFiskal"] = ArtikulliIFunditIShtuar.PershkrimiFiskal;
                drIRi["NumriKatallogut"] = ArtikulliIFunditIShtuar.NumriKatallogut;
                drIRi["ArtikulliId"] = ArtikulliIFunditIShtuar.ArtikulliId;
                drIRi["NjesiaID"] = ArtikulliIFunditIShtuar.NjesiaID;
                drIRi["Njesia"] = ArtikulliIFunditIShtuar.Njesia;
                if (drIRi["BrendId"] != DBNull.Value)
                    drIRi["BrendId"] = ArtikulliIFunditIShtuar.BrendId;
                drIRi["Brendi"] = ArtikulliIFunditIShtuar.Brendi;
                drIRi["TvshKategoria"] = ArtikulliIFunditIShtuar.TvshKategoria;
                drIRi["Sasia"] = ArtikulliIFunditIShtuar.Sasia * largimi;
                drIRi["QmimiRregullt"] = ArtikulliIFunditIShtuar.QmimiRregullt;
                drIRi["QmimiShumice"] = ArtikulliIFunditIShtuar.QmimiShumice;
                drIRi["QmimiShitjes"] = ArtikulliIFunditIShtuar.QmimiShitjes;
                drIRi["Rabati"] = ArtikulliIFunditIShtuar.Rabati;
                drIRi["EkstraRabati"] = ArtikulliIFunditIShtuar.EkstraRabati;
                drIRi["Tvsh"] = ArtikulliIFunditIShtuar.Tvsh;
                if (drIRi["QmimiPaTvsh"] != DBNull.Value)
                    drIRi["QmimiPaTvsh"] = ArtikulliIFunditIShtuar.QmimiPaTvsh;
                if (drIRi["QmimiFinal"] != DBNull.Value)
                    drIRi["QmimiFinal"] = ArtikulliIFunditIShtuar.QmimiFinal;
                if (drIRi["NdryshuarNgaShfrytezuesi"] != DBNull.Value)
                    drIRi["NdryshuarNgaShfrytezuesi"] =
                        ArtikulliIFunditIShtuar.NdryshuarNgaShfrytezuesi;
                drIRi["LlojiZbritjes"] = ArtikulliIFunditIShtuar.LlojiZbritjes;
                drIRi["ArtikullPaZbritje"] = ArtikulliIFunditIShtuar.ArtikullPaZbritje;
                drIRi["NumriSerik"] = ArtikulliIFunditIShtuar.NumriSerik;
                drIRi["DataESkadences"] = ArtikulliIFunditIShtuar.DataESkadences;
                drIRi["SasiaMaksimale"] = ArtikulliIFunditIShtuar.SasiaMaksimale;
                if (drIRi["ArtikujtEProdhuarId"] != DBNull.Value)
                    drIRi["ArtikujtEProdhuarId"] = ArtikulliIFunditIShtuar.ArtikujtEProdhuarId;
                if (drIRi["HyrjaDetaleId"] != DBNull.Value)
                    drIRi["HyrjaDetaleId"] = ArtikulliIFunditIShtuar.HyrjaDetaleId;
                drIRi["IdUnikeProdhimHyrje"] = ArtikulliIFunditIShtuar.IdUnikeProdhimHyrje;
                drIRi["Paketimi"] = ArtikulliIFunditIShtuar.Paketimi;
                if (drIRi["DaljaMallitDetaleKthimiId"] != DBNull.Value)
                    drIRi["DaljaMallitDetaleKthimiId"] = 
                        ArtikulliIFunditIShtuar.DaljaMallitDetaleKthimiId;
                drIRi["SasiaPaketave"] = ArtikulliIFunditIShtuar.SasiaPaketave;
                drIRi["StatusiQmimitId"] = ArtikulliIFunditIShtuar.StatusiQmimitId;
                drIRi["Kursi"] = ArtikulliIFunditIShtuar.Kursi;
                drIRi["KaLirim"] = ArtikulliIFunditIShtuar.KaLirim;
                drIRi["NumriArkes"] = ArtikulliIFunditIShtuar.NumriArkes;
                if (drIRi["AplikimiVoucherit"] != DBNull.Value)
                    drIRi["AplikimiVoucherit"] = ArtikulliIFunditIShtuar.AplikimiVoucherit;

                dtDaljaMallitDetale.Rows.Add(drIRi);
            }
            return "OK";
        }

        public string ShtoPagesen(int _MenyraPagesesId,
            PosPcb posPcbObj = null, decimal? balanceDue = null)
        {
            try
            {
                CL.EkzekutimiPagesesCL ep = 
                    new CL.EkzekutimiPagesesCL(_MenyraPagesesId);
                PosPcb posObj = new PosPcb();

                if (posPcbObj != null)
                    posObj = posPcbObj;

                string temp1 = "";

                if (!(ep != null && ep.MenyraEPagesesId > 0))
                    return "Menyra e pageses nuk u gjet!";

                if (_MenyraPagesesId == 13)
                {
                    if (posPcbObj != null)
                        temp1 = posObj.Purchase(Convert.ToUInt32(balanceDue * 100), 0, 1);

                    if (temp1 != "OK")
                        return "Pagesa ne POS nuk eshte kryer me sukses";
                }

                if (dtEkzekutimiPageses.Rows.Count > 0 && _MenyraPagesesId != 13)
                {
                    var rows = dtEkzekutimiPageses.AsEnumerable().Where(row => row.Field<int>("MenyraEPagesesId") == 22);
                    if (rows.Any())
                    {
                        DataTable dt = rows.CopyToDataTable<DataRow>();
                        if (Convert.ToDecimal(dt.Rows[0]["Paguar"]) != TotaliFatures)
                        {
                            foreach (DataRow dataRow in dtEkzekutimiPageses.Rows)
                            {
                                if(Convert.ToInt32(dataRow["MenyraEPagesesId"]) == 22)
                                {
                                    dataRow.Delete();
                                    break;
                                }
                            }
                            ep.Paguar = TotaliFatures;
                            ep.Vlera = ep.Paguar;
                            ep.DaljaMallitID = DaljaMallitCL.Id;
                            DataRow dr = dtEkzekutimiPageses.NewRow();
                            dr["MenyraEPagesesId"] = ep.MenyraEPagesesId;
                            dr["MenyraEPageses"] = ep.MenyraEPageses;
                            dr["Paguar"] = ep.Paguar;
                            dr["Vlera"] = ep.Vlera;
                            dr["DaljaMallitID"] = ep.DaljaMallitID;
                            dtEkzekutimiPageses.Rows.Add(dr);
                        }
                    }
                    else
                    {
                        ep.Paguar = TotaliFatures;
                        ep.Vlera = ep.Paguar;
                        ep.DaljaMallitID = DaljaMallitCL.Id;
                        DataRow dr = dtEkzekutimiPageses.NewRow();
                        dr["MenyraEPagesesId"] = ep.MenyraEPagesesId;
                        dr["MenyraEPageses"] = ep.MenyraEPageses;
                        dr["Paguar"] = ep.Paguar;
                        dr["Vlera"] = ep.Vlera;
                        dr["DaljaMallitID"] = ep.DaljaMallitID;
                        dtEkzekutimiPageses.Rows.Add(dr);
                    }
                }
                else
                {
                    ep.Paguar = TotaliFatures;
                    ep.Vlera = ep.Paguar;
                    ep.DaljaMallitID = DaljaMallitCL.Id;
                    DataRow dr = dtEkzekutimiPageses.NewRow();
                    dr["MenyraEPagesesId"] = ep.MenyraEPagesesId;
                    dr["MenyraEPageses"] = ep.MenyraEPageses;
                    dr["Paguar"] = ep.Paguar;
                    dr["Vlera"] = ep.Vlera;
                    dr["DaljaMallitID"] = ep.DaljaMallitID;
                    dtEkzekutimiPageses.Rows.Add(dr);
                }

                return "OK";
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Gabim gjate ekzekutimit te pageses: " + ex.Message);
                return "Gabim gjate ekzekutimit te pageses";
            }
        }

        public string ShtoFaturen()
        {
            try
            {
                CL.DaljaMallitCL dm = new CL.DaljaMallitCL();
                if (dm != null)
                {
                    DataRow dr = dtDaljaMallit.NewRow();
                    dr["OrganizataId"] = dm.OrganizataId;
                    dr["Viti"] = dm.Viti;
                    dr["Data"] = dm.Data;
                    dr["NrFatures"] = dm.NrFatures;
                    dr["DokumentiId"] = dm.DokumentiId;
                    dr["RegjistruarNga"] = dm.RegjistruarNga;
                    dr["DataERegjistrimit"] = dm.DataERegjistrimit;
                    if (dr["NumriArkes"] != DBNull.Value)
                        dr["NumriArkes"] = dm.NumriArkes;
                    if (dr["SubjektiId"] != DBNull.Value)
                        dr["SubjektiId"] = dm.SubjektiId;
                    dr["ShitjeEPerjashtuar"] = dm.ShitjeEPerjashtuar;
                    dr["Koment"] = dm.Koment;
                    if (dr["Xhirollogari"] != DBNull.Value)
                        dr["Xhirollogari"] = dm.Xhirollogari;
                    dr["Sinkronizuar"] = dm.Sinkronizuar;
                    dr["NeTransfer"] = dm.NeTransfer;
                    dr["DepartamentiId"] = dm.DepartamentiId;
                    if (dr["Validuar"] != DBNull.Value)
                        dr["Validuar"] = dm.Validuar;
                    dr["AfatiPageses"] = dm.AfatiPageses;
                    dr["NrDuditX3"] = dm.NrDuditX3;
                    dr["Kursi"] = dm.Kursi;
                    dr["DaljaMallitImportuarId"] = dm.DaljaMallitImportuarId;
                    if (dr["DaljaMallitKorrektuarId"] != DBNull.Value)
                        dr["DaljaMallitKorrektuarId"] = dm.DaljaMallitKorrektuarId;
                    if (dr["DataFatures"] != DBNull.Value)
                        dr["DataFatures"] = dm.DataFatures;
                    dr["KuponiFiskalShtypur"] = dm.KuponiFiskalShtypur;
                    dr["NumriFaturesManual"] = dm.NumriFaturesManual;
                    dr["IdLokal"] = dm.IdLokal;
                    dr["NumriFaturesManual"] = dm.NumriFaturesManual;
                    if (dr["ValutaId"] != DBNull.Value)
                        dr["ValutaId"] = dm.ValutaId;
                    if (dr["FaturaKomulativeId"] != DBNull.Value)
                        dr["FaturaKomulativeId"] = dm.FaturaKomulativeId;
                    if (dr["TrackingId"] != DBNull.Value)
                        dr["TrackingId"] = dm.TrackingId;
                    dr["MeVete"] = dm.MeVete;
                    dr["ReferencaID"] = dm.ReferencaID;
                    dr["NumriArkes"] = dm.NumriArkes;
                    if (dr["ZbritjeNgaOperatori"] != DBNull.Value)
                        dr["ZbritjeNgaOperatori"] = dm.ZbritjeNgaOperatori;

                    dtDaljaMallit.Rows.Add(dr);
                }

                return "OK";
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Gabim gjate shtimit te fatures: " + ex.Message);
                return "Gabim gjate shtimit te fatures";
            }
        }

        public decimal TotaliFatures
        {
            get
            {
                object sum = dtDaljaMallitDetale.Compute("Sum(VleraMeTvsh)", string.Empty);
                string sumDecimal = Convert.ToDecimal(sum).ToString("0.00");
                return Convert.ToDecimal(sumDecimal);
            }
        }

        public decimal Subtotali
        {
            get
            {
                object sum = dtDaljaMallitDetale.Compute("Sum(VleraPaTvsh)", string.Empty);
                return Convert.ToDecimal(sum);
            }
        }
        public decimal TotaliVleraTvsh
        {
            get
            {
                object sum = dtDaljaMallitDetale.Compute("Sum(VleraTvsh)", string.Empty);
                return Convert.ToDecimal(sum);
            }
        }
        public decimal TotaliPageses
        {
            get
            {
                int tot = 0;
                int.TryParse(dtEkzekutimiPageses.Compute("Sum(Vlera)", "").ToString(), out tot);
                return tot;
            }
        }
        public string Ruaj()
        {
            if (DaljaMallitCL.Id > 0)
            {
                return "Fatura është ruajtur njëherë";
            }

            try
            {
                string ruajtja = DAL.FaturaClass.Ruaj(this);
                if (ruajtja != "OK")
                {
                    Logger.Error("Gabim gjate ruajtjes se fatures", "Gabim gjate ruajtjes se fatures");
                    return "Ruajtja nuk ka perfunduar me sukses!";
                }
                else
                {
                    try
                    {
                        PrintoKuponinFiskal();
                        Logger.Info("Shtypja e kuponit fiskal u perfundua");

                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex, "Gabim gjate ruajtjes se kuponit fiskal ne disk: " + ex.Message);
                    }

                    POSForm grd = new POSForm();
                    grd.grdDaljaMallitDetale.Rows.Clear();
                    grd.grdCash.Rows.Clear();
                    return "OK";
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Gabim gjate ruajtjes se fatures: " + ex.Message);
                return "Gabim gjate ruajtjes se fatures";
            }

        }
        public DataRow GetArtikullin(string shifra)
        {
            return ArtikujtClass.GetArtikullin(shifra);
        }

        public string PrintoKuponinFiskal()
        {
            try
            {
                FiscalPrinterClass.ShtypPermbajtjen(
                    this.DaljaMallitCL.IdLokal,
                    FiscalPrinterClass.MerrTXTFiskal(DaljaMallitCL.Id));

                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public DataTable getArtikujtSkanuar()
        {
            DataTable dtArtikujtSaknuar = new DataTable();

            dtArtikujtSaknuar.Columns.Add("DaljaMallitDetaleId", typeof(int));
            dtArtikujtSaknuar.Columns.Add("ArtikulliId", typeof(int));
            dtArtikujtSaknuar.Columns.Add("Artikulli", typeof(string));
            dtArtikujtSaknuar.Columns.Add("Sasia", typeof(decimal));
            dtArtikujtSaknuar.Columns.Add("QmimiShitjes", typeof(decimal));
            dtArtikujtSaknuar.Columns.Add("Rabati", typeof(decimal));
            dtArtikujtSaknuar.Columns.Add("Komenti", typeof(string));
            dtArtikujtSaknuar.Columns.Add("EkstraRabati", typeof(decimal));

            dtArtikujtSaknuar.Columns["DaljaMallitDetaleId"].AutoIncrement = true;
            dtArtikujtSaknuar.Columns["DaljaMallitDetaleId"].AutoIncrementSeed = -1;
            dtArtikujtSaknuar.Columns["DaljaMallitDetaleId"].AutoIncrementStep = -1;

            dtArtikujtSaknuar.Columns["ArtikulliId"].AutoIncrement = true;
            dtArtikujtSaknuar.Columns["ArtikulliId"].AutoIncrementSeed = -1;
            dtArtikujtSaknuar.Columns["ArtikulliId"].AutoIncrementStep = -1;

            foreach (DataRow dr in dtDaljaMallitDetale.Rows)
            {
                dtArtikujtSaknuar.Rows.Add(dr["Id"], dr["ArtikulliId"],
                    "", dr["Sasia"], dr["QmimiShitjes"], dr["Rabati"], "", dr["EkstraRabati"]);
            }
            return dtArtikujtSaknuar;
        }

        private void KalkuloTotalin()
        {
            //if (dtDaljaMallitDetale.Rows.Count == 0) { return; }
            //TotaliFatures = string.Format("{0:0.00}", Convert.ToDecimal(dtDaljaMallitDetale.Compute("Sum(VleraMeTvsh)", "")));
            //KalkuloMbetjen();
        }

        private void KalkuloMbetjen()
        {
            //decimal qmimiTotal = 0;
            //decimal paguar = 0;
            //decimal.TryParse(lblTotali, out qmimiTotal);
            //decimal.TryParse(lblTotaliIPaguar.Text, out paguar);

            //lblMbetja.Text = string.Format("{0:0.00}", paguar - qmimiTotal);
        }
    }
}

