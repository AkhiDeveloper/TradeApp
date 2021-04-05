using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TradeApp.Data
{
    public class CartProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        public DateTime AddedDate { get; set; }

        //Navigation Property
        public Cart Cart { get; set; }
        public Product product { get; set; }
        public decimal quantity { get; set; }
    }
}
