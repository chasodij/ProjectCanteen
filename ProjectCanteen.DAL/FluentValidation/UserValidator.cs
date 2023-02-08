using FluentValidation;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.DAL.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.FirstName).NotEmpty().MaximumLength(Constants.MaxNameLength);
            RuleFor(user => user.LastName).NotEmpty().MaximumLength(Constants.MaxNameLength);
            RuleFor(user => user.Patronymic).NotEmpty().MaximumLength(Constants.MaxNameLength);
            RuleFor(user => user.Email).NotEmpty().EmailAddress().Length(Constants.MinEmailLength, Constants.MaxEmailLength);
        }
    }
}
