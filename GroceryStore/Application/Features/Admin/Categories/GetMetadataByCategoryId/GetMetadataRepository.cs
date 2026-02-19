namespace GroceryStore.Application.Features.Admin.Categories.GetMetadataByCategoryId;

using Dapper;
using GroceryStore.Infrastructure;
using Infrastructure.Interfaces;
using Models;
using Services;

public class GetMetadataRepository
{
    private readonly CategoryAttributeService _service;

    public GetMetadataRepository(CategoryAttributeService service)
    {
        _service = service;
    }

    public async Task<List<MetadataAttributes>> GetAttributesResponseAsync(
        int categoryId,
        CancellationToken ct)
    {
        var result = await _service.GetMetadataSchemaAsync(categoryId, ct);
        return result;
    }
}