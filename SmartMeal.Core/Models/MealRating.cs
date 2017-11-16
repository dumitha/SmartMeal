using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMeal.Core.Models
{
    public class MealRating
    {
        public MealRating()
        {

        }

        public int RatingId { get; set; }

        public float Grade { get; set; }

        public DateTime SubmittedOn { get; set; }

    }
}
