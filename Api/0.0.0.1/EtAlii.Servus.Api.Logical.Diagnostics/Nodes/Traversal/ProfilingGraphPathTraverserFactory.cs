namespace EtAlii.Servus.Api.Diagnostics.Profiling
{
    using EtAlii.Servus.Api.Fabric;
    using EtAlii.Servus.Api.Logical;

    public class ProfilingGraphPathTraverserFactory : IGraphPathTraverserFactory
    {
        private readonly IGraphPathTraverserFactory _decoree;
        private readonly IProfiler _profiler;

        public ProfilingGraphPathTraverserFactory(
            IGraphPathTraverserFactory decoree, 
            IProfiler profiler)
        {
            _decoree = decoree;
            _profiler = profiler;
        }

        public IGraphPathTraverser Create(IGraphPathTraverserConfiguration configuration)
        {
            configuration.Use(new IGraphPathTraverserExtension[]
            {
                new ProfilingGraphPathTraverserExtension(_profiler), 
            });
            return _decoree.Create(configuration);
        }
    }
}