// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.ConsoleHost
{
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Hosting;
    using EtAlii.xTechnology.Hosting.Diagnostics;
    using Microsoft.Extensions.Configuration;

    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var configurationRoot = new ConfigurationBuilder()
                .AddJsonFile("settings.json")
                .ExpandEnvironmentVariablesInJson()
                .Build();

            var hostOptions = new HostOptions(configurationRoot)
                .Use<InfrastructureHostServicesFactory>()
                .UseConsoleHost()
                .UseHostDiagnostics();

            await ConsoleHost
                .Start(hostOptions)
                .ConfigureAwait(false);
        }
    }
}
