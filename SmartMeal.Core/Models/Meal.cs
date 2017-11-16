using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMeal.Core.Models
{
    public class Meal
    {
        public Meal()
        {

        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public DateTime CreatedOn { get; set; }

        public int LikesCount { get; set; }

        public string ImageUrl { get; set; }

        public object RenderedImage { get; set; }

        public string Type { get; set; }

        public double Grade { get; set; }

        public int GradesCount { get; set; }
    }
}
