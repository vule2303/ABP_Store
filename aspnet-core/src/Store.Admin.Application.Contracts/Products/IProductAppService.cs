using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;
using System.Threading.Tasks;
namespace Store.Admin.Products
{
    public interface IProductsAppService : ICrudAppService
       <ProductDto,
       Guid,
       PagedResultRequestDto,
       CreateUpdateProductDto,
       CreateUpdateProductDto>
    {
        Task<PagedResultDto<ProductInListDto>> GetListFilterAsync(ProductListFilterDto input);
        Task<List<ProductInListDto>> GetListAllAsync();
        Task DeleteMultipleAsync(IEnumerable<Guid> ids);
        Task<string> GetThumbnailImageAsync(string fileName);
        Task<string> GetSuggestNewCodeAsync();
    }
}
