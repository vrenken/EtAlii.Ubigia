namespace EtAlii.Servus.Api.Functional
{
    internal interface ISequenceExecutionPlanner : IExecutionPlanner
    {
        ISequenceExecutionPlan Plan(Sequence sequence);
    }
}