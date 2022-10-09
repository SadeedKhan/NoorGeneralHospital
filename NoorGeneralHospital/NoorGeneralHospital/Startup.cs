using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using NoorGeneralHospital.Models;
using Owin;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Configuration;

[assembly: OwinStartupAttribute(typeof(NoorGeneralHospital.Startup))]

namespace NoorGeneralHospital
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.MapSignalR();
            ConfigureAuth(app);
            CreateUserAndRoles("admin@admin.com", "Admin").Wait();
        }

        private static async Task CreateUserAndRoles(string email, string roleName = null)
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser
            {
                PhoneNumber="12345678919",
                Email = email,
            };
            var password = WebConfigurationManager.AppSettings["Password"];

            if (roleName != null)
            {
                var role = await roleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    var result = await roleManager.CreateAsync(new IdentityRole(roleName));
                    if (result.Succeeded)
                        role = await roleManager.FindByNameAsync(roleName);
                }

                if (!UserManager.Users.Any(x => x.Roles.Any(y => y.RoleId == role.Id)))
                {
                    var result = await UserManager.CreateAsync(user, password);

                    if (result.Succeeded)
                    {
                        await UserManager.AddToRoleAsync(user.Id, roleName);
                    }
                }
            }
            else
            {
                if (!UserManager.Users.Any(x => x.Roles.Count() == 0))
                {
                    var result = await UserManager.CreateAsync(user, password);
                }
            }
        }
    }
}
