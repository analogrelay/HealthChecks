using System;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.HealthChecks;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Provides extension methods for registering <see cref="IHealthCheckService"/> in an <see cref="IServiceCollection"/>.
    /// </summary>
    public static class HealthCheckServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <see cref="IHealthCheckService"/> to the container, using the provided delegate to register
        /// health checks.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the <see cref="IHealthCheckService"/> to.</param>
        /// <param name="configure">A delegate which recieves a <see cref="IHealthChecksBuilder"/> and registers health checks on it.</param>
        /// <returns>The original <see cref="IServiceCollection"/> instance</returns>
        public static IServiceCollection AddHealthChecks(this IServiceCollection services, Action<IHealthChecksBuilder> configure)
        {
            services.TryAdd(ServiceDescriptor.Singleton<IHealthCheckService, HealthCheckService>());

            configure(new HealthChecksBuilder(services));

            return services;
        }
    }
}
