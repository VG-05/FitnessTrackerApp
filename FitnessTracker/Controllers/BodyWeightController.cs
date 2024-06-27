using Fitness.DataAccess.Repositories.Interfaces;
using Fitness.Models;
using Fitness.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace FitnessTracker.Controllers
{
	[Authorize]
	public class BodyWeightController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<ApplicationUser> _userManager;
		// GET: BodyWeightController
		public BodyWeightController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
		{
			_unitOfWork = unitOfWork;
			_userManager = userManager;
		}
		public ActionResult Index()
		{
			ApplicationUser? user = _userManager.GetUserAsync(User).Result;
			if (user == null)
			{
				return NotFound();
			}
			IQueryable<BodyWeight> BodyWeights = _unitOfWork.BodyWeight.GetSome(m => m.UserID == user.Id).OrderBy(u => u.Date).AsQueryable();
			return View(BodyWeights);
		}

		// GET: BodyWeightController/Details
		public IActionResult Details()
		{
			ApplicationUser? user = _userManager.GetUserAsync(User).Result;
			if (user == null)
			{
				return NotFound();
			}
			List<BodyWeight> BodyWeights = _unitOfWork.BodyWeight.GetSome(m => m.UserID == user.Id).OrderByDescending(u => u.Date).ToList();
			return View(BodyWeights);
		}

		// GET: BodyWeightController/Create
		public IActionResult Create()
		{
			BodyWeightVM bodyWeightVM = new()
			{
				BodyWeight = new BodyWeight()
				{
					Date = DateTime.Today,
					UserID = _userManager.GetUserAsync(User).Result?.Id
				}
			};
			return View(bodyWeightVM);
		}

		// POST: BodyWeightController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(BodyWeightVM bodyWeightVM)
		{
			if (ModelState.IsValid)
			{
				_unitOfWork.BodyWeight.Add(bodyWeightVM.BodyWeight);
				_unitOfWork.Save();
				return RedirectToAction("Index");
			}
			return View();
		}

		// GET: BodyWeightController/Edit/5
		public IActionResult Edit(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}

			BodyWeight? bodyWeightFromDb = _unitOfWork.BodyWeight.Get(u => u.Id == id);
			if (bodyWeightFromDb == null)
			{
				return NotFound();
			}

			BodyWeightVM bodyWeightVM = new BodyWeightVM()
			{
				BodyWeight = bodyWeightFromDb
			};
			return View(bodyWeightVM);
		}

		// POST: BodyWeightController/Edit
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(BodyWeightVM bodyWeightVM)
		{
			if (ModelState.IsValid)
			{
				_unitOfWork.BodyWeight.Update(bodyWeightVM.BodyWeight);
				_unitOfWork.Save();
				return RedirectToAction("Index");
			}
			return View();
		}

		// GET: BodyWeightController/Delete/5
		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}

			BodyWeight? bodyWeightFromDb = _unitOfWork.BodyWeight.Get(u => u.Id == id);
			if (bodyWeightFromDb == null)
			{
				return NotFound();
			}
			return View(bodyWeightFromDb);
		}

		// POST: BodyWeightController/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeletePOST(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			BodyWeight? bodyWeightFromDb = _unitOfWork.BodyWeight.Get(u => u.Id == id);
			if (bodyWeightFromDb == null)
			{
				return NotFound();
			}
			_unitOfWork.BodyWeight.Remove(bodyWeightFromDb);
			_unitOfWork.Save();
			return RedirectToAction("Index");
		}

		#region APICALLS
		[HttpGet]
		public IActionResult GetAll()
		{
			ApplicationUser? user = _userManager.GetUserAsync(User).Result;
			if (user == null)
			{
				return NotFound();
			}
			List<BodyWeight> bodyWeightList = _unitOfWork.BodyWeight.GetSome(m => m.UserID == user.Id).ToList();
			bodyWeightList.ForEach(bodyWeight => bodyWeight.User = null);
			return Json(new { data = bodyWeightList });
		}
		#endregion
	}
}
