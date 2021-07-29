// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
    using System.Net.Http;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Hosting.Diagnostics;
    using Microsoft.Extensions.Configuration;

    public abstract class HostTestContextBase<THost> : IHostTestContext
		where THost: class, ITestHost
    {
	    private readonly Guid _uniqueId = Guid.Parse("827F11D6-4305-47C6-B42B-1271052FAC86");

        /// <inheritdoc />
        public IConfiguration HostConfiguration { get; private set; }

        /// <inheritdoc />
        public IConfiguration ClientConfiguration { get; private set; }

	    public THost Host { get; private set; }
        protected bool UseInProcessConnection { get; init; }

	    private readonly string _hostConfigurationFile;
        private readonly string _clientConfigurationFile;

        public ReadOnlyDictionary<string, string> Folders { get; private set; }
	    public ReadOnlyDictionary<string, string> Hosts { get; private set; }
	    public ReadOnlyDictionary<string, int> Ports { get; private set; }
	    public ReadOnlyDictionary<string, string> Paths { get; private set; }

	    protected HostTestContextBase(string hostConfigurationFile, string clientConfigurationFile)
	    {
		    _hostConfigurationFile = hostConfigurationFile;
            _clientConfigurationFile = clientConfigurationFile;

            Folders = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>());
		    Hosts = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>());
		    Ports = new ReadOnlyDictionary<string, int>(new Dictionary<string, int>());
		    Paths = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>());
	    }

	    public virtual Task Start(PortRange portRange) => StartExclusive(portRange);

	    private async Task StartExclusive(PortRange portRange)
	    {
		    // We want to start only one test hosting at the same time.
            if (UseInProcessConnection)
            {
                await StartExclusiveInternal(portRange).ConfigureAwait(false);
            }
            else
            {
                using var _ = new SystemSafeExecutionScope(_uniqueId);
                await StartExclusiveInternal(portRange).ConfigureAwait(false);
            }
	    }

        private async Task StartExclusiveInternal(PortRange portRange)
        {
            try
            {
                await Task
                    .Run(async () => await StartInternal(portRange).ConfigureAwait(false))
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Unable to start {nameof(HostTestContextBase<THost>)} on port range {portRange}", e);
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

            var hostConfigurationRoot = new ConfigurationBuilder()
			    .AddConfigurationDetails(details)
                .AddConfiguration(DiagnosticsConfiguration.Instance) // For testing we'll override the configured logging et.
			    .Build();
            HostConfiguration = hostConfigurationRoot;
            var hostConfiguration = new HostConfigurationBuilder()
                .Build(hostConfigurationRoot, details)
                .UseHostDiagnostics(hostConfigurationRoot);

            var clientConfigurationRoot = new ConfigurationBuilder()
                .AddJsonFile(_clientConfigurationFile)
                .AddConfiguration(DiagnosticsConfiguration.Instance) // For testing we'll override the configured logging et.
                .Build();
            ClientConfiguration = clientConfigurationRoot;

		    var host = (THost)new HostFactory<THost>().Create(hostConfiguration, false);

		    Host = host;
		    await host.Start().ConfigureAwait(false);
        }

	    public virtual async Task Stop()
	    {
		    await Host.Stop().ConfigureAwait(false);
		    Host = null;
	    }

        public HttpMessageHandler CreateHandler() => UseInProcessConnection
            ? Host.CreateHandler()
            : new HttpClientHandler();

        public HttpClient CreateClient() => UseInProcessConnection
            ? Host.CreateClient()
            : new HttpClient();
    }
}
