// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Tests;

using System;
using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Transport;
using EtAlii.Ubigia.Api.Transport.Management;
using EtAlii.Ubigia.Infrastructure.Hosting.TestHost;
using EtAlii.xTechnology.Hosting;

public interface ITransportTestContext
{
    IInfrastructureHostTestContext Host { get; }
    Task<(IDataConnection, DataConnectionOptions)> CreateDataConnectionToNewSpace(bool openOnCreation = true);

    Task<(IDataConnection, DataConnectionOptions)> CreateDataConnectionToNewSpace(string accountName, string accountPassword, bool openOnCreation, SpaceTemplate spaceTemplate = null);

    Task<(IDataConnection, DataConnectionOptions)> CreateDataConnectionToExistingSpace(string accountName, string accountPassword, string spaceName, bool openOnCreation);

    Task<(IManagementConnection, ManagementConnectionOptions)> CreateManagementConnection(Uri address, string accountName, string password, bool openOnCreation);
    Task<(IManagementConnection, ManagementConnectionOptions)> CreateManagementConnection(bool openOnCreation = true);

    Task<Account> AddUserAccount(IManagementConnection connection);

    Task Start(PortRange portRange);
    Task Stop();
}
