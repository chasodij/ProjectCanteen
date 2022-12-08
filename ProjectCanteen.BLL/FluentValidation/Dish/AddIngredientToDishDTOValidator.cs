using FluentValidation;
using ProjectCanteen.BLL.DTOs.Dish;
using ProjectCanteen.DAL;

namespace ProjectCanteen.BLL.FluentValidation
{
    public class AddIngredientToDishDTOValidator : AbstractValidator<AddIngredientToDishDTO>
    {
        public AddIngredientToDishDTOValidator()
        {
            RuleFor(ingredient => ingredient.IngredientId).GreaterThanOrEqualTo(Constants.MinId);
            RuleFor(ingredient => ingredient.AmountInGrams).InclusiveBetween(Constants.MinIngredientInDishAmount, Constants.MaxIngredientInDishAmount);
        }
    }
}
