using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Store.Public.Catalog.Orders
{
    public class CreateOrderDto
    {
        [Required(ErrorMessage = "Họ và tên là bắt buộc")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string CustomerPhoneNumber { get; set; }
        [Required(ErrorMessage = "Địa chỉ là bắt buộc")]
        public string CustomerAddress { get; set; }
        public Guid? CustomerUserId { get; set; }

        public List<OrderItemDto> Items { get; set; }
    }
}
