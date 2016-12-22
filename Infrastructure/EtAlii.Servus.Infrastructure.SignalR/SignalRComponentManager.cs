﻿namespace EtAlii.Servus.Infrastructure.SignalR
{
    using System.Linq;
    using EtAlii.Servus.Infrastructure.WebApi;
    using Owin;


    public partial class SignalRComponentManager : ISignalRComponentManager
    {
        private readonly IComponent[] _components;

        public SignalRComponentManager(
            ISignalRUserApiComponent signalRUserApiComponent,
            ISignalRAdminApiComponent signalRAdminApiComponent)
        {
            _components = new IComponent[]
            {
                signalRUserApiComponent,
                signalRAdminApiComponent,
            };
        }

        public void Start(IAppBuilder application)
        {
            foreach (var component in _components)
            {
                component.Start(application);
            }
        }

        public void Stop()
        {
            foreach (var component in _components.Reverse())
            {
                component.Stop();
            }
        }
    }
}