using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Store.Public.Catalog.Orders;
using Store.Public.Web.Extensions;
using Store.Public.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Text.Json;
using Volo.Abp.TextTemplating;
using Store.Emailing;
using System.Security.Claims;
using Volo.Abp.Emailing;

namespace Store.Public.Web.Pages.Cart
{
    public class CheckoutModel : PageModel
    {
        private readonly IOrdersAppService _ordersAppService;
        public CheckoutModel(IOrdersAppService ordersAppService)
        {
            _ordersAppService = ordersAppService;
        }
        public List<CartItem> CartItems { get; set; }

        public bool? CreateStatus { set; get; }

        [BindProperty]
        public OrderDto Order { set; get; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/login.html");
            }
            else
            {
                CartItems = GetCartItems();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                CartItems = GetCartItems();
                return Page();
            }
            var cartItems = new List<OrderItemDto>();
            foreach (var item in GetCartItems())
            {
                cartItems.Add(new OrderItemDto()
                {
                    Price = item.Product.SellPrice,
                    ProductId = item.Product.Id,
                    Quantity = item.Quantity
                });
            }
            Guid? currentUserId = User.Identity.IsAuthenticated ? User.GetUserId() : null;
            var order = await _ordersAppService.CreateAsync(new CreateOrderDto()
            {
                CustomerName = Order.CustomerName,
                CustomerAddress = Order.CustomerAddress,
                CustomerPhoneNumber = Order.CustomerPhoneNumber,
                Items = cartItems,
                CustomerUserId = currentUserId
            });
            CartItems = GetCartItems();

            if (order != null) {
                if (User.Identity.IsAuthenticated)
                {
                    HttpContext.Session.Remove(StoreConsts.Cart);
                    CreateStatus = true;
                }             
            }
            else
                CreateStatus = false;
            return Page();
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
