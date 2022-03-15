// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Console
{
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Hosting.Diagnostics;
    using EtAlii.xTechnology.Hosting.Tests.Local;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;

    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static async Task Main()
        {
            var configurationRoot = new ConfigurationBuilder()
                .AddJsonFile("settings.json")
                .AddConfiguration(DiagnosticsOptions.ConfigurationRoot) // For testing we'll override the configured logging et.
                .Build();

            System.Console.WriteLine("Starting Ubigia infrastructure...");

            var host = Host
                .CreateDefaultBuilder()
                .UseHostLogging(configurationRoot, typeof(Program).Assembly)
                .UseHostServices<System1HostServicesFactory>(configurationRoot)
                .Build();

            await host
                .StartAsync()
                .ConfigureAwait(false);
        }
    }
}
