using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FoodDeliveryApp.Startup))]
namespace FoodDeliveryApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
