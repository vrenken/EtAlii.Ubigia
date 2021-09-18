// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
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
        /// <param name="appBuilder">Configures a branch to take</param>
        /// <param name="services">A method to configure the newly created service collection.</param>
        /// <returns></returns>
        [SuppressMessage(
            category: "Sonar Code Smell",
            checkId: "S1313:RSPEC-1313 - Using hardcoded IP addresses is security-sensitive",
            Justification = "Safe to do so here.")]
        public static IApplicationBuilder IsolatedMapOnCondition(
            this IApplicationBuilder application,
            IWebHostEnvironment environment,
            IService service,
            Action<IApplicationBuilder, IWebHostEnvironment> appBuilder,
            Action<IServiceCollection> services)
        {
            var whenPredicate = new Func<HttpContext, bool>(context =>
            {
                var hostsAreEqual = service.HostString.Host == context.Request.Host.Host;
                hostsAreEqual |= service.HostString.Host == "127.0.0.1" && context.Request.Host.Host == "localhost";
                hostsAreEqual |= service.HostString.Host == "localhost" && context.Request.Host.Host == "127.0.0.1";
                hostsAreEqual |= service.HostString.Host == "0.0.0.0";
                hostsAreEqual |= service.HostString.Host == "255.255.255.255";
                var portsAreEqual = service.HostString.Port == context.Request.Host.Port;

                return hostsAreEqual && portsAreEqual;
            });

            return application.Isolate(builder => builder.MapOnCondition(environment, service.PathString, whenPredicate, appBuilder), services);
        }
    }
}
