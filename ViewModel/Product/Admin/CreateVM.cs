using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TradeApp.Models.Product.Admin
{
    public class CreateVM
    {

        [Required]
        public string ProductName { get; set; }

        [DefaultValue("No Brand")]
        public string BrandName { get; set; }

        [DataType(DataType.Currency)]
        [DefaultValue(0)]
        public decimal MRP { get; set; }

        //Discount in percentage
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:##.##%}")]
        [DefaultValue(0)]
        public decimal Discount { get; set; }
    }
}
