using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Store.Public.ProductCategories;
using Store.Public.Web.Models;
using Volo.Abp.Caching;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Store.Public.Web.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private readonly IProductCategoriesAppService _productCategoriesAppService;
        private readonly IDistributedCache<HeaderCacheItem> _distributedCache;
        public List<CartItem> CartItems { get; set; }
        public CartViewComponent(IProductCategoriesAppService productCategoriesAppService,
            IDistributedCache<HeaderCacheItem> distributedCache)
        {
            _productCategoriesAppService = productCategoriesAppService;
            _distributedCache = distributedCache;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            CartItems = GetCartItems();
            return View(CartItems);               
        }
        private List<CartItem> GetCartItems()
        {
            var cart = HttpContext.Session.GetString(StoreConsts.Cart);
            var productCarts = new Dictionary<string, CartItem>();
            if (cart != null)
            {
                productCarts = JsonSerializer.Deserialize<Dictionary<string, CartItem>>(cart);
            }
            return productCarts.Values.ToList();
        }

    }
}