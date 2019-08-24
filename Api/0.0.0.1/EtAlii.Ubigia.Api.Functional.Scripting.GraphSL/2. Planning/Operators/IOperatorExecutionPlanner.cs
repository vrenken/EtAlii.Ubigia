namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    internal interface IOperatorExecutionPlanner : ISequencePartExecutionPlanner
    {
        IScriptExecutionPlan Plan(SequencePart part, ISubjectExecutionPlan left, ISubjectExecutionPlan right);
    }
}