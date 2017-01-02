namespace EtAlii.Ubigia.Api.Diagnostics.Profiling
{
    using EtAlii.Ubigia.Api.Functional;

    internal class ProfilingScriptProcessorFactory : IScriptProcessorFactory
    {
        private readonly IScriptProcessorFactory _decoree;
        private readonly IProfiler _profiler;

        public ProfilingScriptProcessorFactory(
            IScriptProcessorFactory decoree, 
            IProfiler profiler)
        {
            _decoree = decoree;
            _profiler = profiler;
        }

        public IScriptProcessor Create(IScriptProcessorConfiguration configuration)
        {
            configuration.Use(new IScriptProcessorExtension[]
            {
                new ProfilingScriptProcessorExtension2(_profiler), 
            });

            return _decoree.Create(configuration);
        }
    }
}