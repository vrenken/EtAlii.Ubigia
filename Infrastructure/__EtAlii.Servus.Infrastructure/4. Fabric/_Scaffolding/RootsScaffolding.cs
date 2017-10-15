namespace EtAlii.Servus.Infrastructure.Fabric
{
    using EtAlii.xTechnology.MicroContainer;

    internal class RootsScaffolding : EtAlii.xTechnology.MicroContainer.IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IRootSet, RootSet>();

            container.Register<IRootGetter, RootGetter>();
            container.Register<IRootAdder, RootAdder>();
            container.Register<IRootRemover, RootRemover>();
            container.Register<IRootUpdater, RootUpdater>();
        }
    }
}