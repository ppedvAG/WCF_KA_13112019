using ppedv.VollE.Model;
using ppedv.VollE.Model.Contracts;
using System.Collections.Generic;

namespace ppedv.VollE.Data.EF
{
    public class EfSpielerRepository : EfRepository<Spieler>, ISpielerRepository
    {
        public EfSpielerRepository(EfContext context) : base(context)
        { }

        public IEnumerable<Spieler> GetAlleSpielerThatHavePlayedThisYear()
        {
            return null;
            //todo
            //return context.Spieler.Where(x => x.Mannschaft.Where(y => y.SpielAlsHeim.Union(y.SpielAlsGast).Any(z => z.Datum.Year == DateTime.Now.Year))
            //.SelectMany(s => s.Spieler);
        }
    }
}
