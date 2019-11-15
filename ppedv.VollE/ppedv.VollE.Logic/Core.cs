using ppedv.VollE.Model;
using ppedv.VollE.Model.Contracts;
using System.Linq;

namespace ppedv.VollE.Logic
{
    public class Core
    {
        public IUnitOfWork UnitOfWork { get; private set; }

        public Core(IUnitOfWork uow) //DI in here 
        {
            this.UnitOfWork = uow;
        }

        public Mannschaft GetGewinner(Spiel spiel)
        {
            if (spiel.PunkteGast == spiel.PunkteHeim)
                return null;

            return spiel.PunkteGast > spiel.PunkteHeim ? spiel.GastMannschaft : spiel.HeimMannschaft;
        }

        public Mannschaft GetMannschaftMitMeistenGewinnenOfYear(int year)
        {
            //return UnitOfWork.GetRepo<Mannschaft>().Query().SelectMany().Where(x => x.Datum.Year == year).OrderBy(x =>x.  GetGewinner(x)).FirstOrDefault();
            return UnitOfWork.GetRepo<Mannschaft>().Query().GroupBy(x => x.SpielAlsGast.Union(x.SpielAlsHeim).Where(s => GetGewinner(s) == x)).OrderByDescending(x => x.Key.Count()).FirstOrDefault().FirstOrDefault();
        }

        public Core() : this(new Data.EF.EfUnitOfWork())
        { }

    }
}
