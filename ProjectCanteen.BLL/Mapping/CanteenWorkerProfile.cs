using AutoMapper;
using ProjectCanteen.BLL.DTOs.CanteenWorker;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.BLL.Mapping
{
    public class CanteenWorkerProfile : Profile
    {
        public CanteenWorkerProfile()
        {
            CreateMap<CanteenWorker, FullCanteenWorkerDTO>()
                .ForMember(dest => dest.CanteenName, opt => opt.MapFrom(src => src.Canteen.Name))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Patronymic, opt => opt.MapFrom(src => src.User.Patronymic))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email));

            CreateMap<UpdateCanteenWorkerDTO, CanteenWorker>()
                .ForMember(dest => dest.Canteen, opt => opt.MapFrom(src => 
                    new Canteen { Id = src.CanteenId }))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src =>
                    new User
                    {
                        FirstName = src.FirstName,
                        LastName = src.LastName,
                        Patronymic = src.Patronymic
                    }));
        }
    }
}
