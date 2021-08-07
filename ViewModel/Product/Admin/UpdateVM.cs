using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TradeApp.Models.Product.Admin
{
    public class UpdateVM
    {
        [Required]
        public string ProductName { get; set; }

        public string ImageUrl { get; set; }

        [NotMapped]
        [Display(Name = "Product Image")]
        public IFormFile Image { get; set; }

        [DefaultValue("No Brand")]
        public string BrandName { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:##.##}")]
        [DefaultValue(0)]
        public decimal MRP { get; set; }

        //Discount in percentage
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:##.##%}")]
        [DefaultValue(0)]
        public decimal Discount { get; set; }
    }
}
