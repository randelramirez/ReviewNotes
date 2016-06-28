using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ReviewNotes.WebUI.Startup))]
namespace ReviewNotes.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
