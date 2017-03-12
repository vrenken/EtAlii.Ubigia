namespace EtAlii.Ubigia.Infrastructure.Hosting.Tests
{
    using EtAlii.Ubigia.Api.Transport.WebApi;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Storage;

    public class TestHost : IHost
    {
        public IInfrastructureClient Client => _client;
        private readonly IInfrastructureClient _client;

        public IInfrastructure Infrastructure => _infrastructure;
        private readonly IInfrastructure _infrastructure;

        public IAddressFactory AddressFactory => _addressFactory;
        private readonly IAddressFactory _addressFactory;

        public IStorage Storage => _storage;
        private readonly IStorage _storage;

        public IHostConfiguration Configuration => _configuration;
        private readonly IHostConfiguration _configuration;

        public TestHost(
            IInfrastructureClient client, 
            IAddressFactory addressFactory, 
            IStorage storage, 
            IInfrastructure infrastructure,
            IHostConfiguration configuration)
        {
            _client = client;
            _addressFactory = addressFactory;
            _storage = storage;
            _infrastructure = infrastructure;
            _configuration = configuration;
        }

        public virtual void Start()
        {
            _infrastructure.Start();

            var folder = Storage.PathBuilder.BaseFolder;
            if (Storage.FolderManager.Exists(folder))
            {
                ((IFolderManager)Storage.FolderManager).Delete(folder);
            }
        }

        public virtual void Stop()
        {
            _infrastructure.Stop();
        }
    }
}
