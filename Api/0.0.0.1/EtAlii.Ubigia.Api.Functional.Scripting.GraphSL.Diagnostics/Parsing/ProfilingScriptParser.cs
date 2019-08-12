namespace EtAlii.Ubigia.Api.Functional.Diagnostics
{
    using System.Linq;
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;

    internal class ProfilingScriptParser : IProfilingScriptParser
    {
        public IProfiler Profiler { get; }

        private readonly IScriptParser _decoree;

        public ProfilingScriptParser(
            IScriptParser decoree,
            IProfiler profiler)
        {
            _decoree = decoree;
            Profiler = profiler.Create(ProfilingAspects.Functional.ScriptParser);
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
                dynamic exceptionProfile = Profiler.Begin("Error: " + errorMessage);
                exceptionProfile.Error = errorMessage;
                Profiler.End(exceptionProfile);
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
                dynamic exceptionProfile = Profiler.Begin("Error: " + errorMessage);
                exceptionProfile.Error = errorMessage;
                Profiler.End(exceptionProfile);
            }

            return result;
        }
    }
}