namespace GroceryStore.Features.Admin.Products.GetProductById;

using Dapper;
using GroceryStore.Database;
using GroceryStore.Shared.Models;
using Shared.Interfaces;

public class GetProductByIdRepository(IDbConnectionFactory factory) : IRepository
{
    public async Task<GetProductByIdResponse?> GetProductByIdAsync(int productId, CancellationToken ct)
    {
        using var connection = factory.CreateConnection();

        const string sql =
            """"
            SELECT
                p.id AS Id,
                p.name AS Name,
                p.price AS Price,
                cat.name AS CategoryName,
                b.name AS BrandName,
                c.code AS CountryCode,
                p.sku AS Sku,
                p.description AS Description,
                p.base_unit AS BaseUnit,
                a.name AS AttributeName,
                a.unit AS AttributeUnit,
                mv.value AS AttributeValue
            FROM products p
            JOIN categories cat ON cat.id = p.category_id
            JOIN brands b ON b.id = p.brand_id
            JOIN countries c ON c.id = p.country_id
            LEFT JOIN LATERAL jsonb_each_text(p.metadata) mv(key, value) ON TRUE
            LEFT JOIN attributes a ON a.id = (mv.key)::int
            WHERE p.id = @productId AND p.is_active = true
            """";

        var cmd = new CommandDefinition(
            sql,
            parameters: new { productId },
            cancellationToken: ct);

        var rows = await connection.QueryAsync<ProductRow>(cmd);

        var list = rows.ToList();

        if (list.Count == 0)
        {
            return null;
        }

        var first = list[0];

        return new GetProductByIdResponse(
            first.Id,
            first.Name,
            first.Price,
            first.CategoryName,
            first.BrandName,
            first.CountryCode,
            first.Sku,
            first.Description,
            first.BaseUnit,
            list.Where(x => x.AttributeName != null)
                .Select(a => new GetProductByIdMetadataResponse(
                    a.AttributeName,
                    a.AttributeValue,
                    a.AttributeUnit))
                .ToList());
    }
}