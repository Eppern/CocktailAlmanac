using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CocktailAlmanac.Startup))]
namespace CocktailAlmanac
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
