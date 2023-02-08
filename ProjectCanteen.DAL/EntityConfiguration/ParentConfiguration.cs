using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.DAL.EntityConfiguration
{
    public class ParentConfiguration : IEntityTypeConfiguration<Parent>
    {
        public void Configure(EntityTypeBuilder<Parent> builder)
        {
            builder.HasKey(parent => parent.Id);

            builder.HasMany(parent => parent.Children).WithMany(child => child.Parents)
                .UsingEntity<Dictionary<string, object>>(
                "ParentStudent",
                x => x.HasOne<Student>().WithMany().OnDelete(DeleteBehavior.ClientCascade),
                x => x.HasOne<Parent>().WithMany().OnDelete(DeleteBehavior.ClientCascade));

            builder.HasMany(parent => parent.Orders).WithOne(order => order.Purchaser);

            builder.HasOne(parent => parent.User).WithOne(user => user.Parent)
                .HasForeignKey<Parent>(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
