namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.xTechnology.MicroContainer;

    public class GraphComposerFactory : IGraphComposerFactory
    {
        private readonly IGraphPathTraverserFactory _graphPathTraverserFactory;

        public GraphComposerFactory(
            IGraphPathTraverserFactory graphPathTraverserFactory)
        {
            _graphPathTraverserFactory = graphPathTraverserFactory;
        }

        public IGraphComposer Create(IFabricContext fabric)
        {
            var container = new Container();

            container.Register<IFabricContext>(() => fabric);
            container.Register<IGraphComposer, GraphComposer>();
            container.Register<IGraphPathTraverserFactory>(() => _graphPathTraverserFactory);

            // Helpers:
            container.Register<IHierarchicalRelationDuplicator, HierarchicalRelationDuplicator>();

            container.Register<IGraphAdder, GraphAdder>();
            container.Register<IGraphRemover, GraphRemover>();
            container.Register<IGraphLinker, GraphLinker>();
            container.Register<IGraphUnlinker, GraphUnlinker>();

            container.Register<IGraphUpdater, GraphUpdater>();
            container.Register<IGraphChildAdder, GraphChildAdder>();
            container.Register<IGraphLinkAdder, GraphLinkAdder>();

            container.Register<IGraphRenamer, GraphRenamer>();

            container.Register<IComposeContext, ComposeContext>();

            return container.GetInstance<IGraphComposer>();
        }
    }
}
