
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TradeApp.Data
{
    public class Product
    {
        public Product()
        {
            AddedDate = DateTime.Now;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Code { get; set; }

        [Required]
        public string ProductName { get; set; }

        public string ImageUrl { get; set; }

        [DefaultValue("No Brand")]
        public string BrandName { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        [DefaultValue(0)]
        public decimal MRP { get; set; }

        //Discount in percentage
        [DisplayFormat(ApplyFormatInEditMode = true, 
            DataFormatString = "{0:##.##%}")]
        [DefaultValue(0)]
        public decimal Discount { get; set; }
        public DateTime AddedDate { get; set; }

        [DefaultValue(0)]
        public Rating AvgRating { get; set; }
    }
}
