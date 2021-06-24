// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

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
		private IEnumerable<IDisposable> _subscriptions = Array.Empty<IDisposable>();

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
            await base.Connect(spaceConnection).ConfigureAwait(false);

			_connection = new HubConnectionFactory().Create(spaceConnection.Transport, new Uri(spaceConnection.Transport.Address + UriHelper.Delimiter + _name, UriKind.Absolute));
	        await _connection.StartAsync().ConfigureAwait(false);

			_subscriptions = new[]
			{
				_connection.On<Guid>("added", OnAdded),
				_connection.On<Guid>("changed", OnChanged),
				_connection.On<Guid>("removed", OnRemoved),
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
