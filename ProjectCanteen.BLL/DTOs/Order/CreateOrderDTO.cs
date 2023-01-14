using ProjectCanteen.BLL.DTOs.OrderItem;

namespace ProjectCanteen.BLL.DTOs.Order
{
    public class CreateOrderDTO
    {
        public int StudentId { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
        public int MenuOfTheDayId { get; set; }
    }
}
