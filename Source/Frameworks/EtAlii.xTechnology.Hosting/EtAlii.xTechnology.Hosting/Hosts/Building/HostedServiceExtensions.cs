// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

#pragma warning disable // This is an external file.

namespace EtAlii.xTechnology.Hosting;

using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal static class HostedServiceExtensions
{
    public static Task RunHostedServices(this IApplicationBuilder app)
    {
        var hostedServices = app.ApplicationServices.GetRequiredService<HostedServiceExecutor>();
        var lifeTime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();
        lifeTime.ApplicationStopping.Register(() => { hostedServices.StopAsync(CancellationToken.None).GetAwaiter().GetResult(); });
        return hostedServices.StartAsync(lifeTime.ApplicationStopping);
    }
}
