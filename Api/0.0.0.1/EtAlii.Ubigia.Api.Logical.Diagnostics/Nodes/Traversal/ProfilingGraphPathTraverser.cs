namespace EtAlii.Ubigia.Api.Diagnostics.Profiling
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    public class ProfilingGraphPathTraverser : IProfilingGraphPathTraverser
    {
        private readonly IGraphPathTraverser _decoree;

        public IProfiler Profiler => _profiler;
        private readonly IProfiler _profiler;

        public ProfilingGraphPathTraverser(IGraphPathTraverser decoree, IProfiler profiler)
        {
            _decoree = decoree;
            _profiler = profiler.Create(ProfilingAspects.Logical.Traversal);
        }

        public void Traverse(GraphPath path, Traversal traversal, ExecutionScope scope, IObserver<IReadOnlyEntry> output, bool traverseToFinal = true)
        {
            dynamic profile = _profiler.Begin("Traverse: " + traversal + " (to final: " + traverseToFinal + ")");
            profile.Path = path.ToString();
            profile.Traversal = traversal.ToString();
            profile.TraverseToFinal = traverseToFinal;

            _decoree.Traverse(path, traversal, scope, output, traverseToFinal);

            _profiler.End(profile);
        }
    }
}