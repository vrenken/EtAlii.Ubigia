﻿namespace EtAlii.Servus.Api.Transport.SignalR
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNet.SignalR.Client;

    internal class SignalRContentNotificationClient : SignalRClientBase, IContentNotificationClient<ISignalRSpaceTransport>
    {
        private IHubProxy _proxy;
        private readonly string _name;
        private IEnumerable<IDisposable> _subscriptions = new IDisposable[0];

        public event Action<Identifier> Updated = delegate { };
        public event Action<Identifier> Stored = delegate { };

        public SignalRContentNotificationClient()
        {
            _name = SignalRHub.Content;
        }

        private void OnUpdated(Identifier identifier)
        {
            Updated(identifier);
        }

        private void OnStored(Identifier identifier)
        {
            Stored(identifier);
        }

        public override async Task Connect(ISpaceConnection<ISignalRSpaceTransport> spaceConnection)
        {
            await base.Connect(spaceConnection);

            await Task.Run(() =>
            {
                _proxy = spaceConnection.Transport.HubConnection.CreateHubProxy(_name);
                _subscriptions = new[]
                {
                    _proxy.On<Identifier>("updated", OnUpdated),
                    _proxy.On<Identifier>("stored", OnStored),
                };
            });
        }

        public override async Task Disconnect(ISpaceConnection<ISignalRSpaceTransport> spaceConnection)
        {
            await base.Disconnect(spaceConnection);

            await Task.Run(() =>
            {
                foreach (var subscription in _subscriptions)
                {
                    subscription.Dispose();
                }
                _proxy = null;
            });
        }
    }
}
