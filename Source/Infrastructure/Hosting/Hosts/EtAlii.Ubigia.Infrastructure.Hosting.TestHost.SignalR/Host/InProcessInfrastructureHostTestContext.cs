// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost
{
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost.SignalR;

    public class InProcessInfrastructureHostTestContext : SignalRHostTestContext
    {
        public InProcessInfrastructureHostTestContext()
        {
            UseInProcessConnection = true;
        }
    }
}
