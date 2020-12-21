namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal class RootDefinitionSubjectExecutionPlanner : IRootDefinitionSubjectExecutionPlanner
    {
        private readonly IRootDefinitionSubjectProcessor _processor;

        public RootDefinitionSubjectExecutionPlanner(IRootDefinitionSubjectProcessor processor)
        {
            _processor = processor;
        }

        public ISubjectExecutionPlan Plan(SequencePart part)
        {
            var rootDefinitionSubject = (RootDefinitionSubject)part;
            return new RootDefinitionSubjectExecutionPlan(rootDefinitionSubject, _processor);
        }
    }
}
