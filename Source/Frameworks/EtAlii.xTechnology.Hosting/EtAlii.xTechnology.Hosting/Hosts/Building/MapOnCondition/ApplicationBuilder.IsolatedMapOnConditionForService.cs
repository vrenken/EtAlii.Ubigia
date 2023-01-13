// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting;

using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Predicate = System.Func<Microsoft.AspNetCore.Http.HttpContext, bool>;

/// <summary>
/// Provides extension methods for <see cref="IApplicationBuilder"/>.
/// Reference:  https://github.com/aspnet/AspNetCore/blob/master/src/Hosting/Hosting/src/Internal/StartupLoader.cs
/// </summary>
public static class ApplicationBuilderIsolatedMapWhenExtensions
{
    /// <summary>
    /// Branches the request pipeline in isolation yet based on the result of the given predicate.
    /// </summary>
    /// <param name="application"></param>
    /// <param name="environment"></param>
    /// <param name="service">The service the isolated mapping should be done fore</param>
    /// <returns></returns>
    [SuppressMessage(
        category: "Sonar Code Smell",
        checkId: "S1313:RSPEC-1313 - Using hardcoded IP addresses is security-sensitive",
        Justification = "Safe to do so here.")]
    public static IApplicationBuilder IsolatedMapOnCondition(
        this IApplicationBuilder application,
        IWebHostEnvironment environment,
        INetworkService service)
    {
        var ipAddress = service.Configuration.IpAddress;
        var port = service.Configuration.Port;
        var whenPredicate = new Func<HttpContext, bool>(context =>
        {
            var hostsAreEqual = ipAddress == context.Request.Host.Host;
            hostsAreEqual |= ipAddress == "127.0.0.1" && context.Request.Host.Host == "localhost";
            hostsAreEqual |= ipAddress == "localhost" && context.Request.Host.Host == "127.0.0.1";
            hostsAreEqual |= ipAddress == "0.0.0.0";
            hostsAreEqual |= ipAddress == "255.255.255.255";
            var portsAreEqual = port == context.Request.Host.Port;

            return hostsAreEqual && portsAreEqual;
        });

        return application.Isolate(builder => builder.MapOnCondition(environment, service.Configuration.Path, whenPredicate, service.ConfigureApplication), services => service.ConfigureServices(services, application.ApplicationServices));
    }
}
