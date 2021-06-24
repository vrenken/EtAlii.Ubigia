// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using EtAlii.Ubigia.Diagnostics.Profiling;
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
            container.RegisterDecorator(typeof(IGraphPathAllChildrenRelationTraverser), typeof(ProfilingGraphPathAllChildrenRelationTraverser));
            container.RegisterDecorator(typeof(IGraphPathChildrenRelationTraverser), typeof(ProfilingGraphPathChildrenRelationTraverser));
            container.RegisterDecorator(typeof(IGraphPathAllParentsRelationTraverser), typeof(ProfilingGraphPathAllParentsRelationTraverser));
            container.RegisterDecorator(typeof(IGraphPathParentRelationTraverser), typeof(ProfilingGraphPathParentRelationTraverser));
            container.RegisterDecorator(typeof(IGraphPathAllNextRelationTraverser), typeof(ProfilingGraphPathAllNextRelationTraverser));
            container.RegisterDecorator(typeof(IGraphPathNextRelationTraverser), typeof(ProfilingGraphPathNextRelationTraverser));
            container.RegisterDecorator(typeof(IGraphPathAllPreviousRelationTraverser), typeof(ProfilingGraphPathAllPreviousRelationTraverser));
            container.RegisterDecorator(typeof(IGraphPathPreviousRelationTraverser), typeof(ProfilingGraphPathPreviousRelationTraverser));
            container.RegisterDecorator(typeof(IGraphPathAllUpdatesRelationTraverser), typeof(ProfilingGraphPathAllUpdatesRelationTraverser));
            container.RegisterDecorator(typeof(IGraphPathUpdatesRelationTraverser), typeof(ProfilingGraphPathUpdatesRelationTraverser));
            container.RegisterDecorator(typeof(IGraphPathAllDowndatesRelationTraverser), typeof(ProfilingGraphPathAllDowndatesRelationTraverser));
            container.RegisterDecorator(typeof(IGraphPathDowndateRelationTraverser), typeof(ProfilingGraphPathDowndateRelationTraverser));
            container.RegisterDecorator(typeof(IGraphPathFinalRelationTraverser), typeof(ProfilingGraphPathFinalRelationTraverser));
            container.RegisterDecorator(typeof(IGraphPathOriginalRelationTraverser), typeof(ProfilingGraphPathOriginalRelationTraverser));
            container.RegisterDecorator(typeof(IGraphPathWildcardTraverser), typeof(ProfilingGraphPathWildcardTraverser));
            container.RegisterDecorator(typeof(IGraphPathConditionalTraverser), typeof(ProfilingGraphPathConditionalTraverser));
            container.RegisterDecorator(typeof(IGraphPathTaggedNodeTraverser), typeof(ProfilingGraphPathTaggedNodeTraverser));
        }
    }
}