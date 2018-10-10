using System.Collections.Generic;

namespace IMDB.Data.Contracts
{
	public interface IRepository<T> where T : class
	{
		IEnumerable<T> AllButDeleted();
		IEnumerable<T> All();

		void Add(T entity);
		void Delete(T entity);
		void Update(T entity);
		void Save();
	}
}
