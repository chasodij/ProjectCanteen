using AutoMapper;
using ProjectCanteen.BLL.DTOs.Dish;
using ProjectCanteen.BLL.DTOs.MenuSection;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.BLL.Mapping.Converters
{
    public class DishesToMenuSectionsWithDishesConverter : ITypeConverter<List<Dish>, List<MenuSectionWithDishesDTO>>
    {


        public List<MenuSectionWithDishesDTO> Convert(List<Dish> source, List<MenuSectionWithDishesDTO> destination, ResolutionContext context)
        {
            var result = source.GroupBy(x => x.MenuSection.Id).Select(group =>
                                   new MenuSectionWithDishesDTO
                                   {
                                       Id = group.Key,
                                       Name = group.First().MenuSection.Name,
                                       NumberInMenu = group.First().MenuSection.NumberInMenu,
                                       Dishes = group.Select(dish =>
                                           new ShortDishDTO
                                           {
                                               Id = dish.Id,
                                               Name = dish.Name,
                                               Price = dish.Price
                                           }).ToList()
                                   }).ToList();

            return result;
        }
    }
}
