namespace EtAlii.Ubigia.Api.Transport.Management.Grpc
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc;
    using EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol;
    using global::Grpc.Net.Client;

    public partial class GrpcInformationDataClient : GrpcManagementClientBase, IInformationDataClient<IGrpcStorageTransport>
    {
        private InformationGrpcService.InformationGrpcServiceClient _client;
        private StorageGrpcService.StorageGrpcServiceClient _storageClient;

        public override Task Connect(IStorageConnection<IGrpcStorageTransport> storageConnection)
        {
            SetClients(storageConnection.Transport.Channel);
            return Task.CompletedTask;
        }

        public override Task Disconnect(IStorageConnection<IGrpcStorageTransport> storageConnection)
        {
            _client = null;
            _storageClient = null;
            return Task.CompletedTask;
        }

        private void SetClients(GrpcChannel channel)
        {
            _client = new InformationGrpcService.InformationGrpcServiceClient(channel);
            _storageClient = new StorageGrpcService.StorageGrpcServiceClient(channel);
        }
    }
}
