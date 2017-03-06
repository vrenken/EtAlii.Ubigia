namespace EtAlii.Ubigia.Api.Functional.Diagnostics.Scripting.Parsing
{
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using EtAlii.Ubigia.Api.Functional;

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

        public IScriptParser Create(IScriptParserConfiguration configuration)
        {
            configuration.Use(new IScriptParserExtension[]
            {
                new ProfilingScriptParserExtension(_profiler), 
            });

            return _decoree.Create(configuration);
        }
    }
}