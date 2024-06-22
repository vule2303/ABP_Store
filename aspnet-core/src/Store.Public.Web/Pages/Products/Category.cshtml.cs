using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using Store.Public.ProductCategories;
using Store.Public.Products;
using Store.Public.Manufacturers;

namespace Store.Public.Web.Pages.Products
{
    public class CategoryModel : PageModel
    {
        public ProductCategoryDto Category { set; get; }

        public List<ProductCategoryInListDto> Categories { set; get; }
        public PagedResult<ProductInListDto> ProductData { set; get; }

        private readonly IProductCategoriesAppService _productCategoriesAppService;
        private readonly IProductsAppService _productsAppService;
        private readonly IManufacturersAppService _manufacturersAppService;
        public CategoryModel(IProductCategoriesAppService productCategoriesAppService,
            IProductsAppService productsAppService, IManufacturersAppService manufacturersAppService)
        {
            _productCategoriesAppService = productCategoriesAppService;
            _productsAppService = productsAppService;
            _manufacturersAppService = manufacturersAppService;
        }

        public async Task OnGetAsync(string code, int? pageNumber)
        {
            int pageNumberValue = pageNumber ?? 1;
            Category = await _productCategoriesAppService.GetByCodeAsync(code);
            Categories = await _productCategoriesAppService.GetListAllAsync();
            ProductData = await _productsAppService.GetListFilterAsync(new ProductListFilterDto()
            {
                CurrentPage = pageNumberValue,
                CategoryId = Category.Id,
            });
            if (ProductData != null && ProductData.Results != null)
            {
                foreach (var product in ProductData.Results)
                {
                    var manufacturer = await _manufacturersAppService.GetByIdAsync(product.ManufacturerId);
                    if (manufacturer != null)
                    {
                       product.ManufacturerName = manufacturer.Name;
                    } 
                }
            }

        }
    }
}