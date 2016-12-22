namespace EtAlii.Servus.Infrastructure.Transport
{
    using System;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Infrastructure.Functional;

    public interface ISystemConnectionConfiguration
    {
        ISystemConnectionExtension[] Extensions { get; }
        IStorageTransportProvider TransportProvider { get; }
        Func<ISystemConnection> FactoryExtension { get; }
        IInfrastructure Infrastructure { get; }

        ISystemConnectionConfiguration Use(ISystemConnectionExtension[] extensions);
        ISystemConnectionConfiguration Use(IStorageTransportProvider transportProvider); 
        ISystemConnectionConfiguration Use(Func<ISystemConnection> factoryExtension);
        ISystemConnectionConfiguration Use(IInfrastructure infrastructure);
    }
}