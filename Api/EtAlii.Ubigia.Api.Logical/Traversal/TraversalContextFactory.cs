namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.xTechnology.MicroContainer;

    public class TraversalContextFactory : Factory<ITraversalContext>, ITraversalContextFactory
    {
        private readonly IFabricContext _fabricContext;

        public TraversalContextFactory(IFabricContext fabricContext)
        {
            _fabricContext = fabricContext;
        }

        protected override IScaffolding[] CreateScaffoldings()
        {
            return new IScaffolding[]
            {
                new TraversalContextScaffolding(_fabricContext),
            };
        }
    }
}