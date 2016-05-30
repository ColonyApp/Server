using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ColonyServerWebApplication.Startup))]
namespace ColonyServerWebApplication
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
