// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
    using System.Net.Http;
    using System.Reflection;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Hosting.Diagnostics;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using Serilog;

    public abstract class HostTestContextBase<THost, THostServicesFactory> : IHostTestContext
		where THost: class, ITestHost
        where THostServicesFactory : IHostServicesFactory, new()
    {
        private readonly ILogger _logger = Log.ForContext<HostTestContextBase<THost, THostServicesFactory>>();

	    private readonly Guid _uniqueId = Guid.Parse("827F11D6-4305-47C6-B42B-1271052FAC86");

        /// <inheritdoc />
        public IConfigurationRoot HostConfiguration { get; private set; }

        /// <inheritdoc />
        public IConfigurationRoot ClientConfiguration { get; private set; }

	    public THost Host { get; private set; }
        private IHost _host;
        protected bool UseInProcessConnection { get; init; }

	    private readonly string _hostConfigurationFile;
        private readonly string _clientConfigurationFile;
        private TestServer _testServer;

        public ReadOnlyDictionary<string, string> Folders { get; private set; }
	    public ReadOnlyDictionary<string, string> Hosts { get; private set; }
	    public ReadOnlyDictionary<string, int> Ports { get; private set; }
	    public ReadOnlyDictionary<string, string> Paths { get; private set; }

        protected abstract THost CreateTestHost(IService[] services);

	    protected HostTestContextBase(string hostConfigurationFile, string clientConfigurationFile)
	    {
		    _hostConfigurationFile = hostConfigurationFile;
            _clientConfigurationFile = clientConfigurationFile;

            Folders = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>());
		    Hosts = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>());
		    Ports = new ReadOnlyDictionary<string, int>(new Dictionary<string, int>());
		    Paths = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>());
	    }

        public virtual async Task Start(PortRange portRange)
	    {
            // We want to start only one test hosting at the same time.
            if (UseInProcessConnection)
            {
                await StartInternal(portRange).ConfigureAwait(false);
            }
            else
            {
                using var _ = new SystemSafeExecutionScope(_uniqueId);
                await StartInternal(portRange).ConfigureAwait(false);
            }
	    }

	    private async Task StartInternal(PortRange portRange)
	    {
            // As we're testing with both a hosting environment and clients in the same process we need to use distinct configuration roots.

            var details = await new ConfigurationDetailsParser()
                .ParseForTesting(_hostConfigurationFile, portRange)
                .ConfigureAwait(false);
		    Folders = details.Folders;
		    Hosts = details.Hosts;
		    Ports = details.Ports;
		    Paths = details.Paths;

            HostConfiguration = new ConfigurationBuilder()
			    .AddConfigurationDetails(details)
                .AddConfiguration(DiagnosticsOptions.ConfigurationRoot) // For testing we'll override the configured logging et.
			    .Build();

            ClientConfiguration = new ConfigurationBuilder()
                .AddJsonFile(_clientConfigurationFile)
                .AddConfiguration(DiagnosticsOptions.ConfigurationRoot) // For testing we'll override the configured logging et.
                .Build();

            var hostBuilder = Microsoft.Extensions.Hosting.Host
                .CreateDefaultBuilder();

            // I know, ugly patch, but it works. And it's better than making all global unit test systems trying to phone home...
            if (Environment.MachineName == "FRACTAL")
            {
                hostBuilder = hostBuilder.UseHostLogging(HostConfiguration, Assembly.GetEntryAssembly());
            }

            _host = hostBuilder
                .UseHostTestServices<THostServicesFactory>(HostConfiguration, out var services)
                .Build();

            await _host
                .StartAsync()
                .ConfigureAwait(false);

            _testServer = _host.GetTestServer();
            _testServer.PreserveExecutionContext = false;
            _testServer.AllowSynchronousIO = false;

            _logger.Information("Test server acquired");

            Host = CreateTestHost(services);

            _logger.Information("Started host {HostName}", GetType().Name);
        }

	    public virtual async Task Stop()
	    {
            _logger.Information("Stopping host {HostName}", GetType().Name);

            if (_testServer != null)
            {
                _testServer.Dispose();
                _testServer = null;
            }

            _logger.Information("Test server removed");

            if (_host != null)
            {
                await _host
                    .StopAsync()
                    .ConfigureAwait(false);
                _host.Dispose();
                _host = null;
            }

            _logger.Information("Stopped host {HostName}", GetType().Name);

            Host = null;
        }

        public HttpMessageHandler CreateHandler() => UseInProcessConnection
            ? _testServer.CreateHandler()
            : new HttpClientHandler();

        public HttpClient CreateClient() => UseInProcessConnection
            ? _testServer.CreateClient()
            : new HttpClient();

        public WebSocketClient CreateWebSocketClient() => UseInProcessConnection
            ? _testServer.CreateWebSocketClient()
            : null;
    }
}
