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
        public IManagementConnectionConfiguration Configuration { get; }

        private IStorageConnection _connection;

        public ManagementConnection(IManagementConnectionConfiguration configuration)
        {
            Configuration = configuration;
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
            // // TODO: Temporary patch to make downscaling from a management to a data connection possible.
            // var uriBuilder = new UriBuilder(Configuration.Address)
            // uriBuilder.Path = uriBuilder.Path.Replace("Admin", "User")
            //
            // // TODO: Temporary patch to make downscaling from a management to a data connection possible.
            // uriBuilder.Port = uriBuilder.Port - 1
            //
            // var address = uriBuilder.Uri

            var address = _connection.Details.DataAddress;

			var connectionConfiguration = new DataConnectionConfiguration()
                .UseTransport(Configuration.TransportProvider)
                .Use(address)
                .Use(accountName, spaceName, null);
            var dataConnection = new DataConnectionFactory().Create(connectionConfiguration);
            await dataConnection.Open().ConfigureAwait(false);
            return dataConnection;
        }

        /// <inheritdoc />
        public async Task Open()
        {
            if (IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.ConnectionAlreadyOpen);
            }

            var configuration = new StorageConnectionConfiguration()
                .Use(Configuration.TransportProvider.GetStorageTransport(Configuration.Address));
            _connection = new StorageConnectionFactory().Create(configuration);
            await _connection.Open(Configuration.AccountName, Configuration.Password).ConfigureAwait(false);
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
                task.Wait(); // TODO: HIGH PRIORITY Refactor the dispose into a Disconnect or something similar.
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
