using System;
using System.Collections.Generic;

namespace Microsoft.Extensions.HealthChecks
{
    public struct HealthCheckResult
    {
        private static readonly IReadOnlyDictionary<string, object> _emptyReadOnlyDictionary = new Dictionary<string, object>();

        private string _description;
        private IReadOnlyDictionary<string, object> _data;

        // REVIEW: Put the name of the health check or IHealthCheck instance on here?

        /// <summary>
        /// Gets a <see cref="HealthCheckStatus"/> representing the status of the component being checked.
        /// </summary>
        public HealthCheckStatus Status { get; }

        /// <summary>
        /// Gets an <see cref="Exception"/> representing the exception that was thrown when checking for status (if any).
        /// </summary>
        /// <remarks>
        /// This value is expected to be 'null' if <see cref="Status"/> is <see cref="HealthCheckStatus.Healthy"/>.
        /// </remarks>
        public Exception Exception { get; }

        /// <summary>
        /// Gets a human-readable description of the health check status
        /// </summary>
        public string Description => _description ?? string.Empty;

        /// <summary>
        /// Gets additional key-value pairs representing the health of the component
        /// </summary>
        public IReadOnlyDictionary<string, object> Data => _data ?? _emptyReadOnlyDictionary;

        public HealthCheckResult(HealthCheckStatus status, Exception exception, string description, IReadOnlyDictionary<string, object> data)
        {
            Status = status;
            Exception = exception;
            _description = description;
            _data = data;
        }

        public static HealthCheckResult Unhealthy()
            => new HealthCheckResult(HealthCheckStatus.Unhealthy, exception: null, description: string.Empty, data: null);

        public static HealthCheckResult Unhealthy(string description)
            => new HealthCheckResult(HealthCheckStatus.Unhealthy, exception: null, description: description, data: null);

        public static HealthCheckResult Unhealthy(string description, IReadOnlyDictionary<string, object> data)
            => new HealthCheckResult(HealthCheckStatus.Unhealthy, exception: null, description: description, data: data);

        public static HealthCheckResult Unhealthy(Exception exception)
            => new HealthCheckResult(HealthCheckStatus.Unhealthy, exception, description: string.Empty, data: null);

        public static HealthCheckResult Unhealthy(string description, Exception exception)
            => new HealthCheckResult(HealthCheckStatus.Unhealthy, exception, description, data: null);

        public static HealthCheckResult Unhealthy(string description, Exception exception, IReadOnlyDictionary<string, object> data)
            => new HealthCheckResult(HealthCheckStatus.Unhealthy, exception, description, data);

        public static HealthCheckResult Healthy()
            => new HealthCheckResult(HealthCheckStatus.Healthy, exception: null, description: string.Empty, data: null);

        public static HealthCheckResult Healthy(string description)
            => new HealthCheckResult(HealthCheckStatus.Healthy, exception: null, description: description, data: null);

        public static HealthCheckResult Healthy(string description, IReadOnlyDictionary<string, object> data)
            => new HealthCheckResult(HealthCheckStatus.Healthy, exception: null, description: description, data: data);

        public static HealthCheckResult Degraded()
            => new HealthCheckResult(HealthCheckStatus.Degraded, exception: null, description: string.Empty, data: null);

        public static HealthCheckResult Degraded(string description)
            => new HealthCheckResult(HealthCheckStatus.Degraded, exception: null, description: description, data: null);

        public static HealthCheckResult Degraded(string description, IReadOnlyDictionary<string, object> data)
            => new HealthCheckResult(HealthCheckStatus.Degraded, exception: null, description: description, data: data);

        public static HealthCheckResult Degraded(Exception exception)
            => new HealthCheckResult(HealthCheckStatus.Degraded, exception: null, description: string.Empty, data: null);

        public static HealthCheckResult Degraded(string description, Exception exception)
            => new HealthCheckResult(HealthCheckStatus.Degraded, exception, description, data: null);

        public static HealthCheckResult Degraded(string description, Exception exception, IReadOnlyDictionary<string, object> data)
            => new HealthCheckResult(HealthCheckStatus.Degraded, exception, description, data);

        public static HealthCheckResult FromStatus(HealthCheckStatus status)
            => new HealthCheckResult(status, exception: null, description: string.Empty, data: null);

        public static HealthCheckResult FromStatus(HealthCheckStatus status, string description)
            => new HealthCheckResult(status, exception: null, description: description, data: null);

        public static HealthCheckResult FromStatus(HealthCheckStatus status, string description, IReadOnlyDictionary<string, object> data)
            => new HealthCheckResult(status, exception: null, description: description, data: data);
    }
}
