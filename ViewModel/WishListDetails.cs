using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeApp.Models
{
    public class WishListDetails
    {
        public string Name { get; set; }
        public ICollection<ProductViewModel> Products { get; set; }
    }
}
