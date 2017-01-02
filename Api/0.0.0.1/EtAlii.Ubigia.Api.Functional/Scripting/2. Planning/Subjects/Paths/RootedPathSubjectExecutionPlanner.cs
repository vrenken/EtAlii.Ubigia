namespace EtAlii.Ubigia.Api.Functional
{
    internal class RootedPathSubjectExecutionPlanner : IRootedPathSubjectExecutionPlanner
    {
        private readonly IRootedPathSubjectProcessor _processor;

        public RootedPathSubjectExecutionPlanner(IRootedPathSubjectProcessor processor)
        {
            _processor = processor;
        }

        public ISubjectExecutionPlan Plan(SequencePart part)
        {
            var pathSubject = (RootedPathSubject)part;
            return new RootedPathSubjectExecutionPlan(pathSubject, _processor);
        }
    }
}