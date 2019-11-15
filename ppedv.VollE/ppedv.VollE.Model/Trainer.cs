using System.Collections.Generic;

namespace ppedv.VollE.Model
{
    public class Trainer : Person
    {
        public Trainerlizenzstufe Trainerlizenzstufe { get; set; }
        public virtual ICollection<Mannschaft> Mannschaft { get; set; } = new HashSet<Mannschaft>();
    }

    public enum Trainerlizenzstufe
    {
        A,
        B,
        C,
        Diplom
    }
}
