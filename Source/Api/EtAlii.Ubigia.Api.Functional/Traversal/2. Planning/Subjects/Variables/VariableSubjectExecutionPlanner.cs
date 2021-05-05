namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal class VariableSubjectExecutionPlanner : IVariableSubjectExecutionPlanner
    {
        private readonly IVariableSubjectProcessor _processor;

        public VariableSubjectExecutionPlanner(IVariableSubjectProcessor processor)
        {
            _processor = processor;
        }

        public ISubjectExecutionPlan Plan(SequencePart part)
        {
            var variableSubject = (VariableSubject)part;
            return new VariableSubjectExecutionPlan(variableSubject, _processor);
        }
    }
}
