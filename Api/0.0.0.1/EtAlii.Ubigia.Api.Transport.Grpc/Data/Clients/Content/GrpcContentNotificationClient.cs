namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal class GrpcContentNotificationClient : GrpcClientBase, IContentNotificationClient<IGrpcSpaceTransport>
    {
        private readonly IEnumerable<IDisposable> _subscriptions = new IDisposable[0];

        public event Action<Api.Identifier> Updated = delegate { };
        public event Action<Api.Identifier> Stored = delegate { };

        private void OnUpdated(Api.Identifier identifier)
        {
            Updated(identifier);
        }

        private void OnStored(Api.Identifier identifier)
        {
            Stored(identifier);
        }

        public override async Task Connect(ISpaceConnection<IGrpcSpaceTransport> spaceConnection)
        {
            await base.Connect(spaceConnection);

            // TODO: GRPC
			//_connection = new HubConnectionFactory().Create(spaceConnection.Transport, new Uri(spaceConnection.Storage.Address + GrpcHub.BasePath + "/" + _name, UriKind.Absolute));
	        //await _connection.StartAsync();

	        //_subscriptions = new[]
	        //{
		    //    _connection.On<Identifier>("updated", OnUpdated),
		    //    _connection.On<Identifier>("stored", OnStored),
	        //};
        }

        public override async Task Disconnect(ISpaceConnection<IGrpcSpaceTransport> spaceConnection)
        {
            await base.Disconnect(spaceConnection);

            //await _connection.DisposeAsync();
            //_connection = null;

			foreach (var subscription in _subscriptions)
			{
				subscription.Dispose();
			}
		}
	}
}
