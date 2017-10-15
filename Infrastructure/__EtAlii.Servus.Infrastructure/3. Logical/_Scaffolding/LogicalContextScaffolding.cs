namespace EtAlii.Servus.Infrastructure.Logical
{
    using EtAlii.Servus.Infrastructure.Fabric;
    using EtAlii.xTechnology.MicroContainer;

    internal class LogicalContextScaffolding : EtAlii.xTechnology.MicroContainer.IScaffolding
    {
        private readonly ILogicalContextConfiguration _configuration;

        public LogicalContextScaffolding(ILogicalContextConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            container.Register<ILogicalContext, LogicalContext>();
            container.RegisterInitializer<ILogicalContext>(context =>
            {
                context.Initialize(
                    container.GetInstance<ILogicalStorageSet>(),
                    container.GetInstance<ILogicalSpaceSet>(),
                    container.GetInstance<ILogicalAccountSet>(),
                    container.GetInstance<ILogicalRootSet>(),
                    container.GetInstance<ILogicalEntrySet>(),
                    container.GetInstance<ILogicalContentSet>(),
                    container.GetInstance<ILogicalContentDefinitionSet>(),
                    container.GetInstance<ILogicalPropertiesSet>(),
                    container.GetInstance<ILogicalIdentifierSet>());
            });

            container.Register<ILogicalContextConfiguration>(() => _configuration);
            container.Register<IFabricContext>(() => _configuration.Fabric);

            container.Register<ILogicalStorageSet, LogicalStorageSet>();
            container.Register<ILocalStorageGetter, LocalStorageGetter>();

            container.Register<ILogicalSpaceSet, LogicalSpaceSet>();
            container.Register<ISpaceInitializer, SpaceInitializer>();

            container.Register<ILogicalAccountSet, LogicalAccountSet>();
            container.Register<IAccountInitializer, AccountInitializer>();

            container.Register<ILogicalRootSet, LogicalRootSet>();
            container.Register<IRootInitializer, RootInitializer>();

            container.Register<ILogicalEntrySet, LogicalEntrySet>();
            container.Register<IEntryPreparer, EntryPreparer>();

            container.Register<ILogicalContentSet, LogicalContentSet>();
            container.Register<ILogicalContentDefinitionSet, LogicalContentDefinitionSet>();
            container.Register<ILogicalPropertiesSet, LogicalPropertiesSet>();
        }
    }
}