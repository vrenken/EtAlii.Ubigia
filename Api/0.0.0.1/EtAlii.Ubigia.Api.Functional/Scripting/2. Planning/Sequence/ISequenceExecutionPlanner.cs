namespace EtAlii.Ubigia.Api.Functional
{
    internal interface ISequenceExecutionPlanner : IExecutionPlanner
    {
        ISequenceExecutionPlan Plan(Sequence sequence);
    }
}