using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(file_manager.Startup))]
namespace file_manager
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
