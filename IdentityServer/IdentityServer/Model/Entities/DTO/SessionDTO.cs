using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Model.Entities.DTO
{
    public class SessionDTO
    {
        public Guid Id { get; set; }
        public string Role { get; set; }
    }
}
