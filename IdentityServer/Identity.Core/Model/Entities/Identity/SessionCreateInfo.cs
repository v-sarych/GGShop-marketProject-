namespace Identity.Core.Model.Entities.Identity;

public class SessionCreateInfo
{
    public Guid Id { get; set; }
    public long UserId { get; set; }
    public string Role { get; set; }
    public string RefreshToken { get; set; }
}