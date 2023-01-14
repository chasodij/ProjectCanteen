using ProjectCanteen.BLL.DTOs.OrderItem;

namespace ProjectCanteen.BLL.DTOs.Student
{
    public class OrderOfStudentDTO
    {
        public int Id { get; set; }
        public DateTime OrderTime { get; set; }
        public int PurchaserId { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}
