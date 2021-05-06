using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TradeApp.Models
{
    public class ProductViewModel
    {
        [Required]
        public string Name { get; set; }
        [DefaultValue(0.0)]
        public decimal PurchasingPrice { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true,
            DataFormatString = "{0:##.##%}")]
        public decimal Discount { get; set; }
        
    }
}