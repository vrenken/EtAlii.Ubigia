namespace EtAlii.Ubigia.PowerShell
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.Ubigia.Api.Transport.Management.WebApi;
    using EtAlii.Ubigia.Api.Transport.WebApi;

    public class PowerShellClient : IPowerShellClient
    {
        public const string CompanyName = "EtAlii";
        public const string ProductName = "Ubigia";
        public const string Copyright = "Copyright ©  2014";

        public static IPowerShellClient Current { get { return GetCurrentClient(); } set { _current = value; } }
        private static IPowerShellClient _current;

        public IStorageResolver StorageResolver => _storageResolver;
        private readonly IStorageResolver _storageResolver;

        public IEntryResolver EntryResolver => _entryResolver;
        private readonly IEntryResolver _entryResolver;

        public ISpaceResolver SpaceResolver => _spaceResolver;
        private readonly ISpaceResolver _spaceResolver;

        public IAccountResolver AccountResolver => _accountResolver;
        private readonly IAccountResolver _accountResolver;

        public IRootResolver RootResolver => _rootResolver;
        private readonly IRootResolver _rootResolver;

        public IManagementConnection ManagementConnection => _managementConnection;
        private IManagementConnection _managementConnection;

        public IFabricContext Fabric { get { return _fabric; } set { _fabric = value; } }
        private IFabricContext _fabric;

        public IInfrastructureClient Client => _client;
        private readonly IInfrastructureClient _client;

        public PowerShellClient(
            IStorageResolver storageResolver,
            IEntryResolver entryResolver,
            ISpaceResolver spaceResolver,
            IAccountResolver accountResolver,
            IRootResolver rootResolver,
            IInfrastructureClient infrastructureClient)
        {
            _storageResolver = storageResolver;
            _entryResolver = entryResolver;
            _spaceResolver = spaceResolver;
            _accountResolver = accountResolver;
            _rootResolver = rootResolver;

            _client = infrastructureClient;
        }

        private static IPowerShellClient GetCurrentClient()
        {
            if (_current == null)
            {
                _current = new PowerShellClientFactory().Create<PowerShellClient>();
            }
            return _current;
        }

        public async Task OpenManagementConnection(string address, string accountName, string password)
        {
            if (_managementConnection != null && _managementConnection.IsConnected)
            {
                await _managementConnection.Close();
            }

            var configuration = new ManagementConnectionConfiguration()
                .Use(WebApiStorageTransportProvider.Create(_client))
                .Use(address)
                .Use(accountName, password);
            _managementConnection = new ManagementConnectionFactory().Create(configuration);
            await _managementConnection.Open();
        }
    }
}
