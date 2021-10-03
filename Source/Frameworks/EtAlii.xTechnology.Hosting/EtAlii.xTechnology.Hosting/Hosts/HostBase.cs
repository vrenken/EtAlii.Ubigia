// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;

    public abstract class HostBase : IHost
    {
        protected Microsoft.Extensions.Hosting.IHost Host => _host;
        private Microsoft.Extensions.Hosting.IHost _host;
        private readonly Status _selfStatus;

        event Action<IWebHostBuilder> IHost.ConfigureHost { add => _configureHost += value; remove => _configureHost -= value; }
        private Action<IWebHostBuilder> _configureHost;

        public State State { get => _state; protected set => PropertyChanged.SetAndRaise(this, ref _state, value); }
        private State _state;

        public Status[] Status { get; private set; } = Array.Empty<Status>();

        public ICommand[] Commands { get; private set; }

        protected IService[] Services { get; private set; }

        public IHostOptions Options { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected HostBase(IHostOptions options)
        {
            _selfStatus = new Status(GetType().Name);

            Options = options;
            PropertyChanged += OnPropertyChanged;
            UpdateStatus();
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e) => UpdateStatus();

        public async Task Start()
        {
            State = State.Starting;

            await Starting().ConfigureAwait(false);
            State = State.Running;
            await Started().ConfigureAwait(false);
        }

        protected void ConfigureBackgroundServices(HostBuilderContext context, IServiceCollection services)
        {
            services.AddSingleton<IConfigurationDetails>(Options.Details);
            foreach (var service in Services.OfType<IBackgroundService>())
            {
                service.ConfigureServices(services);
            }
        }

        public async Task Stop()
        {
            State = State.Stopping;
            await Stopping().ConfigureAwait(false);
            State = State.Stopped;
            await Stopped().ConfigureAwait(false);
        }

        public virtual async Task Shutdown()
        {
            await Stop().ConfigureAwait(false);
            State = State.Shutdown;
        }

        private void OnStatusPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Status)));
        }

        /// <summary>
        /// This is the core method that composes the current ASP.NET hosting environment.
        /// In case of the TestHost, this method is overridden to reconfigure usage of the test host.
        /// </summary>
        /// <returns></returns>
        protected virtual Microsoft.Extensions.Hosting.IHost CreateHost()
        {
            return Microsoft.Extensions.Hosting.Host
                .CreateDefaultBuilder()
                .ConfigureServices(ConfigureBackgroundServices)
                .ConfigureHostConfiguration(ConfigureHostConfiguration)
                .ConfigureWebHost(webHostBuilder =>
                {
                    webHostBuilder.UseKestrel(options =>
                    {
                        options.Limits.MaxRequestBodySize = 1024 * 1024 * 2;
                        options.Limits.MaxRequestBufferSize = 1024 * 1024 * 2;
                        options.Limits.MaxResponseBufferSize = 1024 * 1024 * 2;
                        options.AllowSynchronousIO = true;
                    });
                    webHostBuilder.Configure(ConfigureApplication);
                    _configureHost?.Invoke(webHostBuilder);
                })
                .Build();
        }

        protected void ConfigureHostConfiguration(IConfigurationBuilder builder)
        {
            builder.AddConfiguration(Options.ConfigurationRoot);
        }

        protected void ConfigureApplication(WebHostBuilderContext context, IApplicationBuilder application)
        {
            // Each network service gets instantiated in its own isolated environment.
            // The only subsystems that services can share.
            foreach (var service in Services.OfType<INetworkService>())
            {
                application.IsolatedMapOnCondition(context.HostingEnvironment, service);
            }
        }

        protected virtual async Task Starting()
        {
            Services = Options.ServiceFactory.Create(Options);
            _host = CreateHost();
            await _host
                .StartAsync()
                .ConfigureAwait(false);
        }

        protected abstract Task Started();

        protected abstract Task Stopping();

        protected virtual async Task Stopped()
	    {
            if (_host != null)
            {
                await _host
                    .StopAsync(TimeSpan.FromSeconds(30))
                    .ConfigureAwait(false);
                _host.Dispose();
                _host = null;
            }
	    }

		public virtual void Setup(ICommand[] commands, Status[] status)
        {
            Commands = commands;

            commands = commands
                .Concat(new ICommand[]
                {
                    new ToggleLogOutputCommand(this),
                    new IncreaseLogLevelCommand(this),
                    new DecreaseLogLevelCommand(this),
                })
                .ToArray();

            Status = status
                .Concat(new [] {_selfStatus} )
                .ToArray();
            Commands = commands;
            foreach (var s in Status)
            {
                s.PropertyChanged += OnStatusPropertyChanged;
            }
        }

        private void UpdateStatus()
        {
            _selfStatus.Title = ".NET Core Host";
            var sb = new StringBuilder();
            sb.AppendLine($"State: {State}");
            _selfStatus.Summary = _selfStatus.Description = sb.ToString();
        }
    }
}
