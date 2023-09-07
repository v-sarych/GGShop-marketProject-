namespace IdentityServer.Model.Entities.Identity
{
    public class SessionCreateInfo
    {
        public long UserId { get; set; }
        public string Role { get; set; }
        public Guid JwtId { get; set; }
        public string RefreshToken { get; set; }
    }
}
