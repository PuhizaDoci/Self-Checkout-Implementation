
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToshibaPosSinkronizimi
{
    [Serializable]
    public class SubjektetCL
    {
        public SubjektetCL(int _SubjektiId)
        {
            if (_SubjektiId != 0)
                SubjektiId = _SubjektiId;
        }
        public int? Id { get; set; } = null;
        public int SubjektiId
        {
            set
            {
                int.TryParse(value.ToString(), out int _subjektiId);
                if (_subjektiId == 0)
                    return;

                DataTable dt = RaportetClass.GetSubjektet(new DataTable(), _subjektiId);
                if (dt.Rows.Count < 1)
                    return;

                DataRow[] drSubjeti = dt.Select("Id=" + _subjektiId.ToString());
                DataRow row = drSubjeti.FirstOrDefault();
                if (row != null)
                {
                    Id = Convert.ToInt32(row["Id"].ToString());
                    Shifra = row["Shifra"].ToString();
                    Pershkrimi = row["Pershkrimi"].ToString();
                    NRB = row["NRB"].ToString();
                    NrTVSH = row["NrTVSH"].ToString();
                    NumriFiskal = row["NumriFiskal"].ToString();
                    Email = row["Email"].ToString();
                    Telefoni = row["Telefoni"].ToString();
                    Fax = row["Fax"].ToString();
                    Teleporosia = row["Teleporosia"].ToString();
                    Adresa = row["Adresa"].ToString();
                    if (row["VendiId"].ToString() != "")
                        VendiId = Convert.ToInt32(row["VendiId"].ToString());
                    Vendi = row["Vendi"].ToString();
                    NumriPostar = row["NumriPostar"].ToString();
                    Shteti = row["Shteti"].ToString();
                    Komuna = row["Komuna"].ToString();
                    PersoniKontaktues = row["PersoniKontaktues"].ToString();
                    LlojiISubjektitID = Convert.ToInt32(row["LlojiISubjektitID"].ToString());
                    LlojiISubjektit = row["LlojiISubjektit"].ToString();
                    Koment = row["Koment"].ToString();
                    DataERegjistrimit = Convert.ToDateTime(row["DataERegjistrimit"].ToString());
                    RegjistruarNga = Convert.ToInt32(row["RegjistruarNga"].ToString());
                    PranimiFaturaveEmail = row["PranimiFaturaveEmail"].ToString();
                    FinancatEmail = row["FinancatEmail"].ToString();
                    NrLicenses = row["NrLicenses"].ToString();
                    if (row["DataESkadences"].ToString() != "")
                        DataESkadences = Convert.ToDateTime(row["DataESkadences"].ToString());
                    AfatiPageses = Convert.ToByte(row["AfatiPageses"].ToString());
                    AfatiPagesesShitje = Convert.ToByte(row["AfatiPagesesShitje"].ToString());
                    if (row["Statusi"].ToString() != "")
                        Statusi = Convert.ToBoolean(row["Statusi"].ToString());
                    if (row["MenaxhuesiSubjektitId"].ToString() != "")
                        MenaxhuesiSubjektitId = Convert.ToInt32(row["MenaxhuesiSubjektitId"].ToString());
                    MenaxheriPershrimi = row["MenaxheriPershrimi"].ToString();
                    MenaxheriEmail = row["MenaxheriEmail"].ToString();
                    MenaxheriSubjektitTel = row["MenaxheriSubjektitTel"].ToString();
                    SubjektiEshteKompani = Convert.ToBoolean(row["SubjektiEshteKompani"].ToString());
                    #region Koment Koeficientat
                    //if (row["K16"].ToString() != "")
                    //    Subjekti.K16 = Convert.ToInt16(row["K16"].ToString());
                    //Subjekti.PershkrimiK16 = row["PershkrimiK16"].ToString();
                    //if (row["K17"].ToString() != "")
                    //    Subjekti.K17 = Convert.ToInt16(row["K17"].ToString());
                    //Subjekti.PershkrimiK17 = row["PershkrimiK17"].ToString();
                    //if (row["K18"].ToString() != "")
                    //    Subjekti.K18 = Convert.ToInt16(row["K18"].ToString());
                    //Subjekti.PershkrimiK18 = row["PershkrimiK18"].ToString();
                    //if (row["K19"].ToString() != "")
                    //    Subjekti.K19 = Convert.ToInt16(row["K19"].ToString());
                    //Subjekti.PershkrimiK19 = row["PershkrimiK19"].ToString();
                    //if (row["K20"].ToString() != "")
                    //    Subjekti.K20 = Convert.ToInt16(row["K20"].ToString());
                    //Subjekti.PershkrimiK20 = row["PershkrimiK20"].ToString();
                    #endregion
                    if (row["MenyraEPagesesId"].ToString() != "")
                        MenyraEPagesesId = Convert.ToInt32(row["MenyraEPagesesId"].ToString());
                    MenyraEPageses = row["MenyraEPageses"].ToString();
                    PershkrimiShkurter = row["PershkrimiShkurter"].ToString();
                    LlojiISubjektitShkurtesa = row["Shkurtesa"].ToString();
                    KushtetEDergeses = row["KushtetEDergeses"].ToString();
                    ReferencaEKontrates = row["ReferencaEKontrates"].ToString();
                    LimitiSasi = Convert.ToDecimal(row["LimitiSasi"].ToString());
                    LimitiVlere = Convert.ToDecimal(row["LimitiVlere"].ToString());
                    VleraEShpenzuar = row["VleraEShpenzuar"].ToString() != "" ? Convert.ToDecimal(row["VleraEShpenzuar"].ToString()) : 0;
                }
            }
        }
        public decimal VleraEShpenzuar { get; set; }
        public string Shifra
        {
            get;
            set;
        }
        public string Pershkrimi { get; set; }
        public string NRB { get; set; }
        public string NrTVSH { get; set; }
        string _NumriFiskal;
        public string NumriFiskal
        {
            get
            {
                return _NumriFiskal;
            }
            set
            {
                

                _NumriFiskal = value;
                long.TryParse(value.ToString(), out long _nrf);
                if (_nrf == 0)
                    return;

                DataTable dt = RaportetClass.GetSubjektet(new DataTable(), NrFiskal: _NumriFiskal);
                if (dt.Rows.Count < 1)
                    return;
 
                    Id = Convert.ToInt32(dt.Rows[0]["Id"].ToString());
                    Shifra = dt.Rows[0]["Shifra"].ToString();
                    Pershkrimi = dt.Rows[0]["Pershkrimi"].ToString();
                    NRB = dt.Rows[0]["NRB"].ToString();
                    NrTVSH = dt.Rows[0]["NrTVSH"].ToString();
                    _NumriFiskal = dt.Rows[0]["NumriFiskal"].ToString();
                    Email = dt.Rows[0]["Email"].ToString();
                    Telefoni = dt.Rows[0]["Telefoni"].ToString();
                    Fax = dt.Rows[0]["Fax"].ToString();
                    Teleporosia = dt.Rows[0]["Teleporosia"].ToString();
                    Adresa = dt.Rows[0]["Adresa"].ToString();
                    if (dt.Rows[0]["VendiId"].ToString() != "")
                        VendiId = Convert.ToInt32(dt.Rows[0]["VendiId"].ToString());
                    Vendi = dt.Rows[0]["Vendi"].ToString();
                    NumriPostar = dt.Rows[0]["NumriPostar"].ToString();
                    Shteti = dt.Rows[0]["Shteti"].ToString();
                    Komuna = dt.Rows[0]["Komuna"].ToString();
                    PersoniKontaktues = dt.Rows[0]["PersoniKontaktues"].ToString();
                    LlojiISubjektitID = Convert.ToInt32(dt.Rows[0]["LlojiISubjektitID"].ToString());
                    LlojiISubjektit = dt.Rows[0]["LlojiISubjektit"].ToString();
                    Koment = dt.Rows[0]["Koment"].ToString();
                    DataERegjistrimit = Convert.ToDateTime(dt.Rows[0]["DataERegjistrimit"].ToString());
                    RegjistruarNga = Convert.ToInt32(dt.Rows[0]["RegjistruarNga"].ToString());
                    PranimiFaturaveEmail = dt.Rows[0]["PranimiFaturaveEmail"].ToString();
                    FinancatEmail = dt.Rows[0]["FinancatEmail"].ToString();
                    NrLicenses = dt.Rows[0]["NrLicenses"].ToString();
                    if (dt.Rows[0]["DataESkadences"].ToString() != "")
                        DataESkadences = Convert.ToDateTime(dt.Rows[0]["DataESkadences"].ToString());
                    AfatiPageses = Convert.ToByte(dt.Rows[0]["AfatiPageses"].ToString());
                    AfatiPagesesShitje = Convert.ToByte(dt.Rows[0]["AfatiPagesesShitje"].ToString());
                    if (dt.Rows[0]["Statusi"].ToString() != "")
                        Statusi = Convert.ToBoolean(dt.Rows[0]["Statusi"].ToString());
                    if (dt.Rows[0]["MenaxhuesiSubjektitId"].ToString() != "")
                        MenaxhuesiSubjektitId = Convert.ToInt32(dt.Rows[0]["MenaxhuesiSubjektitId"].ToString());
                    MenaxheriPershrimi = dt.Rows[0]["MenaxheriPershrimi"].ToString();
                    MenaxheriEmail = dt.Rows[0]["MenaxheriEmail"].ToString();
                    MenaxheriSubjektitTel = dt.Rows[0]["MenaxheriSubjektitTel"].ToString();
                    SubjektiEshteKompani = Convert.ToBoolean(dt.Rows[0]["SubjektiEshteKompani"].ToString());
                    #region Koment Koeficientat
                    //if (dt.Rows[0]["K16"].ToString() != "")
                    //    Subjekti.K16 = Convert.ToInt16(dt.Rows[0]["K16"].ToString());
                    //Subjekti.PershkrimiK16 = dt.Rows[0]["PershkrimiK16"].ToString();
                    //if (dt.Rows[0]["K17"].ToString() != "")
                    //    Subjekti.K17 = Convert.ToInt16(dt.Rows[0]["K17"].ToString());
                    //Subjekti.PershkrimiK17 = dt.Rows[0]["PershkrimiK17"].ToString();
                    //if (dt.Rows[0]["K18"].ToString() != "")
                    //    Subjekti.K18 = Convert.ToInt16(dt.Rows[0]["K18"].ToString());
                    //Subjekti.PershkrimiK18 = dt.Rows[0]["PershkrimiK18"].ToString();
                    //if (dt.Rows[0]["K19"].ToString() != "")
                    //    Subjekti.K19 = Convert.ToInt16(dt.Rows[0]["K19"].ToString());
                    //Subjekti.PershkrimiK19 = dt.Rows[0]["PershkrimiK19"].ToString();
                    //if (dt.Rows[0]["K20"].ToString() != "")
                    //    Subjekti.K20 = Convert.ToInt16(dt.Rows[0]["K20"].ToString());
                    //Subjekti.PershkrimiK20 = dt.Rows[0]["PershkrimiK20"].ToString();
                    #endregion
                    if (dt.Rows[0]["MenyraEPagesesId"].ToString() != "")
                        MenyraEPagesesId = Convert.ToInt32(dt.Rows[0]["MenyraEPagesesId"].ToString());
                    MenyraEPageses = dt.Rows[0]["MenyraEPageses"].ToString();
                    PershkrimiShkurter = dt.Rows[0]["PershkrimiShkurter"].ToString();
                    LlojiISubjektitShkurtesa = dt.Rows[0]["Shkurtesa"].ToString();
                    KushtetEDergeses = dt.Rows[0]["KushtetEDergeses"].ToString();
                    ReferencaEKontrates = dt.Rows[0]["ReferencaEKontrates"].ToString();
                    LimitiSasi = Convert.ToDecimal(dt.Rows[0]["LimitiSasi"].ToString());
                    LimitiVlere = Convert.ToDecimal(dt.Rows[0]["LimitiVlere"].ToString());
                    VleraEShpenzuar = dt.Rows[0]["VleraEShpenzuar"].ToString() != "" ? Convert.ToDecimal(dt.Rows[0]["VleraEShpenzuar"].ToString()) : 0;
 
            }
        }
        public string Email { get; set; }
        public string Telefoni { get; set; }
        public string Fax { get; set; }
        public string Teleporosia { get; set; }
        public string Adresa { get; set; }
        public int? VendiId { get; set; }
        public string Vendi { get; set; }
        public string NumriPostar { get; set; }
        public string Shteti { get; set; }
        public string Komuna { get; set; }
        public string PersoniKontaktues { get; set; }
        public int LlojiISubjektitID { get; set; }
        public string LlojiISubjektit { get; set; }
        public string Koment { get; set; }
        public DateTime DataERegjistrimit { get; set; }
        public int RegjistruarNga { get; set; }
        public string PranimiFaturaveEmail { get; set; }
        public string FinancatEmail { get; set; }
        public string NrLicenses { get; set; }
        public DateTime? DataESkadences { get; set; }
        public byte AfatiPageses { get; set; }
        public byte AfatiPagesesShitje { get; set; }
        public bool? Statusi { get; set; }
        public int? MenaxhuesiSubjektitId { get; set; }
        public string MenaxheriPershrimi { get; set; }
        public string MenaxheriEmail { get; set; }
        public string MenaxheriSubjektitTel { get; set; }
        public bool? SubjektiEshteKompani { get; set; }
        public int? K16 { get; set; }
        public string PershkrimiK16 { get; set; }
        public int? K17 { get; set; }
        public string PershkrimiK17 { get; set; }
        public int? K18 { get; set; }
        public string PershkrimiK18 { get; set; }
        public int? K19 { get; set; }
        public string PershkrimiK19 { get; set; }
        public int? K20 { get; set; }
        public string PershkrimiK20 { get; set; }
        public int? MenyraEPagesesId { get; set; }
        public string MenyraEPageses { get; set; }
        public string PershkrimiShkurter { get; set; }
        public string LlojiISubjektitShkurtesa { get; set; }
        public string KushtetEDergeses { get; set; }
        public string ReferencaEKontrates { get; set; }
        public decimal LimitiVlere { get; set; }
        public decimal LimitiSasi { get; set; }
    }
}
