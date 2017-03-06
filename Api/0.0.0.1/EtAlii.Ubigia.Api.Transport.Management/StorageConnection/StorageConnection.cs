namespace EtAlii.Ubigia.Api.Transport.Management
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;

    public abstract class StorageConnection<TTransport> : IStorageConnection<TTransport>
        where TTransport : IStorageTransport
    {
        public Storage Storage { get { return _storage; } private set { _storage = value; } }
        private Storage _storage;

        public TTransport Transport => _transport;
        private readonly TTransport _transport;

        public IStorageContext Storages => _storages;
        private IStorageContext _storages;

        public IAccountContext Accounts => _accounts;
        private IAccountContext _accounts;
        private readonly IAuthenticationContext _authentication;

        public ISpaceContext Spaces => _spaces;
        private ISpaceContext _spaces;

        public bool IsConnected => _storage != null;

        public IStorageConnectionConfiguration Configuration => _configuration;
        private readonly IStorageConnectionConfiguration _configuration;

        protected StorageConnection(
            IStorageTransport transport,
            IStorageConnectionConfiguration configuration, 
            IStorageContext storages, 
            ISpaceContext spaces,
            IAccountContext accounts,
            IAuthenticationContext authentication)
        {
            _transport = (TTransport)transport;
            _configuration = configuration;
            _storages = storages;
            _spaces = spaces;
            _accounts = accounts;
            _authentication = authentication;
        }

        public async Task Close()
        {
            if (!IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.ConnectionAlreadyClosed);
            }

            await _accounts.Close(this);
            await _storages.Close(this);
            await _spaces.Close(this);

            await _transport.Stop(this);
            Storage = null;
        }

        public async Task Open()
        {
            if (IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.ConnectionAlreadyOpen);
            }

            await  _authentication.Data.Authenticate(this);

            _storage = await _authentication.Data.GetConnectedStorage(this);

            _transport.Initialize(this, _configuration.Address);

            await _accounts.Open(this);
            await _storages.Open(this);
            await _spaces.Open(this);

            await _transport.Start(this, _configuration.Address);

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
        ~StorageConnection()
        {
            // Simply call Dispose(false).
            Dispose(false);
        }

        #endregion Disposable
    }
}
