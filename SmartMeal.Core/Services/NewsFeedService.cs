using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartMeal.Core.Models;
using SmartMeal.Core.Repositories;
using SmartMeal.Core.Contracts.Repositories;

namespace SmartMeal.Core.Services
{
    public class NewsFeedService
    {
        INewsFeedRepository _Repository;

        public NewsFeedService()
        {
            _Repository = new NewsFeedRepository();
        }

        public async Task<List<EventMessage>> GetNewsFeedForUser(string userId)
        {
            return await _Repository.GetNewsFeedForUser(userId);
        }

    }
}
