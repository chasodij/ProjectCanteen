using FluentValidation;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.DAL.FluentValidation
{
    public class OrderItemValidator : AbstractValidator<OrderItem>
    {
        public OrderItemValidator()
        {
            RuleFor(item => item.Order).NotNull().SetValidator(new OrderValidator());
            RuleFor(item => item.Dish).NotNull().SetValidator(new DishValidator());
            RuleFor(item => item.Portions).InclusiveBetween(Constants.MinPortionsCount, Constants.MaxPortionsCount);
            RuleFor(item => item.DishPrice).InclusiveBetween(Constants.MinPriceUAH, Constants.MaxPriceUAH)
                .ScalePrecision(Constants.PriceUAHScale, Constants.PriceUAHPrecision);
        }
    }
}
