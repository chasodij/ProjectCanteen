using AutoMapper;
using ProjectCanteen.BLL.DTOs.Class;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.BLL.Mapping
{
    public class ClassProfile : Profile
    {
        public ClassProfile()
        {
            CreateMap<Class, FullClassDTO>()
                .ForMember(dest => dest.SchoolId, opt => opt.MapFrom(src => src.School.Id))
                .ForMember(dest => dest.SchoolName, opt => opt.MapFrom(src => src.School.Name))
                .ForMember(dest => dest.ClassTeacherId, opt => opt.MapFrom(src => src.ClassTeacher.Id))
                .ForMember(dest => dest.ClassTeacherFullName, opt => opt.MapFrom(src => 
                    src.ClassTeacher.User.LastName + " " + src.ClassTeacher.User.FirstName + " " + src.ClassTeacher.User.Parent));

            CreateMap<UpdateClassDTO, Class>()
                .ForMember(dest => dest.School, opt => opt.MapFrom(src => new School { Id = src.SchoolId }));

            CreateMap<CreateClassDTO, Class>()
                .ForMember(dest => dest.School, opt => opt.MapFrom(src => new School { Id = src.SchoolId }));


            CreateMap<Class, ClassOrdersDTO>()
                .ForMember(dest => dest.ClassTeacherId, opt => opt.MapFrom(src => src.ClassTeacher.Id))
                .ForMember(dest => dest.ClassTeacherFullName, opt => opt
                    .MapFrom(src => src.ClassTeacher.User.LastName + src.ClassTeacher.User.FirstName + src.ClassTeacher.User.Patronymic))
                .ForMember(dest => dest.StudentsOrders, opt => opt.MapFrom(src => src.Students));
        }
    }
}
