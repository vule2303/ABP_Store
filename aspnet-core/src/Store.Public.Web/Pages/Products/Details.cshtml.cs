using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Store.Public.ProductCategories;
using Store.Public.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Public.Web.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly IProductsAppService _productsAppService;
        private readonly IProductCategoriesAppService _productCategoriesAppService;
        public List<ProductInListDto> GetProductRandomList { set; get; }
        public DetailsModel(IProductsAppService productsAppService,
            IProductCategoriesAppService productCategoriesAppService)
        {
            _productsAppService = productsAppService;
            _productCategoriesAppService = productCategoriesAppService;
        }
        public ProductCategoryDto Category { get; set; }
        public ProductDto Product { get; set; }
        public async Task OnGetAsync(string categorySlug, string slug)
        {
            Category = await _productCategoriesAppService.GetBySlugAsync(categorySlug);
            Product = await _productsAppService.GetBySlugAsync(slug);
            GetProductRandomList = await _productsAppService.GetListRandomAsync(5);
        }
    }
}
