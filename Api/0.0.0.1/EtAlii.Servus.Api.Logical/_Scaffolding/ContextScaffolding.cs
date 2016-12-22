namespace EtAlii.Servus.Api.Logical
{
    using EtAlii.Servus.Api.Fabric;
    using EtAlii.xTechnology.MicroContainer;

    internal class ContextScaffolding : IScaffolding
    {
        private readonly ILogicalContextConfiguration _configuration;

        public ContextScaffolding(ILogicalContextConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            container.Register<ILogicalContext, LogicalContext>();
            container.Register<ILogicalContextConfiguration>(() => _configuration);

            container.Register<IFabricContext>(() => _configuration.Fabric);
            container.Register<ILogicalNodeSet, LogicalNodeSet>();
            container.Register<ILogicalRootSet, LogicalRootSet>();

            container.Register<IPropertiesManager, PropertiesManager>();
            container.Register<IPropertiesGetter, PropertiesGetter>();

            container.Register(() => new ContentManagerFactory().Create(_configuration.Fabric));
        }
    }
}