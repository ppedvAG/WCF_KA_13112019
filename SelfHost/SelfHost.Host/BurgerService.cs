using SelfHost.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace SelfHost.Host
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class BurgerService : IBurgerService
    {
        List<Burger> burgerDB = new List<Burger>();

        public BurgerService()
        {
            burgerDB.Add(new Burger() { Name = "Hamburger", Ei = false, Patties = 1 });
            burgerDB.Add(new Burger() { Name = "Doppelburger", Ei = false, Patties = 2 });
            burgerDB.Add(new Burger() { Name = "Spezialburger", Ei = true, Patties = 2 });
        }

        public void Add(Burger neu)
        {
            if (burgerDB.Any(x => x.Name == neu.Name))
                throw new FaultException($"Einen Burger mit dem Namen {neu.Name} existiert bereits");

            if (neu.Patties <= 0)
                throw new FaultException<OutOfPattiesException>(new OutOfPattiesException()
                {
                    PattieCount = neu.Patties,
                    BurgerThatIsOutOfPatties = neu
                }, new FaultReason("Nix Fleisch!"));

            burgerDB.Add(neu);

        }

        public IEnumerable<Burger> GetAllBurgers()
        {
            return burgerDB;
        }
    }
}
