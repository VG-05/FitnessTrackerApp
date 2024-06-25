using Fitness.DataAccess.Data;
using Fitness.DataAccess.Repositories.Interfaces;
using Fitness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.DataAccess.Repositories
{
	public class MealRepository: Repository<Meal>, IMealRepository
	{
		private readonly ApplicationDbContext _db;
		public MealRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public void Update(Meal meal)
		{
			_db.Meals.Update(meal);
		}
	}
}
