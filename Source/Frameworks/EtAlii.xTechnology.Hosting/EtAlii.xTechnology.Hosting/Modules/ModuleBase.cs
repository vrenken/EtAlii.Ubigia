// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System.ComponentModel;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;

    public abstract class ModuleBase : IModule
    {
        private readonly IConfigurationSection _configuration;
        public event PropertyChangedEventHandler PropertyChanged;

        public State State { get => _state; protected set => PropertyChanged.SetAndRaise(this, ref _state, value); }
        private State _state;

        public Status Status { get; private set; }
        public IModule[] Modules { get; private set; }
        public IService[] Services { get; private set; }

        // ReSharper disable once NotAccessedField.Local
#pragma warning disable S4487
        private ISystem _system;
#pragma warning restore S4487
        private IModule ParentModule { get; set; }

        private IConfigurableHost _host;

        public HostString HostString { get; private set; }
        public PathString PathString { get; private set;}

        protected ModuleBase(IConfigurationSection configuration)
        {
            _configuration = configuration;
        }

        protected virtual Task Starting()
        {
            _host.ConfigureApplication += OnConfigureApplication;
            return Task.CompletedTask;
        }
        protected virtual Task Started() => Task.CompletedTask;
        protected virtual Task Stopping() => Task.CompletedTask;
        protected virtual Task Stopped()
        {
            _host.ConfigureApplication -= OnConfigureApplication;
            return Task.CompletedTask;
        }

        protected abstract void OnConfigureApplication(IApplicationBuilder applicationBuilder);

        public async Task Start()
        {
            await Starting().ConfigureAwait(false);

            foreach (var service in Services)
            {
                await service.Start().ConfigureAwait(false);
            }
            foreach (var module in Modules)
            {
                await module.Start().ConfigureAwait(false);
            }

            await Started().ConfigureAwait(false);
        }

        public async Task Stop()
        {
            await Stopping().ConfigureAwait(false);

            foreach (var module in Modules.Reverse())
            {
                await module.Stop().ConfigureAwait(false);
            }
            foreach (var service in Services.Reverse())
            {
                await service.Stop().ConfigureAwait(false);
            }

            await Stopped().ConfigureAwait(false);
        }

        public void Setup(IHost host, ISystem system, IService[] services, IModule[] modules, IModule parentModule)
        {
            _host = (IConfigurableHost)host;
            _system = system;
            Modules = modules;
            Services = services;
            ParentModule = parentModule;
            Status = CreateInitialStatus();
        }

        public virtual void Initialize()
        {
            // Host magic.
            HostString = new HostStringBuilder().Build(_configuration, ParentModule, IPAddress.None);

            // Path magic.
            PathString = new PathStringBuilder().Build(_configuration, ParentModule);

            foreach (var module in Modules)
            {
                module.Initialize();
            }
            foreach (var service in Services)
            {
                service.Initialize();
            }
        }

        protected virtual Status CreateInitialStatus() => new(GetType().Name);
    }
}
