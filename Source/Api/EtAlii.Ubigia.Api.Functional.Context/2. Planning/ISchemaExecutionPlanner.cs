namespace EtAlii.Ubigia.Api.Functional.Context
{
    internal interface ISchemaExecutionPlanner
    {
        ExecutionPlan[] Plan(Schema schema);
    }
}
