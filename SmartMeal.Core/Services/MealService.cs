using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartMeal.Core.Models;
using SmartMeal.Core.Repositories;
using SmartMeal.Core.Contracts;
using SmartMeal.Core.ViewModels;

namespace SmartMeal.Core.Services
{
    public class MealService
    {
        IMealRepository _Repository;

        public MealService()
        {
            //TODO: Get rid of the hard dependeny if we have time.   It bothers me :/
            _Repository = new MealRepository();
        }

        public async Task<List<Meal>> GetMealsByUserId(string userId)
        {
            return await _Repository.GetMealsByUserId(userId);
        }

        public async Task<Meal> GetMealById(int id)
        {
            return await _Repository.GetMealByid(id);
        }

        public async Task<bool> CreateNew(Meal meal)
        {
            return await _Repository.CreateNew(meal);
        }

        public async Task<Meal> AddGrade(MealGrade grade)
        {
            return await _Repository.AddGrade(grade);
        }

        public async Task<MealRating> GetMealRatingByUser(int userId, int mealId)
        {
            return await _Repository.GetMealRatingByUser(userId, mealId);
        }

        public async Task<List<MealGrade>> GetGrades(int mealId)
        {
            return await _Repository.GetGrades(mealId);
        }

        public List<TwoColumnImageView> GetTwoColumnImageView(List<Meal> meals)
        {
            List<TwoColumnImageView> List = new List<TwoColumnImageView>();
            int ItemsCount = meals.Count;
            int Index = 1;
            TwoColumnImageView ImageRow = new TwoColumnImageView();

            foreach (Meal meal in meals)
            {                
                if (!String.IsNullOrEmpty(ImageRow.ImageOneUrl) && !String.IsNullOrEmpty(ImageRow.ImageTwoUrl))
                {
                    //Only create a new row when both slots are filled.
                    ImageRow = new TwoColumnImageView();
                }
                
                if (!String.IsNullOrEmpty(meal.ImageUrl))
                {
                    if (String.IsNullOrEmpty(ImageRow.ImageOneUrl))
                    {
                        ImageRow.ImageOneUrl = meal.ImageUrl;
                    }
                    else if (String.IsNullOrEmpty(ImageRow.ImageTwoUrl))
                    {
                        ImageRow.ImageTwoUrl = meal.ImageUrl;
                    }
                }

                if (!String.IsNullOrEmpty(ImageRow.ImageOneUrl) && !String.IsNullOrEmpty(ImageRow.ImageTwoUrl))
                {
                    List.Add(ImageRow);
                }
                else if (Index == ItemsCount)
                {
                    List.Add(ImageRow);
                }

                Index++;
            }


            return List;

        }
    }
}
