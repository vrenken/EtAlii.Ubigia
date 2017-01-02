namespace EtAlii.Ubigia.Api.Diagnostics.Profiling
{
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.xTechnology.MicroContainer;

    public class ProfilingGraphPathTraverserExtension : IGraphPathTraverserExtension
    {
        private readonly IProfiler _profiler;

        public ProfilingGraphPathTraverserExtension(IProfiler profiler)
        {
            _profiler = profiler;
        }

        public void Initialize(Container container)
        {
            container.RegisterDecorator(typeof(IGraphPathTraverser), typeof(ProfilingGraphPathTraverser));
            container.Register<IProfiler>(() => new Profiler(_profiler, ProfilingAspects.Logical.Traversal));

            container.RegisterDecorator(typeof(ITemporalGraphPathWeaver), typeof(ProfilingTemporalGraphPathWeaver));

            container.RegisterDecorator(typeof(IGraphPathNodeTraverser), typeof(ProfilingGraphPathNodeTraverser));
            container.RegisterDecorator(typeof(IGraphPathIdentifiersStartNodeTraverser), typeof(ProfilingGraphPathIdentifiersStartNodeTraverser));
            container.RegisterDecorator(typeof(IGraphPathRootStartNodeTraverser), typeof(ProfilingGraphPathRootStartNodeTraverser));
            container.RegisterDecorator(typeof(IGraphPathChildRelationTraverser), typeof(ProfilingGraphPathChildRelationTraverser));
            container.RegisterDecorator(typeof(IGraphPathParentRelationTraverser), typeof(ProfilingGraphPathParentRelationTraverser));
            container.RegisterDecorator(typeof(IGraphPathNextRelationTraverser), typeof(ProfilingGraphPathNextRelationTraverser));
            container.RegisterDecorator(typeof(IGraphPathPreviousRelationTraverser), typeof(ProfilingGraphPathPreviousRelationTraverser));
            container.RegisterDecorator(typeof(IGraphPathUpdateRelationTraverser), typeof(ProfilingGraphPathUpdateRelationTraverser));
            container.RegisterDecorator(typeof(IGraphPathDowndateRelationTraverser), typeof(ProfilingGraphPathDowndateRelationTraverser));
            container.RegisterDecorator(typeof(IGraphPathFinalRelationTraverser), typeof(ProfilingGraphPathFinalRelationTraverser));
            container.RegisterDecorator(typeof(IGraphPathOriginalRelationTraverser), typeof(ProfilingGraphPathOriginalRelationTraverser));
            container.RegisterDecorator(typeof(IGraphPathWildcardTraverser), typeof(ProfilingGraphPathWildcardTraverser));
            container.RegisterDecorator(typeof(IGraphPathConditionalTraverser), typeof(ProfilingGraphPathConditionalTraverser));
        }
    }
}