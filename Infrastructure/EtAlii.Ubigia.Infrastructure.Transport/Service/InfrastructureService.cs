namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Infrastructure.Fabric;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Logical;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.Extensions.Configuration;

    public class InfrastructureService : ServiceBase<IHost, IInfrastructureSystem>, IInfrastructureService
    {
        private readonly IConfiguration _configuration;
        public IInfrastructure Infrastructure { get; private set; }
        private IInfrastructureSystem _system;

        public InfrastructureService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override void Start()
        {
            Infrastructure = CreateInfrastructure();
            Infrastructure.Start();
        }

        public override void Stop()
        {
            Infrastructure.Stop();
        }

        protected override void Initialize(
	        IHost host, IInfrastructureSystem system, 
            IModule[] moduleChain, out Status status)
        {
            _system = system;
            status = new Status(nameof(InfrastructureService));
        }
        private IInfrastructure CreateInfrastructure()
        {
            var storage = _system.Services.OfType<IStorageService>().Single().Storage;

            string name;
            name = _configuration.GetValue<string>(nameof(name));
            if (name == null)
            {
                throw new InvalidOperationException($"Unable to start service {nameof(InfrastructureService)}: {nameof(name)} not set in service configuration.");
            }

            string address;
            address = _configuration.GetValue<string>(nameof(address));
	        if (address == null)
	        {
		        throw new InvalidOperationException($"Unable to start service {nameof(InfrastructureService)}: {nameof(address)} not set in service configuration.");
	        }
	        if (!Uri.IsWellFormedUriString(address, UriKind.Absolute))
	        {
		        throw new InvalidOperationException($"Unable to start service {nameof(InfrastructureService)}: no valid {nameof(address)} set in service configuration.");
	        }

			// Fetch the Infrastructure configuration.
			var systemConnectionCreationProxy = new SystemConnectionCreationProxy();
            var infrastructureConfiguration = new InfrastructureConfiguration(systemConnectionCreationProxy)
                .Use(name, new Uri(address, UriKind.Absolute));

            // Create fabric instance.
            var fabricConfiguration = new FabricContextConfiguration()
                .Use(storage);
            var fabric = new FabricContextFactory().Create(fabricConfiguration);

            // Create logical context instance.
            var logicalConfiguration = new LogicalContextConfiguration()
                .Use(fabric)
                .Use(infrastructureConfiguration.Name, infrastructureConfiguration.Address);
            var logicalContext = new LogicalContextFactory().Create(logicalConfiguration);

            // Create a Infrastructure instance.
            infrastructureConfiguration = infrastructureConfiguration
	            .Use<InfrastructureConfiguration, SystemConnectionInfrastructure>()
                .Use(logicalContext);
            return new InfrastructureFactory().Create(infrastructureConfiguration);
        }
    }
}
