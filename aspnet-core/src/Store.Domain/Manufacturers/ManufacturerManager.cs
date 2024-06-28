using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Store.Manufacturers
{
    public class ManufacturerManager : DomainService
    {
        private readonly IRepository<Manufacturer, Guid> _manufacturerRepository;
        public ManufacturerManager(IRepository<Manufacturer, Guid> manufacturerRepository)
        {
            _manufacturerRepository = manufacturerRepository;
        }

        public async Task<Manufacturer> CreateAsync(
            string name,
            string code,
            string slug,
            bool visibility,
            bool isActive,
            string country
           )
        {
            if (await _manufacturerRepository.AnyAsync(x => x.Name == name))
                throw new UserFriendlyException("Tên nhà cung cấp đã tồn tại", StoreDomainErrorCodes.ManufacturerNameAlreadyExists);
            if (await _manufacturerRepository.AnyAsync(x => x.Code == code))
                throw new UserFriendlyException("Mã nhà cung cấp đã tồn tại", StoreDomainErrorCodes.ManufacturerCodeAlreadyExists);
            return new Manufacturer(Guid.NewGuid(), name, code, slug, null, visibility, isActive, country);
        }
    }
}
  