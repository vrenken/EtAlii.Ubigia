namespace EtAlii.Ubigia.Api.Functional
{
    internal class AddOperatorExecutionPlanner : IAddOperatorExecutionPlanner
    {
        private readonly IAddOperatorProcessor _processor;

        public AddOperatorExecutionPlanner(IAddOperatorProcessor processor)
        {
            _processor = processor;
        }

        public IScriptExecutionPlan Plan(SequencePart part, ISubjectExecutionPlan left, ISubjectExecutionPlan right)
        {
            return new AddOperatorExecutionPlan(left, right, _processor);
        }
    }
}