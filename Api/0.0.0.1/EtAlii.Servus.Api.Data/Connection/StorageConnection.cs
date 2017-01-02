namespace EtAlii.Servus.Api
{
    using System;
    using System.Net;

    public class StorageConnection : IStorageConnection
    {
        public Storage Storage { get { return _storage; } private set { _storage = value; } }
        private Storage _storage;

        private readonly IInfrastructureClient _client;
        private readonly IAddressFactory _addressFactory;

        public bool IsConnected { get { return _storage != null; } }

        private readonly INotificationTransport _notificationTransport;

        protected StorageConnection(
            INotificationTransport notificationTransport,
            IAddressFactory addressFactory,
            IInfrastructureClient client)
        {
            _notificationTransport = notificationTransport;
            _addressFactory = addressFactory;
            _client = client;
        }

        public void Close()
        {
            if (!IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.ConnectionAlreadyClosed);
            }

            _notificationTransport.Close();

            Storage = null;
        }

        public void Open(string address, string accountName)
        {
            if (IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.ConnectionAlreadyOpen);
            }

            _storage = GetStorage(address, accountName);

            _notificationTransport.Open(address);
        }

        public void Open(string address, string accountName, string password)
        {
            if (IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.ConnectionAlreadyOpen);
            }

            _storage = GetStorage(address, accountName, password);

            _notificationTransport.Open(address);
        }

        private Storage GetStorage(string address, string accountName, string password)
        {
            Storage storage;

            string authenticationToken = Authenticate(address, accountName, password);

            if (!String.IsNullOrWhiteSpace(authenticationToken))
            {
                _client.AuthenticationToken = authenticationToken;

                address = _addressFactory.CreateFullAddress(address, RelativeUri.Storages) + "?local";
                storage = _client.Get<Storage>(address);
            }
            else
            {
                string message = String.Format("Unable to connect to the specified storage ({0})", address);
                throw new UnauthorizedInfrastructureOperationException(message);
            }
            return storage;
        }

        private Storage GetStorage(string address, string accountName)
        {
            Storage storage;

            string authenticationToken = _client.AuthenticationToken;

            if (!String.IsNullOrWhiteSpace(authenticationToken))
            {
                _client.AuthenticationToken = authenticationToken;

                address = _addressFactory.CreateFullAddress(address, RelativeUri.Storages) + "?local";
                storage = _client.Get<Storage>(address);
            }
            else
            {
                string message = String.Format("Unable to connect to the specified storage ({0})", address);
                throw new UnauthorizedInfrastructureOperationException(message);
            }
            return storage;
        }

        private string Authenticate(string address, string accountName, string password)
        {
            var credentials = new NetworkCredential(accountName, password);
            address = _addressFactory.CreateFullAddress(address, RelativeUri.Authenticate);
            //var address = BuildFullAddress(RelativeUri.Authenticate);
            var authenticationToken = _client.Get<string>(address, credentials);

            if (String.IsNullOrWhiteSpace(authenticationToken))
            {
                throw new UnableToAuthorizeInfrastructureOperationException(InvalidInfrastructureOperation.UnableToAthorize);
            }
            return authenticationToken;
        }
    }
}
