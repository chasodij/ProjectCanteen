using FluentValidation;
using ProjectCanteen.BLL.DTOs.OrderItem;
using ProjectCanteen.DAL;

namespace ProjectCanteen.BLL.FluentValidation.Order
{
    public class AddDishToOrderDTOValidator : AbstractValidator<OrderItemDTO>
    {
        public AddDishToOrderDTOValidator()
        {
            RuleFor(item => item.DishId).GreaterThanOrEqualTo(Constants.MinId);
            RuleFor(item => item.Portions).InclusiveBetween(Constants.MinPortionsCount, Constants.MaxPortionsCount);
        }
    }
}
