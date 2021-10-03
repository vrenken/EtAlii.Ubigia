// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Console
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Hosting.Diagnostics;
    using Microsoft.Extensions.Configuration;

    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main()
        {
            var configurationRoot = new ConfigurationBuilder()
                .AddJsonFile("settings.json")
                .AddConfiguration(DiagnosticsOptions.ConfigurationRoot) // For testing we'll override the configured logging et.
                .Build();

            var hostOptions = new HostOptions(configurationRoot)
                .UseConsoleHost()
                .UseHostDiagnostics();

            ConsoleHost.Start(hostOptions);
        }
    }
}
