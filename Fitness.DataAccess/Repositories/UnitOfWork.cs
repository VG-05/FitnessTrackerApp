using Fitness.DataAccess.Data;
using Fitness.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.DataAccess.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		public IBodyWeightRepository BodyWeight { get; private set; }
		public IGoalRepository Goal { get; private set; }
		private readonly ApplicationDbContext _db;
		public UnitOfWork(ApplicationDbContext db)
		{
			_db = db;
			BodyWeight = new BodyWeightRepository(_db);
			Goal = new GoalRepository(_db);

		}
		public void Save()
		{
			_db.SaveChanges();
		}
	}
}
