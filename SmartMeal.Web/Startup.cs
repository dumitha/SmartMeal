using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SmartMeal.Web.Startup))]
namespace SmartMeal.Web
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
