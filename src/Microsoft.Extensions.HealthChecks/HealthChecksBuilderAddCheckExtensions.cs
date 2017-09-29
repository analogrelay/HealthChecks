using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.HealthChecks
{
    public static class HealthChecksBuilderAddCheckExtensions
    {
        public static IHealthChecksBuilder AddCheck(this IHealthChecksBuilder builder, string name, Func<CancellationToken, Task<HealthCheckResult>> check)
        {
            builder.Services.AddSingleton<IHealthCheck>(services => new HealthCheck(name, check));
            return builder;
        }

        public static IHealthChecksBuilder AddCheck(this IHealthChecksBuilder builder, string name, Func<Task<HealthCheckResult>> check) =>
            builder.AddCheck(name, _ => check());
    }
}
