namespace EtAlii.Ubigia.Api.Diagnostics.Profiling
{
    using System;
    using EtAlii.Ubigia.Api.Functional;
    using Moppet.Lapa;

    internal class ProfilingSequenceParser : ISequenceParser
    {
        private readonly ISequenceParser _decoree;
        private readonly IProfiler _profiler;

        public ProfilingSequenceParser(
            ISequenceParser decoree,
            IProfiler profiler)
        {
            _decoree = decoree;
            _profiler = profiler.Create(ProfilingAspects.Functional.ScriptSequenceParser);
        }

        public string Id { get { return _decoree.Id; } }
        public LpsParser Parser { get { return _decoree.Parser; } }

        public Sequence Parse(string text)
        {
            dynamic profile = _profiler.Begin("Parsing sequence: " + text);
            profile.Text = text;

            // We need to ensure that the profiler is always ended, even if sequence parsing encounters any exceptions.
            try
            {
                var result = _decoree.Parse(text);
                profile.Result = result;
                return result;
            }
            finally
            {
                _profiler.End(profile);
            }
        }
    }
}