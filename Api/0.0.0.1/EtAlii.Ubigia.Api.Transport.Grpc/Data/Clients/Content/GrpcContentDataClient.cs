namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;

    internal partial class GrpcContentDataClient : GrpcClientBase, IContentDataClient<IGrpcSpaceTransport>
    {
        private ContentGrpcService.ContentGrpcServiceClient _contentClient;
        private ContentDefinitionGrpcService.ContentDefinitionGrpcServiceClient _contentDefinitionClient;
        private IGrpcSpaceTransport _transport;

        public override Task Connect(ISpaceConnection<IGrpcSpaceTransport> spaceConnection)
        {
            _transport = ((IGrpcSpaceConnection) spaceConnection).Transport;
            _contentClient = new ContentGrpcService.ContentGrpcServiceClient(_transport.Channel);
            _contentDefinitionClient = new ContentDefinitionGrpcService.ContentDefinitionGrpcServiceClient(_transport.Channel);
            return Task.CompletedTask;
        }

        public override Task Disconnect(ISpaceConnection<IGrpcSpaceTransport> spaceConnection)
        {
            _transport = null;
            _contentClient = null;
            _contentDefinitionClient = null;
            return Task.CompletedTask;
        }
    }
}
