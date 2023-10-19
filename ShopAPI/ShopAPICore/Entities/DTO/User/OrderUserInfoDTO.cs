using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApiCore.Entities.DTO.User
{
    public class OrderUserInfoDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}
