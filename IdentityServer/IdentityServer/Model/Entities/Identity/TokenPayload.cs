namespace IdentityServer.Model.Entities.Identity
{
    public class TokenPayload
    {
        public Guid Id { get; set; }
        public long UsertId { get; set; }
        public string Role { get; set; }
        public string RefreshToken { get;set; }
    }
}
