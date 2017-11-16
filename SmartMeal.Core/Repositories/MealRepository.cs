using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartMeal.Core.Models;
using SmartMeal.Core.Contracts;
using SmartMeal.Core.Config;
using Newtonsoft.Json;

namespace SmartMeal.Core.Repositories
{
    public class MealRepository : BaseRepository, IMealRepository
    {

        //For testing only

        private static List<MealCategory> MealCategories = new List<MealCategory>()
        {
            new MealCategory()
            {
                Id = 1,
                Name = "Breakfast"
            },
            new MealCategory()
            {
                Id = 2,
                Name = "Lunch"
            },
            new MealCategory()
            {
                Id = 3,
                Name = "Dinner"
            },
            new MealCategory()
            {
                Id = 4,
                Name = "Snack"
            }

        };

        private static List<Meal> Meals = new List<Meal>()
        {
            new Meal()
            {
                Id = 1,
                Name = "Omlet",
                Description = "My Egg Omlet",
                UserId = "G17",
                UserName = "Brownie",
                CreatedOn = DateTime.Now.AddDays(-10),
                ImageUrl = "https://www.bobevans.com/-/media/bobevans_com/images/our-restaurants/2017winter/farmers_choice_core_winterfy17.jpg",
                LikesCount = 15
            },
            new Meal()
            {
                Id = 2,
                Name = "Subway Tuna Sandwich",
                Description = "My Lunch for Today Brahs!",
                UserId = "G17",
                UserName = "Brownie",
                CreatedOn = DateTime.Now.AddDays(-10),
                ImageUrl = "http://crookedcreekguides.com/wp-content/uploads/2016/01/dinner-03.jpg",
                LikesCount = 10
            },
            new Meal()
            {
                Id = 3,
                Name = "Salata Salmon Salad",
                Description = "This is what I had for dinner suckas!",
                UserId = "G17",
                UserName = "Brownie",
                CreatedOn = DateTime.Now.AddDays(-10),
                ImageUrl = "http://crookedcreekguides.com/wp-content/uploads/2016/01/dinner-03.jpg",
                LikesCount = 12
            },
            new Meal()
            {
                Id = 4,
                Name = "Cereal",
                Description = "Count Chocula Cereal",
                UserId = "G18",
                UserName = "Carrie",
                CreatedOn = DateTime.Now.AddDays(-11),
                ImageUrl = "https://www.bobevans.com/-/media/bobevans_com/images/our-restaurants/2017winter/farmers_choice_core_winterfy17.jpg",
                LikesCount = 1001
            },
            new Meal()
            {
                Id = 5,
                Name = "Burger",
                Description = "Super Sized Burger From Five Guys Burgers!",
                UserId = "G18",
                UserName = "Carrie",
                CreatedOn = DateTime.Now.AddDays(-11),
                ImageUrl = "https://www.bobevans.com/-/media/bobevans_com/images/our-restaurants/2017winter/farmers_choice_core_winterfy17.jpg",
                LikesCount = 1342
            },
            new Meal()
            {
                Id = 6,
                Name = "Pizza and Cheetos",
                Description = "My dinner for today!",
                UserId = "G18",
                UserName = "Carrie",
                CreatedOn = DateTime.Now.AddDays(-11),
                ImageUrl = "https://www.bobevans.com/-/media/bobevans_com/images/our-restaurants/2017winter/farmers_choice_core_winterfy17.jpg",
                LikesCount= 10432
            }
        };

        public async Task<List<Meal>> GetMealsByUserId(string userId)
        {
            string Url = String.Format("{0}meals/{1}/foruser",ConfigManager.ApiUri, userId);
            return await GetAsync<List<Meal>>(Url);
        }

        public async Task<Meal> GetMealByid(int id)
        {
            string Url = String.Format("{0}meals/{1}/meal", ConfigManager.ApiUri, id);
            return await GetAsync<Meal>(Url);
        }

        public async Task<bool> CreateNew(Meal meal)
        {
            bool Success = true;

            try
            {
                string Url = ConfigManager.ApiUri + "meal/CreateNew";
                string data = JsonConvert.SerializeObject(meal);
                Meal newMeal = await GetAsyncPost<Meal>(Url, data);
            }
            catch(Exception ex)
            {
                string err = ex.Message;
                Success = false;
            }

            return Success;
        }

        public async Task<Meal> AddGrade(MealGrade grade)
        {
            Meal newMeal = new Meal();

            try
            {
                string Url = ConfigManager.ApiUri + "meal/SubmitGrade";
                string data = JsonConvert.SerializeObject(grade);
                newMeal = await GetAsyncPost<Meal>(Url, data);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }

            return newMeal;
        }

        public async Task<MealRating> GetMealRatingByUser(int userId, int mealId)
        {
            string Url = String.Format("{0}meals/{1}/mealratingforuser?mealId={2}", ConfigManager.ApiUri, userId.ToString(), mealId.ToString());
            return await GetAsync<MealRating>(Url);
        }

        public async Task<List<MealGrade>> GetGrades(int mealId)
        {
            string Url = String.Format("{0}meals/{1}/getgrades", ConfigManager.ApiUri, mealId);
            return await GetAsync<List<MealGrade>>(Url);
        }


        #region data fetch methods


        #endregion

    }
}
