using System.Data;

namespace GroceryStore.Features.Admin.Product.CreateProduct;

using GroceryStore.Infrastructure;
using Dapper;

public class GetMetadataRepository
{
    private readonly IDbConnectionFactory _factory;

    public GetMetadataRepository(IDbConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<List<AttributeDTO>> GetAttributesResponseAsync(
        int categoryId,
        CancellationToken ct)
    {
        using var connection = _factory.CreateConnection();
        string sql = @"
WITH RECURSIVE category_tree AS (
    SELECT id, parent_id 
    FROM categories 
    WHERE id = @categoryId

    UNION ALL

    SELECT c.id, c.parent_id 
    FROM categories c
    JOIN category_tree ct ON c.id = ct.parent_id
)

SELECT DISTINCT
    a.id              AS AttributeId,
    a.name            AS Name,
    a.data_type       AS DataType,
    a.unit            AS Unit,
    a.min_value       AS MinValue,
    a.max_value       AS MaxValue,
    ca.is_required    AS IsRequired
FROM category_tree ct
JOIN category_attributes ca ON ct.id = ca.category_id
JOIN attributes a ON ca.attribute_id = a.id
";
        var cmd = new CommandDefinition(sql, new { categoryId }, cancellationToken: ct);
        var result = await connection.QueryAsync<AttributeDTO>(cmd);
        return result.ToList();
    }
}