using System.Collections.Generic;
using System.ServiceModel;

namespace SelfHost.Contracts
{
    [ServiceContract]
    public interface IBurgerService
    {
        [OperationContract]
        IEnumerable<Burger> GetAllBurgers();

        [OperationContract]
        [FaultContract(typeof(OutOfPattiesException))]
        void Add(Burger neu);
    }
}
