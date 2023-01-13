// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Tests;

using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Tests;
using Xunit;

public class StartedTransportUnitTestContext : IAsyncLifetime
{
    public ITransportTestContext Transport { get; private set; }

    public async Task InitializeAsync()
    {
        Transport = new TransportTestContext().Create();
        await Transport.Start(UnitTestSettings.NetworkPortRange).ConfigureAwait(false);
    }

    public async Task DisposeAsync()
    {
        await Transport.Stop().ConfigureAwait(false);
        Transport = null;
    }
}
