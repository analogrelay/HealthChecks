using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Extensions.HealthChecks
{
    public interface IHealthCheck
    {
        /// <summary>
        /// Gets the name of the health check, which should indicate the component being checked.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Runs the health check and returns the results
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the check.</param>
        /// <returns>A <see cref="HealthCheckResult"/> representing the result of the check.</returns>
        Task<HealthCheckResult> CheckStatusAsync(CancellationToken cancellationToken = default);
    }
}
