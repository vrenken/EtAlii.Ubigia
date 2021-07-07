// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.ConsoleHost
{
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Hosting;
    using EtAlii.xTechnology.Hosting.Diagnostics;
    using Microsoft.Extensions.Configuration;

    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static async Task Main()
        {
            var details = await new ConfigurationDetailsParser().Parse("settings.json").ConfigureAwait(false);

            var configurationRoot = new ConfigurationBuilder()
                .AddConfigurationDetails(details)
                .Build();

            var hostConfiguration = new HostConfigurationBuilder()
                .Build(configurationRoot, details)
                .Use(DiagnosticsConfiguration.Default)
                .UseConsoleHost();

            ConsoleHost.Start(hostConfiguration);
        }
    }
}
