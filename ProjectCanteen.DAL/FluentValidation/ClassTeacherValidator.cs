using FluentValidation;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.DAL.FluentValidation
{
    public class ClassTeacherValidator : AbstractValidator<ClassTeacher>
    {
        public ClassTeacherValidator()
        {
            RuleFor(teacher => teacher.User).NotNull().SetValidator(new UserValidator());
            RuleFor(teacher => teacher.Class).NotNull().SetValidator(new ClassValidator());

        }
    }
}
