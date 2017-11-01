﻿namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using EtAlii.Ubigia.Api.Transport.WebApi;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Storage;
    using xTechnology.Hosting;

    public class TestHost : IHost
    {
        private readonly IServiceManager _serviceManager;
        public IInfrastructureClient Client { get; }

        public IInfrastructure Infrastructure { get; }

        public IAddressFactory AddressFactory { get; }

        public IStorage Storage { get; }

        public IHostConfiguration Configuration { get; }

        public TestHost(
            IServiceManager serviceManager,
            IInfrastructureClient client, 
            IAddressFactory addressFactory, 
            IStorage storage, 
            IInfrastructure infrastructure,
            IHostConfiguration configuration)
        {
            _serviceManager = serviceManager;
            Client = client;
            AddressFactory = addressFactory;
            Storage = storage;
            Infrastructure = infrastructure;
            Configuration = configuration;
        }

        public virtual void Start()
        {
            _serviceManager.Start();

            var folder = Storage.PathBuilder.BaseFolder;
            if (Storage.FolderManager.Exists(folder))
            {
                ((IFolderManager)Storage.FolderManager).Delete(folder);
            }
        }

        public virtual void Stop()
        {
            _serviceManager.Stop();
        }
    }
}
