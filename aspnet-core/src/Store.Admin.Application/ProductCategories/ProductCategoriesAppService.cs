using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;
using Store.ProductCategories;
using Volo.Abp.Domain.Repositories;

namespace Store.Admin.ProductCategories
{
    public class ProductCategoriesAppService : CrudAppService<
        ProductCategory, 
        ProductCategoryDto, 
        Guid, 
        PagedResultRequestDto, 
        CreateUpdateProductCategoryDto, 
        CreateUpdateProductCategoryDto>, IProductCategoriesAppService
    {
        public ProductCategoriesAppService(IRepository<ProductCategory, Guid> repository) : base(repository)
        {

        }
    }
}
