using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDb.Entities
{
    public  class ProductTag
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }

    }
}
