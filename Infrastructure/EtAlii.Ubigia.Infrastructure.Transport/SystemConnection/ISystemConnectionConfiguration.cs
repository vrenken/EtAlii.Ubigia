namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;

    public interface ISystemConnectionConfiguration : IConfiguration
    {
        IStorageTransportProvider TransportProvider { get; }
        Func<ISystemConnection> FactoryExtension { get; }
        IInfrastructure Infrastructure { get; }
    }
}