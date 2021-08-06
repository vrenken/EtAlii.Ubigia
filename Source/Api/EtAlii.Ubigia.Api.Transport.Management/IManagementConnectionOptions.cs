// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management
{
    using System;
    using Microsoft.Extensions.Configuration;

    public interface IManagementConnectionOptions : IExtensible
    {
        /// <summary>
        /// The client configuration root that will be used to configure the management connection.
        /// </summary>
        IConfigurationRoot ConfigurationRoot { get; }

        IStorageTransportProvider TransportProvider { get; }
	    Uri Address { get; }
        string AccountName { get; }
        string Password { get; }
        Func<IManagementConnection> FactoryExtension { get; }

        ManagementConnectionOptions Use(IStorageTransportProvider transportProvider);
        ManagementConnectionOptions Use(Func<IManagementConnection> factoryExtension);
        ManagementConnectionOptions Use(Uri address);
        ManagementConnectionOptions Use(string accountName, string password);
    }
}
