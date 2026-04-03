namespace GroceryStore.Shared.Consts.ValidationMessages;

public static class ProductValidationMessages
{
    public const string NameRequired = "Name is required";
    public const string PriceNegative = "Price must be greater than or equal to 0";
    public const string AttributesRequired = "Attributes are required";
    public const string AttributeDuplicates = "Duplicate attributeId in request";
}