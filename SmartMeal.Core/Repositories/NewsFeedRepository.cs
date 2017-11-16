using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartMeal.Core.Models;
using SmartMeal.Core.Contracts.Repositories;
using SmartMeal.Core.Config;

namespace SmartMeal.Core.Repositories
{
    public class NewsFeedRepository : BaseRepository, INewsFeedRepository
    {

        public static List<EventMessage> NewsFeedList = new List<EventMessage>()
        {
            new EventMessage()
            {
                Id = 1,
                PostedBy = "Brownie Dug",
                PostedById = "1",
                PostedOn = DateTime.Now.AddDays(-2),
                Description = "posted a new picture.",
                Type = "Meal",
                TypeId = 1,
                ImageList = { "https://www.bobevans.com/-/media/bobevans_com/images/our-restaurants/2017winter/farmers_choice_core_winterfy17.jpg"},
                UserId = "1",
                ObjectId = 1
            },
            new EventMessage()
            {
                Id = 3,
                PostedBy = "Carrie",
                PostedById = "2",
                PostedOn = DateTime.Now.AddDays(-3),
                Description = "liked your picture.",
                Type = "Like",
                TypeId = 2,
                ImageList = { "http://crookedcreekguides.com/wp-content/uploads/2016/01/dinner-03.jpg"},
                UserId = "1",
                ObjectId = 2
            },
            new EventMessage()
            {
                Id = 2,
                PostedBy = "Carrie",
                PostedById = "2",
                PostedOn = DateTime.Now.AddDays(-5),
                Description = "posted a new picture.",
                Type = "Meal",
                TypeId = 1,
                ImageList = { "http://crookedcreekguides.com/wp-content/uploads/2016/01/dinner-03.jpg"},
                UserId = "2",
                ObjectId = 3
            },
            new EventMessage()
            {
                Id = 1,
                PostedBy = "Brownie Dug",
                PostedById = "1",
                PostedOn = DateTime.Now.AddDays(-6),
                Description = "liked your picture.",
                Type = "Like",
                TypeId = 2,
                ImageList = { "https://www.bobevans.com/-/media/bobevans_com/images/our-restaurants/2017winter/farmers_choice_core_winterfy17.jpg"},
                UserId = "2",
                ObjectId = 4
            },
        };

        public NewsFeedRepository()
        {

        }

        public async Task<List<EventMessage>> GetNewsFeedForUser(string userId)
        {

            string Url = String.Format("{0}news/{1}/foruser", ConfigManager.ApiUri, userId);
            return await GetAsync<List<EventMessage>>(Url);

        }


        private void GetNewsFeedDataForUser(int userId)
        {

            try
            {

                
            }
            catch(Exception ex)
            {


            }
            
        }
    }
}
