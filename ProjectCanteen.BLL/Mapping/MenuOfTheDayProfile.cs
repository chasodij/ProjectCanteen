using AutoMapper;
using ProjectCanteen.BLL.DTOs.MenuOfTheDay;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.BLL.Mapping
{
    public class MenuOfTheDayProfile : Profile
    {
        public MenuOfTheDayProfile()
        {
            CreateMap<UpdateMenuOfTheDayDTO, MenuOfTheDay>()
                .ForMember(dest => dest.Dishes, opt => opt.MapFrom(src => src.DishesId
                    .Select(x => new Dish { Id = x })));

            CreateMap<CreateMenuOfTheDayDTO, MenuOfTheDay>()
                .ForMember(dest => dest.Dishes, opt => opt.MapFrom(src => src.DishesId
                    .Select(x => new Dish { Id = x })));

            CreateMap<MenuOfTheDay, FullMenuOfTheDayDTO>()
                .ForMember(dest => dest.MenuSections, opt => opt.MapFrom(src => src.Dishes));
        }
    }
}
