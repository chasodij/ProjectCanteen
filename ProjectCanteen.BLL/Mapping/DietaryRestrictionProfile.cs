using AutoMapper;
using ProjectCanteen.BLL.DTOs.DietaryRestriction;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.BLL.Mapping
{
    public class DietaryRestrictionProfile : Profile
    {
        public DietaryRestrictionProfile()
        {
            CreateMap<DietaryRestriction, DietaryRestrictionDTO>().ReverseMap();
            CreateMap<DietaryRestriction, CreateDietaryRestrictionDTO>().ReverseMap();
        }
    }
}
