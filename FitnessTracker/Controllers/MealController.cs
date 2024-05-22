using Fitness.DataAccess.Repositories.Interfaces;
using Fitness.Models;
using Fitness.Models.ViewModels;
using FitnessTracker.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

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
            var query = Meals.GroupBy(meal => meal.Date);

            List<Meal> TodayMeals = new();
            foreach (var group in query)
            {
                if (group.Key == DateOnly.FromDateTime(DateTime.Now))
                {
                    foreach(Meal meal in group)
                    {
                        TodayMeals.Add(meal);
                    }
                }
            }

            Dictionary<string, int> mealOrder = new()
            {
                {"Breakfast", 0 },
                {"Lunch", 1 },
                {"Snacks", 2 },
                {"Dinner", 3 }
            };

            TodayMeals.Sort((x, y) => mealOrder[x.MealTime].CompareTo(mealOrder[y.MealTime]));
			return View(TodayMeals);
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
            MealVM mealVM = new MealVM
            {
                Meal = _unitOfWork.Meals.Get(u => u.Id == id)
            };
            if (mealVM.Meal!=null)
            {
                return View(mealVM);
            }
            return NotFound();
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
            MealVM mealVM = new()
            {
                Meal = _unitOfWork.Meals.Get(u => u.Id == id)
            };
			if (mealVM.Meal != null)
			{
				return View(mealVM);
			}
			return NotFound();
		}

        [ActionName("Delete")]
		[HttpPost]
		public IActionResult DeletePOST(int id)
		{
			Meal meal = _unitOfWork.Meals.Get(u => u.Id == id);
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
