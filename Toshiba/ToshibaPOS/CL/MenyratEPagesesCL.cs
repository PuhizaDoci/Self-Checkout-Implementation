using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToshibaPOS.DAL;

namespace ToshibaPOS.CL
{
    public class MenyratEPagesesCL
    {
        public MenyratEPagesesCL(int? _Id = null)
        {
            if(_Id != null)
            {

                MenyratEPagesesClass.GetMenyratEPageses(dt, _Id);
                if(dt.Rows.Count>0)
                {
                    Id = Convert.ToInt16(dt.Rows[0]["Id"]);
                    Shkurtesa = dt.Rows[0]["Shkurtesa"].ToString();
                    Pershkrimi = dt.Rows[0]["Pershkrimi"].ToString();
                    //if (dt.Rows[0]["Provizioni"].ToString() != "")
                    //    Provizioni = Convert.ToDecimal(dt.Rows[0]["Provizioni"]);
                    //Tipi = dt.Rows[0]["Tipi"].ToString();
                    if (dt.Rows[0]["Renditja"].ToString() != "")
                        Renditja = Convert.ToInt32(dt.Rows[0]["Renditja"]);
                    PershkrimiAnglisht = dt.Rows[0]["PershkrimiAnglisht"].ToString();
                    //DataERegjistrimit = Convert.ToDateTime(dt.Rows[0]["DataERegjistrimit"]);
                    ParaqitetNePos = Convert.ToBoolean(dt.Rows[0]["ParaqitetNePos"]);
                }
            }
        }
        public int Id { get; set; }
        public string Shkurtesa { get; set; }
        public string Pershkrimi { get; set; }
        public decimal? Provizioni { get; set; }
        public string Tipi { get; set; }
        public int? Renditja { get; set; }
        public string PershkrimiAnglisht { get; set; }
        public DateTime DataERegjistrimit { get; set; }
        public bool ParaqitetNePos { get; set; }
        DataTable dt { get; set; } = new DataTable();
    }
}
