﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.ConsoleHost;

using System;
using System.Threading.Tasks;
using EtAlii.xTechnology.Hosting;
using EtAlii.xTechnology.Hosting.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var configurationRoot = new ConfigurationBuilder()
            .AddJsonFile("settings.json")
            .ExpandEnvironmentVariablesInJson()
            .Build();

        Console.WriteLine("Starting Ubigia infrastructure...");

        var host = Host
            .CreateDefaultBuilder()
            .UseHostLogging(configurationRoot, typeof(Program).Assembly)
            .UseHostServices<InfrastructureHostServicesFactory>(configurationRoot)
            .Build();

        await host
            .RunAsync()
            .ConfigureAwait(false);
    }
}
