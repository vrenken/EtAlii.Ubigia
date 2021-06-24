// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

#nullable enable

namespace EtAlii.Ubigia.Infrastructure.Transport.SignalR
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Threading;
    using Microsoft.AspNetCore.SignalR;
    using System.Collections.Generic;
    using EtAlii.xTechnology.Diagnostics;

    public class CorrelationServiceHubFilter : IHubFilter
    {
        private readonly IContextCorrelator _contextCorrelator;

        public CorrelationServiceHubFilter(IContextCorrelator contextCorrelator)
        {
            _contextCorrelator = contextCorrelator;
        }

        /// <summary>
        /// Allows handling of all Hub method invocations.
        /// </summary>
        /// <param name="invocationContext">The context for the method invocation that holds all the important information about the invoke.</param>
        /// <param name="next">The next filter to run, and for the final one, the Hub invocation.</param>
        /// <returns>Returns the result of the Hub method invoke.</returns>
        public ValueTask<object?> InvokeMethodAsync(HubInvocationContext invocationContext, Func<HubInvocationContext, ValueTask<object?>> next)
        {
            using var disposables = BeginCorrelation(invocationContext.Context.Items);
            return next(invocationContext);
        }

        private IDisposable BeginCorrelation(IDictionary<object, object?> items)
        {
            var correlations = new List<IDisposable>();

            var keys = items.Keys.ToArray();
            foreach (var keyObject in keys)
            {
                var valueObject = items[keyObject];

                if (keyObject is not string key || valueObject is not string value)
                {
                    continue;
                }

                foreach (var correlationId in Correlation.AllIds)
                {
                    if (string.Equals(correlationId, key, StringComparison.OrdinalIgnoreCase))
                    {
                        items.Remove(key);

                        var correlation = _contextCorrelator.BeginLoggingCorrelationScope(correlationId, value);
                        correlations.Add(correlation);
                        break;
                    }
                }
            }
            return new CompositeDisposable(correlations);
        }
    }
}
