namespace GroceryStore.Features.Admin.Product.GetProducts;

using Dapper;
using Database;

public class GetProductsRepository(IDbConnectionFactory factory)
{
    public async Task<IEnumerable<GetProductsResponse>> GetProductsAsync(CancellationToken ct)
    {
        using var connection = factory.CreateConnection();
        const string sql =
            """
                 SELECT
                    p.id          AS Id,
                    p.name        AS Name, 
                    p.price       AS Price,
                    p.category_id AS CategoryId,
                    p.sku         AS Sku,
                    p.description AS Description,
                    p.base_unit   AS BaseUnit,
                    a.name        AS AttributeName,
                    a.unit        AS AttributeUnit,
                    mv.value      AS AttributeValue
                        FROM products p
                    LEFT JOIN LATERAL jsonb_each_text(p.metadata) mv(key, value) ON TRUE
                    LEFT JOIN attributes a ON a.id = (mv.key)::int
                        WHERE p.is_active = true
                    ORDER BY p.id
            """;
        // TODO: Filter by DataType;
        var cmd = new CommandDefinition(sql, cancellationToken: ct);
        var rows = await connection.QueryAsync<ProductRow>(cmd);
        return rows
            .GroupBy(r => r.Id)
            .Select(g =>
            {
                var first = g.First();

                return new GetProductsResponse(
                    first.Id,
                    first.Name,
                    first.Price,
                    first.CategoryId,
                    first.Sku,
                    first.Description,
                    first.BaseUnit,
                    g.Where(x => x.AttributeName != null)
                        .Select(x => new GetProductsMetadataResponse(
                            x.AttributeName,
                            x.AttributeValue,
                            x.AttributeUnit))
                        .ToList()
                );
            });
    }
}