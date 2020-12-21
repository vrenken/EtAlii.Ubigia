namespace EtAlii.Ubigia.Infrastructure.Transport.Rest
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Builder;
    using EtAlii.xTechnology.Threading;
    using EtAlii.xTechnology.Diagnostics;
    using Microsoft.AspNetCore.Http;
    using Serilog;

    public static class ApplicationBuilderCorrelationExtensions
    {
        private static readonly ILogger _logger = Log.ForContext(typeof(ApplicationBuilderCorrelationExtensions));

        public static IApplicationBuilder UseCorrelationIdsFromHeaders(this IApplicationBuilder applicationBuilder, IContextCorrelator contextCorrelator)
        {
            return applicationBuilder.Use(async (context, next) =>
            {
                using var correlations = BeginCorrelation(context, contextCorrelator);

                await next().ConfigureAwait(false);
            });
        }

        private static IDisposable BeginCorrelation(HttpContext context, IContextCorrelator contextCorrelator)
        {
            var correlations = new List<IDisposable>();

            foreach (var header in context.Request.Headers)
            {
                var key = header.Key;
                var value = header.Value[0];

                foreach (var correlationId in Correlation.AllIds)
                {
                    if (string.Equals(correlationId, key, StringComparison.OrdinalIgnoreCase))
                    {
                        _logger.Debug("Interpreting header {HeaderName}/{HeaderValue}", key, value);

                        var correlation = contextCorrelator.BeginLoggingCorrelationScope(correlationId, value);
                        correlations.Add(correlation);
                        break;
                    }
                }
            }

            return new CompositeDisposable(correlations);
        }
	}
}
