namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR.Client;

    internal class SignalREntryNotificationClient : SignalRClientBase, IEntryNotificationClient<ISignalRSpaceTransport>
    {
        private HubConnection _connection;
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

			_connection = new HubConnectionFactory().Create(spaceConnection.Transport, new Uri(spaceConnection.Storage.Address + SignalRHub.BasePath + _name, UriKind.Absolute));//spaceConnection.Transport.HttpClientHandler)
	        await _connection.StartAsync();

	        _subscriptions = new[]
	        {
				_connection.On<Identifier>("prepared", OnPrepared),
                _connection.On<Identifier>("stored", OnStored),
            };
        }

        public override async Task Disconnect() 
        {
            await base.Disconnect(); 

            await _connection.DisposeAsync();
            _connection = null;

	        foreach (var subscription in _subscriptions)
	        {
		        subscription.Dispose();
	        }
        }
	}
}
