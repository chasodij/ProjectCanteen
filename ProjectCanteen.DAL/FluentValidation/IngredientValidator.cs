using FluentValidation;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.DAL.FluentValidation
{
    public class IngredientValidator : AbstractValidator<Ingredient>
    {
        public IngredientValidator()
        {
            RuleFor(ingredient => ingredient.Name).NotEmpty().Length(Constants.MinTitleLength, Constants.MaxTitleLength);
            RuleFor(ingredient => ingredient.ProteinsPer100g).InclusiveBetween(Constants.MinMacronutrients, Constants.MaxMacronutrients);
            RuleFor(ingredient => ingredient.FatsPer100g).InclusiveBetween(Constants.MinMacronutrients, Constants.MaxMacronutrients);
            RuleFor(ingredient => ingredient.CarbohydratesPer100g).InclusiveBetween(Constants.MinMacronutrients, Constants.MaxMacronutrients);
            RuleFor(ingredient => ingredient.CaloriesPer100g).InclusiveBetween(Constants.MinCalories, Constants.MaxCalories);
            RuleFor(ingredient => ingredient.Canteen).SetValidator(new CanteenValidator());
            RuleForEach(ingredient => ingredient.DietaryRestrictions).SetValidator(new DietaryRestrictionValidator());
        }
    }
}
