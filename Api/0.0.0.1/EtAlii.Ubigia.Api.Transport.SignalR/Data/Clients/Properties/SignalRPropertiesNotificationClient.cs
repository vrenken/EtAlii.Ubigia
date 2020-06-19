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
		private IEnumerable<IDisposable> _subscriptions = new IDisposable[0];

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

            _connection = new HubConnectionFactory().Create(spaceConnection.Transport, new Uri(spaceConnection.Storage.Address + "/" + _name, UriKind.Absolute));
			_subscriptions = new[]
			{
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
