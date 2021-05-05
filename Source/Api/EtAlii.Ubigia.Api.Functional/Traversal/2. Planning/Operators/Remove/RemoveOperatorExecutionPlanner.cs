namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal class RemoveOperatorExecutionPlanner : IRemoveOperatorExecutionPlanner
    {
        private readonly IRemoveOperatorProcessor _processor;

        public RemoveOperatorExecutionPlanner(IRemoveOperatorProcessor processor)
        {
            _processor = processor;
        }

        public IScriptExecutionPlan Plan(SequencePart part, ISubjectExecutionPlan left, ISubjectExecutionPlan right)
        {
            return new RemoveOperatorExecutionPlan(left, right, _processor);
        }
    }
}
