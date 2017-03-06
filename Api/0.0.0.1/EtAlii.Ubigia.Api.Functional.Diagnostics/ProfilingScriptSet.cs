namespace EtAlii.Ubigia.Api.Functional.Diagnostics
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using EtAlii.Ubigia.Api.Functional;

    public class ProfilingScriptSet : IScriptsSet
    {
        private readonly IScriptsSet _decoree;
        private readonly IProfiler _profiler;

        public ProfilingScriptSet(
            IScriptsSet decoree, 
            IProfiler profiler)
        {
            _decoree = decoree;
            _profiler = profiler.Create(ProfilingAspects.Functional.ScriptSet);
        }

        public ScriptParseResult Parse(string text)
        {
            var profile = _profiler.Begin("Parsing");

            var result = _decoree.Parse(text);

            _profiler.End(profile);

            return result;
        }

        public IObservable<SequenceProcessingResult> Process(Script script, IScriptScope scope)
        {
            dynamic profile = _profiler.Begin("Process");
            profile.Script = script.ToString();

            var result = _decoree.Process(script, scope);

            _profiler.End(profile);

            return result;
        }

        public IObservable<SequenceProcessingResult> Process(string[] text, IScriptScope scope)
        {
            dynamic profile = _profiler.Begin("Process");
            profile.Script = String.Join(Environment.NewLine, text);

            var result = _decoree.Process(text, scope);

            _profiler.End(profile);

            return result;
        }

        public IObservable<SequenceProcessingResult> Process(string[] text)
        {
            dynamic profile = _profiler.Begin("Process");
            profile.Script = String.Join(Environment.NewLine, text);

            var result = _decoree.Process(text);

            _profiler.End(profile);

            return result;
        }

        public IObservable<SequenceProcessingResult> Process(string text)
        {
            dynamic profile = _profiler.Begin("Processing");
            profile.Script = text;
            var result = _decoree.Process(text);

            _profiler.End(profile);

            return result;
        }

        public IObservable<SequenceProcessingResult> Process(string text, params object[] args)
        {
            dynamic profile = _profiler.Begin("Processing");
            profile.Script = text;
            profile.Arguments = String.Join(", ", args.Select(a => a.ToString()));

            var result = _decoree.Process(text, args);

            _profiler.End(profile);

            return result;
        }
    }
}
