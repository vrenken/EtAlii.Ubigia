namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal interface IExecutionPlannerSelector
    {
        IExecutionPlanner Select(object item);
    }
}
