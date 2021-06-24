// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management
{
    using System;

    public class ManagementConnectionConfiguration : ConfigurationBase, IManagementConnectionConfiguration
    {
        public IStorageTransportProvider TransportProvider { get; private set; }

        public Func<IManagementConnection> FactoryExtension { get; private set; }

        public Uri Address { get; private set; }

        public string AccountName { get; private set; }

        public string Password { get; private set; }

        public ManagementConnectionConfiguration Use(Func<IManagementConnection> factoryExtension)
        {
            FactoryExtension = factoryExtension;
            return this;
        }

        public ManagementConnectionConfiguration Use(IStorageTransportProvider transportProvider)
        {
            if (TransportProvider != null)
            {
                throw new ArgumentException("A TransportProvider has already been assigned to this ManagementConnectionConfiguration", nameof(transportProvider));
            }

            TransportProvider = transportProvider ?? throw new ArgumentNullException(nameof(transportProvider));

            return this;
        }

        public ManagementConnectionConfiguration Use(Uri address)
        {
			if (Address != null)
            {
                throw new InvalidOperationException("An address has already been assigned to this ManagementConnectionConfiguration");
            }

            Address = address ?? throw new ArgumentNullException(nameof(address));
            return this;
        }

        public ManagementConnectionConfiguration Use(string accountName, string password)
        {
            if (string.IsNullOrWhiteSpace(accountName))
            {
                throw new ArgumentException("No account name specified: A management connection cannot be made without account identification", nameof(accountName));
            }
            if (AccountName != null)
            {
                throw new InvalidOperationException("An account name has already been assigned to this ManagementConnectionConfiguration");
            }
            if (Password != null)
            {
                throw new InvalidOperationException("A password has already been assigned to this ManagementConnectionConfiguration");
            }

            AccountName = accountName;
            Password = password;
            return this;
        }
    }
}
