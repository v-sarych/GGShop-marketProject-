using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApiCore.Entities.DTO.ProductAvailability
{
    public class AvailabilityForGetOrderItemDTO
    {
        public string Sku {  get; set; }
        public string Size {  get; set; }
    }
}
