using FluentValidation;
using ProjectCanteen.BLL.DTOs.Authentication;
using ProjectCanteen.DAL;

namespace ProjectCanteen.BLL.FluentValidation
{
    public class SignInDTOValidator : AbstractValidator<SignInDTO>
    {
        public SignInDTOValidator()
        {
            RuleFor(login => login.Email).NotEmpty().EmailAddress().Length(Constants.MinEmailLength, Constants.MaxEmailLength);
            RuleFor(login => login.Password).NotEmpty().Length(Constants.MinPasswordLength, Constants.MaxPasswordLength);
        }
    }
}
