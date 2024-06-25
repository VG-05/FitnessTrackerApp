using Fitness.DataAccess.Repositories.Interfaces;
using Fitness.Models;
using Fitness.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace FitnessTracker.Controllers
{
	public class BodyWeightController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		// GET: BodyWeightController
		public BodyWeightController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public ActionResult Index()
		{
			List<BodyWeight> BodyWeights = _unitOfWork.BodyWeight.GetAll().ToList();
			return View(BodyWeights);
		}

		// GET: BodyWeightController/Details
		public IActionResult Details()
		{
			List<BodyWeight> BodyWeights = _unitOfWork.BodyWeight.GetAll().ToList();
			return View(BodyWeights);
		}

		// GET: BodyWeightController/Create
		public IActionResult Create()
		{
			BodyWeightVM bodyWeightVM = new()
			{
				BodyWeight = new BodyWeight()
				{
					Date = DateTime.Today
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
			List<BodyWeight> bodyWeightList = _unitOfWork.BodyWeight.GetAll().ToList();
			return Json(new { data = bodyWeightList });
		}
		#endregion
	}
}
