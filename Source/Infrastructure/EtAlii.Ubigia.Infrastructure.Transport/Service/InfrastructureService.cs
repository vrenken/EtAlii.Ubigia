// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Infrastructure.Diagnostics;
    using EtAlii.Ubigia.Infrastructure.Fabric;
    using EtAlii.Ubigia.Infrastructure.Fabric.Diagnostics;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Logical;
    using EtAlii.Ubigia.Persistence;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using IServiceCollection = Microsoft.Extensions.DependencyInjection.IServiceCollection;

    public class InfrastructureService : IInfrastructureService
    {
        /// <inheritdoc />
        public ServiceConfiguration Configuration { get; }

        /// <inheritdoc />
        public IInfrastructure Infrastructure { get; private set; }

        private IStorageService _storageService;

        private INetworkService[] _networkServices;

        public InfrastructureService(ServiceConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Infrastructure = CreateInfrastructure(_storageService.Storage);
            // TODO: We should work with the cancellationToken somehow.
            await Infrastructure.Start().ConfigureAwait(false);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Infrastructure.Stop().ConfigureAwait(false);
            Infrastructure = null;
        }

        public void ConfigureServices(IServiceCollection serviceCollection, IService[] services)
        {
            _storageService = services.OfType<IStorageService>().Single();

            _networkServices = services
                .OfType<INetworkService>()
                .ToArray();
            serviceCollection.AddSingleton<IInfrastructureService>(this);
            serviceCollection.AddHostedService(_ => this);
        }

        private IInfrastructure CreateInfrastructure(IStorage storage)
        {
            string name = Configuration.Section.GetValue<string>(nameof(name));
            if (name == null)
            {
                throw new InvalidOperationException($"Unable to start service {nameof(InfrastructureService)}: {nameof(name)} not set in service configuration.");
            }

            var serviceDetailsBuilder = new ServiceDetailsBuilder();
            var allServiceDetails = serviceDetailsBuilder.Build(_networkServices);

            // Create fabric instance.
            var fabricContextOptions = new FabricContextOptions(Configuration.Root)
                .Use(storage)
                .UseFabricDiagnostics();
            var fabric = new FabricContextFactory().Create(fabricContextOptions);

            // By convention the first data/management API's will be used for the storageAddress.
            var serviceDetails = allServiceDetails.First();

            // Improve the InfrastructureService.
            // This current approach isn't right. We don't want to give the logical context any address to store and distribute.
            // More information can be found on the Github issue below:
            // https://github.com/vrenken/EtAlii.Ubigia/issues/97

            // Create logical context instance.
            var logicalContextOptions = new LogicalContextOptions(Configuration.Root)
                .Use(fabric)
                .Use(name, serviceDetails.StorageAddress)
                .UseLogicalContextDiagnostics();
            var logicalContext = new LogicalContextFactory().Create(logicalContextOptions);

            // Create a Infrastructure instance.
            var systemConnectionCreationProxy = new SystemConnectionCreationProxy();
            var infrastructureOptions = new InfrastructureOptions(Configuration.Root, systemConnectionCreationProxy)
                .Use(name, allServiceDetails)
	            .Use<InfrastructureOptions, SystemConnectionInfrastructure>()
                .Use(logicalContext)
                .UseInfrastructureDiagnostics();

            return new InfrastructureFactory().Create(infrastructureOptions);
        }
    }
}
