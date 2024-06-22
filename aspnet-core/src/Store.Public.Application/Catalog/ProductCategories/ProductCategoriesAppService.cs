using Store.ProductCategories;
using Store.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Store.Public.ProductCategories
{
    public class ProductCategoriesAppService : CrudAppService<
        ProductCategory,
        ProductCategoryDto,
        Guid,
        PagedResultRequestDto>, IProductCategoriesAppService
    {
        private readonly IRepository<ProductCategory, Guid> _productCategoryRepository;
        private readonly IRepository<Product> _productReponsitory;
        public ProductCategoriesAppService(IRepository<ProductCategory, Guid> repository
            ,IRepository<Product> productReponsitory)
            : base(repository)
        {
            _productCategoryRepository = repository;
            _productReponsitory = productReponsitory;
        }

        public async Task<ProductCategoryDto> GetByCodeAsync(string code)
        {
            var category = await _productCategoryRepository.GetAsync(x => x.Code == code);

            return ObjectMapper.Map<ProductCategory, ProductCategoryDto>(category);
        }

        public async Task<List<ProductCategoryInListDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.IsActive == true);
            int temp;
            var data = await AsyncExecuter.ToListAsync(query);

            var getListProduct = await _productReponsitory.GetQueryableAsync();
         
            var categoryListDto = ObjectMapper.Map<List<ProductCategory>, List<ProductCategoryInListDto>>(data);

            foreach (var item in categoryListDto)
            {
                for (var i = 0; i < getListProduct.Count(); i++)
                {
                    temp = getListProduct.Count(p => p.CategoryId == item.Id);
                    item.productCount = temp;
                }

            }

            return categoryListDto;
        }
        public async Task<PagedResult<ProductCategoryInListDto>> GetListFilterAsync(BaseListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Name.Contains(input.Keyword));

            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter
               .ToListAsync(
                  query.Skip((input.CurrentPage - 1) * input.PageSize)
               .Take(input.PageSize));

            return new PagedResult<ProductCategoryInListDto>(
                ObjectMapper.Map<List<ProductCategory>, List<ProductCategoryInListDto>>(data),
                totalCount,
                input.CurrentPage,
                input.PageSize
            );
        }
        public async Task<ProductCategoryDto> GetBySlugAsync(string slug)
        {
            var productCategory = await _productCategoryRepository.GetAsync(x => x.Slug == slug);
            return ObjectMapper.Map<ProductCategory, ProductCategoryDto>(productCategory);
        }
    }
}