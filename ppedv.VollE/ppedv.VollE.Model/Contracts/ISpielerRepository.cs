using System.Collections.Generic;

namespace ppedv.VollE.Model.Contracts
{
    public interface ISpielerRepository : IRepository<Spieler>
    {
        IEnumerable<Spieler> GetAlleSpielerThatHavePlayedThisYear();
    }

}
