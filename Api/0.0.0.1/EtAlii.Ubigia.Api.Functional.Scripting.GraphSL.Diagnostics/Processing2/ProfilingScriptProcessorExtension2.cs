namespace EtAlii.Ubigia.Api.Functional.Diagnostics.Scripting
{
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.xTechnology.MicroContainer;

    public class ProfilingScriptProcessorExtension2 : IScriptProcessorExtension
    {
        private readonly IProfiler _profiler;

        public ProfilingScriptProcessorExtension2(IProfiler profiler)
        {
            _profiler = profiler;
        }

        public void Initialize(Container container)
        {
            container.Register(() => _profiler);
            
            container.RegisterDecorator(typeof(IEntriesToDynamicNodesConverter), typeof(ProfilingEntriesToDynamicNodesConverter));
        }
    }
}