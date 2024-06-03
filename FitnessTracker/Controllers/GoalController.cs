using Fitness.DataAccess.Repositories.Interfaces;
using Fitness.Models;
using Fitness.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace FitnessTracker.Controllers
{
    public class GoalController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        // GET: GoalController
        public GoalController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ActionResult Index()
        {
            List<Goal> Goals = _unitOfWork.Goal.GetAll().ToList();
            return View(Goals);
        }

        // GET: GoalController/Details
        public IActionResult Details()
        {
            List<Goal> Goals = _unitOfWork.Goal.GetAll().ToList();
            return View(Goals);
        }

        // GET: GoalController/Create
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> Units = new SelectListItem[] {
                new SelectListItem { Text = "kgs" , Value = "kgs"},
                new SelectListItem { Text = "lbs" , Value = "lbs"}
            };
            GoalVM goalVM = new GoalVM()
            {
                Goal = new Goal(),
                Units = Units
            };
            return View(goalVM);
        }

        // POST: GoalController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GoalVM goalVM)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Goal.Add(goalVM.Goal);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: GoalController/Edit/5
        public IActionResult Edit(int? id)
        {
            IEnumerable<SelectListItem> Units = new SelectListItem[] {
                new SelectListItem { Text = "kgs" , Value = "kgs"},
                new SelectListItem { Text = "lbs" , Value = "lbs"}
            };
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Goal? goalFromDb = _unitOfWork.Goal.Get(u => u.Id == id);
            if (goalFromDb == null)
            {
                return NotFound();
            }

            GoalVM goalVM = new GoalVM()
            {
                Goal = goalFromDb,
                Units = Units
            };
            return View(goalVM);
        }

        // POST: GoalController/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(GoalVM goalVM)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Goal.Update(goalVM.Goal);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: GoalController/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Goal? goalFromDb = _unitOfWork.Goal.Get(u => u.Id == id);
            if (goalFromDb == null)
            {
                return NotFound();
            }
            return View(goalFromDb);
        }

        // POST: GoalController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Goal? goalFromDb = _unitOfWork.Goal.Get(u => u.Id == id);
            if (goalFromDb == null)
            {
                return NotFound();
            }
            _unitOfWork.Goal.Remove(goalFromDb);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        #region APICALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Goal> goalList = _unitOfWork.Goal.GetAll().ToList();
            return Json(new { data = goalList });
        }
        #endregion
    }
}