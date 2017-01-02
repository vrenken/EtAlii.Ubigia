namespace EtAlii.Servus.Api.Transport
{
    using System;

    public class StorageConnectionConfiguration : IStorageConnectionConfiguration
    {
        public IInfrastructureClient Client { get { return _client; } }
        private IInfrastructureClient _client;

        public ITransport Transport { get { return _transport; } }
        private ITransport _transport;

        public string Address { get { return _address; } }
        private string _address;

        public string AccountName { get { return _accountName; } }
        private string _accountName;

        public string Password { get { return _password; } }
        private string _password;

        public IStorageConnectionConfiguration Use(ITransport transport)
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

        public IStorageConnectionConfiguration Use(IInfrastructureClient client)
        {
            if (client == null)
            {
                throw new ArgumentException(nameof(client));
            }
            if (_client != null)
            {
                throw new InvalidOperationException("A InfrastructureClient has already been assigned to this StorageConnectionConfiguration");
            }

            _client = client;
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
            if (String.IsNullOrWhiteSpace(accountName))
            {
                throw new ArgumentException(nameof(accountName));
            }
            if (_accountName != null)
            {
                throw new InvalidOperationException("An accountName has already been assigned to this StorageConnectionConfiguration");
            }
            if (String.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException(nameof(password));
            }
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