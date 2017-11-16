using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMeal.Core.Cache
{
    public static class CacheManager
    {

        public static void ResetSessionCache()
        {
            MyMealsCache.MyMealsList = null;
            NewsFeedCache.NewsList = null;
        }

    }
}
