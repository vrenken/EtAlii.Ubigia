// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Tests
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;
    using EtAlii.xTechnology.Hosting;

    public interface ITransportTestContext : ITransportTestContext<InProcessInfrastructureHostTestContext>
    {
    }

    public interface ITransportTestContext<out THostTestContext>
        where THostTestContext : class, new()
    {
        THostTestContext Context { get; }

        Task<IDataConnection> CreateDataConnectionToNewSpace(bool openOnCreation = true);
        Task<IDataConnection> CreateDataConnectionToNewSpace(string accountName, string accountPassword, bool openOnCreation, SpaceTemplate spaceTemplate = null);
        Task<IDataConnection> CreateDataConnectionToExistingSpace(string accountName, string accountPassword, string spaceName, bool openOnCreation);

        Task<IManagementConnection> CreateManagementConnection(Uri address, string accountName, string password, bool openOnCreation);
        Task<IManagementConnection> CreateManagementConnection(bool openOnCreation = true);

        Task<Account> AddUserAccount(IManagementConnection connection);

        Task Start(PortRange portRange);
        Task Stop();
    }
}
