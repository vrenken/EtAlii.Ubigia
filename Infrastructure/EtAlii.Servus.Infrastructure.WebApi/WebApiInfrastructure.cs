namespace EtAlii.Servus.Infrastructure.WebApi
{
    using System.Linq;
    using EtAlii.Servus.Infrastructure.Functional;
    using EtAlii.Servus.Infrastructure.Logical;
    using EtAlii.Servus.Infrastructure.Transport;
    using SimpleInjector;

    public class WebApiInfrastructure : InfrastructureBase
    {
        private readonly Container _container;
        private IComponentManager[] _componentManagers;
        private readonly IApplicationManager _applicationManager;

        public WebApiInfrastructure(
            IApplicationManager applicationManager,
            Container container,
            IInfrastructureConfiguration configuration,
            ISpaceRepository spaces,
            IIdentifierRepository identifiers,
            IEntryRepository entries,
            IRootRepository roots,
            IAccountRepository accounts,
            IContentRepository content,
            IContentDefinitionRepository contentDefinition,
            IPropertiesRepository properties,
            IStorageRepository storages,
            ILogicalContext logicalContext)
            : base(configuration, spaces, identifiers, entries, roots, accounts, content, contentDefinition, properties, storages, logicalContext)
        {
            _applicationManager = applicationManager;
            _container = container;
        }

        public override void Start()
        {
            // This action is needed because the Logical layer needs a fully functional system connection to do 
            // the initialization of the storage and spaces.
            // The functional is the only one that can provide these kind of connections.
            Configuration.SystemConnectionCreationProxy.Initialize(() =>
            {
                var configuration = new SystemConnectionConfiguration()
                    .Use(SystemTransportProvider.Create(this))
                    .Use(this);
                return new SystemConnectionFactory().Create(configuration);
            });

            base.Start();

            _componentManagers = Configuration.ComponentManagerTypes
                .Select(componentManagerType => _container.GetInstance(componentManagerType))
                .Cast<IComponentManager>()
                .ToArray();

            _applicationManager.Start(_componentManagers);
        }

        public override void Stop()
        {
            _applicationManager.Stop(_componentManagers);
            _componentManagers = null;

            base.Stop();
        }
    }
}