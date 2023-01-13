// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

#pragma warning disable // This is an external file.

namespace EtAlii.xTechnology.Hosting;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

internal class HostedServiceExecutor
{
    private readonly IEnumerable<IHostedService> _services;
    // ReSharper disable once NotAccessedField.Local
#pragma warning disable S4487
    private readonly ILogger<HostedServiceExecutor> _logger;
#pragma warning restore S4487

    public HostedServiceExecutor(ILogger<HostedServiceExecutor> logger, IEnumerable<IHostedService> services)
    {
        _logger = logger;
        _services = services;
    }

    public Task StartAsync(CancellationToken token)
    {
        return ExecuteAsync(service => service.StartAsync(token), true);
    }

    public Task StopAsync(CancellationToken token)
    {
        return ExecuteAsync(service => service.StopAsync(token), false);
    }

    // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
    private async Task ExecuteAsync(Func<IHostedService, Task> callback, bool throwOnFirstFailure)
    {
        List<Exception> exceptions = null;

        foreach (var service in _services)
        {
            try
            {
                await callback(service).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                if (throwOnFirstFailure)
                {
                    throw;
                }

                if (exceptions == null)
                {
                    exceptions = new List<Exception>();
                }

                exceptions.Add(ex);
            }
        }

        // Throw an aggregate exception if there were any exceptions
        if (exceptions != null)
        {
            throw new AggregateException(exceptions);
        }
    }
}
