using AutoMapper;
using ShopApiCore.Entities.DTO.ShoppingCart;
using ShopDb.Entities;

namespace ShopApiCore.Mapping.Profiles.DTO
{
    public class ShoppingCartProfile : Profile
    {
        public ShoppingCartProfile()
        {
            CreateProjection<UserShoppingCartItem, GetShoppingCartElementDTO>();

            CreateMap<AddShoppingCartItemDTO, UserShoppingCartItem>();
        }
    }
}
