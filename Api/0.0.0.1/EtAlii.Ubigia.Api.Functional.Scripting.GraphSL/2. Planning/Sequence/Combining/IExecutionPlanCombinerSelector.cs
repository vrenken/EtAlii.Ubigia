namespace EtAlii.Ubigia.Api.Functional
{
    internal interface IExecutionPlanCombinerSelector
    {
        IExecutionPlanCombiner Select(IExecutionPlanner planner);
    }
}