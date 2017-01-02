namespace EtAlii.Servus.Api.Functional
{
    internal interface IExecutionPlannerSelector
    {
        IExecutionPlanner Select(object item);
    }
}