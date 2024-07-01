using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Store.Public.ProductCategories;
using Store.Public.Products;
using Store.Public.Web.Models;
using Volo.Abp.Caching;
using Store.Public.Manufacturers;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Store.Public.Web.Pages;

public class IndexPublicModel : PublicPageModel
{
    private readonly IDistributedCache<HomeCacheItem> _distributedCache;
    private readonly IProductCategoriesAppService _productCategoriesAppService;
    private readonly IProductsAppService _productsAppService;
    private readonly IManufacturersAppService _manufacturersAppService;
    public List<ProductCategoryInListDto> Categories { set; get; }
    public List<ManufacturerInListDto> Manufacturers { set; get; }
    public List<ProductInListDto> TopSellerProducts { set; get; }
    public List<ProductInListDto> GetProductRandomList { set; get; }
    public List<ProductInListDto> GetTopManufacturerProductList { set; get; }
    public List<ProductInListDto> TopWatchProduct { set; get; }

    public IndexPublicModel(
        IProductCategoriesAppService productCategoriesAppService,
        IProductsAppService productsAppService, 
        IDistributedCache<HomeCacheItem> distributedCache, 
        IManufacturersAppService manufacturersAppService)
    {
        _productCategoriesAppService = productCategoriesAppService;
        _productsAppService = productsAppService;
        _distributedCache = distributedCache;
        _manufacturersAppService = manufacturersAppService;
    }

    public async Task OnGetAsync()
    {
      
            var allCategories = await _productCategoriesAppService.GetListAllAsync();
            var allManufacturers = await _manufacturersAppService.GetListAllAsync();
            var rootCategories = allCategories.Where(x => x.ParentId == null).ToList();
            foreach (var category in rootCategories)
            {
                category.Children = rootCategories.Where(x => x.ParentId == category.Id).ToList();
            }

            var topSellerProducts = await _productsAppService.GetListTopSellerAsync(5);
            var getProductRandomList = await _productsAppService.GetListRandomAsync(5);
            var topManufacturerProducts = await _productsAppService.GetListRandomAsync(5);
            var topWatchProduct = await _productsAppService.GetListRandomAsync(5);
        foreach (var item in topSellerProducts)
            {
                var manufacturer = await _manufacturersAppService.GetByIdAsync(item.ManufacturerId);
                item.ManufacturerName = manufacturer.Name;
            }     


            TopSellerProducts = topSellerProducts;
            Categories = rootCategories;
            Manufacturers = allManufacturers;
            GetProductRandomList = getProductRandomList;
            GetTopManufacturerProductList = topManufacturerProducts;
            TopWatchProduct = topWatchProduct;

    }
}