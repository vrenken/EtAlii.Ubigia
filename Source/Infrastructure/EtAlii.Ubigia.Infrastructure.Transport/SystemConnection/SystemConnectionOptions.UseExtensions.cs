// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Infrastructure.Functional;

    public static class SystemConnectionOptionsUseExtensions
    {
        public static TSystemConnectionOptions Use<TSystemConnectionOptions>(this TSystemConnectionOptions options, IStorageTransportProvider transportProvider)
            where TSystemConnectionOptions : SystemConnectionOptions
        {
            var editableOptions = (IEditableSystemConnectionOptions) options;

            if (editableOptions.TransportProvider != null)
            {
                throw new ArgumentException("A TransportProvider has already been assigned to this SystemConnectionOptions", nameof(transportProvider));
            }

            editableOptions.TransportProvider = transportProvider ?? throw new ArgumentNullException(nameof(transportProvider));

            return options;
        }

        public static TSystemConnectionOptions Use<TSystemConnectionOptions>(this TSystemConnectionOptions options, Func<ISystemConnection> factoryExtension)
            where TSystemConnectionOptions : SystemConnectionOptions
        {
            var editableOptions = (IEditableSystemConnectionOptions) options;

            editableOptions.FactoryExtension = factoryExtension;
            return options;
        }

        public static TSystemConnectionOptions Use<TSystemConnectionOptions>(this TSystemConnectionOptions options, IInfrastructure infrastructure)
            where TSystemConnectionOptions : SystemConnectionOptions
        {
            var editableOptions = (IEditableSystemConnectionOptions) options;

            editableOptions.Infrastructure = infrastructure;
            return options;
        }
    }
}
