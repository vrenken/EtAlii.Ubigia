﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Rest;

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using EtAlii.xTechnology.Threading;
using EtAlii.xTechnology.Diagnostics;
using Microsoft.AspNetCore.Http;

public static class ApplicationBuilderCorrelationExtensions
{
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

            var correlationId = Correlation.AllIds.FirstOrDefault(correlationId => string.Equals(correlationId, key, StringComparison.OrdinalIgnoreCase));
            if (correlationId != null)
            {
                var correlation = contextCorrelator.BeginLoggingCorrelationScope(correlationId, value);
                correlations.Add(correlation);
            }
        }

        return new CompositeDisposable(correlations);
    }
}
