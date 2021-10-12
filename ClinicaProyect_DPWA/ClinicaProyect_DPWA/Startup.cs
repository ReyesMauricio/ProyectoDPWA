using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClinicaProyect_DPWA.Startup))]
namespace ClinicaProyect_DPWA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
