namespace GroceryStore.Infrastructure.HealthChecks;

using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Shared.Options;

public class SeqHealthCheck(IOptions<SerilogOptions> options) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken ct = default)
    {
        try
        {
            using var client = new HttpClient();

            var response = await client.GetAsync($"{options.Value.SeqUrl}/api", ct);

            return response.IsSuccessStatusCode
                ? HealthCheckResult.Healthy("Seq is reachable")
                : HealthCheckResult.Unhealthy($"Seq returned {response.StatusCode}");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("Seq service is unreachable", ex);
        }
    }
}