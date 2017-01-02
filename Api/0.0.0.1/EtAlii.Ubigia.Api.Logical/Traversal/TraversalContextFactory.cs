namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.xTechnology.MicroContainer;

    public class TraversalContextFactory : ITraversalContextFactory
    {
        private readonly IFabricContext _fabricContext;

        public TraversalContextFactory(IFabricContext fabricContext)
        {
            _fabricContext = fabricContext;
        }

        public ITraversalContext Create()
        {
            var container = new Container();

            var scaffoldings = new IScaffolding[]
            {
                new TraversalContextScaffolding(_fabricContext),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
            return container.GetInstance<ITraversalContext>();
        }
    }
}