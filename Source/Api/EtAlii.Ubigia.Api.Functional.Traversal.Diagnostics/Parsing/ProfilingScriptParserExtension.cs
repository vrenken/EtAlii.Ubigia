namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Diagnostics.Profiling;
    using EtAlii.xTechnology.MicroContainer;

    public class ProfilingScriptParserExtension : IScriptParserExtension
    {
        private readonly IProfiler _profiler;

        public ProfilingScriptParserExtension(IProfiler profiler)
        {
            _profiler = profiler;
        }

        public void Initialize(Container container)
        {
            container.Register(() => _profiler);
            container.RegisterDecorator(typeof(IScriptParser), typeof(ProfilingScriptParser));
            container.RegisterDecorator(typeof(ISequenceParser), typeof(ProfilingSequenceParser));
            container.RegisterDecorator(typeof(INonRootedPathSubjectParser), typeof(ProfilingNonRootedPathSubjectParser));
        }
    }
}
