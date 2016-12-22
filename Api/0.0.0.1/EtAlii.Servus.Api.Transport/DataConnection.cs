namespace EtAlii.Servus.Api.Transport
{
    using System.Threading.Tasks;

    internal class DataConnection : IDataConnection
    {
        public Storage Storage { get { return _connection?.Storage; } }

        public Space Space { get { return _connection?.Space; } }
        public Account Account { get { return _connection?.Account; } }

        public IRootContext Roots { get { return _connection?.Roots; } }

        public IEntryContext Entries { get { return _connection?.Entries; } }

        public IContentContext Content { get { return _connection?.Content; } }

        public IPropertyContext Properties { get { return _connection?.Properties; } }

        public IDataConnectionConfiguration Configuration { get { return _configuration; } }
        private readonly IDataConnectionConfiguration _configuration;

        public bool IsConnected { get { return _connection?.IsConnected ?? false; } }

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
