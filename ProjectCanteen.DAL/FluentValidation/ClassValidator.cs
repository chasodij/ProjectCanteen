using FluentValidation;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.DAL.FluentValidation
{
    public class ClassValidator : AbstractValidator<Class>
    {
        public ClassValidator()
        {
            RuleFor(cur_class => cur_class.ClassName).NotEmpty().Length(Constants.MinTitleLength, Constants.MaxTitleLength);
            RuleFor(cur_class => cur_class.ClassTeacher).NotNull().SetValidator(new ClassTeacherValidator());
            RuleFor(cur_class => cur_class.School).NotNull().SetValidator(new SchoolValidator());
            RuleForEach(cur_class => cur_class.Students).SetValidator(new StudentValidator());
        }
    }
}
