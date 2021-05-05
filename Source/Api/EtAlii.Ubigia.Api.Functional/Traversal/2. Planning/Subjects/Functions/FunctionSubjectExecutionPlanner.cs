namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal class FunctionSubjectExecutionPlanner : IFunctionSubjectExecutionPlanner
    {
        private readonly IFunctionSubjectProcessor _processor;

        public FunctionSubjectExecutionPlanner(IFunctionSubjectProcessor processor)
        {
            _processor = processor;
        }

        public ISubjectExecutionPlan Plan(SequencePart part)
        {
            var functionSubject = (FunctionSubject)part;
            return new FunctionSubjectExecutionPlan(functionSubject, _processor);
        }
    }
}
