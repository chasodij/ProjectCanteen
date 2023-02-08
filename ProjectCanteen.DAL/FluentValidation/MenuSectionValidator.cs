using FluentValidation;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.DAL.FluentValidation
{
    public class MenuSectionValidator : AbstractValidator<MenuSection>
    {
        public MenuSectionValidator()
        {
            RuleFor(section => section.Name).NotEmpty().Length(Constants.MinTitleLength, Constants.MaxTitleLength);
            RuleFor(section => section.NumberInMenu).InclusiveBetween(Constants.MinNumber, Constants.MaxNumber);
            RuleForEach(section => section.Dishes).SetValidator(new DishValidator());
        }
    }
}
