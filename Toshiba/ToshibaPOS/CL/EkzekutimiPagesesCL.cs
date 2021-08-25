using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToshibaPOS.CL
{
    public class EkzekutimiPagesesCL
    {
        public EkzekutimiPagesesCL(int? _Id = null)
        {
            if(_Id != null)
            {
                MenyratEPagesesCL mp = new MenyratEPagesesCL(_Id);
                MenyraEPagesesId = mp.Id;
                MenyraEPageses = mp.Pershkrimi;
            }
        }
        public int Id { get; set; }
        public int MenyraEPagesesId { get; set; }
        public string MenyraEPageses { get; set; }
        public long DaljaMallitID { get; set; }
        public decimal Vlera { get; set; }
        public decimal Paguar { get; set; }
        public int? ShifraOperatorit { get; set; }

    }
}
