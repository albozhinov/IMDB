using System.Collections.Generic;
using System.Linq;

namespace IMDB.Data.Contracts
{
	public interface IRepository<T> where T : class
	{
		IQueryable<T> AllButDeleted();
		IQueryable<T> All();

		void Add(T entity);
		void Delete(T entity);
		void Update(T entity);
		void Save();
	}
}
