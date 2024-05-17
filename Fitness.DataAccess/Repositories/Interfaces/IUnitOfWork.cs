using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.DataAccess.Repositories.Interfaces
{
	public interface IUnitOfWork
	{
		IBodyWeightRepository BodyWeight { get; }
		IGoalRepository Goal { get; }
		IMealRepository Meals { get; }
		void Save();
	}
}
