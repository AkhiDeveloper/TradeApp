using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TradeApp.Data
{
    public class Cart
    {
        [Key]
        [ForeignKey("customer")]
        public string CustomerId { get; set; }
        //Navigation Property

        public Customer customer { get; set; }
        public ICollection<CartProduct> cartProducts { get; set; }
    }
}
