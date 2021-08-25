using System;
using ToshibaPos.SDK;

namespace ToshibaPOS.CL
{
    public class DaljaMallitCL
    {
        public DaljaMallitCL()
        {
            _Data = PublicClass.DataNgaServeri;
            DataERegjistrimit = _Data;
            Viti = _Data.Year;
            DataFatures = _Data;
            DataValidimit = _Data;
            DokumentiId = 20;
            int.TryParse(PublicClass.Arka.NrArkes.ToString(), out int nra);
            IdLokal = _Data.ToString("yyyyMMddHHmmssfff") + PublicClass.OrganizataId.ToString("000")+DokumentiId.ToString("000") + nra.ToString("00");
            Id = -1;
        }
        public long Id { get; set; }
        public int OrganizataId { get; set; } = PublicClass.OrganizataId;
        public int Viti { get; set; } 
        DateTime _Data;
        public DateTime Data { get { return _Data; }set { _Data = value; } }
        public int NrFatures { get; set; }
        public int DokumentiId { get; set; }
        public int RegjistruarNga { get; set; } = PublicClass.OperatoriId;
        public int? NumriArkes { get; set; } = PublicClass.Arka.NrArkes;
        public DateTime DataERegjistrimit { get; set; }  
        public int? SubjektiId { get; set; }
        public bool ShitjeEPerjashtuar { get; set; } = false;
        public string Koment { get; set; } = "Shitje nga TOSHIBA";
        public bool? Xhirollogari { get; set; } = false;
        public bool Sinkronizuar { get; set; } = false;
        public bool NeTransfer { get; set; } = false;
        public int DepartamentiId { get; set; } = 10;
        public bool? Validuar { get; set; } = true;
        public int AfatiPageses { get; set; } = 0;
        public long? DaljaMallitKorrektuarId { get; set; }
        public int? TavolinaId { get; set; }
        public string NrDuditX3 { get; set; }
        public DateTime? DataFatures { get; set; } 
        public decimal Kursi { get; set; } = 1;
        public int? ValutaId { get; set; }
        public bool KuponiFiskalShtypur { get; set; } = false;
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
        public long? BarazimiId { get; set; }
        public long? DaljaMallitIdServer { get; set; }
        public long? ServerId { get; set; }
        public string ReferencaID { get; set; }
        public int? KartelaId { get; set; }
        public bool MeVete { get; set; }
    }
}
