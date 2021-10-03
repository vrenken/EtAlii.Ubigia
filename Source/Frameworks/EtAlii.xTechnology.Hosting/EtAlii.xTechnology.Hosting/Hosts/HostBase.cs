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

    public abstract partial class HostBase : IHost
    {
        protected Microsoft.Extensions.Hosting.IHost Host => _host;
        private Microsoft.Extensions.Hosting.IHost _host;
        private readonly Status _selfStatus;

        event Action<IWebHostBuilder> IHost.ConfigureHost { add => _configureHost += value; remove => _configureHost -= value; }
        private Action<IWebHostBuilder> _configureHost;

        public State State { get => _state; protected set => PropertyChanged.SetAndRaise(this, ref _state, value); }
        private State _state;

        public Status[] Status { get; private set; }

        public ICommand[] Commands { get; private set; }

        protected IService[] Services { get; private set; }

        public IHostOptions Options { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected HostBase(IHostOptions options)
        {
            _selfStatus = new Status(GetType().Name) { Summary = "Unknown", Title = GetType().Name };

            Status = new[] { _selfStatus };

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
                var sb = new StringBuilder();
                sb.AppendLine($"Type: {service.GetType().Name}");
                sb.AppendLine("State: Configuring...");
                service.Status.Summary = sb.ToString();

                service.ConfigureServices(services);

                sb = new StringBuilder();
                sb.AppendLine($"Type: {service.GetType().Name}");
                sb.AppendLine("State: Running");
                service.Status.Summary = sb.ToString();
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
                    webHostBuilder.UseKestrel(ConfigureKestrel);
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
                var sb = new StringBuilder();
                sb.AppendLine($"Type: {service.GetType().Name}");
                sb.AppendLine("State: Configuring...");
                service.Status.Summary = sb.ToString();

                application.IsolatedMapOnCondition(context.HostingEnvironment, service);

                sb = new StringBuilder();
                sb.AppendLine($"Type: {service.GetType().Name}");
                var uriBuilder = new UriBuilder
                {
                    Host = service.Configuration.IpAddress,
                    Port = (int)service.Configuration.Port,
                    Path = service.Configuration.Path
                };
                sb.AppendLine($"State: Running");
                sb.AppendLine($"Location: {uriBuilder}");
                service.Status.Summary = sb.ToString();

            }
        }

        protected virtual async Task Starting()
        {
            Services = Options.ServiceFactory.Create(Options);
            _host = CreateHost();
            await _host
                .StartAsync()
                .ConfigureAwait(false);
            Status = new[] { _selfStatus }.Concat(Services.Select(s => s.Status)).ToArray();

            foreach (var s in Status)
            {
                s.PropertyChanged += OnStatusPropertyChanged;
            }
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

		public virtual void Setup(ICommand[] commands)
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

            Commands = commands;
        }

        private void UpdateStatus()
        {
            _selfStatus.Title = $"{GetType().Name}";
            _selfStatus.Summary = $"State: {State}";
        }
    }
}
