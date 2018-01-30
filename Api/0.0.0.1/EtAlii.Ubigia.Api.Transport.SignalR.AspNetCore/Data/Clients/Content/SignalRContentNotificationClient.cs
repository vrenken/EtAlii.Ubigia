namespace EtAlii.Ubigia.Api.Transport.SignalR
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR.Client;

    internal class SignalRContentNotificationClient : SignalRClientBase, IContentNotificationClient<ISignalRSpaceTransport>
    {
        private HubConnection _connection;
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

			_connection = new HubConnectionFactory().Create(spaceConnection.Storage.Address + SignalRHub.BasePath + "/" + _name, spaceConnection.Transport);
	        await _connection.StartAsync();

	        _subscriptions = new[]
	        {
		        _connection.On<Identifier>("updated", OnUpdated),
		        _connection.On<Identifier>("stored", OnStored),
	        };
        }

        public override async Task Disconnect(ISpaceConnection<ISignalRSpaceTransport> spaceConnection)
        {
            await base.Disconnect(spaceConnection);

            await _connection.DisposeAsync();
            _connection = null;

			foreach (var subscription in _subscriptions)
			{
				subscription.Dispose();
			}
		}
	}
}
