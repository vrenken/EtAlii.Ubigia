namespace EtAlii.Ubigia.Api.Transport.Management
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Transport;

    public abstract class StorageConnection<TTransport> : IStorageConnection<TTransport>
        where TTransport : IStorageTransport
    {
        public Storage Storage { get; private set; }

        public TTransport Transport { get; }

        public IStorageContext Storages { get; }

        public IAccountContext Accounts { get; }

        private readonly IAuthenticationContext _authentication;

        public ISpaceContext Spaces { get; }

        public bool IsConnected => Storage != null;

        public IStorageConnectionConfiguration Configuration { get; }

        protected StorageConnection(
            IStorageTransport transport,
            IStorageConnectionConfiguration configuration, 
            IStorageContext storages, 
            ISpaceContext spaces,
            IAccountContext accounts,
            IAuthenticationContext authentication)
        {
            Transport = (TTransport)transport;
            Configuration = configuration;
            Storages = storages;
            Spaces = spaces;
            Accounts = accounts;
            _authentication = authentication;
        }

        public async Task Close()
        {
            if (!IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.ConnectionAlreadyClosed);
            }

            await Accounts.Close(this);
            await Storages.Close(this);
            await Spaces.Close(this);

            await Transport.Stop(this);
            Storage = null;
        }

        public async Task Open()
        {
            if (IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.ConnectionAlreadyOpen);
            }

            await  _authentication.Data.Authenticate(this);

            Storage = await _authentication.Data.GetConnectedStorage(this);

            Transport.Initialize(this, Configuration.Address);

            await Accounts.Open(this);
            await Storages.Open(this);
            await Spaces.Open(this);

            await Transport.Start(this, Configuration.Address);

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
                        Storage = null;
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
