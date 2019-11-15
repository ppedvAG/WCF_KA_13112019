using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WebPlaneten.Models
{
    public class Planet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EntfernungZurSonne { get; set; }
        public long Masse { get; set; }
        public int Durchmesser { get; set; }
        public virtual HashSet<Mond> Monde { get; set; } = new HashSet<Mond>();

    }

    public class Mond
    {
        public int Id { get; set; }
        //public virtual Planet Planet { get; set; }
        public string Name { get; set; }
        public int Durchmesser { get; set; }
    }
}
