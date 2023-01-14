// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost;

using EtAlii.Ubigia.Infrastructure.Functional;
using EtAlii.xTechnology.Hosting;

public interface IInfrastructureHostTestContext : IHostTestContext
{
    /// <summary>
    /// The details of the service current under test.
    /// </summary>
    ServiceDetails ServiceDetails { get; }
    string SystemAccountName { get; }
    string SystemAccountPassword { get; }
    string TestAccountName { get; }
    string TestAccountPassword { get; }
    string AdminAccountName { get; }
    string AdminAccountPassword { get; }

    string HostName { get; }
}
