using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.HealthChecks
{
    internal class HealthChecksBuilder : IHealthChecksBuilder
    {
        public IServiceCollection Services { get; }

        public HealthChecksBuilder(IServiceCollection services)
        {
            Services = services;
        }
    }
}
