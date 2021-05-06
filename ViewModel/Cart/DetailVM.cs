using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeApp.Models.Cart
{
    public class DetailVM
    {
        public IList<Models.Cart.CartProductVM> CartProducts { get; set; }

        public decimal GrandTotalPrice 
        { get
            {
                decimal ans = 0;
                foreach(var x in CartProducts)
                {
                    ans = ans + x.TotalPrice;
                }
                return ans;
            } 
        }
    }
}
