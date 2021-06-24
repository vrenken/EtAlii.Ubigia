// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.xTechnology.MicroContainer;

    public class GraphComposerFactory : IGraphComposerFactory
    {
        private readonly IGraphPathTraverser _graphPathTraverser;

        public GraphComposerFactory(IGraphPathTraverser graphPathTraverser)
        {
            _graphPathTraverser = graphPathTraverser;
        }

        public IGraphComposer Create(IFabricContext fabric)
        {
            var container = new Container();

            container.Register(() => fabric);
            container.Register<IGraphComposer, GraphComposer>();
            container.Register(() => _graphPathTraverser);

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

            return container.GetInstance<IGraphComposer>();
        }
    }
}
