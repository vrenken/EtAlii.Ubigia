// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.xTechnology.MicroContainer;

    internal class TraversalScaffolding : IScaffolding
    {
        private readonly IGraphPathTraverserConfiguration _configuration;
        //private readonly bool _useParallelization = false

        public TraversalScaffolding(IGraphPathTraverserConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            container.Register(() => _configuration.FabricContext);
            container.Register(() => _configuration.ConfigurationRoot);
            container.Register<IGraphPathTraverser, GraphPathTraverser>();
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

            container.Register<IPathTraversalContextFactory, PathTraversalContextFactory>();
        }
    }
}
