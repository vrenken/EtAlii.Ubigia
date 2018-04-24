namespace EtAlii.Ubigia.Api.Transport.Grpc
{
	using System;
	using System.Threading.Tasks;

    internal partial class GrpcContentDataClient : GrpcClientBase, IContentDataClient<IGrpcSpaceTransport>
    {
        //private HubConnection _contentConnection;
        //private HubConnection _contentDefinitionConnection;
        //private readonly IHubProxyMethodInvoker _invoker;

        //public GrpcContentDataClient(IHubProxyMethodInvoker invoker)
        //{
        //    _invoker = invoker;
        //}

        public override async Task Connect(ISpaceConnection<IGrpcSpaceTransport> spaceConnection)
        {
            await base.Connect(spaceConnection);

            // TODO: GRPC
            //var factory = new HubConnectionFactory();

            // TODO: GRPC
	        //_contentConnection = factory.Create(spaceConnection.Transport, new Uri(spaceConnection.Storage.Address + GrpcHub.BasePath + "/" + GrpcHub.Content, UriKind.Absolute));
            //_contentDefinitionConnection = factory.Create(spaceConnection.Transport, new Uri(spaceConnection.Storage.Address + GrpcHub.BasePath + "/" + GrpcHub.ContentDefinition, UriKind.Absolute));

	        //await _contentConnection.StartAsync();
	        //await _contentDefinitionConnection.StartAsync();
        }

        public override async Task Disconnect(ISpaceConnection<IGrpcSpaceTransport> spaceConnection)
        {
            await base.Disconnect(spaceConnection);

            // TODO: GRPC
            //await _contentDefinitionConnection.DisposeAsync();
            //_contentDefinitionConnection = null;
            //await _contentConnection.DisposeAsync();
            //_contentConnection = null;
        }
    }
}
