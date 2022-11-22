using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.DAL.EntityConfiguration
{
    public class IngredientInDishConfiguration : IEntityTypeConfiguration<IngredientInDish>
    {
        public void Configure(EntityTypeBuilder<IngredientInDish> builder)
        {
            builder.HasKey(ingredient => new { ingredient.IngredientId, ingredient.DishId });

            builder.HasOne(ingredient => ingredient.Ingredient).WithMany();
            builder.HasOne(ingredient => ingredient.Dish).WithMany(dish => dish.IngredientInDishes).HasForeignKey(ingredient => ingredient.DishId);
        }
    }
}
