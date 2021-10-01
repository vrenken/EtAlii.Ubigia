// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost
{
    using System.Linq;
    using System.Threading.Tasks;
	using EtAlii.Ubigia.Infrastructure.Functional;
	using EtAlii.Ubigia.Infrastructure.Transport;
	using EtAlii.Ubigia.Persistence;
	using EtAlii.xTechnology.Hosting;

    /// <summary>
    /// This is the base class for all infrastructure test hosts.
    /// It provides the host service factory that is able to understand which.
    /// different services can be hosted within an infrastructure host.
    /// </summary>
	public class InfrastructureTestHost : TestHost, IInfrastructureTestHost
    {
	    public IInfrastructure Infrastructure => _infrastructure;
	    private IInfrastructure _infrastructure;

	    public IStorage Storage => _storage;
	    private IStorage _storage;

		public InfrastructureTestHost(IHostOptions options)
		    : base(options, new InfrastructureHostServicesFactory())
	    {
		}

        protected override async Task Started()
        {
            await base.Started().ConfigureAwait(false);

            _infrastructure = Services.OfType<IInfrastructureService>().Single().Infrastructure;
            _storage = Services.OfType<IStorageService>().Single().Storage;
        }
    }
}
