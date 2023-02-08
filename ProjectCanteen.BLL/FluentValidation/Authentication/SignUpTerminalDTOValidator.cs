using FluentValidation;
using ProjectCanteen.BLL.DTOs.Authentication;
using ProjectCanteen.DAL;

namespace ProjectCanteen.BLL.FluentValidation
{
    public class SignUpTerminalDTOValidator : AbstractValidator<SignUpTerminalDTO>
    {
        public SignUpTerminalDTOValidator(IValidator<SignUpBaseDTO> validator)
        {
            Include(validator);

            RuleFor(worker => worker.CanteenId).GreaterThanOrEqualTo(Constants.MinId);
        }
    }
}
