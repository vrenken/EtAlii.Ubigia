namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal interface ISequenceExecutionPlanner : IExecutionPlanner
    {
        ISequenceExecutionPlan Plan(Sequence sequence);
    }
}
