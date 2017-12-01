using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
	public interface IRepository<T> where T:class
	{
		IQueryable<T> Entities { get; }

		IQueryable<T> Query(Expression<Func<T, bool>> predicate);

		T Get(Expression<Func<T, bool>> predicate);

		T GetById(int? id);

		void Add(T item);

		void Commit();
	}
}
