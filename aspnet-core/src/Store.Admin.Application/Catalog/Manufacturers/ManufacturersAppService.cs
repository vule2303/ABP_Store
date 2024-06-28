using Microsoft.AspNetCore.Authorization;
using Store.Admin.Permissions;
using Store.Manufacturers;
using Store.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.Domain.Repositories;

namespace Store.Admin.Manufacturers
{
    [Authorize(StoreAdminPermissions.Manufacturer.Default)]
    public class ManufacturersAppService : CrudAppService<
        Manufacturer,
        ManufacturerDto,
        Guid,
        PagedResultRequestDto,
        CreateUpdateManufacturerDto,
        CreateUpdateManufacturerDto>, IManufacturersAppService
    {
        private readonly IBlobContainer<ProductThumbnailPictureContainer> _fileContainer;
        private readonly ProductCodeGenerator _productCodeGenerator;
        private readonly ManufacturerManager _manufacturerManager;
        public ManufacturersAppService(IRepository<Manufacturer, Guid> repository, 
            ManufacturerManager manufacturerManager, ProductCodeGenerator productCodeGenerator, IBlobContainer<ProductThumbnailPictureContainer> fileContainer)
            : base(repository)
        {
            GetPolicyName = StoreAdminPermissions.Manufacturer.Default;
            GetListPolicyName = StoreAdminPermissions.Manufacturer.Default;
            CreatePolicyName = StoreAdminPermissions.Manufacturer.Create;
            UpdatePolicyName = StoreAdminPermissions.Manufacturer.Update;
            DeletePolicyName = StoreAdminPermissions.Manufacturer.Delete;
            _fileContainer = fileContainer;
            _productCodeGenerator = productCodeGenerator;
            _manufacturerManager = manufacturerManager;

        }
        [Authorize(StoreAdminPermissions.Manufacturer.Create)]
        public override async Task<ManufacturerDto> CreateAsync(CreateUpdateManufacturerDto input)
        {
            var manufacturer = await _manufacturerManager.CreateAsync(
                input.Name,
                input.Code,
                input.Slug,
                input.Visibility,
                input.IsActive,
                input.Country
                );
            if (input.CoverPictureName != null && input.CoverPictureContent.Length > 0)
            {
                await SaveThumbnailImageAsync(input.CoverPictureName, input.CoverPictureContent);
                manufacturer.CoverPicture = input.CoverPictureName;
            }

            var result = await Repository.InsertAsync(manufacturer);

            return ObjectMapper.Map<Manufacturer, ManufacturerDto>(result);
        }
        [Authorize(StoreAdminPermissions.Manufacturer.Update)]

        public override async Task<ManufacturerDto> UpdateAsync(Guid id, CreateUpdateManufacturerDto input)
        {
            var manufacturer = await Repository.GetAsync(id);
            if (manufacturer == null)
                throw new BusinessException(StoreDomainErrorCodes.ProductCategoryIsNotExists);
            manufacturer.Name = input.Name;
            manufacturer.Code = input.Code;
            manufacturer.Slug = input.Slug;
            manufacturer.Visibility = input.Visibility;
            manufacturer.IsActive = input.IsActive;
            if (input.CoverPictureContent != null && input.CoverPictureContent.Length > 0)
            {
                await SaveThumbnailImageAsync(input.CoverPictureName, input.CoverPictureContent);
                manufacturer.CoverPicture = input.CoverPictureName;
            }
            await Repository.UpdateAsync(manufacturer);
            return ObjectMapper.Map<Manufacturer, ManufacturerDto>(manufacturer);
        }
        [Authorize(StoreAdminPermissions.Manufacturer.Default)]
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

        [Authorize(StoreAdminPermissions.Manufacturer.Default)]
        public async Task<string> GetSuggestNewCodeAsync()
        {
            return await _productCodeGenerator.GenerateAsync();
        }
        [Authorize(StoreAdminPermissions.Manufacturer.Update)]

        private async Task SaveThumbnailImageAsync(string fileName, string base64)
        {
            Regex regex = new Regex(@"^[\w/\:.-]+;base64,");
            base64 = regex.Replace(base64, string.Empty);
            byte[] bytes = Convert.FromBase64String(base64);
            await _fileContainer.SaveAsync(fileName, bytes, overrideExisting: true);
        }
        [Authorize(StoreAdminPermissions.Manufacturer.Delete)]
        public async Task DeleteMultipleAsync(IEnumerable<Guid> ids)
        {
            await Repository.DeleteManyAsync(ids);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }

        [Authorize(StoreAdminPermissions.Manufacturer.Default)]
        public async Task<List<ManufacturerInListDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.IsActive == true);
            var data = await AsyncExecuter.ToListAsync(query);

            return ObjectMapper.Map<List<Manufacturer>, List<ManufacturerInListDto>>(data);
        }

        [Authorize(StoreAdminPermissions.Manufacturer.Default)]
        public async Task<PagedResultDto<ManufacturerInListDto>> GetListFilterAsync(BaseListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Name.Contains(input.Keyword));

            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter.ToListAsync(query.Skip(input.SkipCount).Take(input.MaxResultCount));

            return new PagedResultDto<ManufacturerInListDto>(totalCount, ObjectMapper.Map<List<Manufacturer>, List<ManufacturerInListDto>>(data));
        }
    }
}