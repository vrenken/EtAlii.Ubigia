﻿namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Diagnostics;
    using EtAlii.Ubigia.Infrastructure.Fabric;
    using EtAlii.Ubigia.Infrastructure.Fabric.Diagnostics;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Logical;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.Extensions.Configuration;

    public class InfrastructureService : ServiceBase<IHost, IInfrastructureSystem>, IInfrastructureService
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationDetails _configurationDetails;
        private readonly IServiceDetailsBuilder _serviceDetailsBuilder;

        public IInfrastructure Infrastructure { get; private set; }

        public InfrastructureService(
            IConfigurationSection configuration, 
            IConfigurationDetails configurationDetails,
            IServiceDetailsBuilder serviceDetailsBuilder) 
            : base(configuration)
        {
            _configuration = configuration;
            _configurationDetails = configurationDetails;
            _serviceDetailsBuilder = serviceDetailsBuilder;
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
        
        private IInfrastructure CreateInfrastructure()
        {
            var storage = System.Services.OfType<IStorageService>().Single().Storage;

            string name = _configuration.GetValue<string>(nameof(name));
            if (name == null)
            {
                throw new InvalidOperationException($"Unable to start service {nameof(InfrastructureService)}: {nameof(name)} not set in service configuration.");
            }

            var serviceDetails = _serviceDetailsBuilder.Build(_configurationDetails);
            

            // Fetch the Infrastructure configuration.
			var systemConnectionCreationProxy = new SystemConnectionCreationProxy();
            var infrastructureConfiguration = new InfrastructureConfiguration(systemConnectionCreationProxy)
                .Use(name, serviceDetails)
                .Use(DiagnosticsConfiguration.Default);

            // Create fabric instance.
            var fabricConfiguration = new FabricContextConfiguration()
                .Use(storage)
                .Use(DiagnosticsConfiguration.Default);
            var fabric = new FabricContextFactory().Create(fabricConfiguration);

            // TODO: This isn't right. We don't want give the logical context any address to store and distribute.
            var dataService = serviceDetails.FirstOrDefault(sd => !sd.IsSystemService) ?? serviceDetails.First();

            var dataAddress = dataService!.DataAddress;
            var storageAddress = new Uri($"{dataAddress.Scheme}://{dataAddress.Host}");
            
            // Create logical context instance.
            var logicalConfiguration = new LogicalContextConfiguration()
                .Use(fabric)
                .Use(infrastructureConfiguration.Name, storageAddress);
            var logicalContext = new LogicalContextFactory().Create(logicalConfiguration);

            // Create a Infrastructure instance.
            infrastructureConfiguration = infrastructureConfiguration
	            .Use<InfrastructureConfiguration, SystemConnectionInfrastructure>()
                .Use(logicalContext);
            return new InfrastructureFactory().Create(infrastructureConfiguration);
        }
    }
}
