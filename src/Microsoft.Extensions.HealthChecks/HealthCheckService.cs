using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Extensions.HealthChecks
{
    public class HealthCheckService : IHealthCheckService
    {
        public IReadOnlyDictionary<string, IHealthCheck> Checks { get; }

        public HealthCheckService(IEnumerable<IHealthCheck> healthChecks)
        {
            Checks = healthChecks.ToDictionary(c => c.Name);
        }

        public Task<CompositeHealthCheckResult> CheckHealthAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
