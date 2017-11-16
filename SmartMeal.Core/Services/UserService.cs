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
    public class UserService 
    {
        IUserRepository _Repository;

        public UserService()
        {
            _Repository = new UserRepository();

        }

        public async Task<User> GetUser(string userName, string password)
        {
            User user = new User();

            try
            {
                user = await _Repository.GetUser(userName, password);
            }
            catch(Exception ex)
            {
                string err = ex.Message;
            }

            return user;
        }

        public async Task<User> CreateNewUser(User user)
        {

            User NewUser = new User();

            try
            {
                NewUser = await _Repository.CreateNew(user);

            }
            catch(Exception ex)
            {
                string err = ex.Message;
            }

            return NewUser;
        }
    }
}
