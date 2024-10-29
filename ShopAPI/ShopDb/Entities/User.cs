namespace ShopDb.Entities;

public class User
{
    public long Id { get; set; }
    public string Role { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }

    public ICollection<UserShoppingCartItem> UserShoppingCartItems { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<Session> Sessions { get; set; }
    public ICollection<Comment> WrittenComments { get; set; }
}