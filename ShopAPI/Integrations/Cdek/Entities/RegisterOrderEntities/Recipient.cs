using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.Cdek.Entities.RegisterOrderEntities
{
    public class Recipient
    {
        public string Name {  get; set; }
        public Phone[] Phones { get; set; }
    }
}
