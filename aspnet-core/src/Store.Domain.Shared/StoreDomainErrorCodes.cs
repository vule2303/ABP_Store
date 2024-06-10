namespace Store;

public static class StoreDomainErrorCodes
{
    public const string ProductNameAlreadyExists = "Store:ProductNameAlreadyExists";
    public const string ProductCodeAlreadyExists = "Store:ProductCodeAlreadyExists";

    public const string ProductSKUAlreadyExists = "Store:ProductSKUAlreadyExists";
    public const string ProductIsNotExists = "Store:ProductIsNotExists";
    public const string ProductAttributeIdIsNotExists = "Store:ProductAttributeIdIsNotExists";

    public const string ProductAttributeValueIsNotValid = "Store:ProductAttributeValueIsNotValid";
}
