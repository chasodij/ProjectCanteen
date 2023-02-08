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

            builder.Property(canteen => canteen.Name).HasMaxLength(Constants.MaxTitleLength);

            builder.HasOne(canteen => canteen.School).WithMany(school => school.Canteens)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(canteen => canteen.Terminal).WithOne()
                .HasForeignKey<Canteen>(x => x.TerminalId).OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(canteen => canteen.CanteenWorkers).WithOne(worker => worker.Canteen)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(canteen => canteen.Ingredients).WithOne(ingredient => ingredient.Canteen)
                .HasForeignKey(ingredient => ingredient.CanteenId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(canteen => canteen.Dishes).WithOne(dish => dish.Canteen).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
