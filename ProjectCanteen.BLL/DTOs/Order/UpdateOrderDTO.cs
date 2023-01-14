using ProjectCanteen.BLL.DTOs.OrderItem;

namespace ProjectCanteen.BLL.DTOs.Order
{
    public class UpdateOrderDTO
    {
        public int Id { get; set; }
        public DateTime OrderTime { get; set; }
        public int PurchaserId { get; set; }
        public int StudentId { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}
