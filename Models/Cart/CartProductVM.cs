namespace TradeApp.Models.Cart
{
    public class CartProductVM
    {
        public Models.Product.DetailVM product { get; set; }

        public decimal quantity { get; set; }

        public decimal TotalPrice
        {
            get
            {
                return (product.Price * quantity);
            }
        }
    }
}