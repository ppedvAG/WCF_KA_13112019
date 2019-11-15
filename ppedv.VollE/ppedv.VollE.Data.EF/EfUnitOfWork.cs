using ppedv.VollE.Model;
using ppedv.VollE.Model.Contracts;

namespace ppedv.VollE.Data.EF
{
    public class EfUnitOfWork : IUnitOfWork
    {
        EfContext context = new EfContext();

        public ISpielerRepository SpielerRepository => new EfSpielerRepository(context);

        public void Dispose()
        {
            context.Dispose();
        }

        public IRepository<T> GetRepo<T>() where T : Entity
        {
            return new EfRepository<T>(context);
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }
    }
}
