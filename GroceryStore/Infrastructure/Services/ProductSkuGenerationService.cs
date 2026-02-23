namespace GroceryStore.Infrastructure.Services;

using System.Security.Cryptography;
using GroceryStore.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

public class ProductSkuGenerationService
{
    private readonly IMemoryCache _cache;
    private readonly AppDbContext _context;

    public ProductSkuGenerationService(
        IMemoryCache cache,
        AppDbContext context)
    {
        _cache = cache;
        _context = context;
    }

    public async Task<string> CreateSku(int categoryId, CancellationToken ct)
    {
        var categoryName = await GetCategoryNameCached(categoryId, ct);

        var prefix = string.IsNullOrWhiteSpace(categoryName)
            ? "GEN"
            : new string(categoryName
                    .Where(char.IsLetter)
                    .Take(4)
                    .ToArray())
                .ToUpper()
                .PadRight(4, 'X');

        var random = RandomNumberGenerator.GetInt32(100000, 999999);

        return $"{prefix}-{random}";
    }

    private async Task<string?> GetCategoryNameCached(int categoryId, CancellationToken ct)
    {
        var cacheKey = $"category_name_{categoryId}";

        return await _cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(3);
            var name = await _context.Categories
                .AsNoTracking()
                .Where(c => c.Id == categoryId)
                .Select(c => c.Name)
                .FirstOrDefaultAsync(ct);

            if (string.IsNullOrWhiteSpace(name))
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2);
            }

            return name;
        });
    }
}