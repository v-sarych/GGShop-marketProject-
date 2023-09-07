using ShopApi.Model.Entities.DTO.Comment;
using ShopApi.Model.Entities.DTO.Order;
using ShopApi.Model.Entities.DTO.ShoppingCart;
using ShopDb.Entities;

namespace ShopApi.Model.Entities.DTO.User
{
    public class GetUserDTO
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<GetShoppingCartElementDTO> UserShoppingCartItems { get; set; }
        public ICollection<GetUserOrderDTO> Orders { get; set; }
    }
}
