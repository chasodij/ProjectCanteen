using Microsoft.AspNetCore.Identity;
using ProjectCanteen.BLL.Services.Interfaces;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.BLL.Services.Implementations
{
    public class AuthentificationService : IAuthentificationService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthentificationService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task CreateAdminAsync(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            roles.Clear();
            await _userManager.AddToRoleAsync(user, Roles.Admin);
        }
    }
}
