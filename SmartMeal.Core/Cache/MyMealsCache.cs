using SmartMeal.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartMeal.Core.ViewModels;

namespace SmartMeal.Core.Cache
{
    public class MyMealsCache
    {

        public static List<Meal> MyMealsList { get; set; }

        public static List<TwoColumnImageView> MyMealsTwoColumnGallery {get;set;}

        public static string NewMealName{ get; set; }
    }
}
