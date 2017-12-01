using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sport_E.Startup))]
namespace Sport_E
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
