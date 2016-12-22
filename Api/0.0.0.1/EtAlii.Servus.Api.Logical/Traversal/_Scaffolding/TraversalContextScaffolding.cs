namespace EtAlii.Servus.Api.Logical
{
    using EtAlii.Servus.Api.Fabric;
    using EtAlii.xTechnology.MicroContainer;

    internal class TraversalContextScaffolding : IScaffolding
    {
        private readonly IFabricContext _fabricContext;

        public TraversalContextScaffolding(IFabricContext fabricContext)
        {
            _fabricContext = fabricContext;
        }

        public void Register(Container container)
        {
            container.Register<IFabricContext>(() => _fabricContext);

            container.Register<ITraversalContextEntrySet, TraversalContextEntrySet>();
            container.Register<ITraversalContextPropertySet, TraversalContextPropertySet>();
            container.Register<ITraversalContextRootSet, TraversalContextRootSet>();

            container.Register<ITraversalContext, TraversalContext>();
        }
    }
}