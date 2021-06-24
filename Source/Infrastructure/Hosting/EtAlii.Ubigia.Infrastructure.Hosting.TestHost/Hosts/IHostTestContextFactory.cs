// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost
{
    public interface IHostTestContextFactory
    {
        THostTestContext Create<THostTestContext>()
            where THostTestContext : class, IHostTestContext, new();
    } 
}