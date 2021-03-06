using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TradeApp.Models.Product
{
    public class DetailVM
    {
        public string Code { get; set; }
        public string ImageUrl {  get; set; }
        public string ProductName { get; set; }
        public string BrandName { get; set; }
        public decimal MRP { get; set; }

        //Discount in percentage
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:##.##%}")]
        public decimal Discount { get; set; }

        public decimal Price { get
            {
                return (MRP*(1 - Discount));
            }
        }

        [DefaultValue(1)]
        public int Quantity { get; set; }

        [DefaultValue(0)]
        public Data.Rating AvgRating {  get; set; }
    }
}
