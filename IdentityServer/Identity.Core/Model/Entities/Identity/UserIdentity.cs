namespace Identity.Core.Model.Entities.Identity;

public class UserIdentity
{
    public long Id { get; set; }
    public string Role { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
}