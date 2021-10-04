﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.DockerHost
{
    using EtAlii.xTechnology.Hosting;
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
                .ExpandEnvironmentVariablesInJson()
                .Build();

            var hostOptions = new HostOptions(configurationRoot)
                .Use<InfrastructureHostServicesFactory>()
                .UseDockerHost()
                .UseHostDiagnostics();

            DockerHost.Start(hostOptions);
        }
    }
}