﻿namespace EtAlii.Ubigia.Api.Transport.Management
{
    using System;

    public class ManagementConnectionConfiguration : Configuration<ManagementConnectionConfiguration>, IManagementConnectionConfiguration
    {
        public IStorageTransportProvider TransportProvider { get; private set; }

        public Func<IManagementConnection> FactoryExtension { get; private set; }

        public Uri Address { get; private set; }

        public string AccountName { get; private set; }

        public string Password { get; private set; }

        public IManagementConnectionConfiguration Use(Func<IManagementConnection> factoryExtension)
        {
            FactoryExtension = factoryExtension;
            return this;
        }

        public IManagementConnectionConfiguration Use(IStorageTransportProvider transportProvider)
        {
            if (TransportProvider != null)
            {
                throw new ArgumentException("A TransportProvider has already been assigned to this ManagementConnectionConfiguration", nameof(transportProvider));
            }
            if (transportProvider == null)
            {
                throw new ArgumentNullException(nameof(transportProvider));
            }

            TransportProvider = transportProvider;

            return this;
        }

        public IManagementConnectionConfiguration Use(Uri address)
        {
			if (Address != null)
            {
                throw new InvalidOperationException("An address has already been assigned to this ManagementConnectionConfiguration");
            }

            Address = address ?? throw new ArgumentNullException(nameof(address));
            return this;
        }

        public IManagementConnectionConfiguration Use(string accountName, string password)
        {
            if (string.IsNullOrWhiteSpace(accountName))
            {
                throw new ArgumentException(nameof(accountName));
            }
            if (AccountName != null)
            {
                throw new InvalidOperationException("An accountName has already been assigned to this ManagementConnectionConfiguration");
            }
            //if [string.IsNullOrWhiteSpace[password]]
            //[
            //    throw new ArgumentException(nameof(password))
            //]
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
