using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VideoCallConsultant.Startup))]
namespace VideoCallConsultant
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
