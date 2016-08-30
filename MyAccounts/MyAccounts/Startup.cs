using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyAccounts.Startup))]
namespace MyAccounts
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
