﻿namespace EtAlii.xTechnology.Hosting.Tests.Console
{
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Diagnostics;
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

            var applicationConfiguration = new ConfigurationBuilder()
                .AddConfigurationDetails(details)
                .Build();

            var hostConfiguration = new HostConfigurationBuilder()
                .Build(applicationConfiguration, details)
                .Use(DiagnosticsConfiguration.Default)
                .UseConsoleHost();

            ConsoleHost.Start(hostConfiguration);
        }
    }
}
