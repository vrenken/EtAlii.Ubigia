namespace EtAlii.Ubigia.Api.Functional.Diagnostics.Scripting
{
    using EtAlii.Ubigia.Api.Diagnostics.Profiling;
    using EtAlii.Ubigia.Api.Functional;
    using Moppet.Lapa;

    internal class ProfilingNonRootedPathSubjectParser : INonRootedPathSubjectParser
    {
        private readonly INonRootedPathSubjectParser _decoree;
        private readonly IProfiler _profiler;

        public ProfilingNonRootedPathSubjectParser(
            INonRootedPathSubjectParser decoree,
            IProfiler profiler)
        {
            _decoree = decoree;
            _profiler = profiler.Create(ProfilingAspects.Functional.ScriptPathSubjectParser);
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

        public void Validate(SequencePart before, Subject subject, int subjectIndex, SequencePart after)
        {
            _decoree.Validate(before, subject, subjectIndex, after);
        }

        public bool CanValidate(Subject subject)
        {
            return _decoree.CanValidate(subject);
        }
    }
}