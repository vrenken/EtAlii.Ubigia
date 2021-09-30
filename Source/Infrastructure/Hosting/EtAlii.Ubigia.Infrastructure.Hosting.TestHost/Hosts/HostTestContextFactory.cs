// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost
{
    public class HostTestContextFactory : IHostTestContextFactory
    {
        public THostTestContext Create<THostTestContext>()
            where THostTestContext : class, IInfrastructureHostTestContext, new()
        {
            return new();
        }
    }
}
