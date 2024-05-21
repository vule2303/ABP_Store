using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;
using System.Threading.Tasks;
namespace Store.Admin.Products
{
    public interface IProductAppService : ICrudAppService
        <ProductDto, Guid, PagedResultRequestDto, CreateUpdateProductDto, CreateUpdateProductDto>
    {
        Task<PagedResultDto<ProductInListDto>> GetListWithFilterAsync(BaseListFilterDto input);
        Task<List<ProductInListDto>> GetListAllAsync();
        Task DeleteMultipAsync(IEnumerable<Guid> ids);
    }
}
