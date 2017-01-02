namespace EtAlii.Ubigia.Api.Functional
{
    internal interface IExecutionPlannerSelector
    {
        IExecutionPlanner Select(object item);
    }
}