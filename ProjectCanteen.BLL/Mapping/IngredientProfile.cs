using AutoMapper;
using ProjectCanteen.BLL.DTOs.Ingredient;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.BLL.Mapping
{
    public class IngredientProfile : Profile
    {
        public IngredientProfile()
        {
            CreateMap<Ingredient, FullIngredientDTO>()
                .ForMember(dest => dest.CanteenId, opt => opt
                    .MapFrom(src => src.Canteen.Id))
                .ForMember(dest => dest.CanteenName, opt => opt
                    .MapFrom(src => src.Canteen.Name))
                .ForMember(dest => dest.DietaryRestrictions, opt => opt.MapFrom(src => src.DietaryRestrictions));

            CreateMap<UpdateIngredientDTO, Ingredient>()
                .ForMember(dest => dest.DietaryRestrictions, opt => opt.MapFrom(src =>
                    src.DietaryRestrictionsId.Select(id => new DietaryRestriction { Id = id })));

            CreateMap<CreateIngredientDTO, Ingredient>()
                .ForMember(dest => dest.DietaryRestrictions, opt => opt.MapFrom(src =>
                    src.DietaryRestrictionsId.Select(id => new DietaryRestriction { Id = id })));
        }
    }
}
