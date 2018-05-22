namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;

    public partial class GrpcAuthenticationDataClient : GrpcClientBase, IAuthenticationDataClient<IGrpcSpaceTransport>
    {
        private AuthenticationGrpcService.AuthenticationGrpcServiceClient _client;
        private StorageGrpcService.StorageGrpcServiceClient _storageClient;
        private IGrpcSpaceConnection _connection;
        private SpaceGrpcService.SpaceGrpcServiceClient _spaceClient;
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
            _spaceClient = new SpaceGrpcService.SpaceGrpcServiceClient(channel);
            _storageClient = new StorageGrpcService.StorageGrpcServiceClient(channel);
            return Task.CompletedTask;
        }

        public override Task Disconnect(ISpaceConnection<IGrpcSpaceTransport> spaceConnection)
        {
            _connection = null;
            _client = null;
            _spaceClient = null;
            _storageClient = null;
            return Task.CompletedTask;
        }
    }
}
