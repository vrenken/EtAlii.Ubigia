// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;

    public static class SystemConnectionConfigurationUseExtensions
    {
        
        public static TSystemConnectionConfiguration Use<TSystemConnectionConfiguration>(this TSystemConnectionConfiguration configuration, IStorageTransportProvider transportProvider)
            where TSystemConnectionConfiguration : SystemConnectionConfiguration
        {
            var editableConfiguration = (IEditableSystemConnectionConfiguration) configuration;

            if (editableConfiguration.TransportProvider != null)
            {
                throw new ArgumentException("A TransportProvider has already been assigned to this SystemConnectionConfiguration", nameof(transportProvider));
            }

            editableConfiguration.TransportProvider = transportProvider ?? throw new ArgumentNullException(nameof(transportProvider));

            return configuration;
        }

        public static TSystemConnectionConfiguration Use<TSystemConnectionConfiguration>(this TSystemConnectionConfiguration configuration, Func<ISystemConnection> factoryExtension)
            where TSystemConnectionConfiguration : SystemConnectionConfiguration
        {
            var editableConfiguration = (IEditableSystemConnectionConfiguration) configuration;

            editableConfiguration.FactoryExtension = factoryExtension;
            return configuration;
        }

        public static TSystemConnectionConfiguration Use<TSystemConnectionConfiguration>(this TSystemConnectionConfiguration configuration, IInfrastructure infrastructure)
            where TSystemConnectionConfiguration : SystemConnectionConfiguration
        {
            var editableConfiguration = (IEditableSystemConnectionConfiguration) configuration;

            editableConfiguration.Infrastructure = infrastructure;
            return configuration;
        }
    }
}