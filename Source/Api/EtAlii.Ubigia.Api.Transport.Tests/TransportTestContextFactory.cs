// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Tests
{
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;

    public class TransportTestContextFactory
    {
        public TTransportTestContext Create<TTransportTestContext>()
            where TTransportTestContext : TransportTestContextBase<InProcessInfrastructureHostTestContext>, new()
        {
            return new();
        }
    }
}
