// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Diagnostics
{
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using Serilog;
    using ILogger = Serilog.ILogger;

    public class LoggingHost : IHost
    {
        private readonly IHost _decoree;
        private readonly ILogger _logger = Log.ForContext<IHost>();

        public State State => _decoree.State;
        public Status[] Status => _decoree.Status;
        public ICommand[] Commands => _decoree.Commands;
        public event PropertyChangedEventHandler PropertyChanged;
        public IHostOptions Options => _decoree.Options;
        event Action<IWebHostBuilder> IHost.ConfigureHost { add => _configureHost += value; remove => _configureHost -= value; }
        private Action<IWebHostBuilder> _configureHost;

        public LoggingHost(IHost decoree)
        {
            _decoree = decoree;
            _decoree.PropertyChanged += (_, e) => PropertyChanged?.Invoke(this, e);
            _decoree.ConfigureHost += options => (_configureHost)?.Invoke(options);
        }

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
    }
}
