using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OHET_Project.Startup))]
namespace OHET_Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
