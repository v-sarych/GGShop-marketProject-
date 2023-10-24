using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDb.Entities
{
    public class PackegeSize
    {
        [Key]
        public int Id { get; set; }
        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;
        public int Length { get; set; } = 0;
    }
}
