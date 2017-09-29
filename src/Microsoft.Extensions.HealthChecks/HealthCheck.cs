using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Extensions.HealthChecks
{
    public class HealthCheck : IHealthCheck
    {
        private readonly Func<CancellationToken, Task<HealthCheckResult>> _check;

        public string Name { get; }

        public HealthCheck(string name, Func<CancellationToken, Task<HealthCheckResult>> check)
        {
            Name = name;
            _check = check;
        }

        public Task<HealthCheckResult> CheckStatusAsync(CancellationToken cancellationToken = default) => _check(cancellationToken);
    }
}
