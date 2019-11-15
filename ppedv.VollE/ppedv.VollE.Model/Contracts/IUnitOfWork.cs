using System;

namespace ppedv.VollE.Model.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        ISpielerRepository SpielerRepository { get; }
        IRepository<T> GetRepo<T>() where T : Entity;

        int SaveChanges();
    }

}
