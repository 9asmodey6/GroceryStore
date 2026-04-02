namespace GroceryStore.Shared.Interfaces.Repositories;

using GroceryStore.Shared.Models;

public interface ICategoryAttributeRepository
{
    Task<List<MetadataAttribute>> GetMetadataSchemaAsync(int categoryId, CancellationToken ct);
}