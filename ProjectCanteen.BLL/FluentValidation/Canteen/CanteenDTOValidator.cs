using FluentValidation;
using ProjectCanteen.BLL.DTOs.Canteen;
using ProjectCanteen.DAL;

namespace ProjectCanteen.BLL.FluentValidation.Canteen
{
    public class CanteenDTOValidator : AbstractValidator<UpdateCanteenDTO>
    {
        public CanteenDTOValidator()
        {
            RuleFor(canteen => canteen.Id).GreaterThanOrEqualTo(Constants.MinId);
            RuleFor(canteen => canteen.Name).NotNull().NotEmpty().MaximumLength(Constants.MaxTitleLength);
            RuleFor(canteen => canteen.SchoolId).GreaterThanOrEqualTo(Constants.MinId);
        }
    }
}
