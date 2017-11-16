using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartMeal.Core.Models;

namespace SmartMeal.Core.Contracts
{
    public interface IMealRepository
    {
        Task<List<Meal>> GetMealsByUserId(string userId);

        Task<Meal> GetMealByid(int id);

        Task<bool> CreateNew(Meal meal);

        Task<Meal> AddGrade(MealGrade grade);

        Task<MealRating> GetMealRatingByUser(int userId, int mealId);

        Task<List<MealGrade>> GetGrades(int mealId);
    }
}
