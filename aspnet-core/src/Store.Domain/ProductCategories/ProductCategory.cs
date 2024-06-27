using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Store.ProductCategories
{
    public class ProductCategory : CreationAuditedAggregateRoot<Guid>
    {
        public ProductCategory(
            Guid id,
            string name,
            string code,
            string slug,
            int sortOrder,
            Guid? parentId,
            bool visibility,
            bool isActive,
            string seoMetaDescription,
            string coverPicture
        ) 
        {
            Id = id;
            Name = name;
            Code = code;
            Slug = slug;
            SortOrder = sortOrder;
            ParentId = parentId;
            Visibility = visibility;
            IsActive = isActive;
            SeoMetaDescription = seoMetaDescription;
            CoverPicture = coverPicture;
        }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Slug { get; set; }
        public int SortOrder { get; set; }
        public string CoverPicture { get; set; }
        public bool Visibility { get; set; }
        public bool IsActive { get; set; }
        public Guid? ParentId { get; set; }
        public string SeoMetaDescription { get; set; }
    }
}
