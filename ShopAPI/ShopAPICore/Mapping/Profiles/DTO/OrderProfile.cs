using AutoMapper;
using ShopApiCore.Entities.DTO.Order;
using ShopApiCore.Entities.DTO.OrderItem;
using ShopDb.Entities;

namespace ShopApiCore.Mapping.Profiles.DTO
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateProjection<Order, GetUserOrderDTO>();
            CreateProjection<OrderItem, GetOrderItemDTO>();

            CreateProjection<Order, GetFullOrderInfoDTO>()
                .ForMember(m => m.UserPhoneNumber, opt => opt.MapFrom(i => i.User.PhoneNumber));

            //CreateMap<Order, GetUserOrderDTO>();
            CreateMap<CreateOrderDTO, Order>();
            CreateMap<CreateOrderItemDTO, OrderItem>();
        }
    }
}
