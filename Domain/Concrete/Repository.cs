using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Domain.Context;
using System.Data.Entity;

namespace Domain.Concrete
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private DatabaseContext _context = new DatabaseContext();
		private DbSet<T> _entities;
		public Repository()
		{
			_entities = _context.Set<T>();
		}

		public IQueryable<T> Entities => _entities;

		public void Add(T item)
		{
			_entities.Add(item);
		}

		public void Commit()
		{
			_context.SaveChanges();
		}

		public T Get(Expression<Func<T, bool>> predicate = null)
		{
			if (predicate == null)
				return null;

			return _entities.FirstOrDefault(predicate);
		}

		public T GetById(int? id)
		{
			return _entities.Find(id);
		}

		public IQueryable<T> Query(Expression<Func<T, bool>> predicate = null)
		{
			if (predicate == null)
				return null;
			return _entities.Where(predicate);
		}
	}
}
