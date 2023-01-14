using FluentValidation;
using ProjectCanteen.BLL.DTOs.Authentication;
using ProjectCanteen.DAL;

namespace ProjectCanteen.BLL.FluentValidation
{
    public class SignUpParentDTOValidator : AbstractValidator<SignUpParentDTO>
    {
        public SignUpParentDTOValidator(IValidator<SignUpBaseDTO> validator)
        {
            Include(validator);

            RuleFor(worker => worker.LastName).NotEmpty().MaximumLength(Constants.MaxNameLength);
            RuleFor(worker => worker.Patronymic).Length(Constants.MinNameLength, Constants.MaxNameLength);
            RuleForEach(worker => worker.ChildrenId).GreaterThanOrEqualTo(Constants.MinId);
        }
    }
}
