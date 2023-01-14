using AutoMapper;
using ProjectCanteen.BLL.DTOs.Student;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.BLL.Mapping
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentOrdersDTO>()
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(x => x.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(x => x.User.LastName))
                .ForMember(dest => dest.Patronymic, opt => opt.MapFrom(x => x.User.Patronymic))
                .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders));

            CreateMap<Student, FullStudentWithoutParentsDTO>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(x => x.User.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(x => x.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(x => x.User.LastName))
                .ForMember(dest => dest.Patronymic, opt => opt.MapFrom(x => x.User.Patronymic));

            CreateMap<Student, FullStudentDTO>()
               .ForMember(dest => dest.UserId, opt => opt.MapFrom(x => x.User.Id))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(x => x.User.Email))
               .ForMember(dest => dest.FirstName, opt => opt.MapFrom(x => x.User.FirstName))
               .ForMember(dest => dest.LastName, opt => opt.MapFrom(x => x.User.LastName))
               .ForMember(dest => dest.Patronymic, opt => opt.MapFrom(x => x.User.Patronymic))
               .ForMember(dest => dest.ClassName, opt => opt.MapFrom(x => x.Class.ClassName))
               .ForMember(dest => dest.Parents, opt => opt.MapFrom(x => x.Parents))
               .ForMember(dest => dest.DietaryRestrictions, opt => opt.MapFrom(x => x.DietaryRestrictions));

            CreateMap<UpdateStudentDTO, Student>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(x =>
                new User
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Patronymic = x.Patronymic
                }))
                .ForMember(dest => dest.Class, opt => opt.MapFrom(x => new Class { Id = x.ClassId }))
                .ForMember(dest => dest.Parents, opt => opt.MapFrom(x => x.ParentsId
                    .Select(x => new Parent { Id = x })))
                .ForMember(dest => dest.DietaryRestrictions, opt => opt.MapFrom(x => x.DietaryRestrictionsId
                    .Select(x => new DietaryRestriction { Id = x })));

            CreateMap<UpdateStudentDietaryRestrictionsDTO, Student>()
               .ForMember(dest => dest.DietaryRestrictions, opt => opt.MapFrom(x => x.DietaryRestrictionsId
                   .Select(x => new DietaryRestriction { Id = x })));

            CreateMap<UpdateStudentTagDTO, Student>();
        }
    }
}
