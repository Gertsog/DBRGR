using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DBRGR.Startup))]
namespace DBRGR
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
