using System;
using System.Data;

namespace ToshibaPOS.CL
{
    public class ArtikulliCL
    {
        public ArtikulliCL(string _Barkodi = null)
        {
            if (_Barkodi != null)
                Barkodi = _Barkodi;
        }
        public int Id { get; set; }
        public string Shifra { get; set; }
        public string Pershkrimi { get; set; }
        public string PershkrimiTiketa { get; set; }
        public byte NjesiaID { get; set; }
        public string Njesia { get; set; }
        public decimal Sasia { get; set; } = 1;
        public decimal TVSH { get; set; }
        public decimal QmimiIShitjes { get; set; }
        public decimal QmimiFinalPakice { get; set; }
        public decimal QmimiShumice { get; set; }
        public decimal QmimiFinalShumice { get; set; }
        decimal _Rabati;
        public decimal Rabati
        {
            get { return _Rabati; }
            set
            {
                _Rabati = value;
                LlogaritVlerat();
            }
        }

        decimal _EkstraRabati;
        public decimal EkstraRabati
        {
            get { return _EkstraRabati; }
            set
            {
                _EkstraRabati = value;
                LlogaritVlerat();
            }
        }
        public decimal Vlera { get; set; }
        public decimal Stoku { get; set; }
        public decimal Paketimi { get; set; }
        public bool KaLirim { get; set; } = false;
        public bool ArtikullPaZbritje { get; set; } = true;
        public int StatusiQmimitId { get; set; } = 10;

        string _Barkodi;
        private string Barkodi
        {
            get { return _Barkodi; }
            set
            {
                _Barkodi = value;
                DataRow dr = DAL.ArtikujtClass.GetArtikullin(_Barkodi);
                if (dr != null)
                {
                    Id = Convert.ToInt32(dr["Id"]);
                    Shifra = dr["Shifra"].ToString();
                    Pershkrimi = dr["Pershkrimi"].ToString();
                    PershkrimiTiketa = dr["PershkrimiTiketa"].ToString();
                    NjesiaID = Convert.ToByte(dr["NjesiaID"]);
                    Njesia = dr["Njesia"].ToString();

                    if (dr["Sasia"].ToString() != "")
                        Sasia = Convert.ToDecimal(dr["Sasia"]);
                    else
                        Sasia = 1;

                    TVSH = Convert.ToDecimal(dr["TVSH"]);

                    QmimiIShitjes = Convert.ToDecimal(dr["QmimiIShitjes"]); 

                    decimal.TryParse(dr["QmimiIShitjes"].ToString(), out decimal _QmimiShumice);
                    if (_QmimiShumice > 0)
                        QmimiShumice = Convert.ToDecimal(dr["QmimiShumice"]);
                    else
                        QmimiShumice = QmimiIShitjes;


 
                    Stoku = Convert.ToDecimal(dr["Stoku"]);
                    Paketimi = Convert.ToDecimal(dr["Paketimi"]);
                    if (dr["KaLirim"].ToString() != "")
                        KaLirim = Convert.ToBoolean(dr["KaLirim"]);
                    if (dr["ArtikullPaZbritje"].ToString() != "")
                        ArtikullPaZbritje = Convert.ToBoolean(dr["ArtikullPaZbritje"]);
                    if (dr["StatusiQmimitId"].ToString() != "")
                        StatusiQmimitId = Convert.ToByte(dr["StatusiQmimitId"]);

                    LlogaritVlerat();
                }
            }
        }
        private void LlogaritVlerat()
        {
            QmimiFinalPakice = (QmimiIShitjes * (1 - Rabati / 100)) * (1 - EkstraRabati / 100);
            QmimiShumice = (QmimiIShitjes * (1 - Rabati / 100)) * (1 - EkstraRabati / 100);
            Vlera = Sasia * QmimiFinalPakice;
        }
    }
}
