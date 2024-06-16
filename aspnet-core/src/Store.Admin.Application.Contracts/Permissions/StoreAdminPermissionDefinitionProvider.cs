using Store.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Store.Admin.Permissions;

public class StoreAdminPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        //Catalog
        var catalogGroup = context.AddGroup(StoreAdminPermissions.CatalogGroupName, L("Permission:Catalog"));

        //Add product
        var productPermission = catalogGroup.AddPermission(StoreAdminPermissions.Product.Default, L("Permission:Catalog.Product"));
        productPermission.AddChild(StoreAdminPermissions.Product.Create, L("Permission:Catalog.Product.Create"));
        productPermission.AddChild(StoreAdminPermissions.Product.Update, L("Permission:Catalog.Product.Update"));
        productPermission.AddChild(StoreAdminPermissions.Product.Delete, L("Permission:Catalog.Product.Delete"));
        productPermission.AddChild(StoreAdminPermissions.Product.AttributeManage, L("Permission:Catalog.Product.AttributeManage"));

        //Add attribute
        var attributePermission = catalogGroup.AddPermission(StoreAdminPermissions.Attribute.Default, L("Permission:Catalog.Attribute"));
        attributePermission.AddChild(StoreAdminPermissions.Attribute.Create, L("Permission:Catalog.Attribute.Create"));
        attributePermission.AddChild(StoreAdminPermissions.Attribute.Update, L("Permission:Catalog.Attribute.Update"));
        attributePermission.AddChild(StoreAdminPermissions.Attribute.Delete, L("Permission:Catalog.Attribute.Delete"));
        //Manufacture
        var manufacturerPermission = catalogGroup.AddPermission(StoreAdminPermissions.Manufacturer.Default, L("Permission:Catalog.Manufacturer"));
        manufacturerPermission.AddChild(StoreAdminPermissions.Manufacturer.Create, L("Permission:Catalog.Manufacturer.Create"));
        manufacturerPermission.AddChild(StoreAdminPermissions.Manufacturer.Update, L("Permission:Catalog.Manufacturer.Update"));
        manufacturerPermission.AddChild(StoreAdminPermissions.Manufacturer.Delete, L("Permission:Catalog.Manufacturer.Delete"));

        //Product Category
        var productCategoryPermission = catalogGroup.AddPermission(StoreAdminPermissions.ProductCategory.Default, L("Permission:Catalog.ProductCategory"));
        productCategoryPermission.AddChild(StoreAdminPermissions.ProductCategory.Create, L("Permission:Catalog.ProductCategory.Create"));
        productCategoryPermission.AddChild(StoreAdminPermissions.ProductCategory.Update, L("Permission:Catalog.ProductCategory.Update"));
        productCategoryPermission.AddChild(StoreAdminPermissions.ProductCategory.Delete, L("Permission:Catalog.ProductCategory.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<StoreResource>(name);
    }
}
