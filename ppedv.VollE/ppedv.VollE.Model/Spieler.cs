using System.Collections.Generic;

namespace ppedv.VollE.Model
{
    public class Spieler : Person
    {
        public string Position { get; set; }
        public bool Händigkeit { get; set; }
        public virtual ICollection<Mannschaft> Mannschaft { get; set; } = new HashSet<Mannschaft>();
        public virtual ICollection<Mannschaft> AlsCaptain { get; set; } = new HashSet<Mannschaft>();
    }
}
