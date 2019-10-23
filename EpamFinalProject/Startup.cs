using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EpamFinalProject.Startup))]
namespace EpamFinalProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
