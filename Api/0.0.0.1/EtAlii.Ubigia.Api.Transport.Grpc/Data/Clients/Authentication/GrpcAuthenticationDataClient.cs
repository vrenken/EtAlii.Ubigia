namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
    using global::Grpc.Net.Client;

    public partial class GrpcAuthenticationDataClient : GrpcClientBase, IAuthenticationDataClient<IGrpcSpaceTransport>
    {
        //private HubConnection _accountConnection
        //private HubConnection _spaceConnection
        //private readonly IHubProxyMethodInvoker _invoker
        private AuthenticationGrpcService.AuthenticationGrpcServiceClient _client;
        private StorageGrpcService.StorageGrpcServiceClient _storageClient;
        private SpaceGrpcService.SpaceGrpcServiceClient _spaceClient;
        private Api.Account _account;

        public GrpcAuthenticationDataClient()
        {
            _hostIdentifier = CreateHostIdentifier();
        }

        public override async Task Connect(ISpaceConnection<IGrpcSpaceTransport> spaceConnection)
        {
            await base.Connect(spaceConnection);
            SetClients(spaceConnection.Transport.Channel);
        }

        public override async Task Disconnect() 
        {
            await base.Disconnect(); 
            _storageClient = null;
            _spaceClient = null;
        }
        
        private void SetClients(GrpcChannel channel)
        {
            _client = new AuthenticationGrpcService.AuthenticationGrpcServiceClient(channel);
            _storageClient = new StorageGrpcService.StorageGrpcServiceClient(channel);
            _spaceClient = new SpaceGrpcService.SpaceGrpcServiceClient(channel);
        }
        
    }
}
