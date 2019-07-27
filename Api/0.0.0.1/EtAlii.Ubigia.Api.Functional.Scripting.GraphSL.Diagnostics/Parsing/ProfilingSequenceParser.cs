namespace EtAlii.Ubigia.Api.Functional.Diagnostics.Scripting
{
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
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

        public string Id => _decoree.Id;
        public LpsParser Parser => _decoree.Parser;

        public Sequence Parse(LpNode node, bool restIsAllowed)
        {
            var text = node.Match.ToString();
            dynamic profile = _profiler.Begin("Parsing sequence: " + text);
            profile.Text = text;

            // We need to ensure that the profiler is always ended, even if sequence parsing encounters any exceptions.
            try
            {
                var result = _decoree.Parse(node, restIsAllowed);
                profile.Result = result;
                return result;
            }
            finally
            {
                _profiler.End(profile);
            }
        }

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