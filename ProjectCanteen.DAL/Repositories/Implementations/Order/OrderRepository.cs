using Microsoft.EntityFrameworkCore;
using ProjectCanteen.DAL.Entities;
using ProjectCanteen.DAL.Repositories.Implementations.NewFolder;
using ProjectCanteen.DAL.Repositories.Interfaces.Order;

namespace ProjectCanteen.DAL.Repositories.Implementations.Order
{
    public class OrderRepository : BaseRepository<Entities.Order>, IOrderRepository
    {
        public OrderRepository()
        {
            DefaultInclude = func => func
                            .Include(x => x.OrderItems).ThenInclude(x => x.Dish)
                            .Include(x => x.Purchaser).ThenInclude(x => x.User)
                            .Include(x => x.Student).ThenInclude(x => x.User)
                            .Include(x => x.OrderStatus)
                            .Include(x => x.MenuOfTheDay);
        }

        public void ChangeStatusTo(int orderId, string name)
        {
            var connectedEntity = DbContext.Orders.FirstOrDefault(x => x.Id == orderId);

            var connectedStatus = DbContext.OrderStatuses.First(x => x.Name == name);

            connectedEntity.OrderStatus = connectedStatus;
        }

        public async Task<Entities.Order?> GetByIdAsync(int id)
        {
            return await GetFirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<OrderStatus> GetStatusByName(string name)
        {
            return DbContext.OrderStatuses.First(x => x.Name == name);
        }

        public async override Task UpdateAsync(Entities.Order entity)
        {
            var connected_entity = DbContext.Orders.FirstOrDefault(x => x.Id == entity.Id);

            if (connected_entity == null)
            {
                throw new Exception();
            }

            DbContext.Entry(connected_entity).CurrentValues.SetValues(entity);

            var items_from_context = DbContext.OrderItems.Where(x => x.OrderId == entity.Id);

            foreach (var item in items_from_context)
            {
                if (!entity.OrderItems.Exists(x => x.DishId == item.DishId))
                {
                    DbContext.Entry(item).State = EntityState.Deleted;
                }
            }

            foreach (var item_to_add in entity.OrderItems)
            {
                var connected_item = await DbContext.OrderItems.FirstOrDefaultAsync(x =>
                    x.DishId == item_to_add.DishId && x.OrderId == entity.Id);

                if (connected_item != null)
                {
                    connected_item.Portions = item_to_add.Portions;
                }

                else
                {
                    DbContext.OrderItems.Add(item_to_add);
                }
            }

            await base.UpdateAsync(connected_entity);
        }
    }
}
