using SmartMeal.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMeal.Core.Contracts.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUser(string userName, string password);
        Task<User> CreateNew(User user);
    }
}
