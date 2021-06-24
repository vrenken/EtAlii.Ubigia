// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost.Rest
{
	using System.Linq;
	using System.Threading.Tasks;
	using EtAlii.Ubigia.Infrastructure.Functional;
	using EtAlii.Ubigia.Infrastructure.Transport;
	using EtAlii.Ubigia.Persistence;
	using EtAlii.xTechnology.Hosting;

	public class InfrastructureTestHost : TestHost, IInfrastructureTestHost
    {
	    public IInfrastructure Infrastructure => _infrastructure;
	    private IInfrastructure _infrastructure;

	    public IStorage Storage => _storage;
	    private IStorage _storage;

		public InfrastructureTestHost(IHostConfiguration configuration, ISystemManager systemManager)
		    : base(configuration, systemManager)
	    {
		}

        protected override async Task Started()
        {
            await base.Started().ConfigureAwait(false);

            _infrastructure = Systems.Single().Services.OfType<IInfrastructureService>().Select(service => service.Infrastructure).Single();
            _storage = Systems.Single().Services.OfType<IStorageService>().Select(service => service.Storage).Single();
        }
    }
}
