namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR.Client;

    internal class SignalRPropertiesNotificationClient : SignalRClientBase, IPropertiesNotificationClient<ISignalRSpaceTransport>
    {
        private HubConnection _connection;
        private readonly string _name;
        //private IEnumerable<IDisposable> _subscriptions = new IDisposable[0];

        public event Action<Identifier> Stored = delegate { };

        public SignalRPropertiesNotificationClient()
        {
            _name = SignalRHub.Property;
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
                _connection = new HubConnectionFactory().Create(spaceConnection.Storage.Address + "/" + _name, spaceConnection.Transport.HttpClientHandler);
                _connection.On<Identifier>("stored", OnStored);
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
