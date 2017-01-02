namespace EtAlii.Servus.Api.Functional
{
    using EtAlii.xTechnology.MicroContainer;

    internal class IndexSetScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IIndexSet, IndexSet>();
        }
    }
}
