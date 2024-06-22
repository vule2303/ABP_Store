using Store.Public.Products;

namespace Store.Public.Web.Models
{
    public class CartItem
    {
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }
    }
}
