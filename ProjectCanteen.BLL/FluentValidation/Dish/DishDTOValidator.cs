using FluentValidation;
using ProjectCanteen.BLL.DTOs.Dish;
using ProjectCanteen.DAL;

namespace ProjectCanteen.BLL.FluentValidation
{
    public class DishDTOValidator : AbstractValidator<DishDTO>
    {
        public DishDTOValidator(IValidator<AddIngredientToDishDTO> validator)
        {
            RuleFor(dish => dish.Id).GreaterThanOrEqualTo(Constants.MinId);
            RuleFor(dish => dish.MenuSectionId).GreaterThanOrEqualTo(Constants.MinId);
            RuleFor(dish => dish.Name).NotEmpty().Length(Constants.MinDishNameLength, Constants.MaxDishNameLength);
            RuleFor(dish => dish.Price).InclusiveBetween(Constants.MinPriceUAH, Constants.MaxPriceUAH)
                .ScalePrecision(Constants.PriceUAHScale, Constants.PriceUAHPrecision);
            RuleForEach(dish => dish.IngredientsInDish).SetValidator(validator);
        }
    }
}
