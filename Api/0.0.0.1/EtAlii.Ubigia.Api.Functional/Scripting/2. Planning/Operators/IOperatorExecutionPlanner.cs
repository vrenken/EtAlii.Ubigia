namespace EtAlii.Ubigia.Api.Functional
{
    internal interface IOperatorExecutionPlanner : ISequencePartExecutionPlanner
    {
        IExecutionPlan Plan(SequencePart part, ISubjectExecutionPlan left, ISubjectExecutionPlan right);
    }
}