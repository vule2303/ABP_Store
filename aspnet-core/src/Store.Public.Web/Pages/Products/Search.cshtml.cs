using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Store.Public.Manufacturers;
using Store.Public.ProductCategories;
using Store.Public.Products;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Store.Public.Web.Pages.Products
{

    public class SearchModel : PageModel
    {
        public PagedResult<ProductInListDto> ProductData { get; set; }
        public ProductCategoryDto Category { get; set; }
        public List<ProductCategoryInListDto> Categories { get; set; }
        public List<ManufacturerInListDto> Manufacturers { get; set; }

        private readonly IProductCategoriesAppService _productCategoriesAppService;
        private readonly IProductsAppService _productsAppService;
        private readonly IManufacturersAppService _manufacturersAppService;

        public SearchModel(IProductCategoriesAppService productCategoriesAppService,
                           IProductsAppService productsAppService,
                           IManufacturersAppService manufacturersAppService)
        {
            _productCategoriesAppService = productCategoriesAppService;
            _productsAppService = productsAppService;
            _manufacturersAppService = manufacturersAppService;
        }

        public async Task OnGetAsync(string code, string keyword, int? pageNumber)
        {
            // Giải mã các tham số
            string decodedCode = WebUtility.UrlDecode(code);
            string decodedKeyword = WebUtility.UrlDecode(keyword);
            int pageNumberValue = pageNumber ?? 1;

            Category = await _productCategoriesAppService.GetByCodeAsync(decodedCode);
            Categories = await _productCategoriesAppService.GetListAllAsync();
            Manufacturers = await _manufacturersAppService.GetListAllAsync();
            ProductData = await _productsAppService.GetListFilterAsync(new ProductListFilterDto
            {
                Keyword = decodedKeyword,
                CurrentPage = pageNumberValue,
                CategoryId = Category.Id
            });

            if (ProductData != null && ProductData.Results.Count < 1)
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
        public async Task<IActionResult> OnPostAsync(string code, string keyword, int? pageNumber)
        {
            // Kiểm tra nếu keyword bị bỏ trống
            if (string.IsNullOrEmpty(keyword) || string.IsNullOrEmpty(code))
            {
                ModelState.AddModelError(string.Empty, "Vui lòng nhập từ khóa tìm kiếm.");
                
                return RedirectToPage("/");
            }

            var encodedCode = Uri.EscapeDataString(code);
            var encodedKeyword = Uri.EscapeDataString(keyword);
            var encodedPageNumber = pageNumber.HasValue ? pageNumber.Value.ToString() : string.Empty;

            var redirectUrl = $"/search/{encodedCode}/{encodedKeyword}/{encodedPageNumber}";

            return Redirect(redirectUrl);
        }
    }
}
