namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;

    internal partial class GrpcContentDataClient : GrpcClientBase, IContentDataClient<IGrpcSpaceTransport>
    {
        private ContentGrpcService.ContentGrpcServiceClient _contentClient;
        private ContentDefinitionGrpcService.ContentDefinitionGrpcServiceClient _contentDefinitionClient;
        private IGrpcSpaceTransport _transport;
                
        public override async Task Connect(ISpaceConnection<IGrpcSpaceTransport> spaceConnection)
        {
            await base.Connect(spaceConnection);
            
            _transport = ((IGrpcSpaceConnection)spaceConnection).Transport;
            _contentClient = new ContentGrpcService.ContentGrpcServiceClient(_transport.Channel);
            _contentDefinitionClient = new ContentDefinitionGrpcService.ContentDefinitionGrpcServiceClient(_transport.Channel);
        }

        public override async Task Disconnect(ISpaceConnection<IGrpcSpaceTransport> spaceConnection)
        {
            await base.Disconnect(spaceConnection);
            _transport = null;
            _contentClient = null;
            _contentDefinitionClient = null;
        }
    }
}
