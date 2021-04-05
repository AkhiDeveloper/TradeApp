using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TradeApp.Data
{
    public class OrderProduct
    {
        [Key]
        public int Code { get; set; }

        [Required]
        public Product product { get; set; }

        [DefaultValue(1)]
        public int Quantity { get; set; }

        //Navigation property
        [Required]
        public Order Order { get; set; }
    }
}
