using IMDB.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;

namespace IMDB.Data.Repository
{

    public class Repository<T> : IRepository<T>
        where T : class
    {
        private readonly IMDBContext context;

        public Repository(IMDBContext context)
        {
            this.context = context;
        }
        public IQueryable<T> All()
        {
            return context.Set<T>();
        }
        public void Add(T entity)
        {
            EntityEntry entry = context.Entry(entity);

            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                context.Set<T>().Add(entity);
            }
        }
        public void Update(T entity)
        {
            EntityEntry entry = context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                context.Set<T>().Attach(entity);
            }

            entry.State = EntityState.Modified;
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
