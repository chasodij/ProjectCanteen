using FluentValidation;
using ProjectCanteen.BLL.DTOs.Canteen;
using ProjectCanteen.DAL;

namespace ProjectCanteen.BLL.FluentValidation.Canteen
{
    public class CreateCanteenDTOValidator : AbstractValidator<CreateCanteenDTO>
    {
        public CreateCanteenDTOValidator()
        {
            RuleFor(canteen => canteen.Name).NotNull().NotEmpty().MaximumLength(Constants.MaxTitleLength);
            RuleFor(canteen => canteen.SchoolId).GreaterThanOrEqualTo(Constants.MinId);
        }
    }
}
