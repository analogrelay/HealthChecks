using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.HealthChecks.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Microsoft.Extensions.HealthChecks
{
    public class HealthCheckService : IHealthCheckService
    {
        private readonly ILogger<HealthCheckService> _logger;

        public IReadOnlyDictionary<string, IHealthCheck> Checks { get; }

        public HealthCheckService(IEnumerable<IHealthCheck> healthChecks) : this(healthChecks, NullLogger<HealthCheckService>.Instance) { }

        public HealthCheckService(IEnumerable<IHealthCheck> healthChecks, ILogger<HealthCheckService> logger)
        {
            Checks = healthChecks.ToDictionary(c => c.Name);
            _logger = logger;

            if (_logger.IsEnabled(LogLevel.Debug))
            {
                foreach (var check in Checks)
                {
                    _logger.LogDebug("Health check '{healthCheckName}' has been registered", check.Key);
                }
            }
        }

        public async Task<CompositeHealthCheckResult> CheckHealthAsync(CancellationToken cancellationToken = default)
        {
            var results = new Dictionary<string, HealthCheckResult>(Checks.Count);
            foreach (var pair in Checks)
            {
                // If the health check does things like make Database queries using EF or backend HTTP calls,
                // it may be valuable to know that logs it generates are part of a health check. So we start a scope.
                using (_logger.BeginScope(new HealthCheckLogScope(pair.Key)))
                {
                    try
                    {
                        _logger.LogTrace("Running health check: {healthCheckName}", pair.Key);
                        results[pair.Key] = await pair.Value.CheckStatusAsync(cancellationToken);
                        _logger.LogTrace("Health check '{healthCheckName}' completed with status '{healthCheckStatus}'", pair.Key, results[pair.Key].Status);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogTrace(ex, "Health check '{healthCheckName}' threw an unexpected exception", pair.Key);
                        results[pair.Key] = new HealthCheckResult(HealthCheckStatus.Failed, ex, ex.Message, data: null);
                    }
                }
            }
            return new CompositeHealthCheckResult(results);
        }
    }
}
