// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Tests;

using EtAlii.Ubigia.Api.Tests;
using EtAlii.Ubigia.Api.Transport.Grpc.Tests;
using EtAlii.Ubigia.Api.Transport.Tests;

internal class TransportTestContext
{
    public ITransportTestContext Create()
    {
        return new TransportTestContextFactory().Create<GrpcTransportTestContext>();
    }
}
