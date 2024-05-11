﻿using Fitness.DataAccess.Data;
using Fitness.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.DataAccess.Repositories
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext _db;
		internal DbSet<T> dbSet;
		public Repository(ApplicationDbContext db)
		{
			_db = db;
			this.dbSet = _db.Set<T>();
		}
		public void Add(T item)
		{
			dbSet.Add(item);
		}

		public T Get(Expression<Func<T, bool>> predicate)
		{
			IQueryable<T> query = dbSet;
			query = query.Where(predicate);
			return query.FirstOrDefault();
		}

		public IEnumerable<T> GetAll()
		{
			IQueryable<T> query = dbSet;
			return query.ToList();
		}

		public void Remove(T item)
		{
			dbSet.Remove(item);
		}
	}
}
