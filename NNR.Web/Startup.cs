using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NNR.Web.Startup))]
namespace NNR.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
