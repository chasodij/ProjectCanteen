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

            builder.HasOne(student => student.User);
            builder.HasOne(student => student.Class).WithMany(cur_class => cur_class.Students);
            builder.HasMany(student => student.Parents).WithMany(parent => parent.Children);
            builder.HasMany(student => student.Orders).WithOne(order => order.Student);

            builder.Property(student => student.TagId).HasMaxLength(Constants.MaxTagLength);
        }
    }
}
