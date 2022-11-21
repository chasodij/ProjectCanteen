using FluentValidation;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.DAL.FluentValidation
{
    public class DishValidator : AbstractValidator<Dish>
    {
        public DishValidator()
        {
            RuleFor(dish => dish.Name).NotEmpty().Length(Constants.MinDishNameLength, Constants.MaxDishNameLength);
            RuleFor(dish => dish.Price).InclusiveBetween(Constants.MinPriceUAH, Constants.MaxPriceUAH);
            RuleForEach(dish => dish.IngredientInDishes).SetValidator(new IngredientInDishValidator());
            RuleFor(dish => dish.Canteen).NotNull().SetValidator(new CanteenValidator());
            RuleForEach(dish => dish.MenuOfTheDays).SetValidator(new MenuOfTheDayValidator());
            RuleForEach(dish => dish.OrderItems).SetValidator(new OrderItemValidator());
        }
    }
}
