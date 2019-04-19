namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal class GrpcEntryNotificationClient : GrpcClientBase, IEntryNotificationClient<IGrpcSpaceTransport>
    {
        //private HubConnection _connection
//        private readonly string _name
        private readonly IEnumerable<IDisposable> _subscriptions = new IDisposable[0];

        public event Action<Identifier> Prepared = delegate { };
        public event Action<Identifier> Stored = delegate { };

//        public GrpcEntryNotificationClient()
//        [
//            //_name = GrpcHub.Entry
//        }

        private void OnPrepared(Identifier identifier)
        {
            Prepared(identifier);
        }

        private void OnStored(Identifier identifier)
        {
            Stored(identifier);
        }

        public override async Task Connect(ISpaceConnection<IGrpcSpaceTransport> spaceConnection)
        {
            await base.Connect(spaceConnection);

            // TODO: GRPC
            //_connection = new HubConnectionFactory().Create(spaceConnection.Transport, new Uri(spaceConnection.Storage.Address + GrpcHub.BasePath + "/" + _name, UriKind.Absolute));//spaceConnection.Transport.HttpClientHandler)
	        //await _connection.StartAsync()

	        //_subscriptions = new[]
	        //[
				//_connection.On<Identifier>("prepared", OnPrepared),
                //_connection.On<Identifier>("stored", OnStored),
            //}
        }

        public override async Task Disconnect(ISpaceConnection<IGrpcSpaceTransport> spaceConnection)
        {
            await base.Disconnect(spaceConnection);

            // TODO: GRPC
            //return await Task .FromResult<IEnumerable<IReadOnlyEntry>>(null)
            //await _connection.DisposeAsync()
            //_connection = null

	        foreach (var subscription in _subscriptions)
	        {
		        subscription.Dispose();
	        }
        }
	}
}
