using AutoMapper;
using ProjectCanteen.BLL.DTOs.Parent;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.BLL.Mapping
{
    public class ParentProfile : Profile
    {
        public ParentProfile()
        {
            CreateMap<Parent, FullParentWithoutChildrenDTO>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Patronymic, opt => opt.MapFrom(src => src.User.Patronymic));

            CreateMap<Parent, FullParentDTO>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Patronymic, opt => opt.MapFrom(src => src.User.Patronymic))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.Children));

            CreateMap<UpdateParentDTO, Parent>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src =>
                    new User
                    {
                        FirstName = src.FirstName,
                        LastName = src.LastName,
                        Patronymic = src.Patronymic
                    }))
                .ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.ChildrenId.Select(x =>
                    new Student
                    {
                        Id = x
                    })));
        }
    }
}
