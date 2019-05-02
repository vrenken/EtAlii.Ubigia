namespace EtAlii.Ubigia.Api.Logical
{
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
            container.Register(() => _configuration);

            // TODO: Continuation of fabric generalisation. 
            //container.Register(() => new FabricContextFactory().Create(_configuration));
            container.Register(() => _configuration.Fabric);
            container.Register<ILogicalNodeSet, LogicalNodeSet>();
            container.Register<ILogicalRootSet, LogicalRootSet>();

            container.Register<IPropertiesManager, PropertiesManager>();
            container.Register<IPropertiesGetter, PropertiesGetter>();

            container.Register(() =>
            //{
                //var fabric = container.GetInstance<IFabricContext>();
                new ContentManagerFactory().Create(_configuration.Fabric)
            //}
            );
        }
    }
}