using FluentValidation;
using ProjectCanteen.BLL.DTOs.Order;
using ProjectCanteen.BLL.DTOs.OrderItem;
using ProjectCanteen.DAL;

namespace ProjectCanteen.BLL.FluentValidation.Order
{
    public class OrderDTOValidator : AbstractValidator<UpdateOrderDTO>
    {
        public OrderDTOValidator(IValidator<OrderItemDTO> validator)
        {
            RuleFor(order => order.Id).GreaterThanOrEqualTo(Constants.MinId);
            RuleFor(order => order.OrderTime).GreaterThan(Constants.MinDate);
            RuleFor(order => order.PurchaserId).GreaterThanOrEqualTo(Constants.MinId);
            RuleFor(order => order.StudentId).GreaterThanOrEqualTo(Constants.MinId);
            RuleForEach(order => order.OrderItems).SetValidator(validator);
        }
    }
}
