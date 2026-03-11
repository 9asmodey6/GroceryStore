namespace GroceryStore.Features.Admin.Categories.GetCategories;

using Dapper;
using Database;

public class GetCategoriesRepository(IDbConnectionFactory factory)
{
    public async Task<IEnumerable<GetCategoriesResponse>> GetCategoriesAsync(CancellationToken ct)
    {
        using var connection = factory.CreateConnection();
        const string sql =
            """
            WITH RECURSIVE category_tree AS (
                SELECT 
                    id, 
                    name, 
                    parent_id, 
                    name::text AS path_name,
                    1 AS level
                FROM categories 
                WHERE parent_id IS NULL

                UNION ALL

                SELECT 
                    c.id, 
                    c.name, 
                    c.parent_id, 
                    ct.path_name || ' > ' || c.name,
                    ct.level + 1
                FROM categories c
                JOIN category_tree ct ON c.parent_id = ct.id
            )
            SELECT 
                id AS Id, 
                name AS Name, 
                parent_id AS ParentId,
                path_name AS FullPath,
                level AS Level
            FROM category_tree
            ORDER BY path_name;
            """;

        var cmd = new CommandDefinition(sql, cancellationToken: ct);
        var categories = await connection.QueryAsync<GetCategoriesResponse>(cmd);
        return categories;
    }
}