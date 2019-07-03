namespace EtAlii.Ubigia.Api.Functional.Diagnostics.Querying 
{
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;

    internal class ProfilingQueryProcessorFactory : IQueryProcessorFactory
    {
        private readonly IQueryProcessorFactory _decoree;
        private readonly IProfiler _profiler;

        public ProfilingQueryProcessorFactory(
            IQueryProcessorFactory decoree, 
            IProfiler profiler)
        {
            _decoree = decoree;
            _profiler = profiler;
        }

        public IQueryProcessor Create(QueryProcessorConfiguration configuration)
        {
            configuration.Use(new IQueryProcessorExtension[]
            {
                //new ProfilingQueryProcessorExtension2(_profiler), 
            });

            return _decoree.Create(configuration);
        }
    }
}