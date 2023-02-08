using FluentValidation;
using ProjectCanteen.BLL.DTOs.Authentication;

namespace ProjectCanteen.BLL.FluentValidation
{
    public class SignUpAdminDTOValidator : AbstractValidator<SignUpAdminDTO>
    {
        public SignUpAdminDTOValidator(IValidator<SignUpBaseDTO> validator)
        {
            Include(validator);
        }
    }
}
