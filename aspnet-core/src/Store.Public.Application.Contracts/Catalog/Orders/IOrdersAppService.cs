using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Store.Public.Catalog.Orders
{
    public interface IOrdersAppService : ICrudAppService
       <OrderDto,
       Guid,
       PagedResultRequestDto, CreateOrderDto, CreateOrderDto>
    {

    }
}
