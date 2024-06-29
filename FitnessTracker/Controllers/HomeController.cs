using Fitness.DataAccess.Repositories.Interfaces;
using Fitness.Models;
using Fitness.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FitnessTracker.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<ApplicationUser> _userManager;
		public HomeController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
		{
			_unitOfWork = unitOfWork;
			_userManager = userManager;
		}

		public IActionResult Index()
		{
			ApplicationUser? user = _userManager.GetUserAsync(User).Result;
			if (user == null)
			{
				return NotFound();
			}
			IEnumerable<BodyWeight> bodyweights = _unitOfWork.BodyWeight.GetSome(m => m.UserID == user.Id).OrderBy(u => u.Date);
			double? bodyweightProgress = null;
            double? overallBodyweightProgress = null;
			BodyWeight? latestWeight = bodyweights.LastOrDefault();
			string unit = latestWeight?.Unit ?? "kgs";

            if (bodyweights.Count() >= 2)
			{
				BodyWeight secondLastWeight = bodyweights.ElementAt(bodyweights.Count() - 2);
				BodyWeight earliestWeight = bodyweights.First();

                if (secondLastWeight.Unit != latestWeight?.Unit)
				{
					if (secondLastWeight.Unit == "kgs")
					{
                        secondLastWeight.Weight *= 2.205;
                        secondLastWeight.Unit = "lbs";
                    }
                    else
                    {
                        secondLastWeight.Weight /= 2.205;
                        secondLastWeight.Unit = "kgs";
                    }
                }

				bodyweightProgress = Double.Round((latestWeight?.Weight ?? 0) - secondLastWeight.Weight, 2);

                if (earliestWeight.Unit != latestWeight?.Unit)
                {
                    if (earliestWeight.Unit == "kgs")
                    {
                        earliestWeight.Weight *= 2.205;
                        earliestWeight.Unit = "lbs";
                    }
                    else
                    {
                        earliestWeight.Weight /= 2.205;
                        earliestWeight.Unit = "kgs";
                    }
                }
				
				overallBodyweightProgress = Double.Round((latestWeight?.Weight ?? 0) - earliestWeight.Weight, 2);

            }
			HomeVM homeVM = new()
			{
				LatestBodyweight = latestWeight?.Weight,
				BodyweightProgress = bodyweightProgress,
				OverallBodyweightProgress = overallBodyweightProgress,
			};
			return View(homeVM);
		}
	}
}
