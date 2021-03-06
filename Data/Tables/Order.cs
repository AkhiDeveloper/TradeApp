using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TradeApp.Data
{
    public class Order
    {
        public Order()
        {
            PlacedDate = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Code { get; set; }

        public DateTime PlacedDate { get; set; }

        [DefaultValue(false)]
        public bool IsConfirmed { get; set; }

        [DefaultValue(false)]
        public bool IsDelivered { get; set; }

        [Required]
        public string customerId { get; set; }

        //Navigation Property
        [Required]
        public Customer customer { get; set; }
        public ICollection<OrderProduct> orderproducts { get; set; }
    }
}
