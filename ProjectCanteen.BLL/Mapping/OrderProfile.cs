using AutoMapper;
using ProjectCanteen.BLL.DTOs.Order;
using ProjectCanteen.BLL.DTOs.OrderItem;
using ProjectCanteen.BLL.DTOs.Student;
using ProjectCanteen.BLL.Mapping.Converters;
using ProjectCanteen.DAL.Entities;

namespace ProjectCanteen.BLL.Mapping
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
            CreateMap<OrderItem, FullOrderItemDTO>()
                .ForMember(dest => dest.DishName, opt => opt.MapFrom(src => src.Dish.Name));

            CreateMap<UpdateOrderDTO, Order>()
                .ForMember(dest => dest.Purchaser, opt => opt.MapFrom(src => new Parent { Id = src.PurchaserId }))
                .ForMember(dest => dest.Student, opt => opt.MapFrom(src => new Student { Id = src.StudentId }));

            CreateMap<CreateOrderDTO, Order>()
                .ForMember(dest => dest.Student, opt => opt.MapFrom(src => new Student { Id = src.StudentId }));

            CreateMap<Order, OrderOfStudentDTO>()
                .ForMember(dest => dest.PurchaserId, opt => opt.MapFrom(src => src.Purchaser.Id));

            CreateMap<Order, FullOrderDTO>()
                .ForMember(dest => dest.PurchaserId, opt => opt.MapFrom(src => src.Purchaser.Id))
                .ForMember(dest => dest.PurchaserFullName, opt => opt.MapFrom(src =>
                    src.Purchaser.User.FirstName + " " +
                    src.Purchaser.User.LastName + " " +
                    src.Purchaser.User.Patronymic
                ))
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.Student.Id))
                .ForMember(dest => dest.StudentFullName, opt => opt.MapFrom(src =>
                    src.Student.User.FirstName + " " +
                    src.Student.User.LastName + " " +
                    src.Student.User.Patronymic
                ))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.OrderStatus.Name))
                .ForMember(dest => dest.OrderedOnDate, opt => opt.MapFrom(src => src.MenuOfTheDay.Day));

            CreateMap<List<Order>, FullOrdersOfTheDayDTO>()
                .ConvertUsing<OrderToFullOrdersOfTheDayConverter>();
        }
    }
}
