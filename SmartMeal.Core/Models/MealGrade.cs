using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMeal.Core.Models
{
    public class MealGrade
    {
        public MealGrade()
        {

        }
        
        public DateTime SubmittedOn { get; set; }

        public string SubmittedByName { get; set; }

        public int SubmittedBy { get; set; }

        public int MealId { get; set; }

        public double Grade { get; set; }

    }
}
