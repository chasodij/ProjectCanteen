using AutoMapper;
using ProjectCanteen.BLL.DTOs.ClassTeacherDTO;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.BLL.Mapping
{
    public class ClassTeacherProfile : Profile
    {
        public ClassTeacherProfile()
        {
            CreateMap<ClassTeacher, ClassTeacherDTO>().ReverseMap();

            CreateMap<UpdateClassTeacherDTO, ClassTeacher>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => 
                    new User
                    {
                        FirstName = src.FirstName,
                        LastName = src.LastName,
                        Patronymic = src.Patronymic
                    }));

            CreateMap<ClassTeacher, FullClassTeacherDTO>()
                .ForMember(dest => dest.ClassName, opt => opt.MapFrom(src => src.Class.ClassName))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Patronymic, opt => opt.MapFrom(src => src.User.Patronymic))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email));

            CreateMap<FullClassTeacherDTO, ClassTeacher>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src =>
                    new User
                    {
                        Id = src.UserId,
                        FirstName = src.FirstName,
                        LastName = src.LastName,
                        Patronymic = src.Patronymic,
                        Email = src.Email
                    }));
        }
    }
}
