namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost.NetCore
{
	using System.Linq;
	using System.Threading.Tasks;
	using EtAlii.Ubigia.Infrastructure.Functional;
	using EtAlii.Ubigia.Infrastructure.Transport;
	using EtAlii.Ubigia.Infrastructure.Transport.Admin.NetCore;
	using EtAlii.Ubigia.Infrastructure.Transport.User.NetCore;
	using EtAlii.Ubigia.Storage;
	using EtAlii.xTechnology.Hosting;
	using EtAlii.xTechnology.Hosting.TestHost.NetCore;

	public class InProcessInfrastructureTestHost : InProcessTestHost, IInfrastructureTestHost
    {
	    public IInfrastructure Infrastructure => _infrastructure;
	    private IInfrastructure _infrastructure;

	    public IStorage Storage => _storage;
	    private IStorage _storage;

		public AdminModule AdminModule => _adminModule;
	    private AdminModule _adminModule;
	    public UserModule UserModule => _userModule;
	    private UserModule _userModule;

		protected InProcessInfrastructureTestHost(ISystemManager systemManager)
		    : base(systemManager)
	    {
		}

        protected override async Task Started()
        {
            await base.Started();

            _infrastructure = Systems.Single().Services.OfType<IInfrastructureService>().Select(service => service.Infrastructure).Single();
            _storage = Systems.Single().Services.OfType<IStorageService>().Select(service => service.Storage).Single();
            _adminModule = Systems.Single().Modules.OfType<AdminModule>().Single();
            _userModule = Systems.Single().Modules.OfType<UserModule>().Single();
        }
    }
}
