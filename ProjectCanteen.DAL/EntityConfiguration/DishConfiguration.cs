using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.DAL.EntityConfiguration
{
    public class DishConfiguration : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder.HasKey(dish => dish.Id);

            builder.HasMany(dish => dish.IngredientInDishes).WithOne(ingredient => ingredient.Dish);
            builder.HasMany(dish => dish.MenuOfTheDays).WithMany(menu => menu.Dishes);
            builder.HasMany(dish => dish.OrderItems).WithOne(item => item.Dish);
            builder.HasOne(dish => dish.MenuSection).WithMany(section => section.Dishes);

            builder.Property(dish => dish.Name).HasMaxLength(Constants.MaxDishNameLength);
            builder.Property(dish => dish.Price).HasPrecision(Constants.PriceUAHPrecision, Constants.PriceUAHScale);
        }
    }
}
