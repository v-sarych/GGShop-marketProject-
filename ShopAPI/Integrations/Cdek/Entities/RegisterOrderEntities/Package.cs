using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.Cdek.Entities.RegisterOrderEntities
{
    internal class Package
    {
        public string Number {  get; set; }

        public int Weight {  get; set; }

        public int Length {  get; set; }
        public int Width {  get; set; }
        public int Height { get; set; }

        public Item[] Items { get; set; }
    }
}
