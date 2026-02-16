using Microsoft.Extensions.Caching.Memory;

namespace GroceryStore.Features.Admin.Product.CreateProduct;
using GroceryStore.Domain;
public class GetCreateProductMetadata
{
    
}


public static class GetAttributesFormEndpoint
{
    public static void MapCreateProduct(this IEndpointRouteBuilder app, IMemoryCache cache)
    {
        app.MapGet("api/admin/products/metadata/{categoryId}", async (int categoryId, IDbContextFactory factory)
    }
}

public record AttributeDTO(string name,string unit,bool isRequired);