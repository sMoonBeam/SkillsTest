using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SkillsTest.Startup))]
namespace SkillsTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
