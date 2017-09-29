namespace Microsoft.Extensions.HealthChecks
{
    public enum HealthCheckStatus
    {
        Unknown = 0,
        Unhealthy = 1,
        Degraded = 2,
        Healthy = 3,
    }
}
