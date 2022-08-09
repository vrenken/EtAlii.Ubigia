// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using EtAlii.xTechnology.MicroContainer;

    public sealed class ProfilingGraphPathTraverserExtension : IExtension
    {
        // private readonly IProfiler _profiler
        //
        // public ProfilingGraphPathTraverserExtension(IProfiler profiler)
        // [
        //     _profiler = profiler
        // ]

        public void Initialize(IRegisterOnlyContainer container)
        {
            container.RegisterDecorator<IGraphPathTraverser, ProfilingGraphPathTraverser>();
            //container.Register<IProfiler>(() => new Profiler(_profiler, ProfilingAspects.Logical.Traversal))

            container.RegisterDecorator<ITemporalGraphPathWeaver, ProfilingTemporalGraphPathWeaver>();

            container.RegisterDecorator<IGraphPathNodeTraverser, ProfilingGraphPathNodeTraverser>();
            container.RegisterDecorator<IGraphPathIdentifiersStartNodeTraverser, ProfilingGraphPathIdentifiersStartNodeTraverser>();
            container.RegisterDecorator<IGraphPathRootStartNodeTraverser, ProfilingGraphPathRootStartNodeTraverser>();
            container.RegisterDecorator<IGraphPathAllChildrenRelationTraverser, ProfilingGraphPathAllChildrenRelationTraverser>();
            container.RegisterDecorator<IGraphPathChildrenRelationTraverser, ProfilingGraphPathChildrenRelationTraverser>();
            container.RegisterDecorator<IGraphPathAllParentsRelationTraverser, ProfilingGraphPathAllParentsRelationTraverser>();
            container.RegisterDecorator<IGraphPathParentRelationTraverser, ProfilingGraphPathParentRelationTraverser>();
            container.RegisterDecorator<IGraphPathAllNextRelationTraverser, ProfilingGraphPathAllNextRelationTraverser>();
            container.RegisterDecorator<IGraphPathNextRelationTraverser, ProfilingGraphPathNextRelationTraverser>();
            container.RegisterDecorator<IGraphPathAllPreviousRelationTraverser, ProfilingGraphPathAllPreviousRelationTraverser>();
            container.RegisterDecorator<IGraphPathPreviousRelationTraverser, ProfilingGraphPathPreviousRelationTraverser>();
            container.RegisterDecorator<IGraphPathAllUpdatesRelationTraverser, ProfilingGraphPathAllUpdatesRelationTraverser>();
            container.RegisterDecorator<IGraphPathUpdatesRelationTraverser, ProfilingGraphPathUpdatesRelationTraverser>();
            container.RegisterDecorator<IGraphPathAllDowndatesRelationTraverser, ProfilingGraphPathAllDowndatesRelationTraverser>();
            container.RegisterDecorator<IGraphPathDowndateRelationTraverser, ProfilingGraphPathDowndateRelationTraverser>();
            container.RegisterDecorator<IGraphPathFinalRelationTraverser, ProfilingGraphPathFinalRelationTraverser>();
            container.RegisterDecorator<IGraphPathOriginalRelationTraverser, ProfilingGraphPathOriginalRelationTraverser>();
            container.RegisterDecorator<IGraphPathWildcardTraverser, ProfilingGraphPathWildcardTraverser>();
            container.RegisterDecorator<IGraphPathConditionalTraverser, ProfilingGraphPathConditionalTraverser>();
            container.RegisterDecorator<IGraphPathTaggedNodeTraverser, ProfilingGraphPathTaggedNodeTraverser>();
        }
    }
}
