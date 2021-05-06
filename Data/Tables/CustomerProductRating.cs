using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TradeApp.Data
{
    public enum Rating
    {
        No_Rating,
        Very_Poor,
        Poor,
        Average,
        Good,
        Very_good
    }

    public class CustomerProductRating
    {
        //Attributes
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DefaultValue(0)]
        public Rating productrating { get; set; }

        [Required]
        [ForeignKey(nameof(customer))]
        public string customerid { get; set; }

        [Required]
        [ForeignKey(nameof(product))]
        public string productcode { get; set; }

        //Navigation Property
        public Customer customer { get; set; }
        public Product product { get; set; }
    }
}
