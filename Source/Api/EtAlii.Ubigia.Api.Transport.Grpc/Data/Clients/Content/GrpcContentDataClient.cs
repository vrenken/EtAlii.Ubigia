// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Grpc;

using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol;

internal partial class GrpcContentDataClient : GrpcClientBase, IContentDataClient<IGrpcSpaceTransport>
{
    private ContentGrpcService.ContentGrpcServiceClient _contentClient;
    private ContentDefinitionGrpcService.ContentDefinitionGrpcServiceClient _contentDefinitionClient;
    private IGrpcSpaceTransport _transport;

    public override async Task Connect(ISpaceConnection<IGrpcSpaceTransport> spaceConnection)
    {
        await base.Connect(spaceConnection).ConfigureAwait(false);

        _transport = ((IGrpcSpaceConnection)spaceConnection).Transport;
        _contentClient = new ContentGrpcService.ContentGrpcServiceClient(_transport.CallInvoker);
        _contentDefinitionClient = new ContentDefinitionGrpcService.ContentDefinitionGrpcServiceClient(_transport.CallInvoker);
    }

    public override async Task Disconnect()
    {
        await base.Disconnect().ConfigureAwait(false);
        _transport = null;
        _contentClient = null;
        _contentDefinitionClient = null;
    }
}
