namespace EtAlii.Servus.Infrastructure.Hosting.Tests
{
    using EtAlii.Servus.Api.Transport;
    using EtAlii.Servus.Api.Transport.WebApi;
    using EtAlii.Servus.Infrastructure;
    using EtAlii.Servus.Infrastructure.Functional;
    using EtAlii.Servus.Storage;

    public class TestHost : IHost
    {
        public IInfrastructureClient Client { get { return _client; } }
        private readonly IInfrastructureClient _client;

        public IInfrastructure Infrastructure { get { return _infrastructure; } }
        private readonly IInfrastructure _infrastructure;

        public IAddressFactory AddressFactory { get { return _addressFactory; } }
        private readonly IAddressFactory _addressFactory;

        public IStorage Storage { get { return _storage; } }
        private readonly IStorage _storage;

        public IHostConfiguration Configuration { get { return _configuration; } }
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
