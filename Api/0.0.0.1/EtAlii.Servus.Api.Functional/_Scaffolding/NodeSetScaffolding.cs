namespace EtAlii.Servus.Api.Functional
{
    using EtAlii.xTechnology.MicroContainer;

    internal class NodeSetScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<INodeSet, NodeSet>();
            container.Register<INodeQueryProviderFactory, NodeQueryProviderFactory>();
            container.Register<INodeQueryExecutorFactory, NodeQueryExecutorFactory>();

            container.Register<INodeReloadCommand, NodeReloadCommand>();
            container.Register<INodeSaveCommand, NodeSaveCommand>();
        }
    }
}
