using ppedv.VollE.Model;
using ppedv.VollE.Model.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ppedv.VollE.Data.EF
{

    public class EfRepository<T> : IRepository<T> where T : Entity
    {
        protected EfContext context;

        public EfRepository(EfContext context)
        {
            this.context = context;
        }

        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);

        }

        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return context.Set<T>().Find(id);

        }

        public IQueryable<T> Query()
        {
            return context.Set<T>();

        }

        public void Update(T entity)
        {
            var loaded = GetById(entity.Id);
            if (loaded != null)
                context.Entry(loaded).CurrentValues.SetValues(entity);
        }
    }
}
