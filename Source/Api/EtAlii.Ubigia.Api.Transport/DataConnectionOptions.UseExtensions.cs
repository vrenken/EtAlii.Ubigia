// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System;

    public static class DataConnectionOptionsUseExtensions
    {
        public static TDataConnectionOptions UseTransport<TDataConnectionOptions>(this TDataConnectionOptions options, ITransportProvider transportProvider)
            where TDataConnectionOptions : DataConnectionOptions
        {
            var editableOptions = (IEditableDataConnectionOptions) options;

            if (editableOptions.TransportProvider != null)
            {
                throw new ArgumentException("A TransportProvider has already been assigned to this DataConnectionOptions",
                    nameof(transportProvider));
            }

            editableOptions.TransportProvider = transportProvider ?? throw new ArgumentNullException(nameof(transportProvider));

            return options;
        }

        public static TDataConnectionOptions Use<TDataConnectionOptions>(this TDataConnectionOptions options, Func<IDataConnection> factoryExtension)
            where TDataConnectionOptions : DataConnectionOptions
        {
            var editableOptions = (IEditableDataConnectionOptions) options;

            editableOptions.FactoryExtension = factoryExtension;
            return options;
        }

        public static TDataConnectionOptions Use<TDataConnectionOptions>(this TDataConnectionOptions options, Uri address)
            where TDataConnectionOptions : DataConnectionOptions
        {
            var editableOptions = (IEditableDataConnectionOptions) options;

            if (editableOptions.Address != null)
            {
                throw new InvalidOperationException("An address has already been assigned to this DataConnectionOptions");
            }

            editableOptions.Address = address ?? throw new ArgumentNullException(nameof(address));
            return options;
        }

        public static TDataConnectionOptions Use<TDataConnectionOptions>(this TDataConnectionOptions options, string accountName, string space, string password)
            where TDataConnectionOptions : DataConnectionOptions
        {
            var editableOptions = (IEditableDataConnectionOptions) options;

            if (string.IsNullOrWhiteSpace(accountName))
            {
                throw new ArgumentException("No account name specified: A data connection cannot be made without account identification", nameof(accountName));
            }
            if (editableOptions.AccountName != null)
            {
                throw new InvalidOperationException("An accountName has already been assigned to this DataConnectionOptions");
            }
            if (editableOptions.Password != null)
            {
                throw new InvalidOperationException("A password has already been assigned to this DataConnectionOptions");
            }
            if (string.IsNullOrWhiteSpace(space))
            {
                throw new ArgumentException("No space specified: A data connection cannot be made without knowing which space to connect to", nameof(space));
            }
            if (editableOptions.Space != null)
            {
                throw new InvalidOperationException("A space has already been assigned to this DataConnectionOptions");
            }

            editableOptions.AccountName = accountName;
            editableOptions.Password = password;
            editableOptions.Space = space;
            return options;
        }

        public static TDataConnectionOptions Use<TDataConnectionOptions>(this TDataConnectionOptions options, DataConnectionOptions otherOptions)
            where TDataConnectionOptions : DataConnectionOptions
        {
            options.Use((ConfigurationBase)otherOptions);

            var editableOptions = (IEditableDataConnectionOptions) options;

            editableOptions.TransportProvider = otherOptions.TransportProvider;
            editableOptions.FactoryExtension = otherOptions.FactoryExtension;
            editableOptions.Address = otherOptions.Address;
            editableOptions.AccountName = otherOptions.AccountName;
            editableOptions.Password = otherOptions.Password;
            editableOptions.Space = otherOptions.Space;

            return options;
        }

    }
}
