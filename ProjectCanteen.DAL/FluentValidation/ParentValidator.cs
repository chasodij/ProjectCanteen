using FluentValidation;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.DAL.FluentValidation
{
    public class ParentValidator : AbstractValidator<Parent>
    {
        public ParentValidator()
        {
            RuleForEach(parent => parent.Children).SetValidator(new StudentValidator());
            RuleFor(parent => parent.User).NotNull().SetValidator(new UserValidator());
            RuleForEach(parent => parent.Orders).SetValidator(new OrderValidator());
        }
    }
}
