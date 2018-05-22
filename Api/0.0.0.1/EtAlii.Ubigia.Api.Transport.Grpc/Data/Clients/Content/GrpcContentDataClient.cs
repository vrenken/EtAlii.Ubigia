namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;

    internal partial class GrpcContentDataClient : GrpcClientBase, IContentDataClient<IGrpcSpaceTransport>
    {
        private ContentGrpcService.ContentGrpcServiceClient _contentClient;
        private ContentDefinitionGrpcService.ContentDefinitionGrpcServiceClient _contentDefinitionClient;
        private IGrpcSpaceConnection _connection;

        public override Task Connect(ISpaceConnection<IGrpcSpaceTransport> spaceConnection)
        {
            var channel = spaceConnection.Transport.Channel;
            _connection = (IGrpcSpaceConnection) spaceConnection;
            _contentClient = new ContentGrpcService.ContentGrpcServiceClient(channel);
            _contentDefinitionClient = new ContentDefinitionGrpcService.ContentDefinitionGrpcServiceClient(channel);
            return Task.CompletedTask;
        }

        public override Task Disconnect(ISpaceConnection<IGrpcSpaceTransport> spaceConnection)
        {
            _connection = null;
            _contentClient = null;
            _contentDefinitionClient = null;
            return Task.CompletedTask;
        }
    }
}
