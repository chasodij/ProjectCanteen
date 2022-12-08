using AutoMapper;
using ProjectCanteen.BLL.DTOs.MenuOfTheDay;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.BLL.Mapping
{
    public class MenuOfTheDayProfile : Profile
    {
        public MenuOfTheDayProfile()
        {
            CreateMap<MenuOfTheDay, MenuOfTheDayDTO>()
                .ForMember(dest => dest.DishesId, opt => opt.MapFrom(src => src.Dishes
                    .Select(x => x.Id)));

            CreateMap<MenuOfTheDayDTO, MenuOfTheDay>()
                .ForMember(dest => dest.Dishes, opt => opt.MapFrom(src => src.DishesId
                    .Select(x => new Dish { Id = x })));

            CreateMap<MenuOfTheDay, CreateMenuOfTheDayDTO>()
                .ForMember(dest => dest.DishesId, opt => opt.MapFrom(src => src.Dishes
                    .Select(x => x.Id)));

            CreateMap<CreateMenuOfTheDayDTO, MenuOfTheDay>()
                .ForMember(dest => dest.Dishes, opt => opt.MapFrom(src => src.DishesId
                    .Select(x => new Dish { Id = x })));
        }
    }
}
