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
        private IGrpcConnection _connection;

        public GrpcAuthenticationDataClient()
            //IHubProxyMethodInvoker invoker)
        {
            //_invoker = invoker;
            _hostIdentifier = CreateHostIdentifier();

        }

        public override async Task Connect(ISpaceConnection<IGrpcSpaceTransport> spaceConnection)
        {
            var channel = spaceConnection.Transport.Channel;
            _connection = (IGrpcConnection) spaceConnection;
            _client = new AuthenticationGrpcService.AuthenticationGrpcServiceClient(channel);
        }

        public override async Task Disconnect(ISpaceConnection<IGrpcSpaceTransport> spaceConnection)
        {
            _connection = null;
            _client = null;
        }
    }
}
