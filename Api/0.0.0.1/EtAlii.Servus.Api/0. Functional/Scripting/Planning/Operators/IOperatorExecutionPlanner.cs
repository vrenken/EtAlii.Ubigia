namespace EtAlii.Servus.Api.Functional
{
    internal interface IOperatorExecutionPlanner : ISequencePartExecutionPlanner
    {
        IExecutionPlan Plan(SequencePart part, ISubjectExecutionPlan left, ISubjectExecutionPlan right);
    }
}