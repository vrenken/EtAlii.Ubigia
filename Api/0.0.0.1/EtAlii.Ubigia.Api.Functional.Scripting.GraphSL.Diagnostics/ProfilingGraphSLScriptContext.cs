﻿namespace EtAlii.Ubigia.Api.Functional.Diagnostics
{
    using System;
    using System.Linq;
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;

    public class ProfilingGraphSLScriptContext : IProfilingGraphSLScriptContext
    {
        private readonly IGraphSLScriptContext _decoree;
        public IProfiler Profiler { get; }

        public ProfilingGraphSLScriptContext(
            IGraphSLScriptContext decoree, 
            IProfiler profiler)
        {
            _decoree = decoree;
            Profiler = profiler.Create(ProfilingAspects.Functional.ScriptSet);
        }

        public ScriptParseResult Parse(string text)
        {
            var profile = Profiler.Begin("Parsing");

            var result = _decoree.Parse(text);

            Profiler.End(profile);

            return result;
        }

        public IObservable<SequenceProcessingResult> Process(Script script, IScriptScope scope)
        {
            dynamic profile = Profiler.Begin("Process");
            profile.Script = script.ToString();

            var result = _decoree.Process(script, scope);

            Profiler.End(profile);

            return result;
        }

        public IObservable<SequenceProcessingResult> Process(string[] text, IScriptScope scope)
        {
            dynamic profile = Profiler.Begin("Process");
            profile.Script = string.Join(Environment.NewLine, text);

            var result = _decoree.Process(text, scope);

            Profiler.End(profile);

            return result;
        }

        public IObservable<SequenceProcessingResult> Process(string[] text)
        {
            dynamic profile = Profiler.Begin("Process");
            profile.Script = string.Join(Environment.NewLine, text);

            var result = _decoree.Process(text);

            Profiler.End(profile);

            return result;
        }

        public IObservable<SequenceProcessingResult> Process(string text)
        {
            dynamic profile = Profiler.Begin("Processing");
            profile.Script = text;
            var result = _decoree.Process(text);

            Profiler.End(profile);

            return result;
        }

        public IObservable<SequenceProcessingResult> Process(string text, params object[] args)
        {
            dynamic profile = Profiler.Begin("Processing");
            profile.Script = text;
            profile.Arguments = string.Join(", ", args.Select(a => a.ToString()));

            var result = _decoree.Process(text, args);

            Profiler.End(profile);

            return result;
        }
    }
}
