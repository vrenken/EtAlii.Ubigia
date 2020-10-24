namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    internal interface IExecutionPlanCombinerSelector
    {
        IExecutionPlanCombiner Select(IExecutionPlanner planner);
    }
}