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
            builder.HasOne(dish => dish.Canteen).WithMany(canteen => canteen.Dishes);

            builder.Property(dish => dish.Name).HasMaxLength(Constants.MaxDishNameLength);
        }
    }
}
