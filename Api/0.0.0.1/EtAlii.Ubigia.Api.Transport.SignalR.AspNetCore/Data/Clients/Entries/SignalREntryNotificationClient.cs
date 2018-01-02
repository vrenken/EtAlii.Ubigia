namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNet.SignalR.Client;

    internal class SignalREntryNotificationClient : SignalRClientBase, IEntryNotificationClient<ISignalRSpaceTransport>
    {
        private IHubProxy _proxy;
        private readonly string _name;
        private IEnumerable<IDisposable> _subscriptions = new IDisposable[0];

        public event Action<Identifier> Prepared = delegate { };
        public event Action<Identifier> Stored = delegate { };

        public SignalREntryNotificationClient()
        {
            _name = SignalRHub.Entry;
        }

        private void OnPrepared(Identifier identifier)
        {
            Prepared(identifier);
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
                _proxy = Connection.Transport.HubConnection.CreateHubProxy(_name);
                _subscriptions = new[]
                {
                    _proxy.On<Identifier>("prepared", OnPrepared),
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
