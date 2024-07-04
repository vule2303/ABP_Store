﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Store.Public.Manufacturers;
using Store.Public.ProductCategories;
using Store.Public.Products;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Public.Web.Pages.Products
{
    public class ManufacturerModel : PageModel
    {
        public ManufacturerDto Manufacturer { set; get; }
        public ProductCategoryDto Category { set; get; }

        public List<ProductCategoryInListDto> Categories { set; get; }
        public List<ManufacturerInListDto> Manufacturers { set; get; }

        public PagedResult<ProductInListDto> ProductData { set; get; }

        private readonly IProductCategoriesAppService _productCategoriesAppService;
        private readonly IProductsAppService _productsAppService;
        private readonly IManufacturersAppService _manufacturersAppService;
        public ManufacturerModel(IProductCategoriesAppService productCategoriesAppService,
            IProductsAppService productsAppService, IManufacturersAppService manufacturersAppService)
        {
            _productCategoriesAppService = productCategoriesAppService;
            _productsAppService = productsAppService;
            _manufacturersAppService = manufacturersAppService;
        }

        public async Task OnGetAsync(string code, int? pageNumber)
        {
            int pageNumberValue = pageNumber ?? 1;
            Manufacturer = await _manufacturersAppService.GetByCodeAsync(code);
            Categories = await _productCategoriesAppService.GetListAllAsync();
            Manufacturers = await _manufacturersAppService.GetListAllAsync();
           ProductData = await _productsAppService.GetListFilterAsync(new ProductListFilterDto()
            {
                CurrentPage = pageNumberValue,
                ManufacturerId = Manufacturer.Id,
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