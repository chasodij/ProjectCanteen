using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.DAL.EntityConfiguration
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(item => new { item.Order, item.Dish });

            builder.HasOne(item => item.Order).WithMany(order => order.OrderItems);
            builder.HasOne(item => item.Dish).WithMany(dish => dish.OrderItems);
        }
    }
}
