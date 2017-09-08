using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CrudSample.Startup))]
namespace CrudSample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
