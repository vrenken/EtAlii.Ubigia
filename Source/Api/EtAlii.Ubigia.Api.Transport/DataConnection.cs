// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

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
        /// The Configuration used to instantiate this DataConnection.
        /// </summary>
        public IDataConnectionConfiguration Configuration { get; }

        /// <inheritdoc />
        public bool IsConnected => _connection?.IsConnected ?? false;

        private ISpaceConnection _connection;

        public DataConnection(IDataConnectionConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <inheritdoc />
        public async Task Open()
        {
            if (IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.ConnectionAlreadyOpen);
            }

            var configuration = new SpaceConnectionConfiguration()
                .Use(Configuration.TransportProvider.GetSpaceTransport(Configuration.Address))
                .Use(Configuration.Space);
            _connection = new SpaceConnectionFactory().Create(configuration);
            await _connection.Open(Configuration.AccountName, Configuration.Password).ConfigureAwait(false);
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
