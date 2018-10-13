using System.Linq;

namespace IMDB.Data.Repository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> All();

        void Add(T entity);
        void Update(T entity);
        void Save();
    }

}
