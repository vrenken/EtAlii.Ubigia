namespace EtAlii.Servus.Api.Functional
{
    internal interface IExecutionPlanCombinerSelector
    {
        IExecutionPlanCombiner Select(IExecutionPlanner planner);
    }
}