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

        public IPropertyContext Properties => _connection?.Properties;

        public IDataConnectionConfiguration Configuration => _configuration;
        private readonly IDataConnectionConfiguration _configuration;

        public bool IsConnected => _connection?.IsConnected ?? false;

        private ISpaceConnection _connection;

        internal DataConnection(IDataConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Open()
        {
            if (IsConnected)
            {
                throw new InvalidInfrastructureOperationException(InvalidInfrastructureOperation.ConnectionAlreadyOpen);
            }

            //await _connection.Open(address, accountName, password);
            var configuration = new SpaceConnectionConfiguration()
                .Use(_configuration.TransportProvider.GetSpaceTransport())
                .Use(_configuration.Address)
                .Use(_configuration.AccountName, _configuration.Space, _configuration.Password);
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
