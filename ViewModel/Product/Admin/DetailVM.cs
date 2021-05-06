using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TradeApp.Models.Product.Admin
{
    public class DetailVM
    {
        public string Code { get; set; }
        public string ProductName { get; set; }
        public string BrandName { get; set; }
        public decimal MRP { get; set; }

        //Discount in percentage
        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:##.##%}")]
        public decimal Discount { get; set; }

        public DateTime AddedDate { get; set; }
    }
}
