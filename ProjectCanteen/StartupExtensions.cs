using Microsoft.AspNetCore.Identity;
using ProjectCanteen.BLL;

namespace ProjectCanteen.WebApi
{
    public static class StartupExtensions
    {
        public static async Task CreateRoles(this IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var roles = new[]
            {
                Roles.Terminal,
                Roles.Parent,
                Roles.Student,
                Roles.Admin,
                Roles.CanteenWorker,
                Roles.ClassTeacher,
                Roles.SchoolAdmin
            };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var newRole = new IdentityRole(role);
                    await roleManager.CreateAsync(newRole);
                }
            }
        }
    }
}
