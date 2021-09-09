// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class DataConnectionOptions : IExtensible
    {
        /// <summary>
        /// The client configuration root used to instantiate the data connection.
        /// </summary>
        public IConfigurationRoot ConfigurationRoot { get; }

        /// <inheritdoc/>
        IExtension[] IExtensible.Extensions { get => _extensions; set => _extensions = value; }
        private IExtension[] _extensions;

        /// <inheritdoc />
        public ITransportProvider TransportProvider { get; private set; }

        /// <inheritdoc />
        public Func<IDataConnection> FactoryExtension { get; private set; }

        /// <inheritdoc />
        public Uri Address { get; private set; }

        /// <inheritdoc />
        public string AccountName { get; private set; }

        /// <inheritdoc />
        public string Password { get; private set; }

        /// <inheritdoc />
        public string Space { get; private set; }

        public DataConnectionOptions(IConfigurationRoot configurationRoot)
        {
            ConfigurationRoot = configurationRoot;
            _extensions = Array.Empty<IExtension>();
        }

        public DataConnectionOptions UseTransport(ITransportProvider transportProvider)
        {
            if (TransportProvider != null)
            {
                throw new ArgumentException("A TransportProvider has already been assigned to this DataConnectionOptions",
                    nameof(transportProvider));
            }

            TransportProvider = transportProvider ?? throw new ArgumentNullException(nameof(transportProvider));

            return this;
        }

        public DataConnectionOptions Use(Func<IDataConnection> factoryExtension)
        {
            FactoryExtension = factoryExtension;
            return this;
        }

        public DataConnectionOptions Use(Uri address)
        {
            if (Address != null)
            {
                throw new InvalidOperationException("An address has already been assigned to this DataConnectionOptions");
            }

            Address = address ?? throw new ArgumentNullException(nameof(address));
            return this;
        }

        public DataConnectionOptions Use(string accountName, string space, string password)
        {
            if (string.IsNullOrWhiteSpace(accountName))
            {
                throw new ArgumentException("No account name specified: A data connection cannot be made without account identification", nameof(accountName));
            }
            if (AccountName != null)
            {
                throw new InvalidOperationException("An accountName has already been assigned to this DataConnectionOptions");
            }
            if (Password != null)
            {
                throw new InvalidOperationException("A password has already been assigned to this DataConnectionOptions");
            }
            if (string.IsNullOrWhiteSpace(space))
            {
                throw new ArgumentException("No space specified: A data connection cannot be made without knowing which space to connect to", nameof(space));
            }
            if (Space != null)
            {
                throw new InvalidOperationException("A space has already been assigned to this DataConnectionOptions");
            }

            AccountName = accountName;
            Password = password;
            Space = space;
            return this;
        }
    }
}
