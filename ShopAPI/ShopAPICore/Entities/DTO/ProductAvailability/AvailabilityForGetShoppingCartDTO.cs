using ShopApiCore.Entities.DTO.ProductSearch;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApiCore.Entities.DTO.ProductAvailability
{
    public class AvailabilityForGetShoppingCartDTO
    {
        public string Size {  get; set; }

        public SimpleProductDTO Product { get; set; }
    }
}
