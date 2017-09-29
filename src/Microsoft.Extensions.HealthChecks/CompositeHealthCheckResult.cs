using System.Collections.Generic;

namespace Microsoft.Extensions.HealthChecks
{
    /// <summary>
    /// Represents the results of multiple health checks
    /// </summary>
    public struct CompositeHealthCheckResult
    {
        /// <summary>
        /// A <see cref="IReadOnlyDictionary{TKey, T}"/> containing the results from each health check.
        /// </summary>
        /// <remarks>
        /// The keys in this dictionary map to the name of the health check, the values are the <see cref="HealthCheckResult"/>
        /// returned when <see cref="IHealthCheck.CheckStatusAsync(System.Threading.CancellationToken)"/> was called for that health check.
        /// </remarks>
        public IReadOnlyDictionary<string, HealthCheckResult> Results { get; }

        /// <summary>
        /// Create a new <see cref="CompositeHealthCheckResult"/> from the specified results
        /// </summary>
        /// <param name="results">A <see cref="IReadOnlyDictionary{TKey, T}"/> containing the results from each health check.</param>
        public CompositeHealthCheckResult(IReadOnlyDictionary<string, HealthCheckResult> results)
        {
            Results = results;
        }
    }
}
