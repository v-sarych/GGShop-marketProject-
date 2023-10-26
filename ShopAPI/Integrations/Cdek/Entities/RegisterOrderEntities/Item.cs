using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.Cdek.Entities.RegisterOrderEntities
{
    public class Item
    {
        public string Name {  get; set; }
        public string Ware_key {  get; set; }

        public float Cost {  get; set; }
        public Payment Payment { get; set; }

        public int Weight {  get; set; }
        public int Ammount {  get; set; }
    }
}
