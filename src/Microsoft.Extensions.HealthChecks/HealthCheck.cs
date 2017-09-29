using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Extensions.HealthChecks
{
    /// <summary>
    /// A simple implementation of <see cref="IHealthCheck"/> which uses a provided delegate to
    /// implement the check.
    /// </summary>
    public class HealthCheck : IHealthCheck
    {
        private readonly Func<CancellationToken, Task<HealthCheckResult>> _check;

        /// <summary>
        /// Gets the name of the health check, which should indicate the component being checked.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Create an instance of <see cref="HealthCheck"/> from the specified <paramref name="name"/> and <paramref name="check"/>.
        /// </summary>
        /// <param name="name">The name of the health check, which should indicate the component being checked.</param>
        /// <param name="check">A delegate which provides the code to execute when the health check is run.</param>
        public HealthCheck(string name, Func<CancellationToken, Task<HealthCheckResult>> check)
        {
            Name = name;
            _check = check;
        }

        /// <summary>
        /// Runs the health check, returning the status of the component being checked.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the health check.</param>
        /// <returns>A <see cref="Task{HealthCheckResult}"/> that completes when the health check has finished, yielding the status of the component being checked.</returns>
        public Task<HealthCheckResult> CheckStatusAsync(CancellationToken cancellationToken = default) => _check(cancellationToken);
    }
}
