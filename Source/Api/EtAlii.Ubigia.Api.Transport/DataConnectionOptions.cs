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

        /// <summary>
        /// The transport provider used to create the transport layer components of this data connection.
        /// </summary>
        public ITransportProvider TransportProvider { get; private set; }

        /// <summary>
        /// The factory extension method that is used to extend the connection creation and instantiation.
        /// One example is a popup dialog that allows for (re)configuration of the credentials/addresses etc.
        /// </summary>
        public Func<IDataConnection> FactoryExtension { get; private set; }

        /// <summary>
        /// The address to which this management connection should connect.
        /// </summary>
        public Uri Address { get; private set; }

        /// <summary>
        /// The account name to be used when connecting.
        /// </summary>
        public string AccountName { get; private set; }

        /// <summary>
        /// The account password to be used when connecting.
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// The space to connect to.
        /// </summary>
        public string Space { get; private set; }

        public DataConnectionOptions(IConfigurationRoot configurationRoot)
        {
            ConfigurationRoot = configurationRoot;
            _extensions = new IExtension[]
            {
                new CommonDataConnectionExtension(this)
            };
        }

        /// <summary>
        /// Configures the transport provider that should be used to provide the transport layer components of this data connection.
        /// </summary>
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

        /// <summary>
        /// Configures the factory extension method that is used to extend the connection creation and instantiation.
        /// </summary>
        public DataConnectionOptions Use(Func<IDataConnection> factoryExtension)
        {
            FactoryExtension = factoryExtension;
            return this;
        }

        /// <summary>
        /// Configures the address that should be used when connecting.
        /// </summary>
        public DataConnectionOptions Use(Uri address)
        {
            if (Address != null)
            {
                throw new InvalidOperationException("An address has already been assigned to this DataConnectionOptions");
            }

            Address = address ?? throw new ArgumentNullException(nameof(address));
            return this;
        }

        /// <summary>
        /// Configures both the account name, account password and space that should be used when connecting.
        /// </summary>
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

        /// <summary>
        /// This allows the creation of a DataConnectionOptions instance that never will produce a real connection. It is useful in cases the more high-level layers are needed but without connectivity.
        /// </summary>
        /// <returns></returns>
        public DataConnectionOptions UseStubbedConnection()
        {
            return Use(() => new DataConnectionStub())
                .UseTransport(new StubbedTransportProvider());
        }
    }
}
