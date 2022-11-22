using FluentValidation;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.DAL.FluentValidation
{
    public class DishValidator : AbstractValidator<Dish>
    {
        public DishValidator()
        {
            RuleFor(dish => dish.Name).NotEmpty().Length(Constants.MinDishNameLength, Constants.MaxDishNameLength);
            RuleFor(dish => dish.Price).InclusiveBetween(Constants.MinPriceUAH, Constants.MaxPriceUAH)
                .ScalePrecision(Constants.PriceUAHScale, Constants.PriceUAHPrecision);
            RuleFor(dish => dish.MenuSection).NotNull().SetValidator(new MenuSectionValidator());
            RuleForEach(dish => dish.IngredientInDishes).SetValidator(new IngredientInDishValidator());
            RuleForEach(dish => dish.MenuOfTheDays).SetValidator(new MenuOfTheDayValidator());
            RuleForEach(dish => dish.OrderItems).SetValidator(new OrderItemValidator());
        }
    }
}
