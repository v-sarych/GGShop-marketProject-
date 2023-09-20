using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopDb.Entities
{
    public class Session
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public string Role { get; set; }
        public string RefreshTokenHash { get; set; }

        public long UserId { get; set; }
    }
}
