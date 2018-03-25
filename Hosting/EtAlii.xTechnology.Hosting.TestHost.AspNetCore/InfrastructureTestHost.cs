namespace EtAlii.Ubigia.Infrastructure.Hosting
{
	using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Transport.WebApi;
    using EtAlii.Ubigia.Infrastructure.Functional;
	using EtAlii.Ubigia.Infrastructure.Transport;
	using EtAlii.Ubigia.Infrastructure.Transport.Admin.AspNetCore;
	using EtAlii.Ubigia.Infrastructure.Transport.User.AspNetCore;
	using EtAlii.Ubigia.Storage;
	using xTechnology.Hosting;

    public class InfrastructureTestHost : EtAlii.xTechnology.Hosting.TestHost, IAspNetCoreHost
    {
	    public IInfrastructure Infrastructure => _infrastructure.Value;
	    private readonly Lazy<IInfrastructure> _infrastructure;

	    public IStorage Storage => _storage.Value;
	    private readonly Lazy<IStorage> _storage;

		public IAddressFactory AddressFactory { get; } = new AddressFactory();
		public IInfrastructureClient Client { get; }

	    public AdminModule AdminModule => _adminModule.Value;
	    private readonly Lazy<AdminModule> _adminModule;
	    public UserModule UserModule => _userModule.Value;
	    private readonly Lazy<UserModule> _userModule;

		protected InfrastructureTestHost(
		    IHostConfiguration configuration,
		    ISystemManager systemManager)
		    : base(configuration, systemManager)
	    {
		    _infrastructure = new Lazy<IInfrastructure>(() => this.Systems.Single().Services.OfType<IInfrastructureService>().Select(service => service.Infrastructure).Single());
		    _storage = new Lazy<IStorage>(() => this.Systems.Single().Services.OfType<IStorageService>().Select(service => service.Storage).Single());
		    _adminModule = new Lazy<AdminModule>(() => this.Systems.Single().Modules.OfType<AdminModule>().Single());
		    _userModule = new Lazy<UserModule>(() => this.Systems.Single().Modules.OfType<UserModule>().Single());
		}
	}
}
