using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Store.Public.ProductCategories;
using Store.Public.Web.Models;
using Volo.Abp.Caching;
using System.Collections.Generic;

namespace Store.Public.Web.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly IProductCategoriesAppService _productCategoriesAppService;
        private readonly IDistributedCache<HeaderCacheItem> _distributedCache;
        public HeaderViewComponent(IProductCategoriesAppService productCategoriesAppService,
            IDistributedCache<HeaderCacheItem> distributedCache)
        {
            _productCategoriesAppService = productCategoriesAppService;
            _distributedCache = distributedCache;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
        
            var model = await _productCategoriesAppService.GetListAllAsync();                 
            return View(model);               
        }
       
    }
}