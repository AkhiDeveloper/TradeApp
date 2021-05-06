using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TradeApp.Models.Order
{
    public class OrderProductVM
    {
        public Models.Product.DetailVM product { get; set; }

        public decimal quantity { get; set; }

        public decimal TotalPrice
        {
            get
            {
                return (product.Price * quantity);
            }
        }
    }
}
