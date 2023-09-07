using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDb.Entities
{
    public class Comment
    {
        public int ProductId{ get; set; }
        public long UserId{ get; set; }
        public DateTime CreatedDate { get; set; }
        public int Stars { get; set; }
        public string Text { get; set; }

        public User User { get; set; }
        public Product Product { get; set; }
    }
}
