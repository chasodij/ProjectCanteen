using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.DAL.EntityConfiguration
{
    public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.HasKey(ingredient => ingredient.Id);

            builder.HasOne(ingredient => ingredient.Canteen).WithMany(canteen => canteen.Ingredients).HasForeignKey(ingredient => ingredient.CanteenId);
            builder.HasMany(ingredient => ingredient.DietaryRestrictions).WithMany(restriction => restriction.Ingredients);

            builder.Property(ingredient => ingredient.Name).HasMaxLength(Constants.MaxTitleLength);
        }
    }
}
