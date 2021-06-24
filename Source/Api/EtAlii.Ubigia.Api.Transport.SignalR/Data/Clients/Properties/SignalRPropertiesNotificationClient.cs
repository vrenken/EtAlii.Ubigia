// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

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
		private IEnumerable<IDisposable> _subscriptions = Array.Empty<IDisposable>();

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
            await base.Connect(spaceConnection).ConfigureAwait(false);

            _connection = new HubConnectionFactory().Create(spaceConnection.Transport, new Uri(spaceConnection.Transport.Address + UriHelper.Delimiter + _name, UriKind.Absolute));
			_subscriptions = new[]
			{
				_connection.On<Identifier>("stored", OnStored),
			};
		}

        public override async Task Disconnect() 
        {
            await base.Disconnect().ConfigureAwait(false); 

            await _connection.DisposeAsync().ConfigureAwait(false);
            _connection = null;

	        foreach (var subscription in _subscriptions)
	        {
		        subscription.Dispose();
	        }
        }
	}
}
