namespace EtAlii.Ubigia.Api.Transport.Management.Grpc
{
    using System.Security.Cryptography;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc;
    using EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol;
    using global::Grpc.Core;

    public partial class GrpcAuthenticationManagementDataClient : GrpcManagementClientBase, IAuthenticationManagementDataClient<IGrpcStorageTransport>
    {
        private AuthenticationGrpcService.AuthenticationGrpcServiceClient _client;
        private StorageGrpcService.StorageGrpcServiceClient _storageClient;
        private Api.Account _account;

        public GrpcAuthenticationManagementDataClient()
        {
            _hostIdentifier = CreateHostIdentifier();
            _random = RandomNumberGenerator.Create();
        }

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

        private void SetClients(Channel channel)
        {
            _client = new AuthenticationGrpcService.AuthenticationGrpcServiceClient(channel);
            _storageClient = new StorageGrpcService.StorageGrpcServiceClient(channel);
        }
        
    }
}
