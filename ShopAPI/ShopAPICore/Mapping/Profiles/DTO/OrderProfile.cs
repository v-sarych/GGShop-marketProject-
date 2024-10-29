using AutoMapper;
using ShopAPICore.Entities.DTO.Order;
using ShopAPICore.Entities.DTO.OrderItem;
using ShopDb.Entities;

namespace ShopAPICore.Mapping.Profiles.DTO;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateProjection<Order, GetUserOrderDTO>();
        CreateProjection<OrderItem, GetOrderItemDTO>();

        CreateProjection<Order, GetFullOrderInfoDTO>();

        //CreateMap<Order, GetUserOrderDTO>();
        CreateMap<CreateOrderDTO, Order>();
        CreateMap<CreateOrderItemDTO, OrderItem>();
    }
}