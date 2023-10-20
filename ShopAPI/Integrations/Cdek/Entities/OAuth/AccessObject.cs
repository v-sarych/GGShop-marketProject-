using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.Cdek.Entities.OAuth
{
    public class AccessObject
    {
        public string Access_token { get; set; }
        public string Token_type { get; set; }
        public string Expires_in { get; set; }
        public string Scope { get; set; }
        public string Jti { get; set;}
    }
}
