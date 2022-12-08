using AutoMapper;
using ProjectCanteen.BLL.DTOs.Ingredient;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.BLL.Mapping
{
    public class IngredientProfile : Profile
    {
        public IngredientProfile()
        {
            CreateMap<Ingredient, IngredientDTO>()
                .ForMember(dest => dest.DietaryRestrictionsId, opt => opt.MapFrom(src =>
                    src.DietaryRestrictions.Select(restriction => restriction.Id)));

            CreateMap<IngredientDTO, Ingredient>()
                .ForMember(dest => dest.DietaryRestrictions, opt => opt.MapFrom(src =>
                    src.DietaryRestrictionsId.Select(id => new DietaryRestriction { Id = id })));

            CreateMap<CreateIngredientDTO, Ingredient>()
                .ForMember(dest => dest.DietaryRestrictions, opt => opt.MapFrom(src =>
                    src.DietaryRestrictionsId.Select(id => new DietaryRestriction { Id = id })));
        }
    }
}
