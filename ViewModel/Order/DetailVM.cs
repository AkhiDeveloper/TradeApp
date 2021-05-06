using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TradeApp.Data;

namespace TradeApp.Models.Order
{
    public class DetailVM
    {
        public string Code { get; set; }

        public DateTime PlacedDate { get; set; }

        [DefaultValue("Unconfirmed")]
        public string Status { get; set; }

        public ICollection<Models.Order.OrderProductVM> orderproducts { get; set; }

        public decimal ProductCharge
        {
            get
            {
                decimal ans = 0;
                foreach (var x in orderproducts)
                {
                    ans = ans + x.TotalPrice;
                }
                return ans;
            }
        }
    }
}
