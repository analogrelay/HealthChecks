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
        public HealthCheckStatus Status { get; }
        public string Description => _description ?? string.Empty;
        public IReadOnlyDictionary<string, object> Data => _data ?? _emptyReadOnlyDictionary;

        public HealthCheckResult(HealthCheckStatus status, string description, IReadOnlyDictionary<string, object> data)
        {
            Status = status;
            _description = description;
            _data = data;
        }

        public static HealthCheckResult Unhealthy(string description)
            => new HealthCheckResult(HealthCheckStatus.Unhealthy, description, null);

        public static HealthCheckResult Unhealthy(string description, IReadOnlyDictionary<string, object> data)
            => new HealthCheckResult(HealthCheckStatus.Unhealthy, description, data);

        public static HealthCheckResult Healthy(string description)
            => new HealthCheckResult(HealthCheckStatus.Healthy, description, null);

        public static HealthCheckResult Healthy(string description, IReadOnlyDictionary<string, object> data)
            => new HealthCheckResult(HealthCheckStatus.Healthy, description, data);

        public static HealthCheckResult Warning(string description)
            => new HealthCheckResult(HealthCheckStatus.Warning, description, null);

        public static HealthCheckResult Warning(string description, IReadOnlyDictionary<string, object> data)
            => new HealthCheckResult(HealthCheckStatus.Warning, description, data);

        public static HealthCheckResult Unknown(string description)
            => new HealthCheckResult(HealthCheckStatus.Unknown, description, null);

        public static HealthCheckResult Unknown(string description, IReadOnlyDictionary<string, object> data)
            => new HealthCheckResult(HealthCheckStatus.Unknown, description, data);

        public static HealthCheckResult FromStatus(HealthCheckStatus status, string description)
            => new HealthCheckResult(status, description, null);

        public static HealthCheckResult FromStatus(HealthCheckStatus status, string description, IReadOnlyDictionary<string, object> data)
            => new HealthCheckResult(status, description, data);
    }
}
