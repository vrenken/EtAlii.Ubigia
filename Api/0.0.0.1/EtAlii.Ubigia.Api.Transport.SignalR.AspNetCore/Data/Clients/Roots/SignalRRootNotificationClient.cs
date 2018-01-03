namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR.Client;

    internal class SignalRRootNotificationClient : SignalRClientBase, IRootNotificationClient<ISignalRSpaceTransport>
    {
        private HubConnection _connection;
        private readonly string _name;
        //private IEnumerable<IDisposable> _subscriptions = new IDisposable[0];

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
                _connection = new HubConnectionFactory().Create(spaceConnection.Storage.Address + "/" + _name, spaceConnection.Transport.HttpClientHandler);
                _connection.On<Guid>("added", OnAdded);
                _connection.On<Guid>("changed", OnChanged);
                _connection.On<Guid>("removed", OnRemoved);

                //_proxy = spaceConnection.Transport.HubConnection.CreateHubProxy(_name);
                //_subscriptions = new[]
                //{
                //};
            });
        }

        public override async Task Disconnect(ISpaceConnection<ISignalRSpaceTransport> spaceConnection)
        {
            await base.Disconnect(spaceConnection);

            await _connection.DisposeAsync();
            _connection = null;

            //await Task.Run(() =>
            //{
            //    foreach (var subscription in _subscriptions)
            //    {
            //        subscription.Dispose();
            //    }
            //    _proxy = null;
            //});
        }
    }
}
