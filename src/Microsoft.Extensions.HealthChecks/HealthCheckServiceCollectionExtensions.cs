using System;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.HealthChecks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class HealthCheckServiceCollectionExtensions
    {
        public static IServiceCollection AddHealthChecks(this IServiceCollection services, Action<IHealthChecksBuilder> configure)
        {
            services.TryAdd(ServiceDescriptor.Singleton<IHealthCheckService, HealthCheckService>());

            configure(new HealthChecksBuilder(services));

            return services;
        }
    }
}
