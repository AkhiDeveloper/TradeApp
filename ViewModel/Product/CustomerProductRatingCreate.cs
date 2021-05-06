using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TradeApp.ViewModel.Product
{
    public class CustomerProductRatingCreate
    {
        public int Id { get; set; }

        [DefaultValue(0)]
        public Data.Rating productrating { get; set; }

        public Models.Product.DetailVM product { get; set; }
    }
}
