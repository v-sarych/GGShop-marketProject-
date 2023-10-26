using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApiCore.Entities.DTO.Package
{
    public class PackageSizeDTO
    {
        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;
        public int Length { get; set; } = 0;
    }
}
