using ProjectCanteen.BLL.DTOs.OrderItem;

namespace ProjectCanteen.BLL.DTOs.Order
{
    public class FullOrderDTO
    {
        public int Id { get; set; }
        public DateTime OrderTime { get; set; }
        public int PurchaserId { get; set; }
        public string PurchaserFullName { get; set; }
        public int StudentId { get; set; }
        public string StudentFullName { get; set; }
        public string Status { get; set; }
        public DateTime OrderedOnDate { get; set; }

        public List<FullOrderItemDTO> OrderItems { get; set; }
    }
}
