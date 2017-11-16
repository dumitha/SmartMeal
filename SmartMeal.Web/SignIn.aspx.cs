using SmartMeal.Core.Models;
using SmartMeal.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SmartMeal.Web
{
    public partial class SignIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod(EnableSession = false)]
        public static string Test()
        {

            string msg = "Made it to the test methods";
            return msg;
        }

        [WebMethod]
        public static async Task<bool> SignInUser(string email, string psswd)
        {

            bool Result = false;

            try
            {
                UserService service = new UserService();
                User user = await service.GetUser(email, psswd);
                if (user.Token != String.Empty)
                {

                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                Result = false;
                throw;
            }

            return Result;





        }

    }
}