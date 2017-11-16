using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartMeal.Core.Models;
using SmartMeal.Core.Contracts.Repositories;
using Newtonsoft.Json;
using SmartMeal.Core.Config;

namespace SmartMeal.Core.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository()
        {

        }

        public async Task<User> CreateNew(User user)
        {
            string Url = String.Format("{0}/user/CreateNew",ConfigManager.ApiUri);
            string data = JsonConvert.SerializeObject(user);
            return await GetAsyncPost<User>(Url, data);
        }

        public async Task<User> GetUser(string userName, string password)
        {
            string Url = String.Format("{0}/User/Validate", ConfigManager.ApiUri);

            User user = new User();
            user.UserName = userName;
            user.Password = password;
            user.ConfirmPassword = password;

            string data = JsonConvert.SerializeObject(user);
            return await GetAsyncPost<User>(Url, data);
        }
    }
}
