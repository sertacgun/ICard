using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AdminLayer.Startup))]
namespace AdminLayer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
