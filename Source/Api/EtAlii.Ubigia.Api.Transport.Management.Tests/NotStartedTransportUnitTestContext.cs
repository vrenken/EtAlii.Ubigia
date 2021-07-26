// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Tests
{
    using System;
    using EtAlii.Ubigia.Api.Tests;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;

    public class NotStartedTransportUnitTestContext : IDisposable
    {
        public ITransportTestContext<InProcessInfrastructureHostTestContext> TransportTestContext { get; private set; }
        public NotStartedTransportUnitTestContext()
        {
            TransportTestContext = new TransportTestContext().Create();
        }

        public void Dispose()
        {
            TransportTestContext = null;
            GC.SuppressFinalize(this);
        }
    }
}
