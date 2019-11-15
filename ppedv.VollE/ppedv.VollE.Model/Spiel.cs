using System;

namespace ppedv.VollE.Model
{
    public class Spiel : Entity
    {
        public DateTime Datum { get; set; }
        public string Ort { get; set; }
        public virtual Mannschaft HeimMannschaft { get; set; }
        public virtual Mannschaft GastMannschaft { get; set; }
        public int PunkteHeim { get; set; }
        public int PunkteGast { get; set; }
    }
}
