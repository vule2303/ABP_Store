using AutoMapper;
using Store.Manufacturers;
using Store.Orders;
using Store.ProductAttributes;
using Store.ProductCategories;
using Store.Products;
using Store.Public.Catalog.Orders;
using Store.Public.Manufacturers;
using Store.Public.ProductAttributes;
using Store.Public.ProductCategories;
using Store.Public.Products;

namespace Store.Public;

public class StorePublicApplicationAutoMapperProfile : Profile
{
    public StorePublicApplicationAutoMapperProfile()
    {
        //Product Category
        CreateMap<ProductCategory, ProductCategoryDto>();
        CreateMap<ProductCategory, ProductCategoryInListDto>();

        //Product
        CreateMap<Product, ProductDto>();
        CreateMap<Product, ProductInListDto>();

        CreateMap<Manufacturer, ManufacturerDto>();
        CreateMap<Manufacturer, ManufacturerInListDto>();

        //Product attribute
        CreateMap<ProductAttribute, ProductAttributeDto>();
        CreateMap<ProductAttribute, ProductAttributeInListDto>();

        //Order
        CreateMap<Order, OrderDto>();
    }
}
