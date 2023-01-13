// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Rest.Tests;

using EtAlii.Ubigia.Api.Transport.Management.Rest;
using EtAlii.Ubigia.Api.Transport.Tests;
using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;
using EtAlii.xTechnology.Threading;

public class RestTransportTestContext : TransportTestContextBase<InfrastructureHostTestContext>
{
    protected override ITransportProvider CreateTransportProvider(IContextCorrelator contextCorrelator)
    {
        var client = Host.CreateRestInfrastructureClient();
        return RestTransportProvider.Create(client);
    }

    protected override IStorageTransportProvider CreateStorageTransportProvider(IContextCorrelator contextCorrelator)
    {
        var client = Host.CreateRestInfrastructureClient();
        return RestStorageTransportProvider.Create(client);
    }
}
