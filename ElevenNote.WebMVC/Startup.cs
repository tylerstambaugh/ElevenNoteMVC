using ElevenNote.Services;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ElevenNote.WebMVC.Startup))]
namespace ElevenNote.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            var roleSvc = new RoleService();
            roleSvc.CreateAdmin();
            roleSvc.MakeUserAdmin();
        }
    }
}
