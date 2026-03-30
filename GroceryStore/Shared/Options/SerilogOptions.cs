namespace GroceryStore.Shared.Options;

public class SerilogOptions
{
    public const string SectionName = "SerilogSettings";

    public string FilePath { get; init; } = string.Empty;

    public string OutputTemplate { get; init; } = string.Empty;

    public string SeqUrl { get; init; } = string.Empty;
}