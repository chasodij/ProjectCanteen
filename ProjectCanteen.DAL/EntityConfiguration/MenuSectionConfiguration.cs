using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.DAL.EntityConfiguration
{
    public class MenuSectionConfiguration : IEntityTypeConfiguration<MenuSection>
    {
        public void Configure(EntityTypeBuilder<MenuSection> builder)
        {
            builder.HasKey(section => section.Id);

            builder.Property(section => section.Name).HasMaxLength(Constants.MaxTitleLength);
            builder.HasMany(section => section.Dishes).WithOne(dish => dish.MenuSection);
        }
    }
}
