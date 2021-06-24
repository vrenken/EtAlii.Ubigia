// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Grpc
{
    using global::Grpc.Core;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;
    using Account = EtAlii.Ubigia.Account;

    public partial class GrpcAuthenticationDataClient : GrpcClientBase, IAuthenticationDataClient<IGrpcSpaceTransport>
    {
        private AuthenticationGrpcService.AuthenticationGrpcServiceClient _client;
        private StorageGrpcService.StorageGrpcServiceClient _storageClient;
        private SpaceGrpcService.SpaceGrpcServiceClient _spaceClient;
        private Account _account;

        public GrpcAuthenticationDataClient()
        {
            _hostIdentifier = CreateHostIdentifier();
        }

        public override async Task Connect(ISpaceConnection<IGrpcSpaceTransport> spaceConnection)
        {
            await base.Connect(spaceConnection).ConfigureAwait(false);
            SetClients(spaceConnection.Transport.CallInvoker);
        }

        public override async Task Disconnect()
        {
            await base.Disconnect().ConfigureAwait(false);
            _storageClient = null;
            _spaceClient = null;
        }

        private void SetClients(CallInvoker callInvoker)
        {
            _client = new AuthenticationGrpcService.AuthenticationGrpcServiceClient(callInvoker);
            _storageClient = new StorageGrpcService.StorageGrpcServiceClient(callInvoker);
            _spaceClient = new SpaceGrpcService.SpaceGrpcServiceClient(callInvoker);
        }
    }
}
