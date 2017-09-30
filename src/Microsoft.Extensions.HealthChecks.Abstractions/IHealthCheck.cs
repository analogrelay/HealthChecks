using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Extensions.HealthChecks
{
    /// <summary>
    /// Represents a health check, which can be used to check the status of a component in the application, such as a backend service, database or some internal
    /// state.
    /// </summary>
    public interface IHealthCheck
    {
        /// <summary>
        /// Gets the name of the health check, which should indicate the component being checked.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Runs the health check, returning the status of the component being checked.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the health check.</param>
        /// <returns>A <see cref="Task{HealthCheckResult}"/> that completes when the health check has finished, yielding the status of the component being checked.</returns>
        Task<HealthCheckResult> CheckStatusAsync(CancellationToken cancellationToken = default);
    }
}
