namespace EtAlii.Servus.Infrastructure
{
    using System;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Infrastructure;

    public interface ISystemStorageConnectionConfiguration
    {
        ISystemTransport Transport { get; }
        IInfrastructure Infrastructure { get; }

        ISystemStorageConnectionConfiguration Use(IInfrastructure infrastructure);

        ISystemStorageConnectionConfiguration Use(ISystemTransport transport);
    }
}