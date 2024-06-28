﻿using Fitness.DataAccess.Repositories.Interfaces;
using Fitness.Models;
using Fitness.Models.ViewModels;
using FitnessTracker.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace FitnessTracker.Controllers
{
	[Authorize]
	public class GoalController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
		private readonly IFitnessCalculatorService _fitnessCalculatorService;
		private readonly UserManager<ApplicationUser> _userManager;
		// GET: GoalController
		public GoalController(IUnitOfWork unitOfWork, IFitnessCalculatorService fitnessCalculatorService, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _fitnessCalculatorService = fitnessCalculatorService;
            _userManager = userManager;
		}
        public ActionResult Index()
        {
			ApplicationUser? user = _userManager.GetUserAsync(User).Result;
			if (user == null)
			{
				return NotFound();
			}
			List<Goal> Goals = _unitOfWork.Goal.GetSome(m => m.UserID == user.Id).ToList();
            return View(Goals);
        }

        // GET: GoalController/Details
        public IActionResult Details()
        {
			ApplicationUser? user = _userManager.GetUserAsync(User).Result;
			if (user == null)
			{
				return NotFound();
			}
			List<Goal> Goals = _unitOfWork.Goal.GetSome(m => m.UserID == user.Id).OrderByDescending(u => u.TargetDate).ToList();
            return View(Goals);
        }

        // GET: GoalController/Create
        public async Task<IActionResult> Create()
        {
			ApplicationUser? _currentUser = _userManager.GetUserAsync(User).Result;
			if (_currentUser == null)
			{
				return NotFound();
			}
			BodyWeight? latestBodyWeight = _unitOfWork.BodyWeight.GetSome(m => m.UserID == _currentUser.Id).OrderByDescending(u => u.Date).FirstOrDefault();
            GoalVM goalVM = new()
            {
                Goal = new Goal()
                {
					UserID = _userManager.GetUserAsync(User).Result?.Id,
                    TargetDate = DateTime.Today
				}
            };
			if (latestBodyWeight != null)
			{
                int bodyweight = (int)latestBodyWeight.Weight;

				if (latestBodyWeight.Unit == "lbs")
                {
					bodyweight = (int)(latestBodyWeight.Weight / 2.205);       // lbs to kgs
				}

				JObject? data = await _fitnessCalculatorService.GetNutritionInfoAsync(_currentUser.Age, _currentUser.Gender, _currentUser.Height, bodyweight, _currentUser.ActivityLevel);
				if (data == null)
				{
					return NotFound();
				}
				goalVM.CaloriesDesc = (string?)data.SelectToken("BMI_EER.['Estimated Daily Caloric Needs']");
				goalVM.CarbsDesc = (string?)data.SelectToken("macronutrients_table.macronutrients-table[1][1]");
				goalVM.ProteinDesc = (string?)data.SelectToken("macronutrients_table.macronutrients-table[3][1]");
				goalVM.FatsDesc = (string?)data.SelectToken("macronutrients_table.macronutrients-table[4][1]");
			}
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
                TempData["success"] = "Goal added successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: GoalController/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            GoalVM goalVM = new();
			ApplicationUser? _currentUser = _userManager.GetUserAsync(User).Result;
            if (_currentUser == null)
            {
                return NotFound();
            }
			BodyWeight? latestBodyWeight = _unitOfWork.BodyWeight.GetSome(m => m.UserID == _currentUser.Id).OrderByDescending(u => u.Date).FirstOrDefault();
			if (latestBodyWeight != null)
			{
				int bodyweight = (int)latestBodyWeight.Weight;

				if (latestBodyWeight.Unit == "lbs")
				{
					bodyweight = (int)(latestBodyWeight.Weight / 2.205);       // lbs to kgs
				}

				JObject? data = await _fitnessCalculatorService.GetNutritionInfoAsync(_currentUser.Age, _currentUser.Gender, _currentUser.Height, bodyweight, _currentUser.ActivityLevel);
				if (data == null)
				{
					return NotFound();
				}
				goalVM.CaloriesDesc = (string?)data.SelectToken("BMI_EER.['Estimated Daily Caloric Needs']");
				goalVM.CarbsDesc = (string?)data.SelectToken("macronutrients_table.macronutrients-table[1][1]");
				goalVM.ProteinDesc = (string?)data.SelectToken("macronutrients_table.macronutrients-table[3][1]");
				goalVM.FatsDesc = (string?)data.SelectToken("macronutrients_table.macronutrients-table[4][1]");
			}

            goalVM.Goal = goalFromDb;
            
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
                TempData["success"] = "Goal updated successfully";
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
            TempData["success"] = "Goal deleted successfully";
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
			List<Goal> goalList = _unitOfWork.Goal.GetSome(m => m.UserID == user.Id).OrderBy(u => u.TargetDate).ToList();
            goalList.ForEach(goal => goal.User = null);
            return Json(new { data = goalList });
        }
        #endregion
    }
}