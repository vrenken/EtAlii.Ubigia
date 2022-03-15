// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Diagnostics
{
    using System.Reflection;
    using EtAlii.xTechnology.Diagnostics;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using Serilog;

    public static class HostBuilderAddHostLoggingExtensions
    {
        public static IHostBuilder UseHostLogging(
            this IHostBuilder hostBuilder,
            IConfigurationRoot configurationRoot,
            Assembly entryAssembly)
        {
            var options = configurationRoot
                .GetSection("Host:Diagnostics")
                .Get<DiagnosticsOptions>();

            // We want to start logging as soon as possible. This means not waiting until the ASP.NET Core hosting subsystem has started.
            DiagnosticsOptions.Initialize(entryAssembly, configurationRoot);

            if (options.InjectLogging) // logging is enabled
            {
                hostBuilder.UseSerilog((_, loggerConfiguration) => { DiagnosticsOptions.ConfigureLoggerConfiguration(loggerConfiguration, configurationRoot); }, true);
            }

            return hostBuilder;
        }
    }
}
