using ProjectCanteen.DAL.Repositories.Interfaces.Base;

namespace ProjectCanteen.DAL.Repositories.Interfaces.Order
{
    public interface IOrderRepository : IBaseRepository<Entities.Order>
    {
        Task<Entities.Order?> GetByIdAsync(int id);
        Task<Entities.OrderStatus> GetStatusByName(string name);
        void ChangeStatusTo(int orderId, string name);
    }
}
