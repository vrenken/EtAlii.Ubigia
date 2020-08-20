﻿namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost.Grpc
{
	using System.Linq;
	using System.Threading.Tasks;
	using EtAlii.Ubigia.Infrastructure.Functional;
	using EtAlii.Ubigia.Infrastructure.Transport;
	using EtAlii.Ubigia.Storage;
	using EtAlii.xTechnology.Hosting;

	public class InfrastructureTestHost : TestHost, IInfrastructureTestHost 
    {
	    public IInfrastructure Infrastructure => _infrastructure;
	    private IInfrastructure _infrastructure;

	    public IStorage Storage => _storage;
	    private IStorage _storage;

		public InfrastructureTestHost(ISystemManager systemManager)
		    : base(systemManager)
	    {
		}

        protected override async Task Started()
        {
            await base.Started();

            var system = Systems.Single();
            _infrastructure = system.Services.OfType<IInfrastructureService>().Select(service => service.Infrastructure).Single();
            _storage = system.Services.OfType<IStorageService>().Select(service => service.Storage).Single();
        }
    }
}
