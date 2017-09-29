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
    /// <summary>
    /// Default implementation of <see cref="IHealthCheckService"/>
    /// </summary>
    public class HealthCheckService : IHealthCheckService
    {
        private readonly ILogger<HealthCheckService> _logger;

        /// <summary>
        /// A <see cref="IReadOnlyDictionary{TKey, T}"/> containing all the health checks registered in the application.
        /// </summary>
        /// <remarks>
        /// The key maps to the <see cref="IHealthCheck.Name"/> property of the health check, and the value is the <see cref="IHealthCheck"/>
        /// instance itself.
        /// </remarks>
        public IReadOnlyDictionary<string, IHealthCheck> Checks { get; }

        /// <summary>
        /// Constructs a <see cref="HealthCheckService"/> from the provided collection of <see cref="IHealthCheck"/> instances.
        /// </summary>
        /// <param name="healthChecks">The <see cref="IHealthCheck"/> instances that have been registered in the application.</param>
        public HealthCheckService(IEnumerable<IHealthCheck> healthChecks) : this(healthChecks, NullLogger<HealthCheckService>.Instance) { }

        /// <summary>
        /// Constructs a <see cref="HealthCheckService"/> from the provided collection of <see cref="IHealthCheck"/> instances, and the provided logger.
        /// </summary>
        /// <param name="healthChecks">The <see cref="IHealthCheck"/> instances that have been registered in the application.</param>
        /// <param name="logger">A <see cref="ILogger{T}"/> that can be used to log events that occur during health check operations.</param>
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

        /// <summary>
        /// Runs all the health checks in the application and returns the aggregated status.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> which can be used to cancel the health checks</param>
        /// <returns>
        /// A <see cref="Task{CompositeHealthCheckResult}"/> which will complete when all the health checks have been run,
        /// yielding a <see cref="CompositeHealthCheckResult"/> containing the results.
        /// </returns>
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
