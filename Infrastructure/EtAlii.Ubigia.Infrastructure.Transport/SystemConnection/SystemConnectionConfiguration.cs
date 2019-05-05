﻿namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;

    public class SystemConnectionConfiguration : Configuration<SystemConnectionConfiguration>, ISystemConnectionConfiguration, IEditableSystemConnectionConfiguration
    {
        IStorageTransportProvider IEditableSystemConnectionConfiguration.TransportProvider { get => TransportProvider; set => TransportProvider = value; }

        public IStorageTransportProvider TransportProvider { get; private set; }

        Func<ISystemConnection> IEditableSystemConnectionConfiguration.FactoryExtension { get => FactoryExtension; set => FactoryExtension = value; }
        public Func<ISystemConnection> FactoryExtension { get; private set; }


        IInfrastructure IEditableSystemConnectionConfiguration.Infrastructure { get => Infrastructure; set => Infrastructure = value; }
        public IInfrastructure Infrastructure { get; private set; }
    }
}
