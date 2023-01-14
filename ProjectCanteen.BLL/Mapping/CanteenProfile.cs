using AutoMapper;
using ProjectCanteen.BLL.DTOs.Canteen;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.BLL.Mapping
{
    public class CanteenProfile : Profile
    {
        public CanteenProfile()
        {
            CreateMap<UpdateCanteenDTO, Canteen>()
                .ForMember(dest => dest.Terminal, opt => opt.MapFrom(src => src.TerminalId == null ? null : new User { Id = src.TerminalId }))
                .ForMember(dest => dest.School, opt => opt.MapFrom(src => new School { Id = src.SchoolId }));

            CreateMap<CreateCanteenDTO, Canteen>()
                .ForMember(dest => dest.Terminal, opt => opt.MapFrom(src => src.TerminalId == null ? null : new User { Id = src.TerminalId }))
                .ForMember(dest => dest.School, opt => opt.MapFrom(src => new School { Id = src.SchoolId }));

            CreateMap<Canteen, FullCanteenDTO>()
                .ForMember(dest => dest.TerminalId, opt => opt.MapFrom(src => src.Terminal == null ? null : src.Terminal.Id))
                .ForMember(dest => dest.SchoolId, opt => opt.MapFrom(src => src.School.Id))
                .ForMember(dest => dest.SchoolName, opt => opt.MapFrom(src => src.School.Name));

        }
    }
}
