using FluentValidation;
using ProjectCanteen.BLL.DTOs.Ingredient;
using ProjectCanteen.DAL.UnitOfWork;

namespace ProjectCanteen.DAL.FluentValidation
{
    public class CreateIngredientDTOValidator : AbstractValidator<CreateIngredientDTO>
    {
        public CreateIngredientDTOValidator(IProjectCanteenUoW _unitOfWork)
        {
            RuleFor(ingredient => ingredient.Name)
                .NotEmpty()
                .Length(Constants.MinTitleLength, Constants.MaxTitleLength);

            RuleFor(ingredient => ingredient.ProteinsPer100g)
                .InclusiveBetween(Constants.MinMacronutrients, Constants.MaxMacronutrients);

            RuleFor(ingredient => ingredient.FatsPer100g)
                .InclusiveBetween(Constants.MinMacronutrients, Constants.MaxMacronutrients);

            RuleFor(ingredient => ingredient.CarbohydratesPer100g)
                .InclusiveBetween(Constants.MinMacronutrients, Constants.MaxMacronutrients);

            RuleFor(ingredient => ingredient.CaloriesPer100g)
                .InclusiveBetween(Constants.MinCalories, Constants.MaxCalories);

            RuleFor(ingredient => ingredient.DietaryRestrictionsId)
                .Must(restrictions => restrictions.Count() == restrictions.Distinct().Count())
                .WithMessage("Every dietary restriction can be used only once");

            RuleForEach(ingredient => ingredient.DietaryRestrictionsId)
                .MustAsync(async (id, cancellation) => await _unitOfWork.DietaryRestrictionRepository
                    .GetFirstOrDefaultAsync(x => x.Id == id) != null)
                .WithMessage((ingredient, restrictionId) => $"There are no dietary restiction with such id: {restrictionId}");
        }
    }
}
