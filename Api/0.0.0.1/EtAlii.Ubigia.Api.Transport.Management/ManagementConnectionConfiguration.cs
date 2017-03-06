namespace EtAlii.Ubigia.Api.Transport.Management
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Transport;

    public class ManagementConnectionConfiguration : IManagementConnectionConfiguration
    {
        public IManagementConnectionExtension[] Extensions => _extensions;
        private IManagementConnectionExtension[] _extensions;

        public IStorageTransportProvider TransportProvider => _transportProvider;
        private IStorageTransportProvider _transportProvider;

        public Func<IManagementConnection> FactoryExtension => _factoryExtension;
        private Func<IManagementConnection> _factoryExtension;

        public string Address => _address;
        private string _address;

        public string AccountName => _accountName;
        private string _accountName;

        public string Password => _password;
        private string _password;

        public ManagementConnectionConfiguration()
        {
            _extensions = new IManagementConnectionExtension[0];
        }

        public IManagementConnectionConfiguration Use(IManagementConnectionExtension[] extensions)
        {
            if (extensions == null)
            {
                throw new ArgumentException("extensions");
            }

            _extensions = extensions
                .Concat(_extensions)
                .Distinct()
                .ToArray();
            return this;
        }

        public IManagementConnectionConfiguration Use(Func<IManagementConnection> factoryExtension)
        {
            _factoryExtension = factoryExtension;
            return this;
        }

        public IManagementConnectionConfiguration Use(IStorageTransportProvider transportProvider)
        {
            if (_transportProvider != null)
            {
                throw new ArgumentException("A TransportProvider has already been assigned to this ManagementConnectionConfiguration", nameof(transportProvider));
            }
            if (transportProvider == null)
            {
                throw new ArgumentNullException(nameof(transportProvider));
            }

            _transportProvider = transportProvider;

            return this;
        }

        public IManagementConnectionConfiguration Use(string address)
        {
            if (String.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentException(nameof(address));
            }
            if (_address != null)
            {
                throw new InvalidOperationException("An address has already been assigned to this ManagementConnectionConfiguration");
            }

            _address = address;
            return this;
        }

        public IManagementConnectionConfiguration Use(string accountName, string password)
        {
            if (String.IsNullOrWhiteSpace(accountName))
            {
                throw new ArgumentException(nameof(accountName));
            }
            if (_accountName != null)
            {
                throw new InvalidOperationException("An accountName has already been assigned to this ManagementConnectionConfiguration");
            }
            //if (String.IsNullOrWhiteSpace(password))
            //{
            //    throw new ArgumentException(nameof(password));
            //}
            if (_password != null)
            {
                throw new InvalidOperationException("A password has already been assigned to this ManagementConnectionConfiguration");
            }

            _accountName = accountName;
            _password = password;
            return this;
        }
    }
}
