using FluentValidation;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.DAL.FluentValidation
{
    public class StudentValidator : AbstractValidator<Student>
    {
        public StudentValidator()
        {
            RuleFor(student => student.TagId).NotEmpty().Length(Constants.MinTagLength, Constants.MaxTagLength);
            RuleFor(student => student.User).NotNull().SetValidator(new UserValidator());
            RuleFor(student => student.Class).NotNull().SetValidator(new ClassValidator());
            RuleForEach(student => student.Parents).NotNull().SetValidator(new ParentValidator());
            RuleForEach(student => student.Orders).NotNull().SetValidator(new OrderValidator());
        }
    }
}
