// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management
{
    using System;
    using System.Threading.Tasks;

    internal class ManagementConnection : IManagementConnection
    {
        /// <inheritdoc />
        public bool IsConnected => _connection?.IsConnected ?? false;

        /// <inheritdoc />
        public Storage Storage => _connection?.Storage;

        /// <inheritdoc />
        public IStorageContext Storages => _connection?.Storages;

        /// <inheritdoc />
        public IAccountContext Accounts => _connection?.Accounts;

        /// <inheritdoc />
        public ISpaceContext Spaces => _connection?.Spaces;

        /// <inheritdoc />
        public IStorageConnectionDetails Details => _connection?.Details;

        /// <inheritdoc />
        public IManagementConnectionOptions Options { get; }

        private IStorageConnection _connection;

        public ManagementConnection(IManagementConnectionOptions options)
        {
            Options = options;
        }

        /// <inheritdoc />
        public async Task<IDataConnection> OpenSpace(Guid accountId, Guid spaceId)
        {
            var account = await Accounts.Get(accountId).ConfigureAwait(false);
            var space = await Spaces.Get(spaceId).ConfigureAwait(false);
            return await OpenSpace(account.Name, space.Name).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<IDataConnection> OpenSpace(Space space)
        {
            var account = await Accounts.Get(space.AccountId).ConfigureAwait(false);
            return await OpenSpace(account.Name, space.Name).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<IDataConnection> OpenSpace(string accountName, string spaceName)
        {
            var address = _connection.Details.DataAddress;

			var options = new DataConnectionOptions(Options.ConfigurationRoot)
                .UseTransport(Options.TransportProvider)
                .Use(address)
                .Use(accountName, spaceName, null);
            var dataConnection = new DataConnectionFactory().Create(options);
            await dataConnection
                .Open()
                .ConfigureAwait(false);
            return dataConnection;
        }

        /// <inheritdoc />
        public async Task Open()
        {
            if (IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.ConnectionAlreadyOpen);
            }

            var options = new StorageConnectionOptions(Options.ConfigurationRoot)
                .Use(Options.TransportProvider.GetStorageTransport(Options.Address));
            _connection = new StorageConnectionFactory()
                .Create(options);
            await _connection
                .Open(Options.AccountName, Options.Password)
                .ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task Close()
        {
            if (!IsConnected)
            {
                throw new InvalidInfrastructureOperationException("The connection is already closed");
            }

            await _connection.Close().ConfigureAwait(false);
            _connection = null;
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
                // Refactor the dispose in the Connections to a Disconnect or something similar.
                // More details can be found in the GitHub issue below:
                // https://github.com/vrenken/EtAlii.Ubigia/issues/90
                task.Wait();
            }
            // Free your own state (unmanaged objects).
            // Set large fields to null.
            _disposed = true;
        }

        // Use C# destructor syntax for finalization code.
        ~ManagementConnection()
        {
            // Simply call Dispose(false).
            Dispose(false);
        }

        #endregion Disposable

    }
}
