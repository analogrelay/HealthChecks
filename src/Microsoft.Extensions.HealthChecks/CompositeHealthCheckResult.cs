using System.Collections.Generic;

namespace Microsoft.Extensions.HealthChecks
{
    public class CompositeHealthCheckResult
    {
        public IReadOnlyDictionary<string, HealthCheckResult> Results { get; }

        public CompositeHealthCheckResult(IReadOnlyDictionary<string, HealthCheckResult> results)
        {
            Results = results;
        }
    }
}
