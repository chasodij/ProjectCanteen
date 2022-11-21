using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.DAL.EntityConfiguration
{
    public class ClassConfiguration : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> builder)
        {
            builder.HasKey(cur_class => cur_class.Id);

            builder.HasOne(cur_class => cur_class.ClassTeacher).WithOne(teacher => teacher.Class);
            builder.HasOne(cur_class => cur_class.School).WithMany(school => school.Classes);
            builder.HasMany(cur_class => cur_class.Students).WithOne(student => student.Class);

            builder.Property(cur_class => cur_class.ClassName).HasMaxLength(Constants.MaxTitleLength);
        }
    }
}
