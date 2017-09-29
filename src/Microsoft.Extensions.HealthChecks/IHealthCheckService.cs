using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Extensions.HealthChecks
{
    public interface IHealthCheckService
    {
        IReadOnlyDictionary<string, IHealthCheck> Checks { get; }

        Task<CompositeHealthCheckResult> CheckHealthAsync(CancellationToken cancellationToken = default);
    }
}
