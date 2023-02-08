using FluentValidation;
using ProjectCanteen.BLL.DTOs.Authentication;
using ProjectCanteen.DAL;

namespace ProjectCanteen.BLL.FluentValidation.Authentication
{
    public class SignUpBaseDTOValidator : AbstractValidator<SignUpBaseDTO>
    {
        public SignUpBaseDTOValidator()
        {
            RuleFor(register => register.Email).NotEmpty().EmailAddress().Length(Constants.MinEmailLength, Constants.MaxEmailLength);
            RuleFor(register => register.Password).NotEmpty().Length(Constants.MinPasswordLength, Constants.MaxPasswordLength)
                    .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                    .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                    .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                    .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).");
            RuleFor(register => register.FirstName).NotEmpty().MaximumLength(Constants.MaxNameLength);
        }
    }
}
