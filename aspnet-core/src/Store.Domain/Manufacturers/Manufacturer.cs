using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Store.Manufacturers
{
    public class Manufacturer : CreationAuditedAggregateRoot<Guid>
    {
        public Manufacturer(Guid id,
            string name,
            string code,
            string slug,
            string coverPicture,
            bool visibility,
            bool isActive,
            string country
            )
            
        {
            Id = id;
            Name = name;
            Code = code;
            Slug = slug;
            CoverPicture = coverPicture;
            Visibility = visibility;
            IsActive = isActive;
            Country = country;

        }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Slug { get; set; }
        public string CoverPicture { get; set; }
        public bool Visibility { get; set; }
        public bool IsActive { get; set; }
        public string Country { get; set; }
    }
}
