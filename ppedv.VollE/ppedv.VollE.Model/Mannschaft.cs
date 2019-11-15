using System.Collections.Generic;

namespace ppedv.VollE.Model
{
    public class Mannschaft : Entity
    {
        public string Name { get; set; }

        public virtual Trainer Trainer { get; set; }
        public virtual Spieler Captain { get; set; }
        public virtual ICollection<Spieler> Spieler { get; set; } = new HashSet<Spieler>();
        public virtual ICollection<Spiel> SpielAlsGast { get; set; } = new HashSet<Spiel>();
        public virtual ICollection<Spiel> SpielAlsHeim { get; set; } = new HashSet<Spiel>();

    }
}
