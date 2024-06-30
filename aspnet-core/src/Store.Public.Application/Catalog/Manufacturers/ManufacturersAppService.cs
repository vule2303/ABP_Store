using Store.Manufacturers;
using Store.Products;
using Store.Public;
using Store.Public.Manufacturers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace SAtore.Public.Manufacturers
{
    public class ManufacturersAppService : ReadOnlyAppService<
        Manufacturer,
        ManufacturerDto,
        Guid,
        PagedResultRequestDto>, IManufacturersAppService
    {
        private readonly IRepository<Manufacturer, Guid> _manufacturerRepository;
        private readonly  IBlobContainer<ProductThumbnailPictureContainer> _fileContainer;
        private readonly IRepository<Product> _productReponsitory;
        public ManufacturersAppService(IRepository<Manufacturer, Guid> repository, IRepository<Product> productReponsitory,
            IBlobContainer<ProductThumbnailPictureContainer> fileContainer)
            : base(repository)
        {
            _manufacturerRepository = repository;
            _productReponsitory = productReponsitory;
            _fileContainer = fileContainer;
        }

        public async Task<ManufacturerDto> GetByIdAsync(Guid id)
        {
            var manufacturer = await Repository.GetAsync(id);
            return ObjectMapper.Map<Manufacturer, ManufacturerDto>(manufacturer);
        }
        public async Task<ManufacturerDto> GetByCodeAsync(string code)
        {
            var manufacturer = await _manufacturerRepository.GetAsync(x => x.Code == code);

            return ObjectMapper.Map<Manufacturer, ManufacturerDto>(manufacturer);
        }
        public async Task<List<ManufacturerInListDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.IsActive == true);
            int temp;
            var data = await AsyncExecuter.ToListAsync(query);

            var getListProduct = await _productReponsitory.GetQueryableAsync();
            var manufacturerListDto = ObjectMapper.Map<List<Manufacturer>, List<ManufacturerInListDto>>(data);
            foreach (var item in manufacturerListDto)
            {
                for (var i = 0; i < getListProduct.Count(); i++)
                {
                    temp = getListProduct.Count(p => p.ManufacturerId == item.Id);
                    item.productCount = temp;
                }

            }
            return manufacturerListDto;

        }

        public async Task<PagedResult<ManufacturerInListDto>> GetListFilterAsync(BaseListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Name.Contains(input.Keyword));

            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter
           .ToListAsync(
              query.Skip((input.CurrentPage - 1) * input.PageSize)
           .Take(input.PageSize));

            return new PagedResult<ManufacturerInListDto>(
                ObjectMapper.Map<List<Manufacturer>, List<ManufacturerInListDto>>(data),
                totalCount,
                input.CurrentPage,
                input.PageSize
            );
        }
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
    }
}