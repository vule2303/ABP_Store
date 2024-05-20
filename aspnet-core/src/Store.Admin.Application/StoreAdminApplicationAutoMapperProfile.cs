using AutoMapper;
using Store.Admin.ProductCategories;
using Store.ProductCategories;

namespace Store.Admin;

public class StoreAdminApplicationAutoMapperProfile : Profile
{
    public StoreAdminApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<ProductCategory, ProductCategoryDto>();
        CreateMap<ProductCategory, ProductCategoryInListDto>();
        CreateMap<CreateUpdateProductCategoryDto, ProductCategory>();
    }
}
