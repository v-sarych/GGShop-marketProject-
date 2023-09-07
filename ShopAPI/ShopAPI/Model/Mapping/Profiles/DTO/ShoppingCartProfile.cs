using AutoMapper;
using ShopApi.Model.Entities.DTO.ShoppingCart;
using ShopDb.Entities;

namespace ShopApi.Model.Mapping.Profiles.DTO
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
