﻿using Fitness.DataAccess.Repositories.Interfaces;
using Fitness.Models;
using Fitness.Models.ViewModels;
using FitnessTracker.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;

namespace FitnessTracker.Controllers
{
    public class MealController : Controller
    {
        private readonly IUSDAFoodService _usdaFoodService;
        private readonly IUnitOfWork _unitOfWork;
        public MealController(IUSDAFoodService usdaFoodService, IUnitOfWork unitOfWork)
        {
            _usdaFoodService = usdaFoodService;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Meal> Meals = _unitOfWork.Meals.GetAll().ToList();
			return View(Meals);
        }

        public IActionResult Details(DayMealVM dayMealVM)
        {
		    List<Meal> dayMeals = _unitOfWork.Meals.GetSome(u => u.Date == dayMealVM.Date).ToList();
			Dictionary<string, List<Meal>> dayMealsDict = dayMeals.GroupBy(o => o.MealTime)
				                                                  .ToDictionary(g => g.Key, g => g.ToList());

            dayMealVM.Breakfast = dayMealsDict.GetValueOrDefault("Breakfast", []);
			dayMealVM.Lunch = dayMealsDict.GetValueOrDefault("Lunch", []);
			dayMealVM.Snacks = dayMealsDict.GetValueOrDefault("Snacks", []);
			dayMealVM.Dinner = dayMealsDict.GetValueOrDefault("Dinner", []);

			return View(dayMealVM);
        }

        [ActionName("Search")]
        public async Task<IActionResult> SearchAsync(MealsVM? mealsVM)
        {
            if (mealsVM==null || mealsVM.SearchString == "")
            {
                return View();
            }
            JObject foodData = await _usdaFoodService.GetFoodDataAsync(mealsVM.SearchString);

			mealsVM.Meals = foodData["foods"].Select(food => new Meal
			{
				Api_Id = (int)food["fdcId"],
				FoodName = (string?)food["description"],
				BrandName = (string?)food["brandName"],
				Calories = (int)food["foodNutrients"].FirstOrDefault(n => n["nutrientName"].Value<string>() == "Energy")["value"]
			}).ToList();
            return View(mealsVM);
        }

        [ActionName("Add")]
        [HttpGet]
        public async Task<IActionResult> AddAsync(int api_id)
        {
            JObject foodItem = await _usdaFoodService.GetFoodDataByIdAsync(api_id);
            if (foodItem != null)
            {
                MealVM mealVM = new()
                {
                    Meal = new Meal
                    {
                        Api_Id = api_id,
                        FoodName = (string?)foodItem["description"],
                        BrandName = (string?)foodItem["brandName"],
                        Calories = (double)foodItem["foodNutrients"].FirstOrDefault(n => n["nutrient"]["name"].Value<string>() == "Energy")["amount"],
                        Carbohydrates = (double)foodItem["foodNutrients"].FirstOrDefault(n => n["nutrient"]["name"].Value<string>() == "Carbohydrate, by difference")["amount"],
                        Protein = (double)foodItem["foodNutrients"].FirstOrDefault(n => n["nutrient"]["name"].Value<string>() == "Protein")["amount"],
                        Fat = (double)foodItem["foodNutrients"].FirstOrDefault(n => n["nutrient"]["name"].Value<string>() == "Total lipid (fat)")["amount"],
                        Date = DateOnly.FromDateTime(DateTime.Now),
                        Servings = 1,
                        ServingSizeAmount = (double?)foodItem["servingSize"],
                        ServingSizeUnit = (string?)foodItem["servingSizeUnit"]
                    }
                };

                return View(mealVM);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Add(MealVM mealVM)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Meals.Add(mealVM.Meal);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(mealVM.Meal.Id);
        }


        public IActionResult Edit(int id)
        {
            Meal? mealToEdit = _unitOfWork.Meals.Get(u => u.Id == id);

            if (mealToEdit == null)
            {
                return NotFound();
            }

			MealVM mealVM = new MealVM
            {
                Meal = mealToEdit
            };
            return View(mealVM);
        }

        [HttpPost]
        public IActionResult Edit(MealVM mealVM)
        {
            if (ModelState.IsValid)
            {
				_unitOfWork.Meals.Update(mealVM.Meal);
                _unitOfWork.Save();
                return RedirectToAction("Index");
			}
            return View(mealVM.Meal.Id);
        }

		public IActionResult Delete(int id)
		{
            Meal? mealToDelete = _unitOfWork.Meals.Get(u => u.Id == id);

            if (mealToDelete==null)
            {
                return NotFound();
            }

			MealVM mealVM = new()
            {
                Meal = mealToDelete
            };
			return View(mealVM);
		}

        [ActionName("Delete")]
		[HttpPost]
		public IActionResult DeletePOST(int id)
		{
			Meal? meal = _unitOfWork.Meals.Get(u => u.Id == id);
			if (meal != null)
			{
				_unitOfWork.Meals.Remove(meal);
                _unitOfWork.Save();
                return RedirectToAction("Index");
			}
            return NotFound();
		}
	}
}
