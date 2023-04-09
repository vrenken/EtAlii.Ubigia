// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Logical.Tests;

using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EtAlii.Ubigia.Infrastructure.Fabric;
using EtAlii.Ubigia.Infrastructure.Functional;
using EtAlii.Ubigia.Infrastructure.Hosting;
using EtAlii.Ubigia.Infrastructure.Logical;
using EtAlii.Ubigia.Infrastructure.Transport;
using EtAlii.Ubigia.Persistence;
using EtAlii.Ubigia.Tests;
using EtAlii.xTechnology.Diagnostics;
using EtAlii.xTechnology.Hosting;
using EtAlii.xTechnology.Hosting.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Xunit;

public class LogicalUnitTestContext : IAsyncLifetime
{
    private readonly ILogger _logger = Log.ForContext<LogicalUnitTestContext>();

    public ILogicalContext Logical { get; private set; }
    public IFunctionalContext Functional { get; private set; }

    public IStorage Storage { get; private set; }

    public string HostName { get; private set; }
    public Uri DataAddress { get; private set; }

    private const string ConfigurationFile = "LogicalSettings.json";
    private IHost _host;

    public IConfigurationRoot Configuration { get; private set; }
    public TestContentDefinitionFactory TestContentDefinitionFactory { get; }
    public TestContentFactory TestContentFactory { get; }
    public TestPropertiesFactory TestPropertiesFactory { get; }
    public ContentComparer ContentComparer { get; }
    public ByteArrayComparer ByteArrayComparer { get; }
    public PropertyDictionaryComparer PropertyDictionaryComparer { get; }

    public LogicalUnitTestContext()
    {
        TestContentDefinitionFactory = new TestContentDefinitionFactory();
        TestContentFactory = new TestContentFactory();
        TestPropertiesFactory = new TestPropertiesFactory();
        ByteArrayComparer = new ByteArrayComparer();
        ContentComparer = new ContentComparer(ByteArrayComparer);
        PropertyDictionaryComparer = new PropertyDictionaryComparer();
    }

    public async Task InitializeAsync()
    {
        _logger.Information("Starting host {HostName}", GetType().Name);

        var details = await new ConfigurationDetailsParser()
            .ParseForTesting(ConfigurationFile, new PortRange())
            .ConfigureAwait(false);

        Configuration = new ConfigurationBuilder()
            .AddConfigurationDetails(details)
            .AddConfiguration(DiagnosticsOptions.ConfigurationRoot) // For testing we'll override the configured logging et.
            .Build();

        var hostBuilder = Host
            .CreateDefaultBuilder();

        // I know, ugly patch, but it works. And it's better than making all global unit test systems trying to phone home...
        if (Environment.MachineName == "FRACTAL")
        {
            hostBuilder = hostBuilder.UseHostLogging(Configuration, Assembly.GetEntryAssembly());
        }

        _host = hostBuilder
            .UseServicesFactoryOnTestHost<InfrastructureHostServicesFactory>(Configuration, out var services)
            .Build();

        await _host
            .StartAsync()
            .ConfigureAwait(false);

        _logger.Information("Started host {HostName}", GetType().Name);

        Functional = services.OfType<IInfrastructureService>().Single().Functional;
        Logical = Functional.LogicalContext;
        HostName = Functional.Options.Name;
        DataAddress = Functional.Options.ServiceDetails.First().DataAddress;

        Storage = services.OfType<IStorageService>().Single().Storage;
    }

    public async Task DisposeAsync()
    {
        _logger.Information("Stopping host {HostName}", GetType().Name);

        if (_host != null)
        {
            await _host
                .StopAsync()
                .ConfigureAwait(false);
            _host.Dispose();
            _host = null;
        }

        _logger.Information("Stopped host {HostName}", GetType().Name);

        Logical = null;
        Functional = null;
        HostName = null;
        DataAddress = null;
        Storage = null;
    }
}
