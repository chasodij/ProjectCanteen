using AutoMapper;
using ProjectCanteen.BLL.DTOs.Dish;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.BLL.Mapping
{
    public class DishProfile : Profile
    {
        public DishProfile()
        {
            CreateMap<Dish, DishDTO>()
                .ForMember(dest => dest.MenuSectionId, opt => opt.MapFrom(src => src.MenuSection.Id))
                .ForMember(dest => dest.IngredientsInDish, opt => opt.MapFrom(src => src.IngredientInDishes
                    .Select(x => new AddIngredientToDishDTO { IngredientId = x.IngredientId, AmountInGrams = x.AmountInGrams })));

            CreateMap<DishDTO, Dish>()
                .ForMember(dest => dest.MenuSection, opt => opt.MapFrom(src => new MenuSection { Id = src.MenuSectionId }))
                .ForMember(dest => dest.IngredientInDishes, opt => opt.MapFrom(src => src.IngredientsInDish
                    .Select(x => new IngredientInDish { DishId = src.Id, IngredientId = x.IngredientId, AmountInGrams = x.AmountInGrams })));

            CreateMap<Dish, CreateDishDTO>()
                .ForMember(dest => dest.MenuSectionId, opt => opt.MapFrom(src => src.MenuSection.Id))
                .ForMember(dest => dest.IngredientsInDish, opt => opt.MapFrom(src => src.IngredientInDishes
                    .Select(x => new AddIngredientToDishDTO { IngredientId = x.IngredientId, AmountInGrams = x.AmountInGrams })));

            CreateMap<CreateDishDTO, Dish>()
                .ForMember(dest => dest.MenuSection, opt => opt.MapFrom(src => new MenuSection { Id = src.MenuSectionId }))
                .ForMember(dest => dest.IngredientInDishes, opt => opt.MapFrom(src => src.IngredientsInDish
                    .Select(x => new IngredientInDish { IngredientId = x.IngredientId, AmountInGrams = x.AmountInGrams })));
        }
    }
}
