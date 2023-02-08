using AutoMapper;
using ProjectCanteen.BLL.DTOs.MenuSection;
using ProjectCanteen.BLL.Mapping.Converters;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.BLL.Mapping
{
    public class MenuSectionProfile : Profile
    {
        public MenuSectionProfile()
        {
            CreateMap<MenuSection, MenuSectionDTO>().ReverseMap();
            CreateMap<MenuSection, CreateMenuSectionDTO>().ReverseMap();

            CreateMap<List<Dish>, List<MenuSectionWithDishesDTO>>()
                .ConvertUsing<DishesToMenuSectionsWithDishesConverter>();
        }
    }
}
