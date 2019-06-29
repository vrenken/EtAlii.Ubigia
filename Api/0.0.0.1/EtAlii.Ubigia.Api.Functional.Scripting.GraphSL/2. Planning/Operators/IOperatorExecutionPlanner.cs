namespace EtAlii.Ubigia.Api.Functional
{
    internal interface IOperatorExecutionPlanner : ISequencePartExecutionPlanner
    {
        IScriptExecutionPlan Plan(SequencePart part, ISubjectExecutionPlan left, ISubjectExecutionPlan right);
    }
}