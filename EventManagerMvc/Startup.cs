using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EventManagerMvc.Startup))]
namespace EventManagerMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
