using FluentValidation;
using ProjectCanteen.BLL.DTOs.Order;
using ProjectCanteen.BLL.DTOs.OrderItem;
using ProjectCanteen.DAL;

namespace ProjectCanteen.BLL.FluentValidation.Order
{
    public class CreateOrderDTOValidator : AbstractValidator<CreateOrderDTO>
    {
        public CreateOrderDTOValidator(IValidator<OrderItemDTO> validator)
        {
            RuleFor(order => order.StudentId).GreaterThanOrEqualTo(Constants.MinId);
            RuleForEach(order => order.OrderItems).SetValidator(validator);
        }
    }
}
