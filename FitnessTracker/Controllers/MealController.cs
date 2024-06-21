using Fitness.DataAccess.Repositories.Interfaces;
using Fitness.Models;
using Fitness.Models.ViewModels;
using FitnessTracker.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Claims;

namespace FitnessTracker.Controllers
{
    public class MealController : Controller
    {
        private readonly IUSDAFoodService _usdaFoodService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFitnessCalculatorService _fitnessCalculatorService;
        private readonly UserManager<ApplicationUser> _userManager;
        
        public MealController(IUSDAFoodService usdaFoodService, IUnitOfWork unitOfWork, IFitnessCalculatorService fitnessCalculatorService, UserManager<ApplicationUser> userManager)
        {
            _usdaFoodService = usdaFoodService;
            _unitOfWork = unitOfWork;
            _fitnessCalculatorService = fitnessCalculatorService;
            _userManager = userManager;
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

            mealsVM.Meals = (foodData.SelectToken("foods", false) ?? new JArray()).Select(food => new Meal
            {
                Api_Id = (int)(food.SelectToken("fdcId", true) ?? throw new ArgumentNullException()),
                FoodName = (string?)food.SelectToken("description"),
                BrandName = (string?)food.SelectToken("brandName"),
                Calories = (double?)(food.SelectToken("foodNutrients") ?? new JArray()).FirstOrDefault(n => (n.SelectToken("nutrientName") ?? "").Value<string>() == "Energy", new JObject()).SelectToken("value")
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
                        FoodName = (string?)foodItem.SelectToken("description"),
                        BrandName = (string?)foodItem.SelectToken("brandName"),
						Calories = (double?)(foodItem.SelectToken("foodNutrients") ?? new JArray()).FirstOrDefault(n => (n.SelectToken("nutrient.name") ?? "").Value<string>() == "Energy", new JObject()).SelectToken("amount"),
                        Carbohydrates = (double?)(foodItem.SelectToken("foodNutrients") ?? new JArray()).FirstOrDefault(n => (n.SelectToken("nutrient.name") ?? "").Value<string>() == "Carbohydrate, by difference", new JObject()).SelectToken("amount"),
                        Protein = (double?)(foodItem.SelectToken("foodNutrients") ?? new JArray()).FirstOrDefault(n => (n.SelectToken("nutrient.name") ?? "").Value<string>() == "Protein", new JObject()).SelectToken("amount"),
						Fat = (double?)(foodItem.SelectToken("foodNutrients") ?? new JArray()).FirstOrDefault(n => (n.SelectToken("nutrient.name") ?? "").Value<string>() == "Total lipid (fat)", new JObject()).SelectToken("amount"),
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
