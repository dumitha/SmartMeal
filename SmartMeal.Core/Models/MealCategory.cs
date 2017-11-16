using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMeal.Core.Models
{
    public class MealCategory
    {

        private List<Meal> _Meals = new List<Meal>();

        public MealCategory()
        {

        }

        public int Id { get; set; }

        public string Name { get; set; }

        public List<Meal> Meals
        {
            get
            {
                return _Meals;
            }
            set
            {
                _Meals = value;
            }
        }
    }
}
