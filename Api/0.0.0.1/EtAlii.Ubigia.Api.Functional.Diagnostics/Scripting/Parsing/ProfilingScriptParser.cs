namespace EtAlii.Ubigia.Api.Functional.Diagnostics.Scripting
{
    using System.Linq;
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using EtAlii.Ubigia.Api.Functional;

    internal class ProfilingScriptParser : IProfilingScriptParser
    {
        public IProfiler Profiler => _profiler;
        private readonly IProfiler _profiler;

        private readonly IScriptParser _decoree;

        public ProfilingScriptParser(
            IScriptParser decoree,
            IProfiler profiler)
        {
            _decoree = decoree;
            _profiler = profiler.Create(ProfilingAspects.Functional.ScriptParser);
        }

        public ScriptParseResult Parse(string text)
        {
            var result = _decoree.Parse(text);

            var errorMessage = result.Errors
                .Select(e => e.Message)
                .FirstOrDefault();
            if (errorMessage != null)
            {
                // Let's show an error message in the profiling view if we encountered an exception.
                dynamic exceptionProfile = _profiler.Begin("Error: " + errorMessage);
                exceptionProfile.Error = errorMessage;
                _profiler.End(exceptionProfile);
            }

            return result;
        }

        public ScriptParseResult Parse(string[] text)
        {
            var result = _decoree.Parse(text);

            var errorMessage = result.Errors
                .Select(e => e.Message)
                .FirstOrDefault();
            if (errorMessage != null)
            {
                // Let's show an error message in the profiling view if we encountered an exception.
                dynamic exceptionProfile = _profiler.Begin("Error: " + errorMessage);
                exceptionProfile.Error = errorMessage;
                _profiler.End(exceptionProfile);
            }

            return result;
        }
    }
}