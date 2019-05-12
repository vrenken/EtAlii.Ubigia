namespace EtAlii.Ubigia.PowerShell
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.Ubigia.Api.Transport.Management.WebApi;
    using EtAlii.Ubigia.Api.Transport.WebApi;

    public class PowerShellClient : IPowerShellClient
    {
        public const string CompanyName = "EtAlii";
        public const string ProductName = "Ubigia";
        public const string Copyright = "Copyright ©  2014";

        public static IPowerShellClient Current { get => GetCurrentClient(); set => SetCurrentClient(value); }
        private static IPowerShellClient _current;

        public IStorageResolver StorageResolver { get; }

        public IEntryResolver EntryResolver { get; }

        public ISpaceResolver SpaceResolver { get; }

        public IAccountResolver AccountResolver { get; }

        public IRootResolver RootResolver { get; }

        public IManagementConnection ManagementConnection { get; private set; }

        public IFabricContext Fabric { get; set; }

        public IInfrastructureClient Client { get; }

        public PowerShellClient(
            IStorageResolver storageResolver,
            IEntryResolver entryResolver,
            ISpaceResolver spaceResolver,
            IAccountResolver accountResolver,
            IRootResolver rootResolver,
            IInfrastructureClient infrastructureClient)
        {
            StorageResolver = storageResolver;
            EntryResolver = entryResolver;
            SpaceResolver = spaceResolver;
            AccountResolver = accountResolver;
            RootResolver = rootResolver;

            Client = infrastructureClient;
        }

        private static IPowerShellClient GetCurrentClient()
        {
            return _current ?? (_current = new PowerShellClientFactory().Create<PowerShellClient>());
        }
        
        private static void SetCurrentClient(IPowerShellClient current)
        {
            _current = current;
        }


        public async Task OpenManagementConnection(Uri address, string accountName, string password)
        {
            if (ManagementConnection != null && ManagementConnection.IsConnected)
            {
                await ManagementConnection.Close();
            }

	        var configuration = new ManagementConnectionConfiguration()
		        .Use(WebApiStorageTransportProvider.Create(Client))
                .Use(address)
                .Use(accountName, password);
            ManagementConnection = new ManagementConnectionFactory().Create(configuration);
            await ManagementConnection.Open();
        }
    }
}
