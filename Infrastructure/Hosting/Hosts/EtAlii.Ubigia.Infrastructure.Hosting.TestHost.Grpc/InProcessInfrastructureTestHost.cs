﻿namespace EtAlii.Ubigia.Infrastructure.Hosting.TestHost.Grpc
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using EtAlii.Ubigia.Infrastructure.Functional;
	using EtAlii.Ubigia.Infrastructure.Transport;
	using EtAlii.Ubigia.Infrastructure.Transport.Admin.Grpc;
	using EtAlii.Ubigia.Infrastructure.Transport.User.Grpc;
	using EtAlii.Ubigia.Storage;
	using EtAlii.xTechnology.Hosting;
	using EtAlii.xTechnology.Hosting.TestHost.Grpc;

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

	    static InProcessInfrastructureTestHost()
	    {
		    // See: https://docs.microsoft.com/en-us/aspnet/core/grpc/troubleshoot?view=aspnetcore-3.0#call-insecure-grpc-services-with-net-core-client
		    AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
	    }

		protected InProcessInfrastructureTestHost(ISystemManager systemManager)
		    : base(systemManager)
	    {
		}

        protected override async Task Started()
        {
            await base.Started();

            var system = Systems.Single();
            _infrastructure = system.Services.OfType<IInfrastructureService>().Select(service => service.Infrastructure).Single();
            _storage = system.Services.OfType<IStorageService>().Select(service => service.Storage).Single();
            _adminModule = system.Modules.OfType<AdminModule>().Single();
            _userModule = system.Modules.OfType<UserModule>().Single();
        }
    }
}
