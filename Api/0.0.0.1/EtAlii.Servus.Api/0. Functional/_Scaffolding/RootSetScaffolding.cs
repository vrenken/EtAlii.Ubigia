namespace EtAlii.Servus.Api.Functional
{
    using EtAlii.xTechnology.MicroContainer;

    internal class RootSetScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IRootQueryProviderFactory, RootQueryProviderFactory>();
            container.Register<IRootQueryExecutorFactory, RootQueryExecutorFactory>();
        }
    }
}
