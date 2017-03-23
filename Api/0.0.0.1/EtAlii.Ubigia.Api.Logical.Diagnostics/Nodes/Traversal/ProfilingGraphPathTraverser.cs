namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using System;
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using EtAlii.Ubigia.Api.Logical;

    public class ProfilingGraphPathTraverser : IProfilingGraphPathTraverser
    {
        private readonly IGraphPathTraverser _decoree;

        public IProfiler Profiler { get; }

        public ProfilingGraphPathTraverser(IGraphPathTraverser decoree, IProfiler profiler)
        {
            _decoree = decoree;
            Profiler = profiler.Create(ProfilingAspects.Logical.Traversal);
        }

        public void Traverse(GraphPath path, Traversal traversal, ExecutionScope scope, IObserver<IReadOnlyEntry> output, bool traverseToFinal = true)
        {
            dynamic profile = Profiler.Begin("Traverse: " + traversal + " (to final: " + traverseToFinal + ")");
            profile.Path = path.ToString();
            profile.Traversal = traversal.ToString();
            profile.TraverseToFinal = traverseToFinal;

            _decoree.Traverse(path, traversal, scope, output, traverseToFinal);

            Profiler.End(profile);
        }
    }
}