namespace EtAlii.Ubigia.Api.Management
{
    using System;
    using EtAlii.Ubigia.Api.Transport;

    public interface IManagementConnectionConfiguration
    {
        IStorageTransportProvider TransportProvider { get; }
        string Address { get; }
        string AccountName { get; }
        string Password { get; }
        IManagementConnectionExtension[] Extensions { get; }
        Func<IManagementConnection> FactoryExtension { get; }

        IManagementConnectionConfiguration Use(IStorageTransportProvider transportProvider);
        IManagementConnectionConfiguration Use(IManagementConnectionExtension[] extensions);
        IManagementConnectionConfiguration Use(Func<IManagementConnection> factoryExtension);
        IManagementConnectionConfiguration Use(string address);
        IManagementConnectionConfiguration Use(string accountName, string password);
    }
}