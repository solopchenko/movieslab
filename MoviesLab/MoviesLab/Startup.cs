using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MoviesLab.Startup))]
namespace MoviesLab
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
