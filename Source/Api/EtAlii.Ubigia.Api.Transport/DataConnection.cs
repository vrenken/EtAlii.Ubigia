// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    internal class DataConnection : IDataConnection
    {
        /// <inheritdoc />
        public Storage Storage => _connection?.Storage;

        /// <inheritdoc />
        public Space Space => _connection?.Space;

        /// <inheritdoc />
        public Account Account => _connection?.Account;

        /// <inheritdoc />
        public IRootContext Roots => _connection?.Roots;

        /// <inheritdoc />
        public IEntryContext Entries => _connection?.Entries;

        /// <inheritdoc />
        public IContentContext Content => _connection?.Content;

        /// <inheritdoc />
        public IPropertiesContext Properties => _connection?.Properties;

        /// <summary>
        /// The Options used to instantiate this DataConnection.
        /// </summary>
        public DataConnectionOptions Options { get; }

        /// <inheritdoc />
        public bool IsConnected => _connection?.IsConnected ?? false;

        private ISpaceConnection _connection;

        public DataConnection(DataConnectionOptions options)
        {
            Options = options;
        }

        /// <inheritdoc />
        public async Task Open()
        {
            if (IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.ConnectionAlreadyOpen);
            }

            var options = new SpaceConnectionOptions(Options.ConfigurationRoot)
                .Use(Options.TransportProvider.GetSpaceTransport(Options.Address))
                .Use(Options.Space);
            _connection = new SpaceConnectionFactory().Create(options);
            await _connection.Open(Options.AccountName, Options.Password).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task Close()
        {
            if (!IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.ConnectionAlreadyClosed);
            }
            await _connection.Close().ConfigureAwait(false);
            _connection = null;
        }
    }
}
