namespace EtAlii.Ubigia.Api.Functional
{
    internal class AbsolutePathSubjectExecutionPlanner : IAbsolutePathSubjectExecutionPlanner
    {
        private readonly IAbsolutePathSubjectProcessor _processor;

        public AbsolutePathSubjectExecutionPlanner(IAbsolutePathSubjectProcessor processor)
        {
            _processor = processor;
        }

        public ISubjectExecutionPlan Plan(SequencePart part)
        {
            var pathSubject = (AbsolutePathSubject)part;
            return new AbsolutePathSubjectExecutionPlan(pathSubject, _processor);
        }
    }
}