namespace IdentityServer.Model.Entities.Identity
{
    public class TokenPayload
    {
        public long UsertId { get; set; }
        public string Role { get; set; }
        public Guid JwtId { get; set; }
        public string RefreshToken { get;set; }
    }
}
