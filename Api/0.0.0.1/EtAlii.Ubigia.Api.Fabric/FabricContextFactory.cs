namespace EtAlii.Ubigia.Api.Fabric
{
    using EtAlii.xTechnology.MicroContainer;

    public class FabricContextFactory : Factory<IFabricContext, FabricContextConfiguration, IFabricContextExtension>
    {
        protected override IScaffolding[] CreateScaffoldings(FabricContextConfiguration configuration)
        {
            return new IScaffolding[]
            {
                new ContextScaffolding(configuration.Connection),
                new EntryContextScaffolding(configuration.TraversalCachingEnabled),
                new ContentContextScaffolding(configuration.TraversalCachingEnabled),
                new PropertyContextScaffolding(configuration.TraversalCachingEnabled), 
                new RootContextScaffolding(),
            };
        }
    }
}
