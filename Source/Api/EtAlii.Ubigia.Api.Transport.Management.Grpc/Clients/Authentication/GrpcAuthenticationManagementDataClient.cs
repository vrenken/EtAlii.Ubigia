// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Grpc
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport.Grpc;
    using EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol;
    using global::Grpc.Core;

    public partial class GrpcAuthenticationManagementDataClient : GrpcManagementClientBase, IAuthenticationManagementDataClient<IGrpcStorageTransport>
    {
        private AuthenticationGrpcService.AuthenticationGrpcServiceClient _client;

        public GrpcAuthenticationManagementDataClient()
        {
            _hostIdentifier = CreateHostIdentifier();
        }

        public override Task Connect(IStorageConnection<IGrpcStorageTransport> storageConnection)
        {
            SetClients(storageConnection.Transport.CallInvoker);
            return Task.CompletedTask;
        }

        public override Task Disconnect(IStorageConnection<IGrpcStorageTransport> storageConnection)
        {
            _client = null;
            return Task.CompletedTask;
        }

        private void SetClients(CallInvoker callInvoker)
        {
            _client = new AuthenticationGrpcService.AuthenticationGrpcServiceClient(callInvoker);
        }
    }
}
