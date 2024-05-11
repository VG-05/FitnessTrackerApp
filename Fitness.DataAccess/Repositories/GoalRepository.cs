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
    public class GoalRepository : Repository<Goal>, IGoalRepository
    {
        private readonly ApplicationDbContext _db;
        public GoalRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Goal goal)
        {
            _db.Goal.Update(goal);
        }
    }
}
