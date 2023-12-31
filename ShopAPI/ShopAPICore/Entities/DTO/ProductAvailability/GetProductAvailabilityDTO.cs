﻿using ShopApiCore.Entities.DTO.Package;
using ShopDb.Entities;

namespace ShopApiCore.Entities.DTO.ProductAvailability
{
    public class GetProductAvailabilityDTO
    {
        public string Sku { get; set; }
        public int Count { get; set; }
        public string Size { get; set; }
        public float Cost { get; set; }


        public int Weight {  get; set; }
        public int PackageSizeId {  get; set; }
        public PackageSizeDTO PakageSize { get; set; }
    }
}
