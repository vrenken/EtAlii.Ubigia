namespace EtAlii.Servus.Api.Transport
{
    using System;
    using System.Net;
    using System.Threading.Tasks;

    internal class SpaceConnection : ISpaceConnection
    {
        public Storage Storage { get { return _storage; } private set { _storage = value; } }
        private Storage _storage;

        public Space Space { get { return _space; } private set { _space = value; } }
        private Space _space;

        public Account Account { get { return _account; } private set { _account = value; } }
        private Account _account;

        public IInfrastructureClient Client { get { return _client; } }
        private readonly IInfrastructureClient _client;
        public IAddressFactory AddressFactory { get { return _addressFactory; } }
        private readonly IAddressFactory _addressFactory;
        public ITransport Transport { get { return _transport; } }
        private readonly ITransport _transport;

        public bool IsConnected { get { return _storage != null && _space != null; } }

        public ISpaceConnectionConfiguration Configuration {get { return _configuration; } }
        private readonly ISpaceConnectionConfiguration _configuration;

        internal SpaceConnection(
            ITransport transport,
            IAddressFactory addressFactory,
            IInfrastructureClient client,
            ISpaceConnectionConfiguration configuration)
        {
            _transport = transport;
            _addressFactory = addressFactory;
            _client = client;
            _configuration = configuration;
        }

        public async Task Close()
        {
            if (!IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.ConnectionAlreadyClosed);
            }

            await _transport.Close(this);
            Storage = null;
        }

        public async Task Open()
        {
            if (IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.ConnectionAlreadyOpen);
            }

            _storage = await GetStorage(_configuration.Address, _configuration.AccountName, _configuration.Password);
            await _transport.Open(this, _configuration.Address);
        }

        private async Task<Storage> GetStorage(string address, string accountName, string password)
        {
            Storage storage;

            string authenticationToken = await Authenticate(address, accountName, password);

            if (!String.IsNullOrWhiteSpace(authenticationToken))
            {
                _client.AuthenticationToken = authenticationToken;

                var localAddress = _addressFactory.CreateFullAddress(address, RelativeUri.Storages) + "?local";
                storage = await _client.Get<Storage>(localAddress);

                // We do not want the address pushed to us from the server. 
                // If we get here then we already know how to contact the server. 
                storage.Address = address;
            }
            else
            {
                string message = String.Format("Unable to connect to the specified storage ({0})", address);
                throw new UnauthorizedInfrastructureOperationException(message);
            }
            return storage;
        }

        private async Task<string> Authenticate(string address, string accountName, string password)
        {
            var credentials = new NetworkCredential(accountName, password);
            address = _addressFactory.CreateFullAddress(address, RelativeUri.Authenticate);
            //var address = BuildFullAddress(RelativeUri.Authenticate);
            var authenticationToken = await _client.Get<string>(address, credentials);

            if (String.IsNullOrWhiteSpace(authenticationToken))
            {
                throw new UnableToAuthorizeInfrastructureOperationException(InvalidInfrastructureOperation.UnableToAthorize);
            }
            return authenticationToken;
        }

        #region Disposable

        private bool _disposed = false;

        //Implement IDisposable.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Free other state (managed objects).
                    if (IsConnected)
                    {
                        var task = Task.Run(async () =>
                        {
                            await Close();
                        });
                        task.Wait();
                        _storage = null;
                    }
                }
                // Free your own state (unmanaged objects).
                // Set large fields to null.
                _disposed = true;
            }
        }

        // Use C# destructor syntax for finalization code.
        ~SpaceConnection()
        {
            // Simply call Dispose(false).
            Dispose(false);
        }

        #endregion Disposable
    }
}
