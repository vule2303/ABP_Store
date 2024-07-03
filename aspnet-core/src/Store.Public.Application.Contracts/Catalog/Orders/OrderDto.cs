using Store.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Store.Public.Catalog.Orders
{
    public class OrderDto : EntityDto<Guid>
    {
        public string Code { get; set; }
        public OrderStatus Status { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public double ShippingFee { get; set; }
        public double Tax { get; set; }
        public double Total { get; set; }
        public double Subtotal { get; set; }
        public double Discount { get; set; }
        public double GrandTotal { get; set; }
        [Required(ErrorMessage = "Họ và tên là bắt buộc")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Địa chỉ là bắt buộc")]
        public string CustomerAddress { get; set; }

        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string CustomerPhoneNumber { get; set; }
        public Guid? CustomerUserId { get; set; }
    }
}
