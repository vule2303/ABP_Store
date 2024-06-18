﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Store.Public.ProductCategories
{
    public interface IProductCategoriesAppService : IReadOnlyAppService
       <ProductCategoryDto,
       Guid,
       PagedResultRequestDto>
    {
        Task<PagedResult<ProductCategoryInListDto>> GetListFilterAsync(BaseListFilterDto input);
        Task<List<ProductCategoryInListDto>> GetListAllAsync();
        Task<ProductCategoryDto> GetByCodeAsync(string code);

    }
}