// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost
{
    using EtAlii.Ubigia.Infrastructure.Functional;

    public interface IHostTestContext<out TInfrastructureTestHost> : IHostTestContext
        where TInfrastructureTestHost : class
    {
        TInfrastructureTestHost Host { get; }
    }

    public interface IHostTestContext : EtAlii.xTechnology.Hosting.IHostTestContext
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

        System.Threading.Tasks.Task<ISystemConnection> CreateSystemConnection();

        System.Threading.Tasks.Task AddUserAccountAndSpaces(ISystemConnection connection, string accountName, string password, string[] spaceNames);
    }
}