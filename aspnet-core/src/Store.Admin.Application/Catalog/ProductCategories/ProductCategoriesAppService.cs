using Microsoft.AspNetCore.Authorization;
using Store.Admin.ProductCategories;
using Store.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Store.Admin.Permissions;
using Store.ProductCategories;
using Store.Products;
using Volo.Abp.BlobStoring;
using System.Text.RegularExpressions;
using Store.Admin.Products;
using Volo.Abp;

namespace Store.Admin.ProductCategories
{
    [Authorize(StoreAdminPermissions.ProductCategory.Default)]
    public class ProductCategoriesAppService : CrudAppService<
        ProductCategory,
        ProductCategoryDto,
        Guid,
        PagedResultRequestDto,
        CreateUpdateProductCategoryDto,
        CreateUpdateProductCategoryDto>, IProductCategoriesAppService
    {
        private readonly IBlobContainer<ProductThumbnailPictureContainer> _fileContainer;
        private readonly ProductCodeGenerator _productCodeGenerator;
        private readonly ProductCategoryManager _productCategoryManager;
        public ProductCategoriesAppService(IRepository<ProductCategory, Guid> repository,
            IBlobContainer<ProductThumbnailPictureContainer> fileContainer,
            ProductCodeGenerator productCodeGenerator, ProductCategoryManager productCategoryManager)
            : base(repository)
        {
            GetPolicyName = StoreAdminPermissions.ProductCategory.Default;
            GetListPolicyName = StoreAdminPermissions.ProductCategory.Default;
            CreatePolicyName = StoreAdminPermissions.ProductCategory.Create;
            UpdatePolicyName = StoreAdminPermissions.ProductCategory.Update;
            DeletePolicyName = StoreAdminPermissions.ProductCategory.Delete;
            _fileContainer = fileContainer;
            _productCodeGenerator = productCodeGenerator;
            _productCategoryManager = productCategoryManager;
        }

        [Authorize(StoreAdminPermissions.ProductCategory.Delete)]

        public async Task DeleteMultipleAsync(IEnumerable<Guid> ids)
        {
            await Repository.DeleteManyAsync(ids);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }
        [Authorize(StoreAdminPermissions.ProductCategory.Create)]
        public override async Task<ProductCategoryDto> CreateAsync(CreateUpdateProductCategoryDto input)
        {
            var productCategory = await _productCategoryManager.CreateAsync(
                input.Name,
                input.Code,
                input.Slug,
                input.SortOrder,
                input.Visibility,
                input.IsActive,
                input.SeoMetaDescription
                );
            if (input.CoverPictureName != null && input.CoverPictureContent.Length > 0)
            {
                await SaveThumbnailImageAsync(input.CoverPictureName, input.CoverPictureContent);
                productCategory.CoverPicture = input.CoverPictureName;
            }

            var result = await Repository.InsertAsync(productCategory);

            return ObjectMapper.Map<ProductCategory, ProductCategoryDto>(result);
        }
        [Authorize(StoreAdminPermissions.ProductCategory.Update)]

        public override async Task<ProductCategoryDto> UpdateAsync(Guid id, CreateUpdateProductCategoryDto input)
        {
            var category = await Repository.GetAsync(id);
            if (category == null)
                throw new BusinessException(StoreDomainErrorCodes.ProductCategoryIsNotExists);
            category.Name = input.Name;
            category.Code = input.Code;
            category.Slug = input.Slug;
            category.SortOrder = input.SortOrder;
            category.Visibility = input.Visibility;
            category.IsActive = input.IsActive;
            category.SeoMetaDescription = input.SeoMetaDescription;
            if (input.CoverPictureContent != null && input.CoverPictureContent.Length > 0)
            {
                await SaveThumbnailImageAsync(input.CoverPictureName, input.CoverPictureContent);
                category.CoverPicture = input.CoverPictureName;
            }
            await Repository.UpdateAsync(category);
            return ObjectMapper.Map<ProductCategory, ProductCategoryDto>(category);
        }
        [Authorize(StoreAdminPermissions.ProductCategory.Default)]

        public async Task<List<ProductCategoryInListDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.IsActive == true);
            var data = await AsyncExecuter.ToListAsync(query);

            return ObjectMapper.Map<List<ProductCategory>, List<ProductCategoryInListDto>>(data);

        }
        [Authorize(StoreAdminPermissions.ProductCategory.Default)]
        public async Task<string> GetThumbnailImageAsync(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return null;
            }
            var thumbnailContent = await _fileContainer.GetAllBytesOrNullAsync(fileName);

            if (thumbnailContent is null)
            {
                return null;
            }
            var result = Convert.ToBase64String(thumbnailContent);
            return result;
        }

        [Authorize(StoreAdminPermissions.ProductCategory.Default)]
        public async Task<string> GetSuggestNewCodeAsync()
        {
            return await _productCodeGenerator.GenerateAsync();
        }
        [Authorize(StoreAdminPermissions.Product.Update)]

        private async Task SaveThumbnailImageAsync(string fileName, string base64)
        {
            Regex regex = new Regex(@"^[\w/\:.-]+;base64,");
            base64 = regex.Replace(base64, string.Empty);
            byte[] bytes = Convert.FromBase64String(base64);
            await _fileContainer.SaveAsync(fileName, bytes, overrideExisting: true);
        }
        [Authorize(StoreAdminPermissions.ProductCategory.Default)]

        public async Task<PagedResultDto<ProductCategoryInListDto>> GetListWithFilterAsync(BaseListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Name.Contains(input.Keyword));

            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter.ToListAsync(query.Skip(input.SkipCount).Take(input.MaxResultCount));

            return new PagedResultDto<ProductCategoryInListDto>(totalCount, ObjectMapper.Map<List<ProductCategory>, List<ProductCategoryInListDto>>(data));
        }
    }
}