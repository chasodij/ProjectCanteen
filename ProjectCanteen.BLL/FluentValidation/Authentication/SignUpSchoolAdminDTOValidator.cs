using FluentValidation;
using ProjectCanteen.BLL.DTOs.Authentication;
using ProjectCanteen.DAL;

namespace ProjectCanteen.BLL.FluentValidation
{
    public class SignUpSchoolAdminDTOValidator : AbstractValidator<SignUpSchoolAdminDTO>
    {
        public SignUpSchoolAdminDTOValidator(IValidator<SignUpBaseDTO> validator)
        {
            Include(validator);

            RuleFor(worker => worker.LastName).NotEmpty().MaximumLength(Constants.MaxNameLength);
            RuleFor(worker => worker.Patronymic).Length(Constants.MinNameLength, Constants.MaxNameLength);
            RuleFor(worker => worker.SchoolId).GreaterThanOrEqualTo(Constants.MinId);
        }
    }
}
