using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.DAL.EntityConfiguration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(order => order.Id);

            builder.HasOne(order => order.Purchaser).WithMany(parent => parent.Orders);
            builder.HasOne(order => order.Student).WithMany(student => student.Orders);
            builder.HasMany(order => order.OrderItems).WithOne(item => item.Order);


        }
    }
}
