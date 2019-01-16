namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost.Grpc
{
	using System;
	using System.Linq;
    using EtAlii.Ubigia.Infrastructure.Functional;
	using EtAlii.Ubigia.Infrastructure.Transport;
	using EtAlii.Ubigia.Infrastructure.Transport.Admin.Grpc;
	using EtAlii.Ubigia.Infrastructure.Transport.User.Grpc;
	using EtAlii.Ubigia.Storage;
	using EtAlii.xTechnology.Hosting.TestHost.Grpc;
	using EtAlii.xTechnology.Hosting;

    public class InProcessInfrastructureTestHost : InProcessTestHost, IInfrastructureTestHost
    {
	    public IInfrastructure Infrastructure => _infrastructure.Value;
	    private readonly Lazy<IInfrastructure> _infrastructure;

	    public IStorage Storage => _storage.Value;
	    private readonly Lazy<IStorage> _storage;

		public AdminModule AdminModule => _adminModule.Value;
	    private readonly Lazy<AdminModule> _adminModule;
	    public UserModule UserModule => _userModule.Value;
	    private readonly Lazy<UserModule> _userModule;

		protected InProcessInfrastructureTestHost(ISystemManager systemManager)
		    : base(systemManager)
	    {
		    _infrastructure = new Lazy<IInfrastructure>(() => Systems.Single().Services.OfType<IInfrastructureService>().Select(service => service.Infrastructure).Single());
		    _storage = new Lazy<IStorage>(() => Systems.Single().Services.OfType<IStorageService>().Select(service => service.Storage).Single());
		    _adminModule = new Lazy<AdminModule>(() => Systems.Single().Modules.OfType<AdminModule>().Single());
		    _userModule = new Lazy<UserModule>(() => Systems.Single().Modules.OfType<UserModule>().Single());
		}
	}
}
