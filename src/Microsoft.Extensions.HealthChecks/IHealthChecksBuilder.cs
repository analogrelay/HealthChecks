using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.HealthChecks
{
    public interface IHealthChecksBuilder
    {
        IServiceCollection Services { get; }
    }
}
