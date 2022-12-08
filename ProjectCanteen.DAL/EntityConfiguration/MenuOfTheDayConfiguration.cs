using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.DAL.EntityConfiguration
{
    public class MenuOfTheDayConfiguration : IEntityTypeConfiguration<MenuOfTheDay>
    {
        public void Configure(EntityTypeBuilder<MenuOfTheDay> builder)
        {
            builder.HasKey(menu => menu.Id);

            builder.HasMany(menu => menu.Dishes).WithMany(dish => dish.MenuOfTheDays);
            builder.Navigation(ingredient => ingredient.Dishes).AutoInclude();
        }
    }
}
