using System;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.HealthChecks;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Provides extension methods for registering <see cref="HealthCheckService"/> in an <see cref="IServiceCollection"/>.
    /// </summary>
    public static class HealthCheckServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the <see cref="HealthCheckService"/> to the container.
        /// </summary>
        /// <remarks>
        /// This operation is idempotent - multiple invocations will still only result in a single
        /// <see cref="HealthCheckService"/> instance in the <see cref="IServiceCollection"/>. It can be invoked
        /// multiple times in order to get access to the <see cref="IHealthChecksBuilder"/> in multiple places.
        /// </remarks>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the <see cref="HealthCheckService"/> to.</param>
        /// <returns>The original <see cref="IServiceCollection"/> instance</returns>
        public static IServiceCollection AddHealthChecks(this IServiceCollection services) =>
            services.AddHealthChecks(_ => { });

        /// <summary>
        /// Adds the <see cref="HealthCheckService"/> to the container, using the provided delegate to register
        /// health checks.
        /// </summary>
        /// <remarks>
        /// This operation is idempotent - multiple invocations will still only result in a single
        /// <see cref="HealthCheckService"/> instance in the <see cref="IServiceCollection"/>. It can be invoked
        /// multiple times in order to get access to the <see cref="IHealthChecksBuilder"/> in multiple places.
        /// </remarks>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the <see cref="HealthCheckService"/> to.</param>
        /// <param name="configure">A delegate which recieves a <see cref="IHealthChecksBuilder"/> and registers health checks on it.</param>
        /// <returns>The original <see cref="IServiceCollection"/> instance</returns>
        public static IServiceCollection AddHealthChecks(this IServiceCollection services, Action<IHealthChecksBuilder> configure)
        {
            services.TryAdd(ServiceDescriptor.Singleton<HealthCheckService, DefaultHealthCheckService>());

            configure(new HealthChecksBuilder(services));

            return services;
        }
    }
}
