using ShopApiCore.Entities.DTO.Comment;
using ShopApiCore.Entities.DTO.Order;
using ShopApiCore.Entities.DTO.ShoppingCart;
using ShopDb.Entities;

namespace ShopApiCore.Entities.DTO.User
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
