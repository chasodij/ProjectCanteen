using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.DAL.EntityConfiguration
{
    public class IngredientInDishConfiguration : IEntityTypeConfiguration<IngredientInDish>
    {
        public void Configure(EntityTypeBuilder<IngredientInDish> builder)
        {
            builder.HasKey(ingredient => new { ingredient.Ingredient, ingredient.Dish });

            builder.HasOne(ingredient => ingredient.Ingredient).WithMany();
            builder.HasOne(ingredient => ingredient.Dish).WithMany();
        }
    }
}
