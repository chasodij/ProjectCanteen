using FluentValidation;
using ProjectCanteen.BLL.DTOs.MenuSection;
using ProjectCanteen.DAL;

namespace ProjectCanteen.BLL.FluentValidation.MenuSection
{
    public class MenuSectionDTOValidator : AbstractValidator<MenuSectionDTO>
    {
        public MenuSectionDTOValidator()
        {
            RuleFor(section => section.Id).GreaterThanOrEqualTo(Constants.MinId);
            RuleFor(section => section.Name).NotEmpty().Length(Constants.MinTitleLength, Constants.MaxTitleLength);
            RuleFor(section => section.NumberInMenu).InclusiveBetween(Constants.MinNumber, Constants.MaxNumber);
        }
    }
}
