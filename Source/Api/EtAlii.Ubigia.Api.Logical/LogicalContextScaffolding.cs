// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical;

using EtAlii.Ubigia.Api.Fabric;
using EtAlii.xTechnology.MicroContainer;

internal class LogicalContextScaffolding : IScaffolding
{
    private readonly LogicalOptions _options;

    public LogicalContextScaffolding(LogicalOptions options)
    {
        _options = options;
    }

    public void Register(IRegisterOnlyContainer container)
    {
        container.Register<ILogicalContext>(serviceCollection =>
        {
            var nodes = serviceCollection.GetInstance<ILogicalNodeSet>();
            var roots = serviceCollection.GetInstance<ILogicalRootSet>();
            var content = serviceCollection.GetInstance<IContentManager>();
            var properties = serviceCollection.GetInstance<IPropertiesManager>();
            return new LogicalContext(_options, nodes, roots, content, properties);
        });
        container.Register(() => _options.ConfigurationRoot);
        container.Register(() => _options.FabricContext);
        container.Register<ILogicalNodeSet, LogicalNodeSet>();
        container.Register<ILogicalRootSet, LogicalRootSet>();

        container.Register<IPropertiesManager, PropertiesManager>();
        container.Register<IPropertiesGetter, PropertiesGetter>();

        container.Register(services =>
        {
            var fabric = services.GetInstance<IFabricContext>();
            return new ContentManagerFactory().Create(fabric);
        });

        // Traversal context
        container.Register<ITraversalContextEntrySet, TraversalContextEntrySet>();
        container.Register<ITraversalContextPropertySet, TraversalContextPropertySet>();
        container.Register<ITraversalContextRootSet, TraversalContextRootSet>();

        // Traversal
            container.Register<IGraphPathPartTraverserSelector, GraphPathPartTraverserSelector>();

        container.Register<IDepthFirstTraversalAlgorithm, ObservableTraversalAlgorithm>();
        container.Register<IBreadthFirstTraversalAlgorithm, ObservableTraversalAlgorithm>();

        //if [_useParallelization]
        //[
        //    // Parallel algorithms
        //    container.Register<IDepthFirstTraversalAlgorithm, ParallelDepthFirstTraversalAlgorithm>()
        //    container.Register<IBreadthFirstTraversalAlgorithm, ParallelBreadthFirstTraversalAlgorithm>()
        //]
        //else
        //[
        //    // Sequential algorithms
        //    container.Register<IDepthFirstTraversalAlgorithm, DepthFirstTraversalAlgorithm>()
        //    container.Register<IBreadthFirstTraversalAlgorithm, BreadthFirstTraversalAlgorithm>()
        //]
        container.Register<IGraphPathNodeTraverser, GraphPathNodeTraverser>();
        container.Register<IGraphPathIdentifiersStartNodeTraverser, GraphPathIdentifiersStartNodeTraverser>();
        container.Register<IGraphPathRootStartNodeTraverser, GraphPathRootStartNodeTraverser>();
        container.Register<IGraphPathAllChildrenRelationTraverser, GraphPathAllChildrenRelationTraverser>();
        container.Register<IGraphPathChildrenRelationTraverser, GraphPathChildrenRelationTraverser>();
        container.Register<IGraphPathAllParentsRelationTraverser, GraphPathAllParentsRelationTraverser>();
        container.Register<IGraphPathParentRelationTraverser, GraphPathParentRelationTraverser>();
        container.Register<IGraphPathAllNextRelationTraverser, GraphPathAllNextRelationTraverser>();
        container.Register<IGraphPathNextRelationTraverser, GraphPathNextRelationTraverser>();
        container.Register<IGraphPathAllPreviousRelationTraverser, GraphPathAllPreviousRelationTraverser>();
        container.Register<IGraphPathPreviousRelationTraverser, GraphPathPreviousRelationTraverser>();
        container.Register<IGraphPathAllUpdatesRelationTraverser, GraphPathAllUpdatesRelationTraverser>();
        container.Register<IGraphPathUpdatesRelationTraverser, GraphPathUpdatesRelationTraverser>();
        container.Register<IGraphPathAllDowndatesRelationTraverser, GraphPathAllDowndatesRelationTraverser>();
        container.Register<IGraphPathDowndateRelationTraverser, GraphPathDowndateRelationTraverser>();
        container.Register<IGraphPathFinalRelationTraverser, GraphPathFinalRelationTraverser>();
        container.Register<IGraphPathOriginalRelationTraverser, GraphPathOriginalRelationTraverser>();
        container.Register<IGraphPathTaggedNodeTraverser, GraphPathTaggedNodeTraverser>();
        container.Register<IGraphPathWildcardTraverser, GraphPathWildcardTraverser>();
        container.Register<ITraversingGraphPathWildcardTraverser, TraversingGraphPathWildcardTraverser>();
        container.Register<IGraphPathConditionalTraverser, GraphPathConditionalTraverser>();

        container.Register<ITemporalGraphPathWeaver, TemporalGraphPathWeaver>();

        container.Register<IPathTraversalContext, PathTraversalContext>();

        // Graphs
        container.Register<IGraphPathBuilder, GraphPathBuilder>();
        container.Register<IGraphComposerFactory, GraphComposerFactory>();
        container.Register<IGraphAssignerFactory, GraphAssignerFactory>();
        container.Register<IGraphPathTraverser, GraphPathTraverser>();
    }
}
