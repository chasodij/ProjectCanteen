using AutoMapper;
using ProjectCanteen.BLL.DTOs.Order;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.BLL.Mapping.Converters
{
    public class OrderToFullOrdersOfTheDayConverter : ITypeConverter<List<Order>, FullOrdersOfTheDayDTO>
    {
        public FullOrdersOfTheDayDTO Convert(List<Order> source, FullOrdersOfTheDayDTO destination, ResolutionContext context)
        {
            var result = new FullOrdersOfTheDayDTO
            {
                Date = source.First().MenuOfTheDay.Day,
                Dishes = source.SelectMany(order => order.OrderItems)
                    .GroupBy(item => item.DishId).Select(group =>
                        new DTOs.OrderItem.FullOrderItemDTO
                        {
                            DishId = group.Key,
                            DishName = group.First().Dish.Name,
                            Portions = group.Sum(x => x.Portions)
                        }).ToList()
            };

            return result;
        }
    }
}
