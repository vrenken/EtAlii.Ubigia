namespace EtAlii.Ubigia.Api.Transport
{
    using System;

    public interface IDataConnectionConfiguration : IConfiguration<DataConnectionConfiguration>
    {
        Uri Address { get; }
        string AccountName { get; }
        string Password { get; }
        string Space { get; }

        ITransportProvider TransportProvider { get; }
        Func<IDataConnection> FactoryExtension { get; }

        IDataConnectionConfiguration Use(Func<IDataConnection> factoryExtension);
        IDataConnectionConfiguration Use(ITransportProvider transportProvider);
        IDataConnectionConfiguration Use(Uri address);
        IDataConnectionConfiguration Use(string accountName, string space, string password);

    }
}