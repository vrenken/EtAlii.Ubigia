namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal interface IOperatorExecutionPlanner : ISequencePartExecutionPlanner
    {
        IScriptExecutionPlan Plan(SequencePart part, ISubjectExecutionPlan left, ISubjectExecutionPlan right);
    }
}
