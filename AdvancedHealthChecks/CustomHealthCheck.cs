using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace AdvancedHealthChecks;

public class CustomHealthCheck : IHealthCheck
{
    private static bool _isHealthy = true;

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var result = _isHealthy ? HealthCheckResult.Healthy("OK") : HealthCheckResult.Unhealthy("Fail");

        _isHealthy = !_isHealthy;

        return Task.FromResult(result);
    }
}
