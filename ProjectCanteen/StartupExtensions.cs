using Microsoft.AspNetCore.Identity;
using ProjectCanteen.BLL;
using ProjectCanteen.BLL.Services.Implementations;
using ProjectCanteen.BLL.Services.Interfaces;

namespace ProjectCanteen.WebApi
{
    public static class StartupExtensions
    {
        public static void AddServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IJwtService, JwtService>();
            serviceCollection.AddScoped<IAuthentificationService, AuthentificationService>();
            serviceCollection.AddScoped<IIngredientService, IngredientService>();
            serviceCollection.AddScoped<IDishService, DishService>();
            serviceCollection.AddScoped<IMenuOfTheDayService, MenuOfTheDayService>();
            serviceCollection.AddScoped<IOrderService, OrderService>();
            serviceCollection.AddScoped<ICanteenService, CanteenService>();
            serviceCollection.AddScoped<IClassService, ClassService>();
            serviceCollection.AddScoped<IDietaryRestrictionService, DietaryRestrictionService>();
            serviceCollection.AddScoped<IMenuSectionService, MenuSectionService>();
            serviceCollection.AddScoped<ISchoolService, SchoolService>();
            serviceCollection.AddScoped<IClassTeacherService, ClassTeacherService>();
            serviceCollection.AddScoped<IRightsService, RightsService>();
            serviceCollection.AddScoped<ICanteenWorkerService, CanteenWorkerService>();
            serviceCollection.AddScoped<IParentService, ParentService>();
            serviceCollection.AddScoped<IStudentService, StudentService>();
            serviceCollection.AddScoped<ISchoolAdminService, SchoolAdminService>();
            serviceCollection.AddScoped<IMessageService, MessageService>();
        }

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
