// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Grpc;

using System.Threading.Tasks;
using global::Grpc.Core;
using EtAlii.Ubigia.Api.Transport.Grpc;
using EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol;

public partial class GrpcInformationDataClient : GrpcManagementClientBase, IInformationDataClient<IGrpcStorageTransport>
{
    private InformationGrpcService.InformationGrpcServiceClient _client;
    private StorageGrpcService.StorageGrpcServiceClient _storageClient;

    public override Task Connect(IStorageConnection<IGrpcStorageTransport> storageConnection)
    {
        SetClients(storageConnection.Transport.CallInvoker);
        return Task.CompletedTask;
    }

    public override Task Disconnect(IStorageConnection<IGrpcStorageTransport> storageConnection)
    {
        _client = null;
        _storageClient = null;
        return Task.CompletedTask;
    }

    private void SetClients(CallInvoker callInvoker)
    {
        _client = new InformationGrpcService.InformationGrpcServiceClient(callInvoker);
        _storageClient = new StorageGrpcService.StorageGrpcServiceClient(callInvoker);
    }
}
