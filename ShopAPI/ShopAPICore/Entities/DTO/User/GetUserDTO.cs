using ShopAPICore.Entities.DTO.Order;
using ShopAPICore.Entities.DTO.ShoppingCart;

namespace ShopAPICore.Entities.DTO.User;

public class GetUserDTO
{
    public string Name { get; set; }
    public string Role { get; set; }
    public string PhoneNumber { get; set; }

    public ICollection<GetShoppingCartElementDTO> UserShoppingCartItems { get; set; }
    public ICollection<GetUserOrderDTO> Orders { get; set; }
}