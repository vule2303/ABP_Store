using Store.Public.Manufacturers;
using Store.Public.ProductCategories;
using Store.Public.Products;
using System.Collections.Generic;

namespace Store.Public.Web.Models
{
    public class HomeCacheItem
    {
        public List<ProductCategoryInListDto> Categories { set; get; }
        public List<ProductInListDto> TopSellerProducts { set; get; }
        public List<ManufacturerInListDto> Manufacturers { set; get; }
        public List<ProductInListDto> GetProductRandomList { set; get; }
    }
}
