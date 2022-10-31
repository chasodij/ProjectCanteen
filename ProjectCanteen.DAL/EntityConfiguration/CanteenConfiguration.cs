using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.DAL.EntityConfiguration
{
    public class CanteenConfiguration : IEntityTypeConfiguration<Canteen>
    {
        public void Configure(EntityTypeBuilder<Canteen> builder)
        {
            builder.HasKey(canteen => canteen.Id);

            builder.HasOne(canteen => canteen.School).WithMany(school => school.Canteens);
            builder.HasOne(canteen => canteen.Terminal).WithOne();
            builder.HasMany(canteen => canteen.CanteenWorkers).WithOne(worker => worker.Canteen);

            builder.Property(canteen => canteen.Name).HasMaxLength(Constants.MaxTitleLength);

        }
    }
}
