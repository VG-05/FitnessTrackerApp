using Fitness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.DataAccess.Repositories.Interfaces
{
    public interface IGoalRepository : IRepository<Goal>
    {
        void Update(Goal goal);
    }
}
