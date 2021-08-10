// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Diagnostics;
    using EtAlii.Ubigia.Infrastructure.Fabric;
    using EtAlii.Ubigia.Infrastructure.Fabric.Diagnostics;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Logical;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.Extensions.Configuration;

    public class InfrastructureService : ServiceBase<IHost, IInfrastructureSystem>, IInfrastructureService
    {
        private readonly IConfigurationRoot _configurationRoot;
        private readonly IConfiguration _configuration;
        private readonly IConfigurationDetails _configurationDetails;
        private readonly IServiceDetailsBuilder _serviceDetailsBuilder;

        /// <inheritdoc />
        public IInfrastructure Infrastructure { get; private set; }

        public InfrastructureService(
            IConfigurationRoot configurationRoot,
            IConfigurationSection configuration,
            IConfigurationDetails configurationDetails,
            IServiceDetailsBuilder serviceDetailsBuilder)
            : base(configuration)
        {
            _configurationRoot = configurationRoot;
            _configuration = configuration;
            _configurationDetails = configurationDetails;
            _serviceDetailsBuilder = serviceDetailsBuilder;
        }

        /// <inheritdoc />
        public override async Task Start()
        {
            Infrastructure = CreateInfrastructure();
            await Infrastructure.Start().ConfigureAwait(false);
        }

        /// <inheritdoc />
        public override async Task Stop()
        {
            await Infrastructure.Stop().ConfigureAwait(false);
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

            // Create fabric instance.
            var fabricContextOptions = new FabricContextOptions(_configurationRoot)
                .Use(storage)
                .UseFabricDiagnostics();
            var fabric = new FabricContextFactory().Create(fabricContextOptions);

            // Improve the InfrastructureService.
            // This current approach isn't right. We don't want to give the logical context any address to store and distribute.
            // More information can be found on the Github issue below:
            // https://github.com/vrenken/EtAlii.Ubigia/issues/97
            var dataService = serviceDetails.FirstOrDefault(sd => !sd.IsSystemService) ?? serviceDetails.First();

            var dataAddress = dataService!.DataAddress;
            var storageAddress = new Uri($"{dataAddress.Scheme}://{dataAddress.Host}");

            // Create logical context instance.
            var logicalContextOptions = new LogicalContextOptions(_configurationRoot)
                .Use(fabric)
                .Use(name, storageAddress)
                .UseLogicalContextDiagnostics();
            var logicalContext = new LogicalContextFactory().Create(logicalContextOptions);

            // Create a Infrastructure instance.
            var systemConnectionCreationProxy = new SystemConnectionCreationProxy();
            var infrastructureOptions = new InfrastructureOptions(_configurationRoot, systemConnectionCreationProxy)
                .Use(name, serviceDetails)
	            .Use<InfrastructureOptions, SystemConnectionInfrastructure>()
                .Use(logicalContext)
                .UseInfrastructureDiagnostics();

            return new InfrastructureFactory().Create(infrastructureOptions);
        }
    }
}
