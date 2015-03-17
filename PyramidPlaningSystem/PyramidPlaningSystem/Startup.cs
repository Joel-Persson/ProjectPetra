using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PyramidPlaningSystem.Startup))]
namespace PyramidPlaningSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
