using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TradeApp.Data
{
    public class CustomerWishlist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }

        //Navigation Property
        [Required]
        public Customer customer { get; set; }
        public ICollection<Product> products { get; set; }
    }
}
