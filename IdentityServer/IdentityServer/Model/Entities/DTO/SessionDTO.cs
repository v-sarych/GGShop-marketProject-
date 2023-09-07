using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Model.Entities.DTO
{
    public class SessionDTO
    {
        public long Id { get; set; }
        public string Role { get; set; }
        public Guid JwtId { get; set; }
    }
}
