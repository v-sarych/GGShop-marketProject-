using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.Cdek.Entities.OAuth
{
    public class AuthorizeParametrs
    {
        public string Url {  get; set; }
        public string ContentType {  get; set; }
        public string Grant_type { get; set; }
        public string Client_id { get;set; }
        public string Client_secret { get; set; }
    }
}
