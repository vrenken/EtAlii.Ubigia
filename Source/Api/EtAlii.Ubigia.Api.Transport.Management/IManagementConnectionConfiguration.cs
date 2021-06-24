// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management
{
    using System;

    public interface IManagementConnectionConfiguration : IConfiguration
    {
        IStorageTransportProvider TransportProvider { get; }
	    Uri Address { get; }
        string AccountName { get; }
        string Password { get; }
        Func<IManagementConnection> FactoryExtension { get; }

        ManagementConnectionConfiguration Use(IStorageTransportProvider transportProvider);
        ManagementConnectionConfiguration Use(Func<IManagementConnection> factoryExtension);
        ManagementConnectionConfiguration Use(Uri address);
        ManagementConnectionConfiguration Use(string accountName, string password);
    }
}