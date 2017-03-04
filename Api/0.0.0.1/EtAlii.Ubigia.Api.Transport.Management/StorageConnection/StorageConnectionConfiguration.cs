namespace EtAlii.Ubigia.Api.Management
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Transport;

    public class StorageConnectionConfiguration : IStorageConnectionConfiguration
    {
        public IStorageTransport Transport => _transport;
        private IStorageTransport _transport;

        public string Address => _address;
        private string _address;

        public string AccountName => _accountName;
        private string _accountName;

        public string Password => _password;
        private string _password;

        public IStorageConnectionExtension[] Extensions => _extensions;
        private IStorageConnectionExtension[] _extensions;

        public StorageConnectionConfiguration()
        {
            _extensions = new IStorageConnectionExtension[0];
        }

        public IStorageConnectionConfiguration Use(IStorageConnectionExtension[] extensions)
        {
            if (extensions == null)
            {
                throw new ArgumentException(nameof(extensions));
            }

            _extensions = extensions
                .Concat(_extensions)
                .Distinct()
                .ToArray();
            return this;
        }

        public IStorageConnectionConfiguration Use(IStorageTransport transport)
        {
            if (transport == null)
            {
                throw new ArgumentException(nameof(transport));
            }
            if (_transport != null)
            {
                throw new InvalidOperationException("A Transport has already been assigned to this StorageConnectionConfiguration");
            }

            _transport = transport;
            return this;
        }

        public IStorageConnectionConfiguration Use(string address)
        {
            if (String.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentException(nameof(address));
            }
            if (_address != null)
            {
                throw new InvalidOperationException("An address has already been assigned to this StorageConnectionConfiguration");
            }

            _address = address;
            return this;
        }
        public IStorageConnectionConfiguration Use(string accountName, string password)
        {
            //if (String.IsNullOrWhiteSpace(accountName))
            //{
            //    throw new ArgumentException(nameof(accountName));
            //}
            if (_accountName != null)
            {
                throw new InvalidOperationException("An accountName has already been assigned to this StorageConnectionConfiguration");
            }
            //if (String.IsNullOrWhiteSpace(password))
            //{
            //    throw new ArgumentException(nameof(password));
            //}
            if (_password != null)
            {
                throw new InvalidOperationException("A password has already been assigned to this StorageConnectionConfiguration");
            }

            _accountName = accountName;
            _password = password;
            return this;
        }
    }
}