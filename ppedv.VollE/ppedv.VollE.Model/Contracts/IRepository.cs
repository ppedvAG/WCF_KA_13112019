using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ppedv.VollE.Model.Contracts
{

    public interface IRepository<T> where T : Entity
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        IQueryable<T> Query();
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }

}
