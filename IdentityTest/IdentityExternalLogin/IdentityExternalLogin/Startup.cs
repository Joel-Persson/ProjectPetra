using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IdentityExternalLogin.Startup))]
namespace IdentityExternalLogin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
