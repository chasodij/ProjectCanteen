using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.DAL.EntityConfiguration
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(student => student.Id);

            builder.Property(student => student.TagId).HasMaxLength(Constants.MaxTagLength);

            builder.HasOne(student => student.User).WithOne(user => user.Student)
                .HasForeignKey<Student>(x => x.UserId).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(student => student.Class).WithMany(cur_class => cur_class.Students)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(student => student.Parents).WithMany(parent => parent.Children);

            builder.HasMany(student => student.Orders).WithOne(order => order.Student)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(student => student.DietaryRestrictions).WithMany();
        }
    }
}
