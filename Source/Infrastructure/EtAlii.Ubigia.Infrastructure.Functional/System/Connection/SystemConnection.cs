// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Functional;

using System.Linq;
using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Transport;
using EtAlii.Ubigia.Api.Transport.Management;
using EtAlii.xTechnology.MicroContainer;

internal sealed class SystemConnection : ISystemConnection
{
    private readonly SystemConnectionOptions _options;

    public SystemConnection(SystemConnectionOptions options)
    {
        _options = options;
    }

    /// <inheritdoc />
    public async Task<(IDataConnection, DataConnectionOptions)> OpenSpace(string accountName, string spaceName)
    {
        var serviceDetails = _options.ServiceDetails.First(); // We'll take the first ServiceDetails to build the system connection with.

        var address = serviceDetails.DataAddress;

        var options = new DataConnectionOptions(_options.ConfigurationRoot)
            .UseTransport(_options.TransportProvider)
            .Use(address)
            .Use(accountName, spaceName, null);
        var dataConnection = Factory.Create<IDataConnection>(options);
        await dataConnection
            .Open()
            .ConfigureAwait(false);
        return (dataConnection, options);
    }

    /// <inheritdoc />
    public async Task<IManagementConnection> OpenManagementConnection()
    {
        var serviceDetails = _options.ServiceDetails.First(); // We'll take the first ServiceDetails to build the system connection with.

        var address = serviceDetails.ManagementAddress;

        var connectionOptions = new ManagementConnectionOptions(_options.ConfigurationRoot)
            .Use(_options.TransportProvider)
            .Use(address);
        var managementConnection = Factory.Create<IManagementConnection>(connectionOptions);
        await managementConnection
            .Open()
            .ConfigureAwait(false);
        return managementConnection;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        // Nothing to clean up right now.
    }
}
