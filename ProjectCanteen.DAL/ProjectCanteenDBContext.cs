using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectCanteen.DAL.Entities;
using ProjectCanteen.DAL.EntityConfiguration;

namespace ProjectCanteen.DAL
{
    public class ProjectCanteenDBContext : IdentityDbContext<User, IdentityRole, string>
    {
        public DbSet<Canteen> Canteens { get; set; }
        public DbSet<CanteenWorker> CanteenWorkers { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<ClassTeacher> ClassTeachers { get; set; }
        public DbSet<DietaryRestriction> DietaryRestrictions { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<IngredientInDish> IngredientInDishes { get; set; }
        public DbSet<MenuOfTheDay> MenuOfTheDays { get; set; }
        public DbSet<MenuSection> MenuSections { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CanteenConfiguration());
            builder.ApplyConfiguration(new CanteenWorkerConfiguration());
            builder.ApplyConfiguration(new ClassConfiguration());
            builder.ApplyConfiguration(new ClassTeacherConfiguration());
            builder.ApplyConfiguration(new DietaryRestrictionConfiguration());
            builder.ApplyConfiguration(new DishConfiguration());
            builder.ApplyConfiguration(new IngredientConfiguration());
            builder.ApplyConfiguration(new IngredientInDishConfiguration());
            builder.ApplyConfiguration(new MenuOfTheDayConfiguration());
            builder.ApplyConfiguration(new MenuSectionConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new OrderItemConfiguration());
            builder.ApplyConfiguration(new ParentConfiguration());
            builder.ApplyConfiguration(new SchoolConfiguration());
            builder.ApplyConfiguration(new StudentConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
