using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Microsoft.Extensions.HealthChecks.Test
{
    public class ServiceCollectionExtensionsTest
    {
        [Fact]
        public void AddHealthChecks_RegistersSingletonHealthCheckServiceIdempotently()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddHealthChecks(_ => { });
            services.AddHealthChecks(_ => { });

            // Assert
            Assert.Collection(services,
                actual =>
                {
                    Assert.Equal(ServiceLifetime.Singleton, actual.Lifetime);
                    Assert.Equal(typeof(HealthCheckService), actual.ServiceType);
                    Assert.Equal(typeof(DefaultHealthCheckService), actual.ImplementationType);
                    Assert.Null(actual.ImplementationInstance);
                    Assert.Null(actual.ImplementationFactory);
                });
        }
    }
}
