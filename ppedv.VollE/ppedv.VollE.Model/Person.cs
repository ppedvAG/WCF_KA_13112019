using System;

namespace ppedv.VollE.Model
{
    public abstract class Person : Entity
    {
        public string Name { get; set; }
        public DateTime GebDatum { get; set; }
        public Geschlecht Geschlecht { get; set; }
    }

    public enum Geschlecht
    {
        Divers,
        Weiblich,
        Männlich
    }
}
