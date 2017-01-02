namespace EtAlii.Servus.Api.Functional
{
    using EtAlii.xTechnology.Structure;

    internal class PathSubjectPartProcessor : IPathSubjectPartProcessor
    {
        private readonly ISelector<PathSubjectPart, IPathSubjectPartProcessor> _selector;

        public PathSubjectPartProcessor(ISelector<PathSubjectPart, IPathSubjectPartProcessor> selector)
        {
            _selector = selector;
        }

        public bool CanProcess(PathSubjectPart part)
        {
            var processor = _selector.TrySelect(part);
            return processor != null;
        }

        public object Process(ProcessParameters<PathSubjectPart, PathSubjectPart> parameters)
        {
            var processor = _selector.Select(parameters.Target);
            return processor.Process(parameters);
        }
    }
}
