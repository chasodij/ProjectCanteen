using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.DAL.EntityConfiguration
{
    public class SchoolConfiguration : IEntityTypeConfiguration<School>
    {
        public void Configure(EntityTypeBuilder<School> builder)
        {
            builder.HasKey(school => school.Id);

            builder.HasMany(school => school.Classes).WithOne(cur_class => cur_class.School);
            builder.HasMany(school => school.Canteens).WithOne(canteen => canteen.School);

            builder.Property(school => school.Name).HasMaxLength(Constants.MaxTitleLength);
        }
    }
}
