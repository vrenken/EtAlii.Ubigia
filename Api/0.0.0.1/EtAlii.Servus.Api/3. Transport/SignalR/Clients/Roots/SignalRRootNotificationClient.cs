namespace EtAlii.Servus.Api.Transport.SignalR
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNet.SignalR.Client;

    internal class SignalRRootNotificationClient : SignalRClientBase, IRootNotificationClient<ISignalRSpaceTransport>
    {
        private IHubProxy _proxy;
        private readonly string _name;
        private IEnumerable<IDisposable> _subscriptions = new IDisposable[0];

        public event Action<Guid> Added = delegate { };
        public event Action<Guid> Changed = delegate { };
        public event Action<Guid> Removed = delegate { };

        public SignalRRootNotificationClient()
        {
            _name = SignalRHub.Root;
        }

        private void OnAdded(Guid id)
        {
            Added(id); 
        }

        private void OnChanged(Guid id)
        {
            Changed(id);
        }

        private void OnRemoved(Guid id)
        {
            Removed(id);
        }

        public override async Task Connect(ISpaceConnection<ISignalRSpaceTransport> spaceConnection)
        {
            await base.Connect(spaceConnection);

            await Task.Run(() =>
            {
                _proxy = spaceConnection.Transport.HubConnection.CreateHubProxy(_name);
                _subscriptions = new IDisposable[]
                {
                    _proxy.On<Guid>("added", OnAdded),
                    _proxy.On<Guid>("changed", OnChanged),
                    _proxy.On<Guid>("removed", OnRemoved),
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
