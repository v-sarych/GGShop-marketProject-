using AutoMapper;
using ShopAPICore.Entities.DTO.ShoppingCart;
using ShopDb.Entities;

namespace ShopAPICore.Mapping.Profiles.DTO;

public class ShoppingCartProfile : Profile
{
    public ShoppingCartProfile()
    {
        CreateProjection<UserShoppingCartItem, GetShoppingCartElementDTO>();

        CreateMap<AddShoppingCartItemDTO, UserShoppingCartItem>();
    }
}