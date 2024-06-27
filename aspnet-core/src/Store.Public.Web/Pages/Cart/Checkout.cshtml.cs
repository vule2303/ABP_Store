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
        private readonly IEmailSender _emailSender;
        private readonly ITemplateRenderer _templateRenderer;
        public CheckoutModel(IOrdersAppService ordersAppService, IEmailSender emailSender,
            ITemplateRenderer templateRenderer)
        {
            _ordersAppService = ordersAppService;
            _emailSender = emailSender;
            _templateRenderer = templateRenderer; 
        }
        public List<CartItem> CartItems { get; set; }

        public bool? CreateStatus { set; get; }

        [BindProperty]
        public OrderDto Order { set; get; }

        public void OnGet()
        {
            CartItems = GetCartItems();

        }

        public async Task OnPostAsync()
        {
            if (ModelState.IsValid == false)
            {

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
                    var email = User.GetSpecificClaim(ClaimTypes.Email);
                    var emailBody = await _templateRenderer.RenderAsync(
                        EmailTemplates.CreateOrderEmail,
                        new
                        {
                            message = "Đơn hàng của bạn được gửi thành công"
                        });
                    await _emailSender.SendAsync(email, "Tạo đơn hàng thành công", emailBody);
                }
                CreateStatus = true; 
            }
            else
                CreateStatus = false;
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
