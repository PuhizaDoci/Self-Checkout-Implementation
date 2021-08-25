using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToshibaPOS.CL
{
    public class DaljaMallitDetaleCL
    {
        public DaljaMallitDetaleCL(string _Barkodi=null)
        {
            if (_Barkodi != null)
            {
                ArtikulliCL clArt = new ArtikulliCL(_Barkodi);
                if(clArt.Id>0)
                {
                    Shifra = clArt.Shifra;
                    Emertimi = clArt.Pershkrimi;
                    PershkrimiFiskal = clArt.PershkrimiTiketa;
                    ArtikulliId = clArt.Id;
                    NjesiaID = clArt.NjesiaID;
                    Njesia = clArt.Njesia;
                    Sasia = clArt.Sasia;
                    QmimiRregullt = clArt.QmimiIShitjes;
                    QmimiShitjes = clArt.QmimiIShitjes;
                    QmimiShumice = clArt.QmimiShumice;
                    Rabati = clArt.Rabati;
                    EkstraRabati = clArt.EkstraRabati;
                    Tvsh = clArt.TVSH;
                    KaLirim = clArt.KaLirim;
                    ArtikullPaZbritje = clArt.ArtikullPaZbritje;
                    StatusiQmimitId = clArt.StatusiQmimitId;
                }
            }
        }
        public short Nr { get; set; }
        public int Id { get; set; } 
        public string Shifra { get; set; }
        public string Emertimi { get; set; }
        public string ShifraProdhuesit { get; set; }
        public string PershkrimiFiskal { get; set; }
        public string NumriKatallogut { get; set; }
        public long DaljaMallitID { get; set; }
        public int ArtikulliId { get; set; }
        public byte NjesiaID { get; set; }
        public string Njesia { get; set; }
        public int? BrendId { get; set; } 
        public string Brendi { get; set; }
        public string TvshKategoria { get; set; }
        public decimal Sasia { get; set; }
        public decimal QmimiRregullt { get; set; }
        public decimal QmimiShumice { get; set; }
        public decimal QmimiShitjes { get; set; }
        public decimal Rabati { get; set; }
        public decimal EkstraRabati { get; set; }
        public decimal Tvsh { get; set; }
        public decimal? QmimiPaTvsh { get; set; }
        public decimal? QmimiFinal { get; set; }

        public bool? KaLirim { get; set; }
        public bool? NdryshuarNgaShfrytezuesi { get; set; }
        public string LlojiZbritjes { get; set; }
        public bool? ArtikullPaZbritje { get; set; }
        public string NumriSerik { get; set; }
        public DateTime DataESkadences { get; set; }
        public decimal SasiaMaksimale { get; set; }
        public int? ArtikujtEProdhuarId { get; set; }
        public int? HyrjaDetaleId { get; set; }
        public string IdUnikeProdhimHyrje { get; set; }
        public int Paketimi { get; set; }
        public int? DaljaMallitDetaleKthimiId { get; set; }
        public int SasiaPaketave { get; set; }
        public int? StatusiQmimitId { get; set; }
        public decimal Kursi { get; set; }
        public int NumriArkes { get; set; }
        public bool? AplikimiVoucherit { get; set; }
        public DataTable dtDaljaMallitDetale { get; set; }
    }
}
