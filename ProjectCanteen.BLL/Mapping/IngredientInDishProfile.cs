using AutoMapper;
using ProjectCanteen.BLL.DTOs.IngredientInDish;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.BLL.Mapping
{
    public class IngredientInDishProfile : Profile
    {
        public IngredientInDishProfile()
        {
            CreateMap<IngredientInDish, IngredientInDishDTO>()
                .ForMember(dest => dest.IngredientName, opt => opt.MapFrom(x => x.Ingredient.Name));
        }
    }
}
