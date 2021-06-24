// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class SystemBase : ISystem
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public State State { get => _state; protected set => PropertyChanged.SetAndRaise(this, ref _state, value); }
        private State _state;

        public Status Status { get; private set; }
        public ICommand[] Commands { get; private set; }
        public IService[] Services { get; private set; }
        public IModule[] Modules { get; private set; }

        protected virtual Task Starting() => Task.CompletedTask;
        protected virtual Task Started() => Task.CompletedTask;
        protected virtual Task Stopping() => Task.CompletedTask;
        protected virtual Task Stopped() => Task.CompletedTask;

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

        public void Setup(IHost host, IService[] services, IModule[] modules)
        {
            Modules = modules;
            Services = services;
            Status = CreateInitialStatus();
            Commands = CreateCommands();
        }

        public void Initialize()
        {
            foreach (var module in Modules)
            {
                module.Initialize();
            }
            foreach (var service in Services)
            {
                service.Initialize();
            }
        }

        protected virtual ICommand[] CreateCommands() => Array.Empty<ICommand>();
        protected virtual Status CreateInitialStatus() => new(GetType().Name);
    }
}
