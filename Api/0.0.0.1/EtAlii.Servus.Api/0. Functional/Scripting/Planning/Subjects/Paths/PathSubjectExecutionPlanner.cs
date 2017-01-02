namespace EtAlii.Servus.Api.Functional
{
    internal class PathSubjectExecutionPlanner : IPathSubjectExecutionPlanner
    {
        private readonly IPathSubjectProcessor _processor;

        public PathSubjectExecutionPlanner(IPathSubjectProcessor processor)
        {
            _processor = processor;
        }

        public ISubjectExecutionPlan Plan(SequencePart part)
        {
            var pathSubject = (PathSubject)part;
            return new PathSubjectExecutionPlan(pathSubject, _processor);
        }
    }
}