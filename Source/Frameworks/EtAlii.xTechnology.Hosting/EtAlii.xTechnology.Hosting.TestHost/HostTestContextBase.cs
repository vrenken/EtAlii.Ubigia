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

	    public THost Host { get; private set; }
        protected bool UseInProcessConnection { get; init; } = false;

	    private readonly string _configurationFile;

	    public ReadOnlyDictionary<string, string> Folders { get; private set; }
	    public ReadOnlyDictionary<string, string> Hosts { get; private set; }
	    public ReadOnlyDictionary<string, int> Ports { get; private set; }
	    public ReadOnlyDictionary<string, string> Paths { get; private set; }

	    protected HostTestContextBase(string configurationFile)
	    {
		    _configurationFile = configurationFile;

		    Folders = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>());
		    Hosts = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>());
		    Ports = new ReadOnlyDictionary<string, int>(new Dictionary<string, int>());
		    Paths = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>());
	    }

	    public virtual Task Start(PortRange portRange)
	    {
		    StartExclusive(portRange);
		    return Task.CompletedTask;
	    }

	    private void StartExclusive(PortRange portRange)
	    {
		    // We want to start only one test hosting at the same time.
            if (UseInProcessConnection)
            {
                StartExclusiveInternal(portRange);
            }
            else
            {
                using var _ = new SystemSafeExecutionScope(_uniqueId);
                StartExclusiveInternal(portRange);
            }
	    }

        private void StartExclusiveInternal(PortRange portRange)
        {
            try
            {
                var task = Task.Run(async () => await StartInternal(portRange, DiagnosticsConfiguration.Default).ConfigureAwait(false));
                task.GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                throw;
                //throw new InvalidOperationException($"Unable to start {nameof(HostTestContextBase<THost>)} on port range {portRange}", e);
            }
        }

	    /// <summary>
	    /// Override this method to implement a custom parsing from the configuration file into a ConfigurationDetails instance.
	    /// </summary>
	    /// <returns></returns>
	    private async Task<ConfigurationDetails> ParseForTesting(string configurationFile, PortRange portRange)
	    {
		    return await new ConfigurationDetailsParser().ParseForTesting(configurationFile, portRange).ConfigureAwait(false);
	    }

	    private async Task StartInternal(PortRange portRange, IDiagnosticsConfiguration diagnosticsConfiguration)
	    {
		    var details = await ParseForTesting(_configurationFile, portRange).ConfigureAwait(false);
		    Folders = details.Folders;
		    Hosts = details.Hosts;
		    Ports = details.Ports;
		    Paths = details.Paths;

		    var configurationRoot = new ConfigurationBuilder()
			    .AddConfigurationDetails(details)
			    .Build();

		    var hostConfiguration = new HostConfigurationBuilder()
			    .Build(configurationRoot, details)
			    .Use(diagnosticsConfiguration);

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
