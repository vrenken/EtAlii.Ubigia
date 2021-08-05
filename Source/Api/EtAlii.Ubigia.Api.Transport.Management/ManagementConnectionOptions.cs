// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management
{
    using System;
    using Microsoft.Extensions.Configuration;

    public class ManagementConnectionOptions : ConfigurationBase, IManagementConnectionOptions
    {
        /// <inheritdoc />
        public IConfiguration ConfigurationRoot { get; }

        /// <inheritdoc />
        public IStorageTransportProvider TransportProvider { get; private set; }

        /// <inheritdoc />
        public Func<IManagementConnection> FactoryExtension { get; private set; }

        /// <inheritdoc />
        public Uri Address { get; private set; }

        /// <inheritdoc />
        public string AccountName { get; private set; }

        /// <inheritdoc />
        public string Password { get; private set; }

        public ManagementConnectionOptions(IConfiguration configurationRoot)
        {
            ConfigurationRoot = configurationRoot;
        }

        /// <inheritdoc />
        public ManagementConnectionOptions Use(Func<IManagementConnection> factoryExtension)
        {
            FactoryExtension = factoryExtension;
            return this;
        }

        /// <inheritdoc />
        public ManagementConnectionOptions Use(IStorageTransportProvider transportProvider)
        {
            if (TransportProvider != null)
            {
                throw new ArgumentException("A TransportProvider has already been assigned to this ManagementConnectionOptions", nameof(transportProvider));
            }

            TransportProvider = transportProvider ?? throw new ArgumentNullException(nameof(transportProvider));

            return this;
        }

        /// <inheritdoc />
        public ManagementConnectionOptions Use(Uri address)
        {
			if (Address != null)
            {
                throw new InvalidOperationException("An address has already been assigned to this ManagementConnectionOptions");
            }

            Address = address ?? throw new ArgumentNullException(nameof(address));
            return this;
        }

        /// <inheritdoc />
        public ManagementConnectionOptions Use(string accountName, string password)
        {
            if (string.IsNullOrWhiteSpace(accountName))
            {
                throw new ArgumentException("No account name specified: A management connection cannot be made without account identification", nameof(accountName));
            }
            if (AccountName != null)
            {
                throw new InvalidOperationException("An account name has already been assigned to this ManagementConnectionOptions");
            }
            if (Password != null)
            {
                throw new InvalidOperationException("A password has already been assigned to this ManagementConnectionOptions");
            }

            AccountName = accountName;
            Password = password;
            return this;
        }
    }
}
