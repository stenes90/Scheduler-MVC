using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SchedulerV3.Startup))]
namespace SchedulerV3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
