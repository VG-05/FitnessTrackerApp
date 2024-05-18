using Fitness.DataAccess.Repositories.Interfaces;
using Fitness.Models;
using Fitness.Models.ViewModels;
using FitnessTracker.Services;
using FitnessTracker.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace FitnessTracker.Controllers
{
    public class MealsController : Controller
    {
        private readonly IUSDAFoodService _usdaFoodService;
        private readonly IUnitOfWork _unitOfWork;
        public MealsController(IUSDAFoodService usdaFoodService, IUnitOfWork unitOfWork)
        {
            _usdaFoodService = usdaFoodService;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Search(MealsVM? mealsVM)
        {
            if (mealsVM==null || mealsVM.SearchString == "")
            {
                return View();
            }
            JObject foodData = await _usdaFoodService.GetFoodDataAsync(mealsVM.SearchString);
            mealsVM.Meals = foodData["foods"].Select(food => new Meal
            {
                Id = (int)food["fdcId"],
                FoodName = (string?)food["description"],
                BrandName = (string?)food["brandName"]
            }).ToList();
            return View(mealsVM);
        }

        [HttpGet]
        public async Task<IActionResult> AddFood(int id)
        {
            JObject foodItem = await _usdaFoodService.GetFoodDataByIdAsync(id);
            if (foodItem != null)
            {
                Meal meal = new Meal
                {
                    Id = id,
                    FoodName = (string?)foodItem["description"],
                    BrandName = (string?)foodItem["brandName"],
                    Calories = (double)foodItem["foodNutrients"].FirstOrDefault(n => n["name"].Value<string>() == "Energy")["amount"],
                    Carbohydrates = (double)foodItem["foodNutrients"].FirstOrDefault(n => n["name"].Value<string>() == "Carbohydrate, by difference")["amount"],
                    Protein = (double)foodItem["foodNutrients"].FirstOrDefault(n => n["name"].Value<string>() == "Protein")["amount"],
                    Fat = (double)foodItem["foodNutrients"].FirstOrDefault(n => n["name"].Value<string>() == "Total lipid (fat)")["amount"],
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    MealTime = "Breakfast"
                };

                return View(meal);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult AddFood(Meal meal)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Meals.Add(meal);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(meal.Id);
        }
    }
}
