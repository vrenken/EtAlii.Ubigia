namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Fabric;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Logical;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.Extensions.Configuration;

    public class InfrastructureService : ServiceBase<IHost, IInfrastructureSystem>, IInfrastructureService
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationDetails _configurationDetails;

        public IInfrastructure Infrastructure { get; private set; }
        // private IInfrastructureSystem _system;

        public InfrastructureService(IConfigurationSection configuration, IConfigurationDetails configurationDetails) 
            : base(configuration)
        {
            _configuration = configuration;
            _configurationDetails = configurationDetails;
        }

        public override async Task Start()
        {
            Infrastructure = CreateInfrastructure();
            await Infrastructure.Start();
        }

        public override async Task Stop()
        {
            await Infrastructure.Stop();
        }

        // protected override Task Initialize(
	       //  IHost host, IInfrastructureSystem system, 
        //     IModule[] moduleChain, out Status status)
        // {
        //     _system = system;
        //     status = new Status(nameof(InfrastructureService));
        //     return Task.CompletedTask;
        // }
        
        private IInfrastructure CreateInfrastructure()
        {
            var storage = System.Services.OfType<IStorageService>().Single().Storage;

            string name;
            name = _configuration.GetValue<string>(nameof(name));
            if (name == null)
            {
                throw new InvalidOperationException($"Unable to start service {nameof(InfrastructureService)}: {nameof(name)} not set in service configuration.");
            }
            // TODO: Ugly. This needs to change and not be needed at all.
            var dataAddressBuilder = new StringBuilder();
            dataAddressBuilder.Append($"http://{_configurationDetails.Hosts["UserHost"]}:{_configurationDetails.Ports["UserPort"]}");
            dataAddressBuilder.Append(_configurationDetails.Paths["UserApi"]);
            if (_configurationDetails.Paths.TryGetValue("UserApiRest", out var userApiRest)) dataAddressBuilder.Append(userApiRest);
            var dataAddress = dataAddressBuilder.ToString();

            var managementAddressBuilder = new StringBuilder();
            managementAddressBuilder.Append($"http://{_configurationDetails.Hosts["AdminHost"]}:{_configurationDetails.Ports["AdminPort"]}");
            managementAddressBuilder.Append(_configurationDetails.Paths["AdminApi"]);
            if (_configurationDetails.Paths.TryGetValue("AdminApiRest", out var adminApiRest)) managementAddressBuilder.Append(adminApiRest);
            var managementAddress = managementAddressBuilder.ToString();

            if (dataAddress == null)
            {
                throw new InvalidOperationException($"Unable to start service {nameof(InfrastructureService)}: {nameof(dataAddress)} cannot be build from configuration.");
            }
            if (!Uri.IsWellFormedUriString(dataAddress, UriKind.Absolute))
            {
                throw new InvalidOperationException($"Unable to start service {nameof(InfrastructureService)}: no valid {nameof(dataAddress)} can be build from configuration.");
            }

            if (managementAddress == null)
            {
                throw new InvalidOperationException($"Unable to start service {nameof(InfrastructureService)}: {nameof(managementAddress)} cannot be build from configuration.");
            }
            if (!Uri.IsWellFormedUriString(managementAddress, UriKind.Absolute))
            {
                throw new InvalidOperationException($"Unable to start service {nameof(InfrastructureService)}: no valid {nameof(managementAddress)} can be build from configuration.");
            }

            // Fetch the Infrastructure configuration.
			var systemConnectionCreationProxy = new SystemConnectionCreationProxy();
            var infrastructureConfiguration = new InfrastructureConfiguration(systemConnectionCreationProxy)
                .Use(name, new Uri(managementAddress, UriKind.Absolute), new Uri(dataAddress, UriKind.Absolute));

            // Create fabric instance.
            var fabricConfiguration = new FabricContextConfiguration()
                .Use(storage);
            var fabric = new FabricContextFactory().Create(fabricConfiguration);

            // Create logical context instance.
            var logicalConfiguration = new LogicalContextConfiguration()
                .Use(fabric)
                .Use(infrastructureConfiguration.Name, infrastructureConfiguration.DataAddress);
            var logicalContext = new LogicalContextFactory().Create(logicalConfiguration);

            // Create a Infrastructure instance.
            infrastructureConfiguration = infrastructureConfiguration
	            .Use<InfrastructureConfiguration, SystemConnectionInfrastructure>()
                .Use(logicalContext);
            return new InfrastructureFactory().Create(infrastructureConfiguration);
        }
    }
}
