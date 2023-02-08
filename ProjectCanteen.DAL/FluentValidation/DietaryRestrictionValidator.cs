using FluentValidation;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.DAL.FluentValidation
{
    public class DietaryRestrictionValidator : AbstractValidator<DietaryRestriction>
    {
        public DietaryRestrictionValidator()
        {
            RuleFor(restriction => restriction.ShortTitle).NotEmpty().Length(Constants.MinTitleLength, Constants.MaxTitleLength);
            RuleFor(restriction => restriction.Description).Length(Constants.MinDescriptionLength, Constants.MaxDescriptionLength);
            RuleForEach(restriction => restriction.Ingredients).SetValidator(new IngredientValidator());
        }
    }
}
