namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;

    public partial class GrpcAuthenticationDataClient : GrpcClientBase, IAuthenticationDataClient<IGrpcSpaceTransport>
    {
        //private HubConnection _accountConnection;
        //private HubConnection _spaceConnection;
        //private readonly IHubProxyMethodInvoker _invoker;
        private AuthenticationGrpcService.AuthenticationGrpcServiceClient _client;
        private StorageGrpcService.StorageGrpcServiceClient _storageClient;
        private SpaceGrpcService.SpaceGrpcServiceClient _spaceClient;
        private IGrpcSpaceConnection _connection;
        private Api.Account _account;

        public GrpcAuthenticationDataClient()
        {
            _hostIdentifier = CreateHostIdentifier();
        }

        public override Task Connect(ISpaceConnection<IGrpcSpaceTransport> spaceConnection)
        {
            var channel = spaceConnection.Transport.Channel;
            _connection = (IGrpcSpaceConnection) spaceConnection;
            _client = new AuthenticationGrpcService.AuthenticationGrpcServiceClient(channel);
            _storageClient = new StorageGrpcService.StorageGrpcServiceClient(channel);
            _spaceClient = new SpaceGrpcService.SpaceGrpcServiceClient(channel);
            return Task.CompletedTask;
        }

        public override Task Disconnect(ISpaceConnection<IGrpcSpaceTransport> spaceConnection)
        {
            _connection = null;
            _client = null;
            _storageClient = null;
            _spaceClient = null;
            return Task.CompletedTask;
        }
    }
}
