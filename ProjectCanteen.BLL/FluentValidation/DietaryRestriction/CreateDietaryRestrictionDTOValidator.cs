using FluentValidation;
using ProjectCanteen.BLL.DTOs.DietaryRestriction;
using ProjectCanteen.DAL;

namespace ProjectCanteen.BLL.FluentValidation.DietaryRestriction
{
    public class CreateDietaryRestrictionDTOValidator : AbstractValidator<CreateDietaryRestrictionDTO>
    {
        public CreateDietaryRestrictionDTOValidator()
        {
            RuleFor(restriction => restriction.ShortTitle).NotEmpty().Length(Constants.MinTitleLength, Constants.MaxTitleLength);
            RuleFor(restriction => restriction.Description).Length(Constants.MinDescriptionLength, Constants.MaxDescriptionLength);
        }
    }
}
