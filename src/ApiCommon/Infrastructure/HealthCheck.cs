using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Common.Infrastructure
{
    public class HealthCheck : IHealthCheck
    {
        public async virtual Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            return HealthCheckResult.Healthy("Not implemented health check");
        }
    }
}
