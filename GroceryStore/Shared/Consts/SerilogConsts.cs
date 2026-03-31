namespace GroceryStore.Shared.Consts;

public static class SerilogConsts
{
    public const string MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms. User: {UserId}";
}