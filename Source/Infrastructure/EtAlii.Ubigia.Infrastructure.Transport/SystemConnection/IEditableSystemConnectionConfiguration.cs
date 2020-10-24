namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;

    public interface IEditableSystemConnectionConfiguration
    {
        IStorageTransportProvider TransportProvider { get; set; }

        Func<ISystemConnection> FactoryExtension { get; set; }

        IInfrastructure Infrastructure { get; set; }

    }
}