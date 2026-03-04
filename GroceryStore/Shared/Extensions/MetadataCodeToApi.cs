namespace GroceryStore.Shared.Extensions;

using Enums;

public static class MetadataCodeToApi
{
    public static string ToApiCode(this MetadataValidationErrorCode code)
    {
        return code switch
        {
            MetadataValidationErrorCode.Required => "required_missing",
            MetadataValidationErrorCode.InvalidType => "invalid_type",
            MetadataValidationErrorCode.OutOfRange => "out_of_range",
            MetadataValidationErrorCode.DuplicateAttribute => "duplicate_attribute",
            MetadataValidationErrorCode.NotFoundMetadata => "not_found_metadata",
            MetadataValidationErrorCode.UnknowAttribute => "unknow_attribute",
            _ => "unknow_error"
        };
    }
}