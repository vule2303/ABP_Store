using AutoMapper;
using Store.Admin.Manufacturers;
using Store.Admin.ProductAttributes;
using Store.Admin.ProductCategories;
using Store.Admin.Products;
using Store.Manufacturers;
using Store.ProductAttributes;
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
        //Manufacturer
        CreateMap<Manufacturer, ManufacturerDto>();
        CreateMap<Manufacturer, ManufacturerInListDto>();
        CreateMap<CreateUpdateManufacturerDto, Manufacturer>();
        //Product attribute
        CreateMap<ProductAttribute, ProductAttributeDto>();
        CreateMap<ProductAttribute, ProductAttributeInListDto>();
        CreateMap<CreateUpdateProductAttributeDto, ProductAttribute>();
    }
}
