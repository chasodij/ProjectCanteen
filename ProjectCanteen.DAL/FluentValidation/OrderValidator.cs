using FluentValidation;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.DAL.FluentValidation
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(order => order.OrderTime).GreaterThan(Constants.MinDate);
            RuleFor(order => order.Purchaser).NotNull().SetValidator(new ParentValidator());
            RuleFor(order => order.Student).NotNull().SetValidator(new StudentValidator());
            RuleForEach(order => order.OrderItems).SetValidator(new OrderItemValidator());
        }
    }
}
