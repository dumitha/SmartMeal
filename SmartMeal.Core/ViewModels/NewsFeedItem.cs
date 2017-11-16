using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMeal.Core.ViewModels
{
    public class NewsFeedItem
    {

        public NewsFeedItem()
        {

        }

        public int Id { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }
    }
}
