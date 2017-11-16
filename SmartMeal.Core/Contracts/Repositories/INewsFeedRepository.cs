using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartMeal.Core.Models;

namespace SmartMeal.Core.Contracts.Repositories
{
    public interface INewsFeedRepository
    {
        Task<List<EventMessage>> GetNewsFeedForUser(string userId);

    }
}
