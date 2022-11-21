using FluentValidation;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.DAL.FluentValidation
{
    public class MenuSectionValidation : AbstractValidator<MenuSection>
    {
        public MenuSectionValidation()
        {
            RuleFor(section => section.Name).NotEmpty().Length(Constants.MinTitleLength, Constants.MaxTitleLength);
            RuleFor(section => section.NumberInMenu).InclusiveBetween(Constants.MinNumber, Constants.MaxNumber);
        }
    }
}
