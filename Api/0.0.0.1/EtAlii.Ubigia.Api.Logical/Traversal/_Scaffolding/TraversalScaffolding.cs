namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.xTechnology.MicroContainer;

    internal class TraversalScaffolding : IScaffolding
    {
        private readonly IGraphPathTraverserConfiguration _configuration;
        //private readonly bool _useParallelization = false;

        public TraversalScaffolding(IGraphPathTraverserConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Register(Container container)
        {
            container.Register(() => _configuration.FabricContext);
            container.Register<IGraphPathTraverser, GraphPathTraverser>();
            container.Register<IGraphPathPartTraverserSelector, GraphPathPartTraverserSelector>();

            
            container.Register<IDepthFirstTraversalAlgorithm, ObservableTraversalAlgorithm>();
            container.Register<IBreadthFirstTraversalAlgorithm, ObservableTraversalAlgorithm>();

            //if (_useParallelization)
            //{
            //    // Parallel algorithms
            //    container.Register<IDepthFirstTraversalAlgorithm, ParallelDepthFirstTraversalAlgorithm>();
            //    container.Register<IBreadthFirstTraversalAlgorithm, ParallelBreadthFirstTraversalAlgorithm>();
            //}
            //else
            //{
            //    // Sequential algorithms
            //    container.Register<IDepthFirstTraversalAlgorithm, DepthFirstTraversalAlgorithm>();
            //    container.Register<IBreadthFirstTraversalAlgorithm, BreadthFirstTraversalAlgorithm>();
            //}

            container.Register<IGraphPathNodeTraverser, GraphPathNodeTraverser>();
            container.Register<IGraphPathIdentifiersStartNodeTraverser, GraphPathIdentifiersStartNodeTraverser>();
            container.Register<IGraphPathRootStartNodeTraverser, GraphPathRootStartNodeTraverser>();
            container.Register<IGraphPathChildRelationTraverser, GraphPathChildRelationTraverser>();
            container.Register<IGraphPathParentRelationTraverser, GraphPathParentRelationTraverser>();
            container.Register<IGraphPathNextRelationTraverser, GraphPathNextRelationTraverser>();
            container.Register<IGraphPathPreviousRelationTraverser, GraphPathPreviousRelationTraverser>();
            container.Register<IGraphPathUpdateRelationTraverser, GraphPathUpdateRelationTraverser>();
            container.Register<IGraphPathDowndateRelationTraverser, GraphPathDowndateRelationTraverser>();
            container.Register<IGraphPathFinalRelationTraverser, GraphPathFinalRelationTraverser>();
            container.Register<IGraphPathOriginalRelationTraverser, GraphPathOriginalRelationTraverser>();
            container.Register<IGraphPathWildcardTraverser, GraphPathWildcardTraverser>();
            container.Register<ITraversingGraphPathWildcardTraverser, TraversingGraphPathWildcardTraverser>();
            container.Register<IGraphPathConditionalTraverser, GraphPathConditionalTraverser>();

            container.Register<ITemporalGraphPathWeaver, TemporalGraphPathWeaver>();

            container.Register<ITraversalContextFactory, TraversalContextFactory>();
        }
    }
}