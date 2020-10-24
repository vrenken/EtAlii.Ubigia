namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    internal interface ISequenceExecutionPlanner : IExecutionPlanner
    {
        ISequenceExecutionPlan Plan(Sequence sequence);
    }
}