namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal interface IExecutionPlanCombinerSelector
    {
        IExecutionPlanCombiner Select(IExecutionPlanner planner);
    }
}
