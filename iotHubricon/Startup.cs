using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(iotHubricon.Startup))]
namespace iotHubricon
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
