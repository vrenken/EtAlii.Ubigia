// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Diagnostics
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Server.Kestrel.Core;
    using Microsoft.Extensions.Logging;
    using Serilog;
    using ILogger = Serilog.ILogger;

    public class LoggingHost : IConfigurableHost
    {
        private readonly IHost _decoree;
        private readonly ILogger _logger = Log.ForContext<IHost>();

        private readonly IConfigurableHost _configurableHost;

        public bool ShouldOutputLog { get => _decoree.ShouldOutputLog; set => _decoree.ShouldOutputLog = value; }
        public LogLevel LogLevel { get => _decoree.LogLevel; set => _decoree.LogLevel = value; }

        public LoggingHost(IHost decoree)
        {
            _decoree = decoree;
            _decoree.PropertyChanged += (_, e) => PropertyChanged?.Invoke(this, e);

            _configurableHost = (IConfigurableHost) decoree;
            _configurableHost.ConfigureApplication += builder => ConfigureApplication?.Invoke(builder);
            _configurableHost.ConfigureHost += builder => ConfigureHost?.Invoke(builder);
            _configurableHost.ConfigureKestrel += options => ConfigureKestrel?.Invoke(options);
            _configurableHost.ConfigureLogging += builder => ConfigureLogging?.Invoke(builder);
        }

        public State State => _decoree.State;
        public Status[] Status => _decoree.Status;
        public ICommand[] Commands => _decoree.Commands;
        public ISystem[] Systems => _decoree.Systems;

        public IHostManager Manager => _configurableHost.Manager;
        public event Action<IApplicationBuilder> ConfigureApplication;
        public event Action<IWebHostBuilder> ConfigureHost;
        public event Action<KestrelServerOptions> ConfigureKestrel;
        public event Action<ILoggingBuilder> ConfigureLogging;

        public event PropertyChangedEventHandler PropertyChanged;
        public IHostConfiguration Configuration => _decoree.Configuration;

        public async Task Start()
        {
            _logger.Information("Starting host {HostName}", GetType().Name);
            await _decoree.Start().ConfigureAwait(false);
        }

        public async Task Stop()
        {
            _logger.Information("Stopping host {HostName}", GetType().Name);
            await _decoree.Stop().ConfigureAwait(false);
        }

        public async Task Shutdown()
        {
            _logger.Information("Shutting down host {HostName}", GetType().Name);
            await _decoree.Shutdown().ConfigureAwait(false);
        }

        public void Setup(ICommand[] commands, Status[] status)
        {
            _logger.Information("Setting up host {HostName}", GetType().Name);
            _decoree.Setup(commands, status);
        }

        public void Initialize()
        {
            _logger.Information("Initializing host {HostName}", GetType().Name);
            _decoree.Initialize();
        }
    }
}
