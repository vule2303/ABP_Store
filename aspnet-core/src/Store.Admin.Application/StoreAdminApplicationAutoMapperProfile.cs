﻿using AutoMapper;
using Store.Admin.Manufacturers;
using Store.Admin.ProductAttributes;
using Store.Admin.ProductCategories;
using Store.Admin.Products;
using Store.Admin.Roles;
using Store.Manufacturers;
using Store.ProductAttributes;
using Store.ProductCategories;
using Store.Products;
using Store.Roles;
using Volo.Abp.Identity;

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

        //Roles
        CreateMap<IdentityRole, RoleDto>().ForMember(x => x.Description,
            map => map.MapFrom(x => x.ExtraProperties.ContainsKey(RoleConsts.DescriptionFieldName)
            ?
            x.ExtraProperties[RoleConsts.DescriptionFieldName]
            :
            null));
        CreateMap<IdentityRole, RoleInListDto>().ForMember(x => x.Description,
            map => map.MapFrom(x => x.ExtraProperties.ContainsKey(RoleConsts.DescriptionFieldName)
            ?
            x.ExtraProperties[RoleConsts.DescriptionFieldName]
            :
            null));
        CreateMap<CreateUpdateRoleDto, IdentityRole>();
    }
}
