namespace EtAlii.Ubigia.Api.Transport.Management
{
    using System;

    public interface IManagementConnectionConfiguration : IConfiguration<ManagementConnectionConfiguration>
    {
        IStorageTransportProvider TransportProvider { get; }
	    Uri Address { get; }
        string AccountName { get; }
        string Password { get; }
        Func<IManagementConnection> FactoryExtension { get; }

        IManagementConnectionConfiguration Use(IStorageTransportProvider transportProvider);
        IManagementConnectionConfiguration Use(Func<IManagementConnection> factoryExtension);
        IManagementConnectionConfiguration Use(Uri address);
        IManagementConnectionConfiguration Use(string accountName, string password);
    }
}