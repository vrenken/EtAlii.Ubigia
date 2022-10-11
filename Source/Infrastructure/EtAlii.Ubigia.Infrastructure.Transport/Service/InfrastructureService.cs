// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using System;
    using System.Diagnostics.CodeAnalysis;
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
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using IServiceCollection = Microsoft.Extensions.DependencyInjection.IServiceCollection;

    public class InfrastructureService : IInfrastructureService
    {
        /// <inheritdoc />
        public ServiceConfiguration Configuration { get; }

        /// <inheritdoc />
        public IFunctionalContext Functional { get; private set; }

        private IStorageService _storageService;

        private INetworkService[] _networkServices;

        public InfrastructureService(ServiceConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Functional = CreateFunctionalContext(_storageService.Storage);
            // TODO: We should work with the cancellationToken somehow.
            await Functional.Start().ConfigureAwait(false);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Functional.Stop().ConfigureAwait(false);
            Functional = null;
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

        [SuppressMessage(
            category: "Sonar Code Smell",
            checkId: "S5332:Using http protocol is insecure. Use https instead",
            Justification = "Safe to do so: The related code only gets run during unit test execution.")]
        [SuppressMessage(
            category: "Sonar Code Smell",
            checkId: "S1075:URIs should not be hardcoded",
            Justification = "The hard coded URIs direct to nowhere and are only used during unit test execution.")]
        private IFunctionalContext CreateFunctionalContext(IStorage storage)
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

            // This should only happen while running unit tests, so maybe this is a smell that the service details should be stored somewhere else?
            if (!allServiceDetails.Any())
            {
                allServiceDetails = new[] { new ServiceDetails("None", new Uri("http://none"), new Uri("http://none"), new Uri("http://none")) };
            }

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
                .UseLogicalDiagnostics();
            var logicalContext = new LogicalContextFactory().Create(logicalContextOptions);

            // Create a Infrastructure instance.
            var systemConnectionCreationProxy = new SystemConnectionCreationProxy();
            var infrastructureOptions = new FunctionalContextOptions(Configuration.Root, systemConnectionCreationProxy)
                .Use(name, allServiceDetails)
                .Use(logicalContext)
                .UseFunctionalDiagnostics();

            return Factory.Create<IFunctionalContext>(infrastructureOptions);
        }
    }
}
