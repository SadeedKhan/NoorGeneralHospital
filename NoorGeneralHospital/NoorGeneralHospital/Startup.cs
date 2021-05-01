using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NoorGeneralHospital.Startup))]
namespace NoorGeneralHospital
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
