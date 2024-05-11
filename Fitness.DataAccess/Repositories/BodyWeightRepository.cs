using Fitness.DataAccess.Data;
using Fitness.DataAccess.Repositories.Interfaces;
using Fitness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.DataAccess.Repositories
{
	public class BodyWeightRepository : Repository<BodyWeight>, IBodyWeightRepository
	{
		private readonly ApplicationDbContext _db;

		public BodyWeightRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public void Update(BodyWeight bodyWeight)
		{
			_db.BodyWeights.Update(bodyWeight);
		}
	}
}