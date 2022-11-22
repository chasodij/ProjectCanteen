using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.DAL.EntityConfiguration
{
    public class ClassTeacherConfiguration : IEntityTypeConfiguration<ClassTeacher>
    {
        public void Configure(EntityTypeBuilder<ClassTeacher> builder)
        {
            builder.HasKey(teacher => teacher.Id);

            builder.HasOne(teacher => teacher.User);
            builder.HasOne(teacher => teacher.Class).WithOne(cur_class => cur_class.ClassTeacher);
        }
    }
}
