using AutoMapper;
using Store.Admin.ProductCategories;
using Store.Admin.Products;
using Store.ProductCategories;
using Store.Products;

namespace Store.Admin;

public class StoreAdminApplicationAutoMapperProfile : Profile
{
    public StoreAdminApplicationAutoMapperProfile()
    {
        //ProductCategory
        CreateMap<ProductCategory, ProductCategoryDto>();
        CreateMap<ProductCategory, ProductCategoryInListDto>();
        CreateMap<CreateUpdateProductCategoryDto, ProductCategory>();
        //Product
        CreateMap<Product, ProductDto>();
        CreateMap<Product, ProductInListDto>();
        CreateMap<CreateUpdateProductDto, Product>();
    }
}
