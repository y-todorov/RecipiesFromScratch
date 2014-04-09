using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RecipiesMVC.Startup))]
namespace RecipiesMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
