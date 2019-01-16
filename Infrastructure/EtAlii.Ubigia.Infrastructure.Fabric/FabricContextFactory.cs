namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using System;
    using EtAlii.xTechnology.MicroContainer;

    public class FabricContextFactory : IFabricContextFactory
    {
        public IFabricContext Create(IFabricContextConfiguration configuration)
        {
            if (configuration.Storage == null)
            {
                throw new NotSupportedException("A Storage is required to construct a FabricContext instance");
            }

            var container = new Container();

            var scaffoldings = new IScaffolding[]
            {
                new FabricContextScaffolding(configuration.Storage),
                new ItemsScaffolding(),
                new IdentifiersScaffolding(), 
                new ContentScaffolding(),
                new ContentDefinitionScaffolding(),
                new RootsScaffolding(),
                new PropertiesScaffolding(),
                new EntryScaffolding(),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }

            return container.GetInstance<IFabricContext>();
        }
    }
}