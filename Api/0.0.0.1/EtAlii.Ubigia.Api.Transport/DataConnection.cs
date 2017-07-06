namespace EtAlii.Ubigia.Api.Transport
{
    using System.Threading.Tasks;

    internal class DataConnection : IDataConnection
    {
        public Storage Storage => _connection?.Storage;

        public Space Space => _connection?.Space;
        public Account Account => _connection?.Account;

        public IRootContext Roots => _connection?.Roots;

        public IEntryContext Entries => _connection?.Entries;

        public IContentContext Content => _connection?.Content;

        public IPropertiesContext Properties => _connection?.Properties;

        public IDataConnectionConfiguration Configuration { get; }

        public bool IsConnected => _connection?.IsConnected ?? false;

        private ISpaceConnection _connection;

        internal DataConnection(IDataConnectionConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task Open()
        {
            if (IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.ConnectionAlreadyOpen);
            }

            //await _connection.Open(address, accountName, password);
            var configuration = new SpaceConnectionConfiguration()
                .Use(Configuration.TransportProvider.GetSpaceTransport())
                .Use(Configuration.Address)
                .Use(Configuration.AccountName, Configuration.Space, Configuration.Password);
            _connection = new SpaceConnectionFactory().Create(configuration);
            await _connection.Open();
            //await OpenSpace();
        }

        public async Task Close()
        {
            if (!IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.ConnectionAlreadyClosed);
            }
            await _connection.Close();
            _connection = null;
        }
    }
}
