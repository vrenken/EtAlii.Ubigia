namespace EtAlii.Ubigia.Api.Transport.Management
{
    using System;
    using System.Threading.Tasks;

    public abstract class StorageConnection<TTransport> : IStorageConnection<TTransport>
        where TTransport : IStorageTransport
    {
        public Storage Storage { get; private set; }

        IStorageTransport IStorageConnection.Transport => Transport;
        
        public TTransport Transport { get; }

        public IStorageContext Storages { get; }

        public IAccountContext Accounts { get; }

        public IAuthenticationManagementContext Authentication { get; }

        public ISpaceContext Spaces { get; }

        public bool IsConnected => Storage != null;

        public IStorageConnectionConfiguration Configuration { get; }

        protected StorageConnection(
            IStorageTransport transport,
            IStorageConnectionConfiguration configuration, 
            IStorageContext storages, 
            ISpaceContext spaces,
            IAccountContext accounts,
            IAuthenticationManagementContext authentication)
        {
            Transport = (TTransport)transport;
            Configuration = configuration;
            Storages = storages;
            Spaces = spaces;
            Accounts = accounts;
            Authentication = authentication;
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

            await Transport.Stop();
            Storage = null;
        }

        public async Task Open(string accountName, string password)
        {
            if (IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.ConnectionAlreadyOpen);
            }

            await  Authentication.Data.Authenticate(this, accountName, password);

            Storage = await Authentication.Data.GetConnectedStorage(this);

            await Accounts.Open(this);
            await Storages.Open(this);
            await Spaces.Open(this);

            await Transport.Start();

        }

        #region Disposable

        private bool _disposed;

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
                if (disposing && IsConnected)
                {
                    // Free other state (managed objects).
                    var task = Task.Run(async () =>
                    {
                        await Close();
                    });
                    task.Wait();
                    Storage = null;
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
