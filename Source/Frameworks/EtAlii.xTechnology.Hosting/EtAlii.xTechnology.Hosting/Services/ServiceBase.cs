// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System.ComponentModel;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public abstract class ServiceBase<THost, TSystem> : IService
        where THost : class, IHost
        where TSystem: class, ISystem
    {
        private readonly IConfigurationSection _configuration;
        protected THost Host { get; private set; }
        private IModule _parentModule;

        // ReSharper disable once NotAccessedField.Local
#pragma warning disable S4487
        protected TSystem System { get; private set; }
#pragma warning restore S4487

        public event PropertyChangedEventHandler PropertyChanged;

        public State State { get => _state; protected set => PropertyChanged.SetAndRaise(this, ref _state, value); }
        private State _state;


        public HostString HostString { get; private set; }

        public PathString PathString { get; private set; }

        public Status Status { get; }

        public virtual Task Start()
        {
            var host = (IConfigurableHost) Host;
            host.ConfigureHost += OnConfigureHost;
            host.ConfigureApplication += OnConfigureApplication;
            return Task.CompletedTask;
        }

        public virtual Task Stop()
        {
            var host = (IConfigurableHost) Host;
            host.ConfigureHost -= OnConfigureHost;
            host.ConfigureApplication -= OnConfigureApplication;
            return Task.CompletedTask;
        }

        private void OnConfigureHost(IWebHostBuilder builder)
        {
            if (!builder.TryUseUrl("http", HostString))
            {
                //throw new InvalidOperationException($"Service url already registered using builder.UseUrls(). Url: {ipAddress}:{Port} Service: {GetType().Name}")
            }
        }

        private void OnConfigureApplication(IApplicationBuilder application, IWebHostEnvironment environment)
        {
            application.IsolatedMapOnCondition(environment, this, ConfigureApplication, ConfigureServicesInternal);

        }

        protected virtual void ConfigureApplication(IApplicationBuilder application, IWebHostEnvironment environment)
        {
        }

        protected virtual void ConfigureServicesInternal(IServiceCollection services)
        {
            services.AddSingleton(Host.Options.ConfigurationRoot);
            services.AddSingleton((IConfiguration)Host.Options.ConfigurationRoot);

            ConfigureServices(services);
        }

        protected virtual void ConfigureServices(IServiceCollection services)
        {
        }

        protected ServiceBase(IConfigurationSection configuration)
        {
            _configuration = configuration;
            Status = new Status(GetType().Name);
        }

        public void Setup(IHost host, ISystem system, IModule parentModule)
        {
            Host = host as THost;
            System = system as TSystem;
            _parentModule = parentModule;
        }

        public virtual void Initialize()
        {
            // Host magic.
            HostString = new HostStringBuilder().Build(_configuration, _parentModule, IPAddress.Any);

            // Path magic.
            PathString = new PathStringBuilder().Build(_configuration, _parentModule);
        }
    }

    public abstract class ServiceBase<THost> : ServiceBase<THost, ISystem>
        where THost : class, IHost
    {
        protected ServiceBase(IConfigurationSection configuration)
            : base(configuration)
        {
        }
    }

    public abstract class ServiceBase : ServiceBase<IHost, ISystem>
    {
        protected ServiceBase(IConfigurationSection configuration)
            : base(configuration)
        {
        }
    }
}
