using FluentValidation;
using ProjectCanteen.BLL.DTOs.MenuSection;
using ProjectCanteen.DAL;

namespace ProjectCanteen.BLL.FluentValidation.MenuSection
{
    public class CreateMenuSectionDTOValidator : AbstractValidator<CreateMenuSectionDTO>
    {
        public CreateMenuSectionDTOValidator()
        {
            RuleFor(section => section.Name).NotEmpty().Length(Constants.MinTitleLength, Constants.MaxTitleLength);
            RuleFor(section => section.NumberInMenu).InclusiveBetween(Constants.MinNumber, Constants.MaxNumber);
        }
    }
}
