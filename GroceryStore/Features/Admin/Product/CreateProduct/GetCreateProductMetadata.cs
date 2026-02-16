using Dapper;
using Microsoft.Extensions.Caching.Memory;
using GroceryStore.Infrastructure;

namespace GroceryStore.Features.Admin.Product.CreateProduct;

using GroceryStore.Domain;

public class GetCreateProductMetadata
{
}

public static class GetAttributesFormEndpoint
{
    public static void MapCreateProduct(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/admin/products/metadata/{categoryId}", async (
            int categoryId,
            IDbConnectionFactory factory,
            IMemoryCache cache) =>
        {
            var cacheKey = $"category_metadata_{categoryId}";

            if (cache.TryGetValue(cacheKey, out List<AttributeDTO>? cachedMetadata))
                return Results.Ok(cachedMetadata);
        });
    }

    public static async Task<List<AttributeDTO>> GetAttributesRecursiveGetAttributesRecursive(int categoryId,
        IDbConnectionFactory factory)
    {
        using var connection = factory.CreateConnection();

        string sql = @"WITH RECURSIVE category_tree AS (
        SELECT id, parent_id FROM categories WHERE  id = @categoryId
        UNION ALL
        SELECT c.id, c.parent_id 
        FROM categories c
        JOIN  category_tree ct
        ON c.id = ct.parent_id
        )
            SELECT 
                a.name AS Name, 
                a.unit AS Unit, 
                ca.is_required AS IsRequired
            FROM category_tree ct
            JOIN category_attributes ca ON ct.id = ca.category_id
            JOIN attributes a ON ca.attribute_id = a.id";
        
        var result = await connection.QueryAsync<AttributeDTO>(sql);
    }
}

public record AttributeDTO(string name, string unit, bool isRequired);