namespace EtAlii.Ubigia.Api.Transport.Grpc
{
	using System;
	using System.Threading.Tasks;

    public partial class GrpcAuthenticationDataClient : GrpcClientBase, IAuthenticationDataClient<IGrpcSpaceTransport>
    {
        //private HubConnection _accountConnection;
        //private HubConnection _spaceConnection;
        //private readonly IHubProxyMethodInvoker _invoker;
        private AuthenticationGrpcService.AuthenticationGrpcServiceClient _client;

        public GrpcAuthenticationDataClient()
            //IHubProxyMethodInvoker invoker)
        {
            //_invoker = invoker;
            _hostIdentifier = CreateHostIdentifier();

        }

        public override async Task Connect(ISpaceConnection<IGrpcSpaceTransport> spaceConnection)
        {
            await base.Connect(spaceConnection);

            // TODO: GRPC
            //var factory = new HubConnectionFactory();

            // TODO: GRPC
            //_accountConnection = factory.Create(spaceConnection.Transport, new Uri(spaceConnection.Storage.Address + GrpcHub.BasePath + "/" + GrpcHub.Account, UriKind.Absolute));
            //_spaceConnection = factory.Create(spaceConnection.Transport, new Uri(spaceConnection.Storage.Address + GrpcHub.BasePath + "/" + GrpcHub.Space, UriKind.Absolute));
            //await _accountConnection.StartAsync();
            //await _spaceConnection.StartAsync();
        }

        public override async Task Disconnect(ISpaceConnection<IGrpcSpaceTransport> spaceConnection)
        {
            await base.Disconnect(spaceConnection);

            // TODO: GRPC
            //await _accountConnection.DisposeAsync();
            //_accountConnection = null;
            //await _spaceConnection.DisposeAsync();
            //_spaceConnection = null;
        }
    }
}
