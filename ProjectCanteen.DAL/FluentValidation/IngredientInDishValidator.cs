using FluentValidation;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.DAL.FluentValidation
{
    public class IngredientInDishValidator : AbstractValidator<IngredientInDish>
    {
        public IngredientInDishValidator()
        {
            RuleFor(ingredient => ingredient.Ingredient).NotNull().SetValidator(new IngredientValidator());
            RuleFor(ingredient => ingredient.Dish).NotNull().SetValidator(new DishValidator());
            RuleFor(ingredient => ingredient.AmountInGrams).InclusiveBetween(Constants.MinIngredientInDishAmount, Constants.MaxIngredientInDishAmount);
        }
    }
}
