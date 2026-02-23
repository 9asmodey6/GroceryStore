namespace GroceryStore.Features.Admin.Product.GetProducts;

using Dapper;
using Database;
using Database.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

public class GetProductsRepository(IDbConnectionFactory factory)
{
    public async Task<IEnumerable<GetProductsResponse>> GetProductsAsync(CancellationToken ct)
    {
        using var connection = factory.CreateConnection();
        const string sql =
            """
                 SELECT
                    p.id,
                    p.name,
                    p.price,
                    p.category_id,
                    p.sku,
                    p.description,
                    p.base_unit,
                    a.name        AS attribute_name,
                    a.unit        AS attribute_unit,
                    mv.value      AS attribute_value
                        FROM products p
                    LEFT JOIN LATERAL jsonb_each_text(p.metadata) mv(key, value) ON TRUE
                    LEFT JOIN attributes a ON a.id = (mv.key)::int
                        WHERE p.is_active = true
                    ORDER BY p.id
            """;

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