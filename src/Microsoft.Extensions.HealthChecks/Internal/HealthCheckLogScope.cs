using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Extensions.HealthChecks.Internal
{
    public class HealthCheckLogScope : IReadOnlyList<KeyValuePair<string, object>>
    {
        public string HealthCheckName { get; }

        public int Count { get; } = 1;

        public KeyValuePair<string, object> this[int index]
        {
            get
            {
                if (index == 0)
                {
                    return new KeyValuePair<string, object>(nameof(HealthCheckName), HealthCheckName);
                }

                throw new ArgumentOutOfRangeException(nameof(index));
            }
        }

        public HealthCheckLogScope(string name)
        {
            HealthCheckName = name;
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            yield return this[0];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
