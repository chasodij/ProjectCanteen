using ProjectCanteen.BLL.DTOs.OrderItem;

namespace ProjectCanteen.BLL.DTOs.Order
{
    public class FullOrdersOfTheDayDTO
    {
        public DateTime Date { get; set; }
        public List<FullOrderItemDTO> Dishes { get; set; }
    }
}
