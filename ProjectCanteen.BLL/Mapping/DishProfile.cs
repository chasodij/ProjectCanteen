using AutoMapper;
using ProjectCanteen.BLL.DTOs.Dish;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.BLL.Mapping
{
    public class DishProfile : Profile
    {
        public DishProfile()
        {
            CreateMap<Dish, FullDishDTO>()
                .ForMember(dest => dest.IngredientsInDish, opt => opt.MapFrom(src => src.IngredientInDishes));

            CreateMap<Dish, ShortDishDTO>();

            CreateMap<UpdateDishDTO, Dish>()
                .ForMember(dest => dest.MenuSection, opt => opt.MapFrom(src => new MenuSection { Id = src.MenuSectionId }))
                .ForMember(dest => dest.IngredientInDishes, opt => opt.MapFrom(src => src.IngredientsInDish
                    .Select(x => new IngredientInDish { DishId = src.Id, IngredientId = x.IngredientId, AmountInGrams = x.AmountInGrams })));

            CreateMap<CreateDishDTO, Dish>()
                .ForMember(dest => dest.MenuSection, opt => opt.MapFrom(src => new MenuSection { Id = src.MenuSectionId }))
                .ForMember(dest => dest.IngredientInDishes, opt => opt.MapFrom(src => src.IngredientsInDish
                    .Select(x => new IngredientInDish { IngredientId = x.IngredientId, AmountInGrams = x.AmountInGrams })));
        }
    }
}
