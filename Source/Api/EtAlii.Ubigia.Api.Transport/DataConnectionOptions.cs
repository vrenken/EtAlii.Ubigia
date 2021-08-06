// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using Microsoft.Extensions.Configuration;

    public class DataConnectionOptions : ConfigurationBase, IDataConnectionOptions, IEditableDataConnectionOptions
    {
        /// <inheritdoc />
        public IConfigurationRoot ConfigurationRoot { get; }

        /// <inheritdoc />
        ITransportProvider IEditableDataConnectionOptions.TransportProvider { get => TransportProvider; set => TransportProvider = value; }

        /// <inheritdoc />
        public ITransportProvider TransportProvider { get; private set; }

        /// <inheritdoc />
        Func<IDataConnection> IEditableDataConnectionOptions.FactoryExtension { get => FactoryExtension; set => FactoryExtension = value; }

        /// <inheritdoc />
        public Func<IDataConnection> FactoryExtension { get; private set; }

        /// <inheritdoc />
        Uri IEditableDataConnectionOptions.Address { get => Address; set => Address = value; }

        /// <inheritdoc />
        public Uri Address { get; private set; }

        /// <inheritdoc />
        string IEditableDataConnectionOptions.AccountName { get => AccountName; set => AccountName = value; }

        /// <inheritdoc />
        public string AccountName { get; private set; }

        /// <inheritdoc />
        string IEditableDataConnectionOptions.Password { get => Password; set => Password = value; }

        /// <inheritdoc />
        public string Password { get; private set; }

        /// <inheritdoc />
        string IEditableDataConnectionOptions.Space { get => Space; set => Space = value; }

        /// <inheritdoc />
        public string Space { get; private set; }

        public DataConnectionOptions(IConfigurationRoot configurationRoot)
        {
            ConfigurationRoot = configurationRoot;
        }
    }
}
