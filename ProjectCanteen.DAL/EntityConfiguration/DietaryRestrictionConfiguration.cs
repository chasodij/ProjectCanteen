using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.DAL.EntityConfiguration
{
    public class DietaryRestrictionConfiguration : IEntityTypeConfiguration<DietaryRestriction>
    {
        public void Configure(EntityTypeBuilder<DietaryRestriction> builder)
        {
            builder.HasKey(rest => rest.Id);

            builder.Property(rest => rest.ShortTitle).HasMaxLength(Constants.MaxTitleLength);
            builder.Property(rest => rest.Description).HasMaxLength(Constants.MaxDescriptionLength);
        }
    }
}
