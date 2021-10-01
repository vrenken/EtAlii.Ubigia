// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost
{
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost.SignalR;

    /// <summary>
    /// We need to make the name of this HostTestContext transport-agnostic in order for it to be used in all
    /// unit tests. Reason is that these are reused using shared projects.
    /// </summary>
    public class NetworkedInfrastructureHostTestContext : SignalRInfrastructureHostTestContext
    {
    }
}
