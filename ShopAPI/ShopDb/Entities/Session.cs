namespace ShopDb.Entities
{
    public class Session
    {
        public long Id { get; set; }
        public string Role { get; set; }
        public Guid JwtId { get; set; }
        public string RefreshTokenHash { get; set; }

        public long UserId { get; set; }
    }
}
