namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using System.ComponentModel;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Storage;
    using xTechnology.Hosting;

    public class TestHost : HostBase, IHost
    {
        //public IInfrastructureClient Client { get; }

        public IInfrastructure Infrastructure { get; }

        //public IAddressFactory AddressFactory { get; }

        public IStorage Storage { get; }

        public IHostConfiguration Configuration { get; }

        public TestHost(
            IServiceManager serviceManager,
			//IInfrastructureClient client, 
			//IAddressFactory addressFactory, 
			IStorage storage, 
            IInfrastructure infrastructure,
            IHostConfiguration configuration)
            : base(serviceManager)
        {
			//Client = client;
	        //AddressFactory = addressFactory;
			Storage = storage;
            Infrastructure = infrastructure;
            Configuration = configuration;

            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(State):
                    if (State == HostState.Running)
                    {
                        var folder = Storage.PathBuilder.BaseFolder;
                        if (Storage.FolderManager.Exists(folder))
                        {
                            ((IFolderManager) Storage.FolderManager).Delete(folder);
                        }
                    }
                    break;
            }
        }
    }
}
