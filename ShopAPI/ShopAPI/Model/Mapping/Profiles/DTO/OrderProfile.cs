using AutoMapper;
using ShopApi.Model.Entities.DTO.Order;
using ShopApi.Model.Entities.DTO.OrderItem;
using ShopDb.Entities;

namespace ShopApi.Model.Mapping.Profiles.DTO
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
