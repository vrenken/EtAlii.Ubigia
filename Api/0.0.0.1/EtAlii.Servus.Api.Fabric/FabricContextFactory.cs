namespace EtAlii.Servus.Api.Fabric
{
    using EtAlii.xTechnology.MicroContainer;

    public class FabricContextFactory 
    {
        public IFabricContext Create(IFabricContextConfiguration configuration)
        {
            var container = new Container();

            var scaffoldings = new IScaffolding[]
            {
                new ContextScaffolding(configuration.Connection),
                new EntryContextScaffolding(configuration.TraversalCachingEnabled),
                new ContentContextScaffolding(configuration.TraversalCachingEnabled),
                new PropertyContextScaffolding(configuration.TraversalCachingEnabled), 
                new RootContextScaffolding(),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }

            foreach (var extension in configuration.Extensions)
            {
                extension.Initialize(container);
            }

            return container.GetInstance<IFabricContext>();
        }

    }
}
