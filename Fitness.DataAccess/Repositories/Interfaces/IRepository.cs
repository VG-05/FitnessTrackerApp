﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.DataAccess.Repositories.Interfaces
{
	public interface IRepository<T> where T : class
	{
		IEnumerable<T> GetAll();
		T? Get(Expression<Func<T, bool>> predicate);
		IEnumerable<T> GetSome(Expression<Func<T, bool>> predicate);
		void Add(T item);
		void Remove(T item);
	}
}
