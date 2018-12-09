namespace EtAlii.Ubigia.Api.Functional
{
    internal class AssignOperatorExecutionPlanner : IAssignOperatorExecutionPlanner
    {
        private readonly IAssignOperatorProcessor _processor;

        public AssignOperatorExecutionPlanner(IAssignOperatorProcessor processor)
        {
            _processor = processor;
        }

        public IExecutionPlan Plan(SequencePart part, ISubjectExecutionPlan left, ISubjectExecutionPlan right)
        {
            return new AssignOperatorExecutionPlan(left, right, _processor);
        }
    }
}