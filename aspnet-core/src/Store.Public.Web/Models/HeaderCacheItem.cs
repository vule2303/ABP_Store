using Store.Public.ProductCategories;
using System.Collections.Generic;

namespace Store.Public.Web.Models
{
    public class HeaderCacheItem
    { 
        public List<ProductCategoryInListDto> Categories { set; get; }
    }
}
