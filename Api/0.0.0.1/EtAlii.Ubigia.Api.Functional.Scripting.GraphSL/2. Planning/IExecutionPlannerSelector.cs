namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    internal interface IExecutionPlannerSelector
    {
        IExecutionPlanner Select(object item);
    }
}