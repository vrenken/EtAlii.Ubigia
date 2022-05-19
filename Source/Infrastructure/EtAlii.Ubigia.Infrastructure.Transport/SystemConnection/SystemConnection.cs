﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.xTechnology.MicroContainer;

    internal class SystemConnection : ISystemConnection
    {
        private readonly ISystemConnectionOptions _options;

        public SystemConnection(ISystemConnectionOptions options)
        {
            _options = options;
        }

        /// <inheritdoc />
        public async Task<(IDataConnection, DataConnectionOptions)> OpenSpace(string accountName, string spaceName)
        {
            var serviceDetails = _options.Infrastructure.Options.ServiceDetails.First(); // We'll take the first ServiceDetails to build the system connection with.

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
            var serviceDetails = _options.Infrastructure.Options.ServiceDetails.First(); // We'll take the first ServiceDetails to build the system connection with.

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
        }
    }
}
