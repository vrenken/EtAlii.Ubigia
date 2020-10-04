﻿namespace EtAlii.Ubigia.Api.Transport.Management
{
    using System;
    using System.Threading.Tasks;

    public abstract class StorageConnection<TTransport> : IStorageConnection<TTransport>
        where TTransport : IStorageTransport
    {
        /// <inheritdoc />
        public Storage Storage { get; private set; }

        IStorageTransport IStorageConnection.Transport => Transport;
        
        /// <inheritdoc />
        public TTransport Transport { get; }

        /// <inheritdoc />
        public IStorageContext Storages { get; }

        /// <inheritdoc />
        public IAccountContext Accounts { get; }

        private readonly IAuthenticationManagementContext _authentication;
        private readonly IInformationContext _information;

        /// <inheritdoc />
        public ISpaceContext Spaces { get; }

        public bool IsConnected => Storage != null;

        /// <inheritdoc />
        public IStorageConnectionDetails Details => _details;
        private readonly StorageConnectionDetails _details;

        /// <inheritdoc />
        public IStorageConnectionConfiguration Configuration { get; }

        protected StorageConnection(
            IStorageTransport transport,
            IStorageConnectionConfiguration configuration, 
            IStorageContext storages, 
            ISpaceContext spaces,
            IAccountContext accounts,
            IAuthenticationManagementContext authentication,
            IInformationContext information)
        {
            Transport = (TTransport)transport;
            Configuration = configuration;
            Storages = storages;
            Spaces = spaces;
            Accounts = accounts;
            _authentication = authentication;
            _information = information;
            
            _details = new StorageConnectionDetails();
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
            await _information.Close(this);
            
            await Transport.Stop();
            Storage = null;
        }

        public async Task Open(string accountName, string password)
        {
            if (IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.ConnectionAlreadyOpen);
            }

            await  _authentication.Data.Authenticate(this, accountName, password);

            Storage = await _information.Data.GetConnectedStorage(this);
            var details = await _information.Data.GetConnectivityDetails(this);
            _details.Update(Storage, details.ManagementAddress, details.DataAddress);
                
            await _information.Open(this);
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
            if (_disposed) return;
            
            if (disposing && IsConnected)
            {
                var task = Close();
                task.Wait(); // TODO: HIGH PRIORITY Refactor the dispose into a Disconnect or something similar. 
                Storage = null;
            }
            // Free your own state (unmanaged objects).
            // Set large fields to null.
            _disposed = true;
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
