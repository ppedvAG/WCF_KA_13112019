using System;
using System.Collections.Generic;
using System.Linq;

namespace WCF_REST_Flugzeuge
{
    public class FlugzeugService : IFlugzeugeService
    {
        static List<Flugzeug> db = new List<Flugzeug>();

        static FlugzeugService()
        {
            db.Add(new Flugzeug() { Id = 1, Baujahr = DateTime.Now.AddYears(-20), AnzahlPassagiere = 660, Gewicht = 176, Modell = "Boing 747" });
            db.Add(new Flugzeug() { Id = 2, Baujahr = DateTime.Now.AddYears(-40), AnzahlPassagiere = 12, Gewicht = 6, Modell = "Antonov An-3" });
        }

        public void Add(Flugzeug f)
        {
            f.Id = db.Max(x => x.Id) + 1;
            db.Add(f);
        }

        public void Delete(int id)
        {
            db.Remove(GetbyId(id));
        }

        public IEnumerable<Flugzeug> GetAll()
        {
            return db;
        }

        public Flugzeug GetbyId(int id)
        {
            return db.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Flugzeug f)
        {
            Delete(f.Id);
            db.Add(f);
        }
    }
}
