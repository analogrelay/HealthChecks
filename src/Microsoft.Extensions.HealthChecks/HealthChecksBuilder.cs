using Microsoft.Extensions.HealthChecks;

namespace Microsoft.Extensions.DependencyInjection
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
