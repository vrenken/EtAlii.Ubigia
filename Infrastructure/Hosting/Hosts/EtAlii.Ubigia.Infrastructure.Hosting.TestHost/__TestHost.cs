//namespace EtAlii.Ubigia.Infrastructure.Hosting
//[
//	using System
//    using System.Linq
//    using EtAlii.Ubigia.Api.Transport.WebApi
//    using EtAlii.Ubigia.Infrastructure.Functional
//	using EtAlii.Ubigia.Infrastructure.Transport
//	using EtAlii.Ubigia.Infrastructure.Transport.Admin.AspNetCore
//	using EtAlii.Ubigia.Infrastructure.Transport.User.AspNetCore
//	using EtAlii.Ubigia.Storage
//	using Microsoft.AspNetCore.Builder
//	using Microsoft.AspNetCore.Hosting
//	using xTechnology.Hosting

//    public class TestHost : EtAlii.xTechnology.Hosting.HostBase, IAspNetCoreHost
//    [
//		private IAspNetCoreHostManager _manager

//		public event Action<IApplicationBuilder> ConfigureApplication
//		public event Action<IWebHostBuilder> ConfigureWebHost

//		private readonly IHostConfiguration _configuration

//		public IInfrastructure Infrastructure => _infrastructure.Value
//	    private readonly Lazy<IInfrastructure> _infrastructure

//	    public IStorage Storage => _storage.Value
//	    private readonly Lazy<IStorage> _storage

//		public IAddressFactory AddressFactory { get; } = new AddressFactory()
//		public IInfrastructureClient Client { get; }

//	    public AdminModule AdminModule => _adminModule.Value
//	    private readonly Lazy<AdminModule> _adminModule
//	    public UserModule UserModule => _userModule.Value
//	    private readonly Lazy<UserModule> _userModule

//		protected TestHost(
//		    IHostConfiguration configuration,
//		    ISystemManager systemManager)
//		    : base(systemManager)
//	    [
//			_configuration = configuration
//			_infrastructure = new Lazy<IInfrastructure>(() => this.Systems.Single().Services.OfType<IInfrastructureService>().Select(service => service.Infrastructure).Single())
//		    _storage = new Lazy<IStorage>(() => this.Systems.Single().Services.OfType<IStorageService>().Select(service => service.Storage).Single())
//		    _adminModule = new Lazy<AdminModule>(() => this.Systems.Single().Modules.OfType<AdminModule>().Single())
//		    _userModule = new Lazy<UserModule>(() => this.Systems.Single().Modules.OfType<UserModule>().Single())
//		]
//		public override void Initialize(ICommand[] commands, Status[] status)
//		[
//			_manager = new AspNetCoreHostManager()
//			_manager.Initialize(ref commands, ref status, this)
//			_manager.ConfigureApplication += builder => ConfigureApplication?.Invoke(builder)
//			_manager.ConfigureWebHost += builder => ConfigureWebHost?.Invoke(builder)

//			base.Initialize(commands, status)
//		]
//		protected override void Starting()
//		[
//			_manager.Starting()
//		]
//		protected override async void Started()
//		[
//			await _manager.Started()
//		]
//		protected override void Stopping()
//		[
//			_manager.Stopping()
//		]
//		protected override async void Stopped()
//		[
//			await _manager.Stopped()
//		]
//	]
//]