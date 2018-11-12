using IMDB.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

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
		public async Task AddAsync(T entity)
		{
			EntityEntry entry = context.Entry(entity);

			if (entry.State != EntityState.Detached)
			{
				entry.State = EntityState.Added;
			}
			else
			{
				await context.Set<T>().AddAsync(entity);
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
		public async Task SaveAsync()
		{
			await context.SaveChangesAsync();
		}

		void IRepository<T>.Add(T entity)
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


		void IRepository<T>.Save()
		{
			context.SaveChanges();
		}
	}
}
