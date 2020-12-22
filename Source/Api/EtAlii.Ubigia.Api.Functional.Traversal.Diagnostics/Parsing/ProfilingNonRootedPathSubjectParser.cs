namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Diagnostics.Profiling;
    using Moppet.Lapa;

    internal class ProfilingNonRootedPathSubjectParser : INonRootedPathSubjectParser
    {
        public string Id { get; }
        private readonly INonRootedPathSubjectParser _decoree;
        private readonly IProfiler _profiler;

        public ProfilingNonRootedPathSubjectParser(
            INonRootedPathSubjectParser decoree,
            IProfiler profiler)
        {
            _decoree = decoree;
            _profiler = profiler.Create(ProfilingAspects.Functional.ScriptPathSubjectParser);

            Id = _decoree.Id;
        }

        public LpsParser Parser => _decoree.Parser;

        public Subject Parse(LpNode node)
        {
            dynamic profile = _profiler.Begin("Parsing path subject");
            profile.Node = node;

            var result = _decoree.Parse(node);
            profile.Result = result;
            profile.Action = result.ToString();

            _profiler.End(profile);

            return result;

        }

        public bool CanParse(LpNode node)
        {
            return _decoree.CanParse(node);
        }

        public void Validate(SequencePart before, Subject item, int itemIndex, SequencePart after)
        {
            _decoree.Validate(before, item, itemIndex, after);
        }

        public bool CanValidate(Subject item)
        {
            return _decoree.CanValidate(item);
        }
    }
}
