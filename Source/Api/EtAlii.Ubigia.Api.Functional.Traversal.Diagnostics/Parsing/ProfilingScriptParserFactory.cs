namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Diagnostics.Profiling;

    internal class ProfilingScriptParserFactory : IScriptParserFactory
    {
        private readonly IScriptParserFactory _decoree;
        private readonly IProfiler _profiler;

        public ProfilingScriptParserFactory(
            IScriptParserFactory decoree,
            IProfiler profiler)
        {
            _decoree = decoree;
            _profiler = profiler;
        }

        public IScriptParser Create(ScriptParserConfiguration configuration)
        {
            configuration.Use(new IScriptParserExtension[]
            {
                new ProfilingScriptParserExtension(_profiler),
            });

            return _decoree.Create(configuration);
        }
    }
}
