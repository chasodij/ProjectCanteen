using ProjectCanteen.BLL.DTOs.Base;
using ProjectCanteen.BLL.DTOs.Order;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.BLL.Services.Interfaces
{
    public interface IOrderService
    {
        Task<(IEnumerable<FullOrderDTO> orders, int totalCount)> GetOrdersAsync(int page, int pageSize, int workerId);
        Task<FullOrdersOfTheDayDTO> GetOrdersOfTheDayAsync(DateTime date, CanteenWorker canteenWorker);
        Task<BaseResponseDTO> CreateOrderAsync(CreateOrderDTO createOrderDTO, CanteenWorker canteenWorker);
        Task<BaseResponseDTO> CreateOrderAsync(CreateOrderDTO createOrderDTO, Parent parent);
        Task UpdateOrderAsync(UpdateOrderDTO orderDTO);
        Task<bool> IsOrderExistWithIdAsync(int id);
        Task<bool> DeleteOrderAsync(int id);
        Task<(string fullStudentName, bool isSuccess, List<int> ordersId, int canteenId)> RequestOrders(string tagId, User userTerminal);
        Task<bool> CompleteOrder(int orderId, CanteenWorker worker);
    }
}
