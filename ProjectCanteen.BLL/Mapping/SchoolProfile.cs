using AutoMapper;
using ProjectCanteen.BLL.DTOs.School;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.BLL.Mapping
{
    public class SchoolProfile : Profile
    {
        public SchoolProfile()
        {
            CreateMap<School, SchoolDTO>().ReverseMap();
            CreateMap<School, CreateSchoolDTO>().ReverseMap();
        }
    }
}
