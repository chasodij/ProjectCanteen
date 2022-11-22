using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.DAL.EntityConfiguration
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(item => new { item.OrderId, item.DishId });

            builder.HasOne(item => item.Order).WithMany(order => order.OrderItems).HasForeignKey(item => item.OrderId);
            builder.HasOne(item => item.Dish).WithMany(dish => dish.OrderItems).HasForeignKey(item => item.DishId);

            builder.Property(item => item.DishPrice).HasPrecision(Constants.PriceUAHPrecision, Constants.PriceUAHScale);
        }
    }
}
